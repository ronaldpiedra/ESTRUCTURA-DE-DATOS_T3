using System;
using System.Collections.Generic;
using System.Diagnostics;

//
// ==============================
//   GRAFO: LISTA DE ADYACENCIA
// ==============================
//
public class Graph
{
    private readonly int V;               // número de vértices
    private readonly List<int>[] adj;     // arreglo de listas (adyacencias)

    public Graph(int vertices)
    {
        V = vertices;
        adj = new List<int>[V];
        for (int i = 0; i < V; i++)
            adj[i] = new List<int>();
    }

    // Agrega arista v -> w. Si undirected==true, también w -> v.
    public void AddEdge(int v, int w, bool undirected = false)
    {
        adj[v].Add(w);
        if (undirected) adj[w].Add(v);
    }

    // Reportería: imprimir lista de adyacencia
    public void PrintAdjacencyList()
    {
        Console.WriteLine("=== Lista de Adyacencia ===");
        for (int v = 0; v < V; v++)
            Console.WriteLine($"{v}: {string.Join(" -> ", adj[v])}");
        Console.WriteLine();
    }

    // BFS: recorrido por anchura
    public List<int> BFS(int start)
    {
        var sw = Stopwatch.StartNew();

        bool[] visited = new bool[V];
        List<int> order = new List<int>();
        Queue<int> q = new Queue<int>();

        visited[start] = true;
        q.Enqueue(start);

        while (q.Count > 0)
        {
            int u = q.Dequeue();
            order.Add(u);

            foreach (int w in adj[u])
            {
                if (!visited[w])
                {
                    visited[w] = true;
                    q.Enqueue(w);
                }
            }
        }

        sw.Stop();
        Console.WriteLine($"[Tiempo BFS] {sw.ElapsedTicks} ticks ({sw.ElapsedMilliseconds} ms)");
        return order;
    }

    // DFS: recorrido en profundidad (iterativo con pila)
    public List<int> DFS(int start)
    {
        var sw = Stopwatch.StartNew();

        bool[] visited = new bool[V];
        List<int> order = new List<int>();
        Stack<int> st = new Stack<int>();

        st.Push(start);
        while (st.Count > 0)
        {
            int u = st.Pop();
            if (!visited[u])
            {
                visited[u] = true;
                order.Add(u);

                // Para un orden determinista, apilar vecinos en orden inverso
                var neighbors = adj[u];
                for (int i = neighbors.Count - 1; i >= 0; i--)
                {
                    int w = neighbors[i];
                    if (!visited[w]) st.Push(w);
                }
            }
        }

        sw.Stop();
        Console.WriteLine($"[Tiempo DFS] {sw.ElapsedTicks} ticks ({sw.ElapsedMilliseconds} ms)");
        return order;
    }

    // Camino más corto (no ponderado) con BFS
    public List<int> ShortestPath(int start, int goal)
    {
        var sw = Stopwatch.StartNew();

        bool[] visited = new bool[V];
        int[] prev = new int[V];
        for (int i = 0; i < V; i++) prev[i] = -1;

        Queue<int> q = new Queue<int>();
        visited[start] = true;
        q.Enqueue(start);

        bool found = false;
        while (q.Count > 0 && !found)
        {
            int u = q.Dequeue();
            foreach (int w in adj[u])
            {
                if (!visited[w])
                {
                    visited[w] = true;
                    prev[w] = u;
                    if (w == goal)
                    {
                        found = true;
                        break;
                    }
                    q.Enqueue(w);
                }
            }
        }

        List<int> path = new List<int>();
        if (!found)
        {
            sw.Stop();
            Console.WriteLine($"[Tiempo Camino más corto] {sw.ElapsedTicks} ticks ({sw.ElapsedMilliseconds} ms)");
            return path; // vacío: no hay camino
        }

        // reconstrucción start ... goal
        for (int cur = goal; cur != -1; cur = prev[cur])
            path.Add(cur);
        path.Reverse();

        sw.Stop();
        Console.WriteLine($"[Tiempo Camino más corto] {sw.ElapsedTicks} ticks ({sw.ElapsedMilliseconds} ms)");
        return path;
    }
}

//
// ==========================
//   GRAFO: MATRIZ (OPCIONAL)
// ==========================
//
public class GraphMatrix
{
    private readonly int V;
    private readonly int[,] adj;

    public GraphMatrix(int vertices)
    {
        V = vertices;
        adj = new int[V, V];
    }

    public void AddEdge(int v, int w, bool undirected = false)
    {
        adj[v, w] = 1;
        if (undirected) adj[w, v] = 1;
    }

    public void PrintAdjacencyMatrix()
    {
        Console.WriteLine("=== Matriz de Adyacencia ===");
        Console.Write("    ");
        for (int j = 0; j < V; j++) Console.Write($"{j,2} ");
        Console.WriteLine();
        for (int i = 0; i < V; i++)
        {
            Console.Write($"{i,2}: ");
            for (int j = 0; j < V; j++) Console.Write($"{adj[i, j],2} ");
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

//
// ==========================
//           MAIN
// ==========================
//
public class Program
{
    public static void Main()
    {
        Console.WriteLine("Estructura de Datos - Grafos (C#)");
        Console.WriteLine("=================================");
        Console.WriteLine();

        // 1) Grafo ejemplo (como en las diapositivas): 4 nodos, dirigido
        var g = new Graph(4);
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 2);
        g.AddEdge(2, 0);
        g.AddEdge(2, 3);
        g.AddEdge(3, 3); // bucle

        // Reportería
        g.PrintAdjacencyList();

        // 2) Recorridos
        var bfsOrder = g.BFS(2);
        Console.WriteLine("BFS desde 2: " + string.Join(" -> ", bfsOrder));

        var dfsOrder = g.DFS(2);
        Console.WriteLine("DFS desde 2: " + string.Join(" -> ", dfsOrder));
        Console.WriteLine();

        // 3) Camino más corto
        int startNode = 2, goalNode = 1;
        var path = g.ShortestPath(startNode, goalNode);
        if (path.Count == 0)
            Console.WriteLine($"No existe camino de {startNode} a {goalNode}.");
        else
            Console.WriteLine($"Camino más corto de {startNode} a {goalNode}: {string.Join(" -> ", path)}");
        Console.WriteLine();

        // 4) (Opcional) Matriz de adyacencia para comparar
        var gm = new GraphMatrix(4);
        gm.AddEdge(0, 1);
        gm.AddEdge(0, 2);
        gm.AddEdge(1, 2);
        gm.AddEdge(2, 0);
        gm.AddEdge(2, 3);
        gm.AddEdge(3, 3);
        gm.PrintAdjacencyMatrix();

        Console.WriteLine("Presiona ENTER para salir...");
        Console.ReadLine();
    }
}
