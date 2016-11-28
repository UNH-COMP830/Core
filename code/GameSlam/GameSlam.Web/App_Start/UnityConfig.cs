using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using GameSlam.Infrastructure.Repositories;
using GameSlam.Services;
using Microsoft.Owin.Security;
using System.Web;
using GameSlam.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GameSlam.Services.Services;
using GameSlam.Web.Workflow;

namespace GameSlam.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IRepository, Repository>();
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationRoleManager>();


            ///http://tech.trailmax.info/2014/09/aspnet-identity-and-ioc-container-registration/
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new InjectionConstructor(typeof(ApplicationDbContext)));

            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole, string, IdentityUserRole>>(
                new InjectionConstructor(typeof(ApplicationDbContext)));

            container.RegisterType<AccountService>();
            container.RegisterType<GameWorkflow>(); 


        }
    }
}
