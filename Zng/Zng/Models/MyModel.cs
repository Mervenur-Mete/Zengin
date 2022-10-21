using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Zng.Models
{
    public partial class MyModel : DbContext
    {
        public MyModel()
            : base("name=MyModel2")
        {
        }

        public virtual DbSet<Hizmetler> Hizmetlers { get; set; }
        public virtual DbSet<Referanslar> Referanslars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
