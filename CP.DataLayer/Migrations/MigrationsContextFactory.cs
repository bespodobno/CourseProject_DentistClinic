using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.DataLayer.EFContext;

namespace CP.DataLayer.Migrations
{
    public class MigrationsContextFactory : IDbContextFactory<CourseProjectContext>
    {
        public CourseProjectContext Create()
        {
            return new CourseProjectContext("DBCP");
        }
    }
}
