using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ApiTemplate.Repositories;
using ApiTemplate.Repositories.Base;
using Autofac;

namespace ApiTemplate.Modules
{
    public class EFModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //https://autofac.readthedocs.io/en/latest/integration/aspnetcore.html#differences-from-asp-net-classic
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(BaseContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();
        }
    }
}