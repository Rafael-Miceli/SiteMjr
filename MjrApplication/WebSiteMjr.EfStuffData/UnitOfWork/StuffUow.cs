using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.UnitOfWork
{
    public class StuffUow : UnitOfWork<StuffContext>
    {
        public StuffUow()
        {}

        public StuffUow(StuffContext context):base(context)
        {}
    }
}
