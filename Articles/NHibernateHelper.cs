using Articles.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Articles
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurePath = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\nhibernate.cfg.xml");
            configuration.Configure(configurePath);
            //configuration.AddAssembly(typeof(Book).Assembly); заменяем вот на этот код\\
            HbmSerializer.Default.Validate = true;
            var stream = HbmSerializer.Default.Serialize(Assembly.GetAssembly(typeof(Cataloges)));
            configuration.AddInputStream(stream);
            //*****************************************************************************************************\\
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            //Позволяет Nhibernate самому создавать в БД таблицу и поля к ним. 
            new SchemaUpdate(configuration).Execute(true, true);
            return sessionFactory.OpenSession();
        }
    }
}