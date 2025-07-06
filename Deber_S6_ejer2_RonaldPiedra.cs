using System;

public class Nodo
{
    public int dato;
    public Nodo siguiente;

    public Nodo(int valor)
    {
        dato = valor;
        siguiente = null;
    }
}

public class ListaEnlazada
{
    private Nodo cabeza;

    public ListaEnlazada()
    {
        cabeza = null;
    }

    public void Agregar(int valor)
    {
        Nodo nuevo = new Nodo(valor);

        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.siguiente != null)
            {
                actual = actual.siguiente;
            }
            actual.siguiente = nuevo;
        }
    }

    public int Buscar(int valorBuscado)
    {
        int contador = 0;
        Nodo actual = cabeza;

        while (actual != null)
        {
            if (actual.dato == valorBuscado)
            {
                contador++;
            }
            actual = actual.siguiente;
        }

        return contador;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ListaEnlazada miLista = new ListaEnlazada();

        // Agregar valores de prueba
        miLista.Agregar(5);
        miLista.Agregar(10);
        miLista.Agregar(20);
        miLista.Agregar(10);
        miLista.Agregar(30);

        Console.WriteLine("Ingrese un número a buscar en la lista:");
        int valor = int.Parse(Console.ReadLine());

        int repeticiones = miLista.Buscar(valor);

        if (repeticiones > 0)
        {
            Console.WriteLine($"El número {valor} aparece {repeticiones} vez/veces en la lista.");
        }
        else
        {
            Console.WriteLine($"El número {valor} no se encuentra en la lista.");
        }
    }
}