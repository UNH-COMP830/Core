using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;            

namespace GameSlam.Core.Extentions
{
    public static class EnumExtention
    {
        /// <summary>
        /// code: http://stackoverflow.com/questions/479410/enum-tostring-with-user-friendly-strings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string GetDescription<TEnum>(this TEnum enumerationValue) where TEnum : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            return enumerationValue.GetType()
               .GetField(enumerationValue.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false)
               .Cast<DescriptionAttribute>()
               .FirstOrDefault()?.Description ?? string.Empty;
        }

        /// <summary>
        /// Help with seeding the Enum values to the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="converter"></param>
        public static void SeedEnumValues<T, TEnum>(this IDbSet<T> dbSet, Func<TEnum, T> converter)
        where T : class => Enum.GetValues(typeof(TEnum))
                               .Cast<object>()
                               .Select(value => converter((TEnum)value))
                               .ToList()
                               .ForEach(instance => dbSet.AddOrUpdate(instance));
    }
}
