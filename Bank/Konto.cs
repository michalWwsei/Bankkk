namespace Bank
{
    public class Konto
    {
        private string klient;
        protected decimal bilans;
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
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wpłaty musi być większa od zera.");
            }

            if (!zablokowane) 
            {
                bilans += kwota;
            }
            
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

    public class KontoPlus : Konto
    {
        private decimal limitDebetowy;
        private bool debetWykorzystany;

        public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limitDebetowy = 0) : base(klient, bilansNaStart)
        {
            this.limitDebetowy = limitDebetowy;
            this.debetWykorzystany = false;
        }

        public decimal LimitDebetowy
        {
            get => limitDebetowy;
            set => limitDebetowy = value;
        }

        public new decimal Bilans => base.Bilans + (debetWykorzystany ? 0 : limitDebetowy); 


        public new void Wplata(decimal kwota)
        {
            base.Wplata(kwota);

            if (base.Bilans >= 0 && debetWykorzystany)
            {
                OdblokujKonto();
                debetWykorzystany = false;
            }
        }


        public new void Wyplata(decimal kwota)
        {
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wypłaty musi być większa od zera.");
            }

            if (Zablokowane && debetWykorzystany)
            {
                throw new InvalidOperationException("Konto jest zablokowane z powodu wykorzystania limitu debetowego.");
            }
            else if (Zablokowane)
            {
                throw new InvalidOperationException("Konto jest zablokowane.");
            }

            if (kwota > base.Bilans + (debetWykorzystany ? 0 : limitDebetowy))
            {
                throw new InvalidOperationException("Niewystarczające środki na koncie, łącznie z limitem debetowym.");
            }

            if (kwota > base.Bilans)
            {
                debetWykorzystany = true;

                decimal dostepneSrodki = base.Bilans; 

                if (dostepneSrodki > 0)
                { 
                    base.Wyplata(dostepneSrodki); 
                }

                BlokujKonto(); 
            }
            else
            {
                base.Wyplata(kwota);
            }
        }
    }
}

