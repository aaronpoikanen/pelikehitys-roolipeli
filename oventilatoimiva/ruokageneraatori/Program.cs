using System;
using System.Collections.Generic;

enum Paaruoka
{
    nautaa,
    kanaa,
    kasviksia

}

enum Lisuke
{
    perunaa,
    riisiä,
    pastaa
}

enum Kastike
{
    curry,
    hapanimelä,
    pippuri,
    chili
}

class Ateria
{
    public Paaruoka Paa { get; set; }
    public Lisuke Lisa { get; set; }
    public Kastike Kasti { get; set; }

    public Ateria(Paaruoka paa, Lisuke lisa, Kastike kasti)
    {
        Paa = paa;
        Lisa = lisa;
        Kasti = kasti;
    }

    public void Tulosta()
    {
        Console.WriteLine($"{Paa} ja {Lisa} {Kasti}-kastikkeella");
    }
}

class Program
{
    static T KysyValinta<T>() where T : struct, Enum
    {
        while (true)
        {
            Console.WriteLine("Valitse jokin seuraavista:");
            foreach (var arvo in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"- (arvo)");
            }
            string syote = Console.ReadLine();

            if (Enum.TryParse<T>(syote, true, out T tulos))
            {
                return tulos; 
            }

            Console.WriteLine("Virheellinen valinta, yritä uudelleen./n");
        }
    }

    static void Main(string[] args)
    {
        List<Ateria> ateriat = new List<Ateria>();

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"\nKootaan ateria {i + 1}");

            Console.WriteLine("Valitse pääraaka-aine:");
            var paa = KysyValinta<Paaruoka>();

            Console.WriteLine("Valitse lisuke:");
            var lisa = KysyValinta<Lisuke>();

            Console.WriteLine("Valitse kastike:");
            var kasti = KysyValinta<Kastike>();

            Ateria uusiAteria = new Ateria(paa, lisa, kasti);
            ateriat.Add(uusiAteria);
        }

        Console.WriteLine("\nValitsemasi ateriat:");
        foreach (var ateria in ateriat)
        {
            ateria.Tulosta();
        }
    }
}

    
    
        



