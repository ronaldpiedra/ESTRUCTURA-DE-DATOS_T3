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

    public int ContarElementos()
    {
        int contador = 0;
        Nodo actual = cabeza;

        while (actual != null)
        {
            contador++;
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

        miLista.Agregar(10);
        miLista.Agregar(20);
        miLista.Agregar(30);
        miLista.Agregar(40);

        int total = miLista.ContarElementos();
        Console.WriteLine($"La lista tiene {total} elementos.");
    }
}
