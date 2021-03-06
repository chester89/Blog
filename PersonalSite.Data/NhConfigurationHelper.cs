﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using PersonalSite.Data.Conventions;
using PersonalSite.Data.Mappings;

namespace PersonalSite.Data
{
    public class NhConfigurationHelper
    {
        public static Configuration GetConfiguration()
        {
            return GetFluentConfiguration().BuildConfiguration();
        }

        private static FluentConfiguration GetFluentConfiguration()
        {
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(
                        builder => builder.FromConnectionStringWithKey("db")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostMap>()
                .Conventions.AddFromAssemblyOf<CustomIdConvention>());
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return GetFluentConfiguration().BuildSessionFactory();
        }
    }
}
