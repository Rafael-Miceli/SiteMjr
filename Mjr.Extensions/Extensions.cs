using WebSiteMjr.Domain.Model.Person;

namespace Mjr.Extensions
{
    public static class Extensions
    {
        public static int? IfEntitieIsNotNullReturnId<T>(this T entitie) where T : IntId
        {
            if (entitie != null)
                return entitie.Id;

            return null;
        }
    }
}
