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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hondenasiel.tests
{
    public class PasFotoAanCommandHandlerTests : BaseTest
    {
        private  Mock<ICommandAsielRepository>
            _commandAsielRepoMock;

        private PasFotoAanCommandHandler _sut;

        [OneTimeSetUp]
        public void InitialSetUp() 
        { 
            _commandAsielRepoMock = new Mock<ICommandAsielRepository>();

            _sut = new PasFotoAanCommandHandler(
                _commandAsielRepoMock.Object);
        }

        [Test]
        public async Task Handle_FilePath_foto_wijzigen_nieuw_path_opgeslagen_in_db() 
        {
            //Arrange
            var filePathFoto =
                Fixture.Create<string>();
            var asielId = 
                Fixture.Create<Guid>();
            var hondId =
                Fixture.Create<Guid>();

            var asielBuilder = 
                Fixture.Build<AsielBuilder>()
                    .With(x => x.ID, asielId)
                    .Create();
            var asiel = asielBuilder.MaakAsiel();

            var hond = Hond.MaakHond(
                hondId,
                Fixture.Create<string>(),
                Fixture.Create<int>(),
                Fixture.Create<Ras>(),
                Fixture.Create<Kleur>(),
                Fixture.Create<Geslacht>(),
                Fixture.Create<bool>(),
                Fixture.Create<string>());

            asiel.Honden.Add(hond);

            _commandAsielRepoMock.Setup(
                    x => x.GetAsiel(asielId))
                .Returns(Task.FromResult(asiel));

            var request = new PasFotoAanCommand(
                asielId, hondId, filePathFoto);

            //Act
            await _sut.Handle(request, new CancellationToken());

            //Assert
            _commandAsielRepoMock.Verify(
                x => x.Save(asiel), Times.Once);

            Assert.That(asiel.Honden.FirstOrDefault(x => x.ID == hondId).Foto,
                Is.EqualTo(filePathFoto));

        }
    }
}