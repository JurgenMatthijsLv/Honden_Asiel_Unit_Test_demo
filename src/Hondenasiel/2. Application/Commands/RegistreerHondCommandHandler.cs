using Hondenasiel.Messages.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hondenasiel.Application.Commands
{
	public class RegistreerHondCommandHandler : IRequestHandler<RegistreerHondCommand>
	{
		private readonly IQueryRefRepository _queryRefRepo;
		private readonly ICommandAsielRepository _commandAsielRepo;

		public RegistreerHondCommandHandler(
			ICommandAsielRepository commandAsielRepo,
			IQueryRefRepository queryRefRepo)
		{
			_commandAsielRepo = commandAsielRepo;
			_queryRefRepo = queryRefRepo;
		}

		public async Task<Unit> Handle(RegistreerHondCommand request, CancellationToken cancellationToken)
		{
			var asiel = await _commandAsielRepo.GetAsiel(request.AsielId);

			if (asiel == null)
			{
				throw new ArgumentException($"'{nameof(request.AsielId)}' does not exists.", nameof(request.AsielId));
			}

			var ras = await _queryRefRepo.GetRasByCode(request.RasCode);
			var kleur = await _queryRefRepo.GetKleurByCode(request.KleurCode);
			var geslacht = await _queryRefRepo.GetGeslachtByCode(request.GeslachtCode);

			asiel.RegistreerHond(
				request.HondId,
				request.HondNaam,
				request.Leeftijd,
				ras,
				kleur,
				geslacht,
				request.HeeftStamboom,
				request.Omschrijving);

			await _commandAsielRepo.Save(asiel);

			return await Unit.Task;
		}
	}
}