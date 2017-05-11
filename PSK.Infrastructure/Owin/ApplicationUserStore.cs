using NHibernate;
using NHibernate.AspNet.Identity;
using PSK.Model;

namespace PSK.Infrastructure.Owin
{
	public class ApplicationUserStore : UserStore<ApplicationUser>
	{
		public ApplicationUserStore(ISession context)
			: base(context)
		{
		}
	}
}