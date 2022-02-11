using Hondenasiel.Domain.Asiel;
using System;
using System.Threading.Tasks;

namespace Hondenasiel.Application.Commands
{
	public interface ICommandAsielRepository
	{
		Task<AsielRoot> GetAsiel(Guid asielId);
		Task Save(AsielRoot asiel);
	}
}