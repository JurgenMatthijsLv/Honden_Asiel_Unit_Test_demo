using Hondenasiel.Messages.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hondenasiel.Application.Commands
{
	public class PasFotoAanCommandHandler : IRequestHandler<PasFotoAanCommand>
	{
		private readonly ICommandAsielRepository _CommandAsielRepo;

		public PasFotoAanCommandHandler(
			ICommandAsielRepository commandAsielRepo)
		{
			_CommandAsielRepo = commandAsielRepo;
		}

		public async Task<Unit> Handle(PasFotoAanCommand request, CancellationToken cancellationToken)
		{
			var asiel = await _CommandAsielRepo.GetAsiel(request.AsielId);

			if (asiel == null)
			{
				throw new ArgumentException($"'{nameof(request.AsielId)}' does not exists.", nameof(request.AsielId));
			}

			asiel.LaadHondFotoOp(request.HondId, request.FotoPath);

			await _CommandAsielRepo.Save(asiel);

			return await Unit.Task;
		}
	}
}