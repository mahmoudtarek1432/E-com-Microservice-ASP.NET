using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entery in ChangeTracker.Entries<EntityBase>())
            {
                switch (entery.State)
                {
                    case EntityState.Added:
                        entery.Entity.CreatedBy = "svn";
                        entery.Entity.CreatedDate = DateTime.Now;  
                        break;
                    case EntityState.Modified:
                        entery.Entity.CreatedBy = "svn";
                        entery.Entity.CreatedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
