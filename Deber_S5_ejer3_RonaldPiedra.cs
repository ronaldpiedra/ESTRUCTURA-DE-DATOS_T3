using System;
using System.Collections.Generic;

// Clase que gestiona asignaturas y notas
public class GestorAsignaturas
{
    private List<(string nombre, string codigo)> asignaturas;
    private Dictionary<string, string> notas;

    // Constructor
    public GestorAsignaturas()
    {
        asignaturas = new List<(string, string)>()
        {
            ("Matemáticas", "MAT101"),
            ("Física", "FIS102"),
            ("Química", "QUI103"),
            ("Historia", "HIS104"),
            ("Lengua", "LEN105")
        };

        notas = new Dictionary<string, string>();
    }

    // Método para pedir notas al usuario
    public void PedirNotas()
    {
        Console.WriteLine("✏️ Ingrese su nota por cada asignatura:");

        foreach (var (nombre, _) in asignaturas)
        {
            Console.Write($"¿Qué nota sacaste en {nombre}? ");
            string nota = Console.ReadLine();
            notas[nombre] = nota;
        }
    }

    // Método para mostrar resultados
    public void MostrarResultados()
    {
        Console.WriteLine("\n📄 Resultados:");
        foreach (var asignatura in asignaturas)
        {
            string nombre = asignatura.nombre;
            Console.WriteLine($"En {nombre} has sacado {notas[nombre]}");
        }
    }
}

// Clase principal
class Program
{
    static void Main(string[] args)
    {
        GestorAsignaturas gestor = new GestorAsignaturas();
        gestor.PedirNotas();
        gestor.MostrarResultados();
    }
}