using AutoFixture;
using Hondenasiel.Common;
using Hondenasiel.Domain.Ref;
using Hondenasiel.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
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
        private List<Ras> _rassen;

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

            _rassen = new List<Ras>();

            VoegAnoniemeRefDataToe();
            VoegConcreteDataToe();

            _hondenasielDbCtx.Rassen.AddRange(
                _rassen);
            _hondenasielDbCtx.SaveChanges();

        }

        private void VoegAnoniemeRefDataToe() 
        {
            //Ras
            Fixture.AddManyTo(_rassen, 5);


        }

        private void VoegConcreteDataToe() 
        {
            //Rassen
            _rassen.Add(new Ras()
            {
                ID = Guid.NewGuid(),    
                Code = "rasCodeA",
                Omschrijving = "omschrijvingA"
            });
            
            _rassen.Add(new Ras()
            {
                ID = Guid.NewGuid(),
                Code = "rasCodeB",
                Omschrijving = "omschrijvingB"
            });
            
            _rassen.Add(new Ras()
            {
                ID = Guid.NewGuid(),
                Code = "rasCodeC",
                Omschrijving = "omschrijvingC"
            });

        }



        protected override void CleanUp()
        {
            base.CleanUp();
        }
    }
}
