using System;
using System.Collections.Generic;

// Clase que representa una asignatura con nombre y código
public class GestorAsignaturas
{
    // Lista de tuplas (nombre, código)
    private List<(string nombre, string codigo)> asignaturas;

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
    }

    // Método para mostrar las asignaturas
    public void MostrarAsignaturas()
    {
        Console.WriteLine("📚 Asignaturas del curso:");
        foreach (var (nombre, codigo) in asignaturas)
        {
            Console.WriteLine($"- {nombre} (código: {codigo})");
        }
    }
}

// Clase principal
class Program
{
    static void Main(string[] args)
    {
        // Creamos el gestor y mostramos las asignaturas
        GestorAsignaturas gestor = new GestorAsignaturas();
        gestor.MostrarAsignaturas();
    }
}