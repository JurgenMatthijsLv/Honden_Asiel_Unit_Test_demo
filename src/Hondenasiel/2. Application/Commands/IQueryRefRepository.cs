using Hondenasiel.Domain.Ref;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hondenasiel.Application.Commands
{
	internal interface IQueryRefRepository
	{
		Task<Ras> GetRasByCode(string rasCode);

		Task<Kleur> GetKleurByCode(string kleurCode);

		Task<Geslacht> GetGeslachtByCode(string geslachtCode);

		Task<List<Ras>> GetAllRassen();
		
	}
}