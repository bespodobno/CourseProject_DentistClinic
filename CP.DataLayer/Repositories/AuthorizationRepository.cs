using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.DataLayer.EFContext;
using CP.DataLayer.Entites;
using CP.DataLayer.Interfaces;
using System.Data.Entity;

namespace CP.DataLayer.Repositories
{
    class AuthorizationRepository : IRepository<Authorization>
    {
        CourseProjectContext context;

        public AuthorizationRepository(CourseProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<Authorization> GetAll()
        {
            return context.Authorizations;
        }
        public Authorization Get(int id)
        {
            return context.Authorizations.Find(id);
        }
        public IEnumerable<Authorization> Find(Func<Authorization, bool> predicate)
        {
            return context.Authorizations.Where(predicate).ToList();
        }
        public void Create(Authorization t)
        {
            context.Authorizations.Add(t);
        }
        public void Update(Authorization t)
        {
            context.Entry<Authorization>(t).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var authorization = context.Authorizations.Find(id);
            context.Authorizations.Remove(authorization);
        }
    }
}

