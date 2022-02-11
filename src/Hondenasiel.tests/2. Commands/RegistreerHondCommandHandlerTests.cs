using AutoFixture;
using Hondenasiel.Application.Commands;
using Hondenasiel.Domain.Asiel;
using Hondenasiel.Domain.Ref;
using Hondenasiel.Infrastructure.Database;
using Hondenasiel.Messages.Commands;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hondenasiel.tests
{
    public class RegistreerHondCommandHandlerTests: BaseTest
    {
        private  Mock<IQueryRefRepository>
            _queryRefRepoMock;
        private  Mock<ICommandAsielRepository>
            _commandAsielRepoMock;

        private RegistreerHondCommandHandler _sut;

        [OneTimeSetUp]
        public void InitialSetUp() 
        { 
            _queryRefRepoMock = new Mock<IQueryRefRepository>();
            _commandAsielRepoMock = new Mock<ICommandAsielRepository>();

            _sut = new RegistreerHondCommandHandler(
                _commandAsielRepoMock.Object,
                _queryRefRepoMock.Object);
        }

        [Test]
        public async Task Handle_Hond_registreren_Hond_toegevoegd_aan_asiel() 
        {
            //Arrange
            var adres =
                 Adres.MaakAdres(
                     Fixture.Create<string>(),
                     Fixture.Create<string>(),
                     Fixture.Create<string>(),
                     Fixture.Create<string>());

            var honden =
                 new List<Hond>();
            var aantalHonden =
                Fixture.Create<int>();

            for (int i = 1; i <= aantalHonden; i++)
            {
                var hond =
                    Hond.MaakHond(
                         Fixture.Create<Guid>(),
                         Fixture.Create<string>(),
                         Fixture.Create<int>(),
                         Fixture.Create<Ras>(),
                         Fixture.Create<Kleur>(),
                         Fixture.Create<Geslacht>(),
                         Fixture.Create<bool>(),
                         Fixture.Create<string>());

                honden.Add(hond);
            }

            var asielBuilder = 
                Fixture.Build<AsielBuilder>()
                    .With(x => x.Adres, adres)
                    .With(x => x.Honden, honden)
                    .Create();

            var asiel = asielBuilder.MaakAsiel();

            _commandAsielRepoMock
                .Setup(x => x.GetAsiel(asiel.ID))
                .Returns(Task.FromResult(asiel));

            var ras = Fixture.Create<Ras>();
            _queryRefRepoMock.Setup(
                    x => x.GetRasByCode(ras.Code))
                .Returns(Task.FromResult(ras));
            
            var kleur = Fixture.Create<Kleur>();
            _queryRefRepoMock.Setup(
                    x => x.GetKleurByCode(kleur.Code))
                .Returns(Task.FromResult(kleur));

            var geslacht = Fixture.Create<Geslacht>();
            _queryRefRepoMock.Setup(
                    x => x.GetGeslachtByCode(geslacht.Code))
                .Returns(Task.FromResult(geslacht));

            var hondId = Fixture.Create<Guid>();
            var hondNaam = Fixture.Create<string>();
            var hondLeeftijd = Fixture.Create<int>();
            var hondOmschrijving = Fixture.Create<string>();

            var request = new RegistreerHondCommand(
                asiel.ID,
                hondId,
                hondNaam,
                hondLeeftijd,
                ras.Code,
                kleur.Code,
                geslacht.Code,
                true,
                hondOmschrijving);

            //Act
            await _sut.Handle(request, new CancellationToken());

            //Assert
            _commandAsielRepoMock.Verify(
                x => x.Save(asiel));

            Assert.Multiple(() =>
            {
                Assert.That(asiel.Honden,
                    Has.Exactly(1).Items
                    .Matches<Hond>(
                        x => x.ID == hondId
                            && x.Naam == hondNaam
                            && x.Leeftijd == hondLeeftijd
                            && x.Omschrijving == hondOmschrijving
                            && x.Kleur == kleur
                            && x.Ras == ras
                            && x.Geslacht == geslacht));
                Assert.That(asiel.Honden, Is.Unique);
                Assert.That(asiel.Honden, Has.Exactly(
                    aantalHonden + 1).Items);
            });

        }
    }
}