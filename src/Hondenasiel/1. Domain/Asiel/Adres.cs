namespace Hondenasiel.Domain.Asiel
{
	public class Adres
	{
		public string Straat { get; private set; }
		public string Huisnummer { get; private set; }
		public string Postcode { get; private set; }
		public string Gemeente { get; private set; }

		private Adres() { }

		public static Adres MaakAdres(
			string straat,
			string huisnummer,
			string postcode,
			string gemeente)
		{ 
			var adres = new Adres() 
			{
				Straat = straat,
				Huisnummer = huisnummer,
				Postcode = postcode,
				Gemeente = gemeente
			};

			return adres;
		
		}
	}
}