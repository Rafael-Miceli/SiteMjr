using System.Data.Entity;

namespace WebSiteMjr.EfBaseData.Context
{
    public class BaseContext<TContext> : DbContext where TContext : DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected BaseContext() :
            base("DefaultConnection")
        {

        }
    }
}
