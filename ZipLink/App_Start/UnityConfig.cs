using AutoMapper;
using BusinessLayer.URLOperations;
using BusinessLayer.URLTrackingOperations;
using ConsumeLayer.URLOperators;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Models.User;
using Repository;
using System.ComponentModel;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;
using ZipLink.Auth;
using ZipLink.Mapper;

namespace ZipLink
{
    public static class UnityConfig
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container => _container ?? (_container = new UnityContainer());

        public static void RegisterComponents()
        {

            // Register AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            Container.RegisterInstance(config.CreateMapper());

            // Register DbContext with PerRequestLifetimeManager
            Container.RegisterType<DbContext, ZiplinkDBContext>(new PerRequestLifetimeManager());

            // Register OWIN components
            Container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            // Register ApplicationDbContext, UserManager, SignInManager, and UserStore
            Container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new PerRequestLifetimeManager());
            Container.RegisterType<UserManager<ApplicationUser>>(new PerRequestLifetimeManager());
            Container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            Container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());

            // Register custom services
            Container.RegisterType<IURLConsumes, URLConsumes>();
            Container.RegisterType<IURLOperators, URLOperators>();
            Container.RegisterType<IURLTrackingOperators, URLTrackingOperators>();
            Container.RegisterType<ConsumeLayer.URLTracking.IURLTrackingOperators, ConsumeLayer.URLTracking.URLTrackingOperators>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(Container));
        }
    }
}