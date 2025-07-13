using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("📖 Verificador de signos balanceados en textos literarios");
        Console.Write("Escriba un fragmento de texto (con paréntesis, corchetes, comillas...): ");
        string texto = Console.ReadLine();

        if (EsTextoBalanceado(texto))
        {
            Console.WriteLine("\n✅ El fragmento está correctamente puntuado y estructurado.");
        }
        else
        {
            Console.WriteLine("\n❌ El fragmento tiene signos de puntuación mal balanceados.");
        }
    }

    /// <summary>
    /// Verifica si los signos de apertura y cierre están balanceados
    /// </summary>
    static bool EsTextoBalanceado(string texto)
    {
        Stack<char> pila = new Stack<char>();

        foreach (char c in texto)
        {
            if (c == '(' || c == '[' || c == '{' || c == '“')
            {
                pila.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}' || c == '”')
            {
                if (pila.Count == 0) return false;

                char tope = pila.Pop();
                if (!SonPareja(tope, c)) return false;
            }
        }

        return pila.Count == 0;
    }

    /// <summary>
    /// Comprueba si los signos forman una pareja correcta
    /// </summary>
    static bool SonPareja(char apertura, char cierre)
    {
        return (apertura == '(' && cierre == ')') ||
               (apertura == '[' && cierre == ']') ||
               (apertura == '{' && cierre == '}') ||
               (apertura == '“' && cierre == '”');
    }
}