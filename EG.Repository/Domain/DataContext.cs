using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class DataContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblWebhookRequest>().HasOne(a => a.TblBitcoin).WithMany(b => b.TblWebhookRequests).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TblWebhookRequest>().HasOne(a => a.TblTron).WithMany(b => b.TblWebhookRequests).OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=.;initial catalog=ExchangeGatewayDb;integrated security=true;MultipleActiveResultSets=True;");
            }
        }

        public virtual DbSet<TblError> TblErrors { get; set; }
        public virtual DbSet<TblTron> TblTrons { get; set; }
        public virtual DbSet<TblBitcoin> TblBitcoins { get; set; }
        public virtual DbSet<TblWallet> TblWallets { get; set; }
        public virtual DbSet<TblSellRequest> TblSellRequests { get; set; }
        public virtual DbSet<TblWebhookRequest> TblWebhookRequests { get; set; }
    }
}
