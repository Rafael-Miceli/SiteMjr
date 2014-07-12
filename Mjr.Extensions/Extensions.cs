using System;
using WebSiteMjr.Domain.Interfaces.Model;
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

        public static DateTime ConvertToTimeZone(this DateTime dataTime)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dataTime, tz);
        }

        public static bool Exists(this INotDeletable notDeletableEntity)
        {
            return notDeletableEntity != null && !notDeletableEntity.IsDeleted;
        }
    }
}
