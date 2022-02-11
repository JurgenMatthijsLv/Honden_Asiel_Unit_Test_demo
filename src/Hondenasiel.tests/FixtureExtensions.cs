using AutoFixture;
using Hondenasiel.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hondenasiel.tests
{
    public static class FixtureExtension
    {
        public static T CreateDataEntity<T>(
            this Fixture fixture)
            where T : new()
        {
            var propertyInfos =
                typeof(T).GetProperties();

            //Propeties met private setter ophalen
            var PropertyInfosWithPrivateSetter = propertyInfos
                .Where(x =>
                    x.GetSetMethod(true).IsPrivate)
                .ToList();

            //Types zonder properties met een private
            //setter komen niet in aanmerking
            if (PropertyInfosWithPrivateSetter.Count() == 0)
            {
                throw new ArgumentException(
                    string.Format("{0} heeft geen properties met een private setter", typeof(T).Name));
            }

            //Object aanmaken
            var dataEntity = (T)Activator.CreateInstance(typeof(T));

            //Properties opvullen
            foreach (var propertyInfo in PropertyInfosWithPrivateSetter)
            {
                var propertyTypeName =
                    propertyInfo.PropertyType.Name;

                if (propertyTypeName == typeof(int).Name)
                {
                    propertyInfo
                         .SetValue(dataEntity, fixture.Create<int>());
                }
                else if (propertyTypeName == typeof(string).Name)
                {
                    propertyInfo
                         .SetValue(dataEntity, fixture.Create<string>());
                }
                else if (propertyTypeName == typeof(int?).Name)
                {
                    propertyInfo
                         .SetValue(dataEntity, fixture.Create<int?>());
                }
                else if (propertyTypeName == typeof(DateTime).Name)
                {
                    propertyInfo
                         .SetValue(dataEntity, fixture.Create<DateTime>());
                }

            }

            return dataEntity;
        }
    }
}
