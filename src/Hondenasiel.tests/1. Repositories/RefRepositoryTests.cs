using AutoFixture;
using Hondenasiel.Common;
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
        [Category(Constants.TestCategories.RefDataTests)]
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
        [Category(Constants.TestCategories.RefDataTests)]
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

        [Test]
        [Category(Constants.TestCategories.RefDataTests)]
        public async Task GetAllRassen_bevat_enkel_unieke_elemeneten() 
        {
            //Arrange

            //Act
            var rassen = await _sut.GetAllRassen();

            //Assert
            Assert.That(rassen, Is.Unique);

        }

        [Test]
        [Category(Constants.TestCategories.RefDataTests)]
        public async Task GetAllRassen_bevat_8_elemeneten()
        {
            //Arrange

            //Act
            var rassen = await  _sut.GetAllRassen();

            //Assert
            Assert.That(rassen, Has.Exactly(8).Items);
        }


        [Test]
        [Category(Constants.TestCategories.RefDataTests)]
        public async Task GetAllRassen_bevat_exact_1_element_met_omschrijving_RasA()
        {
            //Arrange

            //Act
            var rassen = await _sut.GetAllRassen();

            //Assert
            Assert.That(rassen, Has.Exactly(1).Matches<Ras>(
                item => item.Omschrijving == "omschrijvingA"));

        }


        [OneTimeTearDown]
        public void FinalTearDown() 
        {
        
        }

    }
}