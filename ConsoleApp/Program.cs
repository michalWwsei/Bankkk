using Bank;

class Program
{
    static void Main(string[] args)
    {
        Konto kontoStandard = new Konto("Michu Kowalski", 1000);
        OperacjeNaKoncie(kontoStandard);

        KontoPlus kontoPlus = new KontoPlus("Anna Nowak", 500, 200);
        OperacjeNaKonciePlus(kontoPlus);

        KontoLimit kontoLimit = new KontoLimit("Zbigniew Kowalski", 750, 150);
        OperacjeNaKoncieLimit(kontoLimit);
        Console.ReadKey();
    }

    static void OperacjeNaKoncie(Konto konto)
    {
        Console.WriteLine($"Operacje na koncie standardowym ({konto.Nazwa}):");
        WyswietlStanKonta(konto);
        konto.Wplata(200);
        WyswietlStanKonta(konto);
        konto.Wyplata(500);
        WyswietlStanKonta(konto);
        konto.BlokujKonto();
        try
        {
            konto.Wyplata(100);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        WyswietlStanKonta(konto);
        konto.OdblokujKonto();
        konto.Wyplata(100);
        WyswietlStanKonta(konto);
        Console.WriteLine();
    }


    static void OperacjeNaKonciePlus(KontoPlus konto)
    {
        Console.WriteLine($"Operacje na koncie Plus ({konto.Nazwa}):");
        WyswietlStanKontaPlus(konto);

        try
        {
            konto.Wyplata(600); 
            konto.Wyplata(100);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        Console.WriteLine();
    }

    static void OperacjeNaKoncieLimit(KontoLimit konto)
    {
        Console.WriteLine($"Operacje na koncie Limit ({konto.Nazwa}):");
        WyswietlStanKontaLimit(konto);
        konto.Wyplata(800);
        WyswietlStanKontaLimit(konto);
        konto.Wyplata(100);
        WyswietlStanKontaLimit(konto);
        konto.Wplata(50); 
        WyswietlStanKontaLimit(konto);
        konto.Limit = 300;
        WyswietlStanKontaLimit(konto);
        try
        {
            konto.Wyplata(1100);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        Console.WriteLine();
    }



    static void WyswietlStanKonta(Konto konto)
    {
        Console.WriteLine($"Bilans: {konto.Bilans}, Zablokowane: {konto.Zablokowane}");
    }


    static void WyswietlStanKontaPlus(KontoPlus konto)
    {
        Console.WriteLine($"Bilans: {konto.Bilans}, Zablokowane: {konto.Zablokowane}, Limit Debetowy: {konto.LimitDebetowy}");
    }


    static void WyswietlStanKontaLimit(KontoLimit konto)
    {
        Console.WriteLine($"Bilans: {konto.Bilans}, Zablokowane: {konto.Zablokowane}, Limit: {konto.Limit}");
    }
}
