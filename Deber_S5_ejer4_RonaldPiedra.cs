using System;
using System.Collections.Generic;

public class Loteria
{
    private List<int> numerosGanadores;

    public Loteria()
    {
        numerosGanadores = new List<int>();
    }

    public void PedirNumeros()
    {
        Console.WriteLine("🎰 Ingrese 6 números ganadores de la lotería:");
        for (int i = 1; i <= 6; i++)
        {
            Console.Write($"Número {i}: ");
            string entrada = Console.ReadLine();
            int numero;

            while (!int.TryParse(entrada, out numero))
            {
                Console.Write("❌ Entrada inválida. Ingrese un número válido: ");
                entrada = Console.ReadLine();
            }

            numerosGanadores.Add(numero);
        }
    }

    public void MostrarOrdenados()
    {
        numerosGanadores.Sort();
        Console.WriteLine("\n🏆 Números ganadores ordenados:");
        foreach (int num in numerosGanadores)
        {
            Console.WriteLine(num);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Loteria loteria = new Loteria();
        loteria.PedirNumeros();
        loteria.MostrarOrdenados();
    }
}
