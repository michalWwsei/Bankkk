using Bank;

namespace TestBank
{
    [TestClass]
    public class TestBank
    {
        private Konto konto;

        [TestInitialize]
        public void Setup()
        {
            konto = new Konto("Jan Kowalski", 100);
        }

        [TestMethod]
        public void Wplata_PrawidłowaKwota_ZwiększaBilans()
        {
            konto.Wplata(50);
            Assert.AreEqual(150, konto.Bilans);
        }

        [TestMethod]
        public void Wplata_NegatywnaKwota_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => konto.Wplata(-50));
        }

        [TestMethod]
        public void Wplata_KontoZablokowane_NieZmieniaBilansu()
        {
            konto.BlokujKonto();
            decimal bilansPrzed = konto.Bilans;
            konto.Wplata(50);
            Assert.AreEqual(bilansPrzed, konto.Bilans); 
        }

        [TestMethod]
        public void Wyplata_PrawidłowaKwota_ZmniejszaBilans()
        {
            konto.Wyplata(50);
            Assert.AreEqual(50, konto.Bilans);
        }
        [TestMethod]
        public void Wyplata_NegatywnaKwota_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(-50));
        }

        [TestMethod]
        public void Wyplata_NiewystarczająceSrodki_ThrowsInvalidOperationException()
        {
            Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(150));
        }

        [TestMethod]
        public void Wyplata_KontoZablokowane_ThrowsInvalidOperationException()
        {
            konto.BlokujKonto();
            Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(50));
        }

        [TestMethod]
        public void BlokujKonto_UstawiaStanZablokowane()
        {
            konto.BlokujKonto();
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void OdblokujKonto_UstawiaStanNieZablokowane()
        {
            konto.BlokujKonto();
            konto.OdblokujKonto();
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        public void Konstruktor_InicjalizujePola()
        {
            Assert.AreEqual("Jan Kowalski", konto.Nazwa);
            Assert.AreEqual(100, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
        }
    }

    [TestClass]
    public class TestKontoPlus
    {
        private KontoPlus konto;

        [TestInitialize]
        public void Setup()
        {
            konto = new KontoPlus("Jan Kowalski", 100, 50);
        }

        [TestMethod]
        public void Konstruktor_InicjalizujePola()
        {
            Assert.AreEqual("Jan Kowalski", konto.Nazwa);
            Assert.AreEqual(150, konto.Bilans); 
            Assert.IsFalse(konto.Zablokowane);
            Assert.AreEqual(50, konto.LimitDebetowy);
        }


        [TestMethod]
        public void Wyplata_WykorzystanieDebetu_BlokujeKonto()
        {
            konto.Wyplata(120); 
            Assert.AreEqual(0, konto.Bilans);  
            Assert.IsTrue(konto.Zablokowane);
        }


        [TestMethod]
        public void Wyplata_PrzekroczenieDebetu_ZwracaWyjatek()
        {
            Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(200));
        }

        [TestMethod]
        public void Wplata_PoWykorzystaniuDebetu_OdblokowujeKonto()
        {
            konto.Wyplata(120);  
            Assert.IsTrue(konto.Zablokowane);
            konto.Wplata(20);   
            Assert.IsFalse(konto.Zablokowane);
            Assert.AreEqual(50, konto.Bilans); 
        }

        [TestMethod]
        public void ZmianaLimitu_ZmieniaDostepneSrodki()
        {
            konto.LimitDebetowy = 100;
            Assert.AreEqual(200, konto.Bilans);
        }

        [TestMethod]
        public void Wyplata_CzescioweWykorzystanieDebetu()
        {
            konto.Wyplata(50);
            Assert.AreEqual(100, konto.Bilans); 
            Assert.IsFalse(konto.Zablokowane);
        }

    }

    [TestClass]
    public class TestKontoLimit
    {
        private KontoLimit konto;

        [TestInitialize]
        public void Setup()
        {
            konto = new KontoLimit("Jan Kowalski", 100, 50);
        }

        [TestMethod]
        public void Konstruktor_InicjalizujePola()
        {
            Assert.AreEqual("Jan Kowalski", konto.Nazwa);
            Assert.AreEqual(150, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
            Assert.AreEqual(50, konto.Limit);
        }

        [TestMethod]
        public void Wyplata_WykorzystanieDebetu_BlokujeKonto()
        {
            konto.Wyplata(120);
            Assert.AreEqual(0, konto.Bilans);
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void Wyplata_PrzekroczenieDebetu_ZwracaWyjatek()
        {
            Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(200));
        }

        [TestMethod]
        public void Wplata_PoWykorzystaniuDebetu_OdblokowujeKonto()
        {
            konto.Wyplata(120);
            Assert.IsTrue(konto.Zablokowane);
            konto.Wplata(20);
            Assert.IsFalse(konto.Zablokowane);
            Assert.AreEqual(50, konto.Bilans); 
        }

        [TestMethod]
        public void ZmianaLimitu_ZmieniaDostepneSrodki()
        {
            konto.Limit = 100;
            Assert.AreEqual(200, konto.Bilans);
        }

        [TestMethod]
        public void Wyplata_CzescioweWykorzystanieDebetu()
        {
            konto.Wyplata(50);
            Assert.AreEqual(100, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
        }

    }

}
