using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        //En este apartado se guardan las palabras enfocadas en World of Warcraft en inglés y su tradicción
        Dictionary<string, string> diccionario = new Dictionary<string, string>()
        {
            {"quest", "misión"},
            {"dungeon", "mazmorra"},
            {"raid", "banda"},
            {"character", "personaje"},
            {"guild", "hermandad"},
            {"spell", "hechizo"},
            {"warrior", "guerrero"},
            {"mage", "mago"},
            {"hunter", "cazador"},
            {"paladin", "paladín"},
            {"orc", "orco"},
            {"alliance", "alianza"},
            {"horde", "horda"},
        };

        while (true)
        //Este es el menú de cartel de opciones que el usuario podrá elegir :D
        {
            Console.WriteLine("\n====================MENÚ===================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.WriteLine("Seleecione una opción por favor");
            string opcion = Console.ReadLine();

            if (opcion == "0")
            {
                Console.WriteLine("Gracias por usar el traductor de WoW");
                break; //Aquí podremos salir del programa
            }

            if (opcion == "1")
            {
                Console.WriteLine("Escriba una frase para traducir:");
                string frase = Console.ReadLine().ToLower();

                //Aquí se puede separar la frase en las palabras
                string[] palabras = frase.Split(' ');

                //Aquí sale la lista para guardar la traducción
                List<string> traduccion = new List<string>();
                foreach (string palabra in palabras)
                {
                    if (diccionario.ContainsKey(palabra))
                    {
                        traduccion.Add(diccionario[palabra]);
                    }
                    else
                    {
                        //Aquí si no existe la palabra en el diccionario se la dejaría igual
                        traduccion.Add(palabra);
                    }
                }

                Console.WriteLine("Traducción: " + string.Join(" ", traduccion));
            }

            if (opcion == "2")
            {
                Console.WriteLine("Ingrese la palabra en inglés que desea traducir:");
                string palabraIngles = Console.ReadLine()?.ToLower() ?? "";

                Console.WriteLine("Ingrese la traducción en español:");
                string palabraEspanol = Console.ReadLine()?.ToLower() ?? "";

                diccionario[palabraIngles] = palabraEspanol;

                Console.WriteLine($"La palabra '{palabraIngles}' se agregó como '{palabraEspanol}' en el diccionario.");
                
            }

                    }
                    }
                }

