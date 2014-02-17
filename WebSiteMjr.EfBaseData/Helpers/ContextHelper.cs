using System.Data.Entity;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.EfBaseData.Helpers
{
    public static class ContextHelper
    {
        //Only use with short lived contexts 
        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>().Where(c => c.Entity != null))
            {
                IObjectWithState stateInfo = entry.Entity;
                entry.State = StateHelpers.ConvertState(stateInfo.State);
            }
        }
    }
}
