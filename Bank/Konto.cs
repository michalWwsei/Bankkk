namespace Bank
{
    public class Konto
    {
        private string klient;
        private decimal bilans;
        private bool zablokowane = false;

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        public string Nazwa => klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        public void Wplata(decimal kwota)
        {
            if (zablokowane)
            {
                throw new InvalidOperationException("Konto jest zablokowane. Nie można dokonać wpłaty.");
            }
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wpłaty musi być większa od zera.");
            }
            bilans += kwota;
        }

        public void Wyplata(decimal kwota)
        {
            if (zablokowane)
            {
                throw new InvalidOperationException("Konto jest zablokowane. Nie można dokonać wypłaty.");
            }
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wypłaty musi być większa od zera.");
            }
            if (kwota > bilans)
            {
                throw new InvalidOperationException("Niewystarczające środki na koncie.");
            }
            bilans -= kwota;
        }

        public void BlokujKonto()
        {
            zablokowane = true;
        }

        public void OdblokujKonto()
        {
            zablokowane = false;
        }
    }
}
