using Hondenasiel.Application.Commands;
using Hondenasiel.Domain.Ref;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hondenasiel.Infrastructure.Database
{
	public class QueryRefRepository : IQueryRefRepository
	{
		private readonly HondenasielDbContext _hondenasielDbContext;

		public QueryRefRepository(HondenasielDbContext hondenasielDbContext)
		{
			_hondenasielDbContext = hondenasielDbContext;
		}

		public async Task<Geslacht> GetGeslachtByCode(string geslachtCode)
		{
			return await _hondenasielDbContext.Geslachten.FirstAsync(x => x.Code == geslachtCode);
		}

		public async Task<Kleur> GetKleurByCode(string kleurCode)
		{
			return await _hondenasielDbContext.Kleuren.FirstAsync(x => x.Code == kleurCode);
		}

		public async Task<Ras> GetRasByCode(string rasCode)
		{
           return await _hondenasielDbContext.Rassen.FirstAsync(x => x.Code == rasCode);
		}

		public async Task<List<Ras>> GetAllRassen() 
		{
			return await _hondenasielDbContext.Rassen.ToListAsync();
		}
	}
}