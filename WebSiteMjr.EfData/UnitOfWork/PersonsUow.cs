using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.UnitOfWork
{
    public class PersonsUow : UnitOfWork<PersonsContext>
    {
        public PersonsUow()
        {}

        public PersonsUow(PersonsContext context): base(context)
        {}
    }
}
