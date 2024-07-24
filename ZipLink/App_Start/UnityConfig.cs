using AutoMapper;
using BusinessLayer.URLOperations;
using ConsumeLayer.URLOperators;
using Repository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using ZipLink.Mapper;

namespace ZipLink
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // Register AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            container.RegisterInstance(config.CreateMapper());

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IURLConsumes, URLConsumes>();
            container.RegisterType<ZiplinkDBContext>();
            container.RegisterType<IURLOperators, URLOperators>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}