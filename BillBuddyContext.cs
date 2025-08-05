using System.Data.Entity;

namespace BillBuddy
{
    public class BillBuddyContext : DbContext
    {
        public BillBuddyContext() : base("name=BillBuddyConnection")
        {
        }

        public DbSet<Bill> Bills { get; set; }
    }
}
