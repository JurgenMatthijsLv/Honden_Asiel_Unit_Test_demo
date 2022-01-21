using AutoFixture;
using Hondenasiel.Domain.Ref;
using Hondenasiel.Infrastructure.Database;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hondenasiel.tests
{
    public class RefRepositoryTests : BaseRepositoryTest
    {
        private QueryRefRepository _sut;

        [OneTimeSetUp]   
        public void InitialSetup()
        {
            _sut = new QueryRefRepository(_hondenasielDbCtx);
        }

        [Test]
        public async Task GetRasByCode_ongeldige_rascode_throws_exception()
        { 
            //Arrange
            var ongeldigeRasCode =
                Fixture.Create<string>();

            //Act

            //Assert
            Assert.That(async () => await _sut.GetRasByCode(ongeldigeRasCode)
                , Throws.TypeOf<InvalidOperationException>()
                   .With.Message.EqualTo("Sequence contains no elements")
                   .With.Matches<InvalidOperationException>(
                        ex =>  ex.Source == "System.Linq"));
                
        }

        [Test]
        [TestCaseSource(typeof(HondenasielTestData), "RasTestData")]
        public async Task GetRasByCode_geldige_rascode_geeft_corresponderend_ras(
            Ras rasFromDataSource)
        {
            //Arrange

            //Act
            var ras  = await _sut.GetRasByCode(rasFromDataSource.Code);

            //Assert
            Assert.That(rasFromDataSource.Omschrijving, Is.EqualTo(
                _hondenasielDbCtx.Rassen.FirstOrDefault(
                    x => x.Code == ras.Code).Omschrijving));
        }

       [OneTimeTearDown]
        public void FinalTearDown() 
        {
        
        }

    }
}