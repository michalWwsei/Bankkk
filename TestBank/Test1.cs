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
        public void Wplata_KontoZablokowane_ThrowsInvalidOperationException()
        {
            konto.BlokujKonto();
            Assert.ThrowsException<InvalidOperationException>(() => konto.Wplata(50));
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
}
