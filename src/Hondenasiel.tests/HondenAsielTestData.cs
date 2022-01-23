using Hondenasiel.Domain.Ref;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hondenasiel.tests
{
    internal class HondenasielTestData
    {
        public static IEnumerable<TestCaseData> RasTestData
            {
                get
                {
                    yield return new TestCaseData(
                        new Ras()
                        {
                            Code = "rasCodeA",
                            Omschrijving = "omschrijvingA"
                        });

                    yield return new TestCaseData(
                        new Ras()
                        {
                            Code = "rasCodeB",
                            Omschrijving = "omschrijvingB"
                        });

                    yield return new TestCaseData(
                        new Ras()
                        {
                            Code = "rasCodeC",
                            Omschrijving = "omschrijvingC"
                        });
                  }
            }
    }
}
