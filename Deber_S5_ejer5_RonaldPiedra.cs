using System;
using System.Collections.Generic;
using System.Linq;

public class NumerosInversos
{
    private List<int> numeros;

    public NumerosInversos()
    {
        numeros = Enumerable.Range(1, 10).ToList();
    }

    public void MostrarEnOrdenInverso()
    {
        numeros.Reverse();
        Console.WriteLine("📉 Números del 10 al 1:");
        Console.WriteLine(string.Join(", ", numeros));
    }
}

class Program
{
    static void Main(string[] args)
    {
        NumerosInversos inversor = new NumerosInversos();
        inversor.MostrarEnOrdenInverso();
    }
}