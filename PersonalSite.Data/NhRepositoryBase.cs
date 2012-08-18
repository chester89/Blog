using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using PersonalSite.Core.Contracts;

namespace PersonalSite.Data
{
    public class NhRepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly ISession Session;
        protected static bool autoCreateDb;

        static NhRepositoryBase()
        {
            autoCreateDb = false;

            if (autoCreateDb)
            {
                var configuration = NhConfigurationHelper.GetConfiguration();

                new SchemaExport(configuration).Create(true, true);
            }
        }

        protected NhRepositoryBase(ISession session)
        {
            Session = session;
        }

        public T Get(int id)
        {
            return Session.Get<T>(id);
        }

        public void Add(T newEntity)
        {
            Session.Save(newEntity);
        }

        public void Update(T entity)
        {
            Session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
            Session.Flush();
        }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>().AsQueryable();
        }

        public void Transaction(Action action)
        {
            if (Session.Transaction.IsActive)
            {
                action();
                return;
            }
            using (var transaction = Session.BeginTransaction())
            {
                action();
                transaction.Commit();
            }
        }
    }
}