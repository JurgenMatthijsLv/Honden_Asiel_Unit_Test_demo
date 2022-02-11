using Hondenasiel.Domain.Asiel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hondenasiel.tests
{
    internal class AsielBuilder
    {
        public Guid ID { get;  set; }
        public string Naam { get;  set; }
        public Adres Adres { get;  set; }
        public string Eigenaar { get;  set; }
        public List<Hond> Honden { get;  set; }

        public AsielRoot MaakAsiel()     
       {
            var asiel = AsielRoot.MaakAsiel(
                ID,
                Naam,
                Adres,
                Eigenaar,
                Honden);

            return asiel;
        }
    }
}
