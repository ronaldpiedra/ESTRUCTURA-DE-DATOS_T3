using System;
using System.Collections.Generic;

class Programa
{
    static void Main()
    {
        Console.WriteLine("UEA - Estructura de Datos - Semana 10");
        Console.WriteLine("Simulación de vacunación COVID-19 con teoría de conjuntos\n");

        // ---------------------------------------------------------------
        // 1. Conjunto total de ciudadanos (U)
        // ---------------------------------------------------------------
        HashSet<string> todos = new HashSet<string>();
        for (int i = 1; i <= 500; i++)
        {
            todos.Add("Persona " + i);
        }

        // ---------------------------------------------------------------
        // 2. Vacunados con Pfizer (conjunto A)
        // ---------------------------------------------------------------
        HashSet<string> pfizer = GenerarVacunados();

        // ---------------------------------------------------------------
        // 3. Vacunados con AstraZeneca (conjunto B)
        // ---------------------------------------------------------------
        HashSet<string> astra = GenerarVacunados();

        // ---------------------------------------------------------------
        // 4. Operaciones de conjuntos
        // ---------------------------------------------------------------

        // Vacunados = A ∪ B
        HashSet<string> vacunados = new HashSet<string>(pfizer);
        vacunados.UnionWith(astra);

        // No vacunados = U - vacunados
        HashSet<string> noVacunados = new HashSet<string>(todos);
        noVacunados.ExceptWith(vacunados);

        // Ambas dosis = A ∩ B
        HashSet<string> ambas = new HashSet<string>(pfizer);
        ambas.IntersectWith(astra);

        // Solo Pfizer = A - B
        HashSet<string> soloPfizer = new HashSet<string>(pfizer);
        soloPfizer.ExceptWith(astra);

        // Solo AstraZeneca = B - A
        HashSet<string> soloAstra = new HashSet<string>(astra);
        soloAstra.ExceptWith(pfizer);

        // ---------------------------------------------------------------
        // 5. Mostrar resultados
        // ---------------------------------------------------------------
        Console.WriteLine("Total ciudadanos: " + todos.Count);
        Console.WriteLine("Vacunados Pfizer: " + pfizer.Count);
        Console.WriteLine("Vacunados AstraZeneca: " + astra.Count);
        Console.WriteLine("Vacunados (Pfizer o AstraZeneca): " + vacunados.Count);
        Console.WriteLine("No vacunados: " + noVacunados.Count);
        Console.WriteLine("Ambas dosis: " + ambas.Count);
        Console.WriteLine("Solo Pfizer: " + soloPfizer.Count);
        Console.WriteLine("Solo AstraZeneca: " + soloAstra.Count);
    }

    // Método auxiliar para generar vacunados de forma aleatoria
    static HashSet<string> GenerarVacunados()
    {
        HashSet<string> conjunto = new HashSet<string>();
        Random rand = new Random();
        while (conjunto.Count < 75)
        {
            int numero = rand.Next(1, 501); // 1 a 500 inclusive
            conjunto.Add("Persona " + numero);
        }
        return conjunto;
    }
}
