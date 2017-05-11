using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PSK.Model;
using NHibernate.AspNet.Identity.Helpers;

namespace PSK.NHibernate
{
	public class SessionFactoryProvider
	{
		private static ISessionFactory _sessionFactory;
		private static readonly object _padlock = 1;



		public static void CreateSessionFactory()
		{
			lock (_padlock)
			{
				var myEntities = new[] {
					typeof(ApplicationUser)
				};

				var connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
				var configuration = Fluently.Configure()
					.Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql)
					.Mappings(m => m.FluentMappings.AddFromNamespaceOf<DummyMappings>())
					.ExposeConfiguration(c => c.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null))
					.BuildConfiguration();
#if DEBUG
				var exporter = new SchemaExport(configuration);
				exporter.Execute(true, true, false);
#endif
				_sessionFactory = configuration.BuildSessionFactory();
			}
		}


		public static ISessionFactory Get()
		{
			if (_sessionFactory == null)
				CreateSessionFactory();

			return _sessionFactory;
		}
	}
}
