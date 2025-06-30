using System;
using System.Collections.Generic;

// Clase que representa un gestor de asignaturas
public class GestorAsignaturas
{
    // Lista de tuplas (nombre de asignatura, código)
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

    // Método para mostrar mensaje personalizado
    public void MostrarMensajeEstudio()
    {
        Console.WriteLine("📘 Mensajes de estudio:");
        foreach (var (nombre, _) in asignaturas) // ignoramos el código con "_"
        {
            Console.WriteLine($"Yo estudio {nombre}");
        }
    }
}

// Clase principal
class Program
{
    static void Main(string[] args)
    {
        // Creamos el gestor y mostramos los mensajes
        GestorAsignaturas gestor = new GestorAsignaturas();
        gestor.MostrarMensajeEstudio();
    }
}