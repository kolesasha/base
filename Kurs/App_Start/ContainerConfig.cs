using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Kurs.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Kurs
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var bulider = new ContainerBuilder();

            bulider.RegisterControllers(typeof(MvcApplication).Assembly);
            bulider.RegisterApiControllers(typeof(MvcApplication).Assembly);
            bulider.RegisterType<SqlRestaurantData>()
                .As<IRestaurantData>()
                .InstancePerRequest();
            bulider.RegisterType<OdeToFoodDbContext>().InstancePerRequest();
                

            var container = bulider.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}