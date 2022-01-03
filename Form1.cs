using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void FELADAT()
		{
			/* Ezt indítja a START gomb! */
			Padló(8, 16, 30);

		}
		void Alap1(double d) => Rombusz(120, d, Color.Yellow);
		void Alap2(double d) => Rombusz(60, d, Color.Green);

		void Rombusz(double f, double d) => Rombusz(f, d, f == 120 ? Color.Yellow : Color.Green);
		void Rombusz(double f , double d, Color szín)
		{
            using (new Átmenetileg(Jobbra, (180-f)/2))
            {
				Ismétlés(2, delegate () 
				{ 
					EJ(d, f);
					EJ(d, 180-f);
				});
            }
			Odatölt(-90, d / 2, szín);				
		}

		void EJ(double d, double f) { Előre(d); Jobbra(f); }
		void Odatölt(double f, double d, Color szín)
		{
			using (new Rajzol(false))
			using (new Átmenetileg(Balra, f))
			using (new Átmenetileg(Előre, d))
				Tölt(szín);
		}
        void Cikk(double f, double d)
        {
            using (new Rajzol(false))
			using (new Átmenetileg(Jobbra, (180 - f) / 2))
			{
				EJ(d, f);
				EJ(d, 180 - f);
			}
			Balra(180);
		}
        void Cakk(double f, double d)
        {
			using (new Rajzol(false))
			{
				Jobbra(90 - f / 2);
				EJ(d, -90);
				EJ(d, f/2);
			}
		}
        void inverz_Cakk(double f, double d)
        {
			using (new Rajzol(false))
			{ 
				Jobbra(-f);
				Előre(-d);
				Jobbra(90);
				Előre(-d);
			}
		}

		void sor1(int N, double d) => sor(N, d, 120);
		void sor2(int N, double d) => sor(N, d, 60);
		void sor(int N, double d, double f)
		{
			Ismétlés(N, delegate () 
			{
				Rombusz(f, d);
				Cikk(f, d);
				f = 180 - f;
			});
			f = 180 - f;
			Ismétlés(N, delegate () { Cikk(f, -d); f = 180 - f; });
		}

		void Mozaik(int M, int N, double d)
		{
			double f = 120;
			Ismétlés(M, delegate ()
			{
				sor(N, d, f);
				Cakk(f, d);
				f = 180 - f;
			});

			using (new Átmenetileg(Jobbra, 30))
			{
				

				Ismétlés(M, delegate ()
				{
					inverz_Cakk(180 - f, d);
					f = 180 - f;
				});

			}

			/*
			*/

		}
		void Padló(int M, int N, double d) 
		{
			Mozaik(M, N, d);
			Négyzetek(M - 1, N - 1, d);
		}


		void Négyzetek(int M, int N, double d)
		{
			double négyzetközépponttávolság = d * 1.36603;
			using (new Rajzol(false))
			using (new Átmenetileg(Jobbra, 30))
			using (new Átmenetileg(Előre, d))
			using (new Átmenetileg(Jobbra, 30 + 45))
			using (new Átmenetileg(Előre, d * Math.Sqrt(2) / 2))
			using (new Átmenetileg(Balra, 60 + 45))
			{
				Ismétlés(M, delegate ()
				{
					Ismétlés(N, delegate ()
					{
						Tölt(Color.Orange);
						Oldalaz(négyzetközépponttávolság);
					});
					Oldalaz(-N * négyzetközépponttávolság);
					Előre(négyzetközépponttávolság);
				});
				Hátra(M * négyzetközépponttávolság);
			}
		}

		void Oldalaz(double d)
		{
			using (new Átmenetileg(Jobbra, 90))
				Előre(d);
		}
	}
}
