using AutoFixture;
using Hondenasiel.Common;
using Hondenasiel.Domain.Ref;
using Hondenasiel.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hondenasiel.tests
{
    public abstract class BaseRepositoryTest: BaseTest
    {
        protected DbContextOptionsBuilder<HondenasielDbContext>
            _dbCtxOptionsBuilder;
        private DbContextOptions<HondenasielDbContext> _dbCtxOptions;

        protected HondenasielDbContext _hondenasielDbCtx;

        public BaseRepositoryTest()
        {
            Init();
        }

        private void Init()
        {
            _dbCtxOptionsBuilder = 
                new DbContextOptionsBuilder<HondenasielDbContext>();

            _dbCtxOptions = _dbCtxOptionsBuilder
                .UseInMemoryDatabase(
                    Constants.HondenasielDbContext).Options;

            _hondenasielDbCtx = new HondenasielDbContext(_dbCtxOptions);

            VoegAnoniemeRefDataToe();

        }

        private void VoegAnoniemeRefDataToe() 
        {
            //Ras
            var rassen = new List<Ras>();
            Fixture.AddManyTo(rassen, 5);
            _hondenasielDbCtx.Rassen.AddRange(
                rassen.ToArray());
            _hondenasielDbCtx.SaveChanges();

            //Kleur
            var kleuren = new List<Kleur>();
             Fixture.AddManyTo(kleuren, 5);
        }



        protected override void CleanUp()
        {
            base.CleanUp();
        }
    }
}
