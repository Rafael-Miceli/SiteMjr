using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.Helpers;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.DataRepository
{
    public class ToolRepository: GenericStuffRepository<Tool>, IToolRepository
    {
        public ToolRepository(UnitOfWork<StuffContext> uow) : base(uow){}

        public void AddOrUpdateGraph(Tool tool)
        {
            if (tool.State == State.Added)
                Context.Set<Tool>().Add(tool);
            else
            {
                Context.Set<Tool>().Add(tool);
                Context.ApplyStateChanges();
            }
        }

        public void UpdateGraph(Tool tool)
        {
            Context.Set<Tool>().Add(tool);
            Context.ApplyStateChanges();
        }

        public Tool GetToolByName(string name)
        {
            return Get(n => n.Name.ToLower() == name.ToLower());
        }
    }
}
