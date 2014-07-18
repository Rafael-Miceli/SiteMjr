using WebSiteMjr.EfBaseData.UnitOfWork;

namespace WebSiteMjr.EfConfigurationMigrationData.UnitOfWork
{
    public class MigrationUow : UnitOfWork<MjrSolutionContext>
    {
        
        public MigrationUow()
        {}

        public MigrationUow(MjrSolutionContext context)
            : base(context)
        {}
    }
}
