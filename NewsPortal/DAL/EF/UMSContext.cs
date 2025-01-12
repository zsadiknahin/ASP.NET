

using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace DAL.EF
{
    internal class UMSContext : DbContext
    {
        public DbSet<News> News { get; set; }
    }
}

