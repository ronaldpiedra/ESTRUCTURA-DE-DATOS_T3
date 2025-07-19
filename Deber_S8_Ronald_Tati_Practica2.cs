using System;
using System.Collections.Generic;
using System.Text;

namespace ThemePark
{
    public class Person
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime ArrivedAt { get; }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
            ArrivedAt = DateTime.Now;
        }

        public override string ToString() =>
            $"{Id:00} - {Name} (llegó a las {ArrivedAt:HH:mm:ss})";
    }

    public class RideQueue
    {
        private readonly Queue<Person> _queue = new();
        public int Capacity { get; } = 30;

        public bool EnqueuePerson(Person p)
        {
            if (IsFull) return false;
            _queue.Enqueue(p);
            return true;
        }

        public bool IsFull => _queue.Count >= Capacity;
        public int TicketsLeft => Capacity - _queue.Count;

        public string GenerateReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Boletos vendidos: {_queue.Count}/{Capacity}");
            sb.AppendLine(IsFull
                ? "La atracción está llena; no hay boletos disponibles."
                : $"Disponibles: {TicketsLeft} boleto(s)\n");
            int position = 1;
            foreach (var person in _queue)
                sb.AppendLine($"{position++,2}. {person}");
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            var ride = new RideQueue();
            int idCounter = 1;

            while (true)
            {
                Console.WriteLine("\n1) Vender boleto  2) Ver reporte  0) Salir");
                Console.Write("Elige opción: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (ride.IsFull)
                        {
                            Console.WriteLine("Lo siento, ya no quedan boletos.");
                            break;
                        }
                        Console.Write("Nombre del visitante: ");
                        var name = Console.ReadLine();
                        var ok = ride.EnqueuePerson(new Person(idCounter++, name ?? "SinNombre"));
                        Console.WriteLine(ok
                            ? "Boleto emitido correctamente."
                            : "Error al emitir el boleto.");
                        break;

                    case "2":
                        Console.WriteLine("\n=== REPORTE DE COLA ===");
                        Console.WriteLine(ride.GenerateReport());
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }
}
