using System;
using System.Collections.Generic;

namespace CatalogoRevistasGaming
{
    
    class Nodo
    {
        public string Titulo;
        public Nodo Izq;
        public Nodo Der;

        public Nodo(string titulo) => Titulo = titulo;
    }

    
    class ArbolBST
    {
        private Nodo _raiz;

        public bool Vacio => _raiz == null;

        
        public void Insertar(string titulo) => _raiz = InsertarRec(_raiz, titulo);

        private Nodo InsertarRec(Nodo actual, string titulo)
        {
            if (actual == null) return new Nodo(titulo);

            int comp = string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase);
            if (comp < 0)
                actual.Izq = InsertarRec(actual.Izq, titulo);
            else if (comp > 0)
                actual.Der = InsertarRec(actual.Der, titulo);
            
            return actual;
        }

    
        public bool Buscar(string titulo) => BuscarRec(_raiz, titulo);

        private bool BuscarRec(Nodo actual, string titulo)
        {
            if (actual == null) return false;
            int comp = string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase);
            if (comp == 0) return true;
            return comp < 0
                ? BuscarRec(actual.Izq, titulo)
                : BuscarRec(actual.Der, titulo);
        }

        
        public void Eliminar(string titulo) => _raiz = EliminarRec(_raiz, titulo);

        private Nodo EliminarRec(Nodo actual, string titulo)
        {
            if (actual == null) return null;

            int comp = string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase);
            if (comp < 0)
                actual.Izq = EliminarRec(actual.Izq, titulo);
            else if (comp > 0)
                actual.Der = EliminarRec(actual.Der, titulo);
            else
            {
                
                if (actual.Izq == null && actual.Der == null) return null;
                
                if (actual.Izq == null) return actual.Der;
                if (actual.Der == null) return actual.Izq;
                
                Nodo sucesor = Minimo(actual.Der);
                actual.Titulo = sucesor.Titulo;
                actual.Der = EliminarRec(actual.Der, sucesor.Titulo);
            }
            return actual;
        }

        private Nodo Minimo(Nodo actual)
        {
            while (actual.Izq != null) actual = actual.Izq;
            return actual;
        }

        
        public List<string> Inorden()
        {
            var r = new List<string>();
            InordenRec(_raiz, r);
            return r;
        }
        private void InordenRec(Nodo n, List<string> r)
        {
            if (n == null) return;
            InordenRec(n.Izq, r);
            r.Add(n.Titulo);
            InordenRec(n.Der, r);
        }

        public List<string> Preorden()
        {
            var r = new List<string>();
            PreordenRec(_raiz, r);
            return r;
        }
        private void PreordenRec(Nodo n, List<string> r)
        {
            if (n == null) return;
            r.Add(n.Titulo);
            PreordenRec(n.Izq, r);
            PreordenRec(n.Der, r);
        }

        public List<string> Postorden()
        {
            var r = new List<string>();
            PostordenRec(_raiz, r);
            return r;
        }
        private void PostordenRec(Nodo n, List<string> r)
        {
            if (n == null) return;
            PostordenRec(n.Izq, r);
            PostordenRec(n.Der, r);
            r.Add(n.Titulo);
        }

        
        public void Limpiar() => _raiz = null;

        public string Min()
        {
            if (_raiz == null) return null;
            return Minimo(_raiz).Titulo;
        }

        public string Max()
        {
            if (_raiz == null) return null;
            var n = _raiz;
            while (n.Der != null) n = n.Der;
            return n.Titulo;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var arbol = new ArbolBST();

            
            string[] revistasBase = {
                "Game Informer", "Edge", "Level Up", "Hobby Consolas",
                "Nintendo Power", "PC Gamer", "Retro Gamer",
                "PlayStation Magazine", "Xbox World", "IGN Weekly"
            };
            foreach (var r in revistasBase) arbol.Insertar(r);

            while (true)
            {
                Console.WriteLine("\n=== CATÁLOGO DE REVISTAS GAMING ===");
                Console.WriteLine("1) Insertar revista");
                Console.WriteLine("2) Eliminar revista");
                Console.WriteLine("3) Buscar revista");
                Console.WriteLine("4) Mostrar catálogo (orden alfabético)");
                Console.WriteLine("5) Mostrar Preorden");
                Console.WriteLine("6) Mostrar Postorden");
                Console.WriteLine("7) Mínimo / Máximo (alfabéticamente)");
                Console.WriteLine("8) Limpiar catálogo");
                Console.WriteLine("0) Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1": 
                        Console.Write("Ingrese el título de la revista: ");
                        string nueva = Console.ReadLine();
                        arbol.Insertar(nueva);
                        Console.WriteLine($"✔ Revista '{nueva}' insertada.");
                        break;

                    case "2": 
                        Console.Write("Ingrese el título de la revista a eliminar: ");
                        string elim = Console.ReadLine();
                        bool existia = arbol.Buscar(elim);
                        arbol.Eliminar(elim);
                        Console.WriteLine(existia
                            ? $"✔ Revista '{elim}' eliminada."
                            : "⚠ Esa revista no está en el catálogo.");
                        break;

                    case "3": 
                        Console.Write("Ingrese el título de la revista a buscar: ");
                        string buscar = Console.ReadLine();
                        Console.WriteLine(arbol.Buscar(buscar)
                            ? $"✔ '{buscar}' SÍ está en el catálogo."
                            : $"✖ '{buscar}' NO está en el catálogo.");
                        break;

                    case "4": 
                        ImprimirLista("Catálogo ordenado", arbol.Inorden());
                        break;

                    case "5": 
                        ImprimirLista("Preorden", arbol.Preorden());
                        break;

                    case "6": 
                        ImprimirLista("Postorden", arbol.Postorden());
                        break;

                    case "7": 
                        Console.WriteLine(arbol.Vacio
                            ? "El catálogo está vacío."
                            : $"Primera revista (A-Z): {arbol.Min()}\nÚltima revista (A-Z): {arbol.Max()}");
                        break;

                    case "8": 
                        arbol.Limpiar();
                        Console.WriteLine("✔ Catálogo limpiado.");
                        break;

                    case "0":
                        Console.WriteLine("¡Hasta luego gamer!");
                        return;

                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            }
        }

        static void ImprimirLista(string titulo, List<string> datos)
        {
            if (datos.Count == 0)
            {
                Console.WriteLine($"[{titulo}] Catálogo vacío.");
                return;
            }
            Console.WriteLine($"[{titulo}] {string.Join(" | ", datos)}");
        }
    }
}
