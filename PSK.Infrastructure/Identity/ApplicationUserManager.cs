﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using PSK.Model.Identity;

namespace PSK.Infrastructure.Identity
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser> store, IDataProtectionProvider dataProtectionProvider)
			: base(store)
		{
			UserValidator = new UserValidator<ApplicationUser>(this)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			// Configure validation logic for passwords
			PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				//RequireNonLetterOrDigit = true,
				//RequireDigit = true,
				//RequireLowercase = true,
				//RequireUppercase = true,
			};

			// Configure user lockout defaults
			UserLockoutEnabledByDefault = true;
			DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			MaxFailedAccessAttemptsBeforeLockout = 5;

			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug it in here.
			RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
			{
				MessageFormat = "Your security code is {0}"
			});
			RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
			{
				Subject = "Security Code",
				BodyFormat = "Your security code is {0}"
			});
			EmailService = new EmailService();
			SmsService = new SmsService();
			if (dataProtectionProvider != null)
			{
				UserTokenProvider =
					new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
		}
	}
}