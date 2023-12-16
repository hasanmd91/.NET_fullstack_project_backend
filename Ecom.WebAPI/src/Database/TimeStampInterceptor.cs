using Ecom.Core.src.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecom.WebAPI.src.Database
{
    public class TimeStampInterceptor : SaveChangesInterceptor
    {


        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var changedData = eventData?.Context?.ChangeTracker.Entries();

            if (changedData is not null)
            {
                var updatedEntries = changedData.Where(entity => entity.State == EntityState.Modified);
                var createdEntries = changedData.Where(entity => entity.State == EntityState.Added);

                foreach (var e in updatedEntries)
                {
                    if (e.Entity is BaseEntity entity)
                    {
                        entity.UpdatedDate = DateTime.Now;
                    }
                }

                foreach (var e in createdEntries)
                {
                    if (e.Entity is BaseEntity entity)
                    {
                        entity.CreatedDate = DateTime.Now;
                    }
                }

            }
            return base.SavingChanges(eventData, result);
        }
    }
}