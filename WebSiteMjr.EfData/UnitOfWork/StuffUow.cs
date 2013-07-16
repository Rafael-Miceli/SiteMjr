using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.UnitOfWork
{
    public class StuffUow : UnitOfWork<StuffContext>
    {
        public StuffUow()
        {}

        public StuffUow(StuffContext context):base(context)
        {}
    }
}
