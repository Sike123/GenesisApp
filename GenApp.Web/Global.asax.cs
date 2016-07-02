using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using AutoMapper;
using AutoMapper.Configuration;
using GenApp.Repository;
using GenApp.Web.Controllers;
using GenApp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GenApp.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IContainer Container { get; set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Custom Addition for xml 
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //custom addition for json 
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);



            CreateMappings();

            SetAutofacContainer();








        }

        // http://docs.autofac.org/en/stable/integration/webapi.html#register-controllers
        //WebApi 2 verson of Autofac.WebApi Integration has been used surprisingly. The WebApi 1 version didnt work for some reason 
        protected void SetAutofacContainer()
        {

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

          
            // You can register controllers all at once using assembly scanning...
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            

            // ...or you can register individual controlllers manually.
            builder.RegisterType<AssetController>().InstancePerRequest();


            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<AssetRepository>().As<IAssetRepository>();

            // Set the dependency resolver to be Autofac
            Container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);


        }

        protected void CreateMappings()
        {
            Mapper.CreateMap<Asset, AssetViewModel>().ForMember(dest => dest.Name,
                        opts => opts.MapFrom(src => src.Name));

            Mapper.CreateMap<Book, BookViewModel>()
                .ForMember(dest=>dest.Id,opts=>opts.MapFrom(src=>src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Isbn, opts => opts.MapFrom(src => src.Isbn))
                .ForMember(dest => dest.Publisher, opts => opts.MapFrom(src => src.Publisher))
                .ForMember(dest => dest.Edition, opts => opts.MapFrom(src => src.Edition));

            Mapper.CreateMap<BookViewModel, Book>()
              
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Isbn, opts => opts.MapFrom(src => src.Isbn))
                .ForMember(dest => dest.Publisher, opts => opts.MapFrom(src => src.Publisher))
                .ForMember(dest => dest.Edition, opts => opts.MapFrom(src => src.Edition));

        }
    }
}
