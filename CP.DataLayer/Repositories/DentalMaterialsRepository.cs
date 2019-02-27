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
    class DentalMaterialsRepository:IRepository<DentalMaterial>
    {
        CourseProjectContext context;

        public DentalMaterialsRepository(CourseProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<DentalMaterial> GetAll()
        {
            return context.DentalMaterials.Include(v => v.MaterialConsumptions).OrderBy(v => v.PurchaseDate).ToList();
        }
        public DentalMaterial Get(int id)
        {
            return context.DentalMaterials.Find(id);
        }
        public IEnumerable<DentalMaterial> Find(Func<DentalMaterial, bool> predicate)
        {
            return context.DentalMaterials.Include(v => v.MaterialConsumptions).Where(predicate).ToList();
        }
        public void Create(DentalMaterial t)
        {
            context.DentalMaterials.Add(t);
        }
        public void Update(DentalMaterial t)
        {
            context.Entry<DentalMaterial>(t).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var dentalMaterial = context.DentalMaterials.Find(id);
            context.DentalMaterials.Remove(dentalMaterial);
        }
    }

}
