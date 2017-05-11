using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using NHibernate;
using PSK.NHibernate;

namespace PSK.Web
{
	public class AutofacConfig
	{
		public static void ConfigureContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			// Register dependencies in filter attributes
			builder.RegisterFilterProvider();

			// Register dependencies in custom views
			builder.RegisterSource(new ViewRegistrationSource());

			// Register our Data dependencies

			builder.Register(c =>
					new Repository(
						new Lazy<ISession>(
							() => SessionFactoryProvider.Get().OpenSession())))
				.As<IRepository>();



			var container = builder.Build();

			// Set MVC DI resolver to use our Autofac container
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}