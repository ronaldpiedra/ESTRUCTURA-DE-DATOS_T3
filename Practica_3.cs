using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PremiacionLoL
{
    enum Medalla { Oro = 1, Plata = 2, Bronce = 3 }

    struct Medallero
    {
        public int Oro;
        public int Plata;
        public int Bronce;

        public void Agregar(Medalla m)
        {
            switch (m)
            {
                case Medalla.Oro: Oro++; break;
                case Medalla.Plata: Plata++; break;
                case Medalla.Bronce: Bronce++; break;
            }
        }
    }

    class Equipo
    {
        public string Id { get; }
        public string Nombre { get; }
        public string Region { get; }

        public Equipo(string id, string nombre, string region)
        {
            Id = id.Trim();
            Nombre = nombre.Trim();
            Region = region.Trim();
        }

        public override string ToString() => $"{Nombre} ({Region})";
    }

    class Program
    {
        static readonly Dictionary<string, Equipo> equipos =
            new(StringComparer.OrdinalIgnoreCase);

        static readonly Dictionary<string, Dictionary<Medalla, HashSet<string>>> podiosPorTorneo =
            new(StringComparer.OrdinalIgnoreCase);

        static readonly Dictionary<string, Medallero> tablaMedallas =
            new(StringComparer.OrdinalIgnoreCase);

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            while (true)
            {
                Console.WriteLine("\n=== Premiación de Equipos - League of Legends ===");
                Console.WriteLine("1) Registrar equipo");
                Console.WriteLine("2) Registrar torneo");
                Console.WriteLine("3) Registrar premiación (torneo, medalla, equipo)");
                Console.WriteLine("4) Listar premiados por torneo");
                Console.WriteLine("5) Ver tabla general de medallas");
                Console.WriteLine("6) Ver ranking de equipos");
                Console.WriteLine("7) Buscar equipo");
                Console.WriteLine("8) Exportar CSV");
                Console.WriteLine("0) Salir");
                Console.Write("Opción: ");

                var op = Console.ReadLine()?.Trim();
                try
                {
                    switch (op)
                    {
                        case "1": RegistrarEquipo(); break;
                        case "2": RegistrarTorneo(); break;
                        case "3": RegistrarPremiacion(); break;
                        case "4": ListarPremiadosPorTorneo(); break;
                        case "5": VerTablaGeneral(); break;
                        case "6": VerRanking(); break;
                        case "7": BuscarEquipo(); break;
                        case "8": ExportarCsv(); break;
                        case "0": return;
                        default: Console.WriteLine("Opción no válida."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void RegistrarEquipo()
        {
            Console.Write("ID del equipo (ej: T1, R7, NRG): ");
            var id = LeerNoVacio();
            if (equipos.ContainsKey(id))
            {
                Console.WriteLine("Ya existe un equipo con ese ID.");
                return;
            }

            Console.Write("Nombre del equipo: ");
            var nombre = LeerNoVacio();

            Console.Write("Región (ej: LLA, LCS, LCK, LEC): ");
            var region = LeerNoVacio();

            var equipo = new Equipo(id, nombre, region);
            equipos[id] = equipo;
            AsegurarFilaMedallas(id);
            Console.WriteLine($"✔ Equipo registrado: {equipo.Id} → {equipo}");
        }

        static void RegistrarTorneo()
        {
            Console.Write("Nombre del torneo: ");
            var torneo = LeerNoVacio();
            if (!podiosPorTorneo.ContainsKey(torneo))
            {
                podiosPorTorneo[torneo] = new Dictionary<Medalla, HashSet<string>>
                {
                    { Medalla.Oro, new HashSet<string>(StringComparer.OrdinalIgnoreCase) },
                    { Medalla.Plata, new HashSet<string>(StringComparer.OrdinalIgnoreCase) },
                    { Medalla.Bronce, new HashSet<string>(StringComparer.OrdinalIgnoreCase) }
                };
                Console.WriteLine($"✔ Torneo registrado: {torneo}");
            }
            else
            {
                Console.WriteLine("Ese torneo ya estaba registrado.");
            }
        }

        static void RegistrarPremiacion()
        {
            Console.Write("Torneo: ");
            var torneo = LeerNoVacio();
            AsegurarTorneo(torneo);

            Console.Write("ID del equipo: ");
            var id = LeerNoVacio();
            AsegurarEquipo(id);

            if (podiosPorTorneo[torneo].Values.Any(set => set.Contains(id)))
            {
                Console.WriteLine("Ese equipo ya tiene una medalla en este torneo.");
                return;
            }

            Console.Write("Medalla (1=Oro, 2=Plata, 3=Bronce): ");
            var medalla = LeerEnumMedalla();

            var podio = podiosPorTorneo[torneo][medalla];
            if (!podio.Add(id))
            {
                Console.WriteLine("Ese equipo ya tenía esa medalla en este torneo.");
                return;
            }

            var fila = tablaMedallas[id];
            fila.Agregar(medalla);
            tablaMedallas[id] = fila;

            Console.WriteLine($"✔ Premiación registrada: {torneo} | {medalla} → {equipos[id]}");
        }

        static void ListarPremiadosPorTorneo()
        {
            Console.Write("Torneo a consultar: ");
            var torneo = LeerNoVacio();
            if (!podiosPorTorneo.TryGetValue(torneo, out var byMedal))
            {
                Console.WriteLine("Ese torneo no existe.");
                return;
            }

            Console.WriteLine($"\n--- Podio: {torneo} ---");
            foreach (var m in new[] { Medalla.Oro, Medalla.Plata, Medalla.Bronce })
            {
                var set = byMedal[m];
                if (set.Count == 0)
                {
                    Console.WriteLine($"{m}: (sin registros)");
                    continue;
                }

                var lista = set.Select(id => equipos.TryGetValue(id, out var eq) ? eq.ToString() : id)
                               .OrderBy(s => s, StringComparer.OrdinalIgnoreCase);
                Console.WriteLine($"{m}: {string.Join(", ", lista)}");
            }
        }

        static void VerTablaGeneral()
        {
            if (tablaMedallas.Count == 0)
            {
                Console.WriteLine("No hay medallas registradas.");
                return;
            }

            Console.WriteLine("\n--- Tabla general de medallas ---");
            foreach (var kv in tablaMedallas
                .OrderBy(k => equipos.TryGetValue(k.Key, out var eq) ? eq.Nombre : k.Key,
                         StringComparer.OrdinalIgnoreCase))
            {
                var eqStr = equipos.TryGetValue(kv.Key, out var eq) ? $"{eq.Id} - {eq}" : kv.Key;
                Console.WriteLine($"{eqStr}: Oro {kv.Value.Oro} | Plata {kv.Value.Plata} | Bronce {kv.Value.Bronce}");
            }
        }

        static void VerRanking()
        {
            if (tablaMedallas.Count == 0)
            {
                Console.WriteLine("No hay datos para ranking.");
                return;
            }

            var ranking = tablaMedallas
                .OrderByDescending(kv => kv.Value.Oro)
                .ThenByDescending(kv => kv.Value.Plata)
                .ThenByDescending(kv => kv.Value.Bronce)
                .ThenBy(kv => equipos.TryGetValue(kv.Key, out var eq) ? eq.Nombre : kv.Key,
                        StringComparer.OrdinalIgnoreCase)
                .ToList();

            Console.WriteLine("\n--- Ranking de equipos ---");
            int pos = 1;
            foreach (var kv in ranking)
            {
                var eqStr = equipos.TryGetValue(kv.Key, out var eq) ? $"{eq.Id} - {eq}" : kv.Key;
                Console.WriteLine($"{pos,2}. {eqStr} | Oro {kv.Value.Oro} | Plata {kv.Value.Plata} | Bronce {kv.Value.Bronce}");
                pos++;
            }
        }

        static void BuscarEquipo()
        {
            Console.Write("ID del equipo a buscar: ");
            var id = LeerNoVacio();
            if (!equipos.TryGetValue(id, out var eq))
            {
                Console.WriteLine("Ese equipo no existe.");
                return;
            }

            var medallas = tablaMedallas.TryGetValue(id, out var fila) ? fila : default;
            Console.WriteLine($"\nEquipo: {eq.Id} - {eq}");
            Console.WriteLine($"Medallas acumuladas → Oro {medallas.Oro} | Plata {medallas.Plata} | Bronce {medallas.Bronce}");

            Console.WriteLine("Presencias en podios:");
            foreach (var (torneo, byMedal) in podiosPorTorneo.OrderBy(k => k.Key, StringComparer.OrdinalIgnoreCase))
            {
                var slots = new List<string>();
                foreach (var m in new[] { Medalla.Oro, Medalla.Plata, Medalla.Bronce })
                {
                    if (byMedal[m].Contains(id)) slots.Add(m.ToString());
                }
                if (slots.Count > 0)
                {
                    Console.WriteLine($"- {torneo}: {string.Join(", ", slots)}");
                }
            }
        }

        static void ExportarCsv()
        {
            var dir = Directory.GetCurrentDirectory();
            var podiosPath = Path.Combine(dir, "podios.csv");
            var rankingPath = Path.Combine(dir, "ranking.csv");

            using (var sw = new StreamWriter(podiosPath, false))
            {
                sw.WriteLine("Torneo;Medalla;EquipoId;Nombre;Region");
                foreach (var (torneo, byMedal) in podiosPorTorneo.OrderBy(k => k.Key, StringComparer.OrdinalIgnoreCase))
                {
                    foreach (var m in new[] { Medalla.Oro, Medalla.Plata, Medalla.Bronce })
                    {
                        foreach (var id in byMedal[m].OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
                        {
                            equipos.TryGetValue(id, out var eq);
                            sw.WriteLine($"{torneo};{m};{id};{eq?.Nombre ?? ""};{eq?.Region ?? ""}");
                        }
                    }
                }
            }

            var ranking = tablaMedallas
                .OrderByDescending(kv => kv.Value.Oro)
                .ThenByDescending(kv => kv.Value.Plata)
                .ThenByDescending(kv => kv.Value.Bronce)
                .ThenBy(kv => equipos.TryGetValue(kv.Key, out var eq) ? eq.Nombre : kv.Key,
                        StringComparer.OrdinalIgnoreCase)
                .ToList();

            using (var sw = new StreamWriter(rankingPath, false))
            {
                sw.WriteLine("Pos;EquipoId;Nombre;Region;Oro;Plata;Bronce");
                int pos = 1;
                foreach (var kv in ranking)
                {
                    equipos.TryGetValue(kv.Key, out var eq);
                    sw.WriteLine($"{pos};{kv.Key};{eq?.Nombre ?? ""};{eq?.Region ?? ""};{kv.Value.Oro};{kv.Value.Plata};{kv.Value.Bronce}");
                    pos++;
                }
            }

            Console.WriteLine($"✔ Archivos exportados:\n- {podiosPath}\n- {rankingPath}");
        }

        // Utilidades
        static string LeerNoVacio()
        {
            while (true)
            {
                var s = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(s)) return s;
                Console.Write("Entrada vacía, intenta de nuevo: ");
            }
        }

        static Medalla LeerEnumMedalla()
        {
            while (true)
            {
                var s = Console.ReadLine()?.Trim();
                if (int.TryParse(s, out var n) && Enum.IsDefined(typeof(Medalla), n))
                    return (Medalla)n;

                if (Enum.TryParse<Medalla>(s, true, out var m))
                    return m;

                Console.Write("Valor inválido. Usa 1=Oro, 2=Plata, 3=Bronce: ");
            }
        }

        static void AsegurarTorneo(string torneo)
        {
            if (!podiosPorTorneo.ContainsKey(torneo))
            {
                podiosPorTorneo[torneo] = new Dictionary<Medalla, HashSet<string>>
                {
                    { Medalla.Oro, new HashSet<string>(StringComparer.OrdinalIgnoreCase) },
                    { Medalla.Plata, new HashSet<string>(StringComparer.OrdinalIgnoreCase) },
                    { Medalla.Bronce, new HashSet<string>(StringComparer.OrdinalIgnoreCase) }
                };
            }
        }

        static void AsegurarEquipo(string id)
        {
            if (!equipos.ContainsKey(id))
            {
                Console.WriteLine("Ese equipo no existe, regístralo primero.");
                throw new InvalidOperationException("Equipo inexistente.");
            }
            AsegurarFilaMedallas(id);
        }

        static void AsegurarFilaMedallas(string id)
        {
            if (!tablaMedallas.ContainsKey(id))
                tablaMedallas[id] = new Medallero { Oro = 0, Plata = 0, Bronce = 0 };
        }
    }
}
