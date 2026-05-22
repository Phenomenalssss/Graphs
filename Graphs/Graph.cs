namespace Graphs
{
    public static class Graph
    {
        private static void ColorPrint(object text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text.ToString());
            Console.ResetColor();
        }

        public static void PrintListOfEdges(List<Tuple<int, int>> edges)
        {
            foreach (var pair in edges)
            {
                ColorPrint($"{pair.Item1} - {pair.Item2}\n", ConsoleColor.Yellow);
            }
        }

        public static List<Tuple<int, int>> CreateListOfEdges(int vertexCount, int edgeCount)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

            HashSet<(int, int)> seenEdges = new HashSet<(int, int)>();

            for (int i = 0; i < edgeCount; i++)
            {
                string[] numbers = Console.ReadLine().Split();
                int minEdge = int.Min(Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1]));
                int maxEdge = int.Max(Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1]));
                if (numbers[0] != numbers[1] && !seenEdges.Contains((minEdge, maxEdge)))
                {
                    seenEdges.Add((minEdge, maxEdge));
                    edges.Add(Tuple.Create(minEdge, maxEdge));
                }
            }

            return edges;
        }

        public static bool IsComplete(int vertexCount, List<Tuple<int, int>> edges)
        {
            HashSet<string> setOfEdges = new HashSet<string>();
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].Item1 != edges[i].Item2)
                {
                    int minEdge = int.Min(edges[i].Item1, edges[i].Item2);
                    int maxEdge = int.Max(edges[i].Item1, edges[i].Item2);
                    setOfEdges.Add($"{minEdge}-{maxEdge}");
                }
            }
            return setOfEdges.Count == vertexCount * (vertexCount - 1) / 2;
        }

        public static void CheckForDisorientation(int[,] matrix)
        {
            bool flag = true;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, i] != 0)
                {
                    flag = false;
                    break;
                }
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != matrix[j, i])
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag)
                {
                    break;
                }
            }
            if (flag)
            {
                ColorPrint("YES\n", ConsoleColor.Green);
            }
            else
            {
                ColorPrint("NO\n", ConsoleColor.Red);
            }
        }

        public static List<List<int>> CreateAdjacencyList(string[] edges)
        {
            int maxVertex = 0;
            for (int i = 0; i < edges.Length; i++)
            {
                string[] numbers = edges[i].Split("-");
                int a = Convert.ToInt32(numbers[0]);
                int b = Convert.ToInt32(numbers[1]);
                a--;
                b--;
                if (a > maxVertex)
                {
                    maxVertex = a;
                }
                if (b > maxVertex)
                {
                    maxVertex = b;
                }
            }
            List<List<int>> graph = new List<List<int>>();
            for (int i = 0; i <= maxVertex; i++)
            {
                graph.Add(new List<int>());
            }
            foreach (var edge in edges)
            {
                string[] numbers = edge.Split("-");
                int a = Convert.ToInt32(numbers[0]) - 1;
                int b = Convert.ToInt32(numbers[1]) - 1;

                if (!graph[a].Contains(b))
                {
                    graph[a].Add(b);
                }
                if (!graph[b].Contains(a))
                {
                    graph[b].Add(a);
                }
            }
            return graph;
        }

        public static void PrintAdjacencyList(List<List<int>> graph)
        {
            int vertexCount = graph.Count;
            for (int i = 0; i < vertexCount; i++)
            {
                ColorPrint($"Вершина {i + 1}:", ConsoleColor.Yellow);
                foreach (var vertex in graph[i])
                {
                    ColorPrint($" {vertex + 1}", ConsoleColor.Yellow);
                }
                Console.WriteLine();
            }
        }

        public static void DFS(List<List<int>> graph, int v, bool[] visited)
        {
            visited[v] = true;
            ColorPrint($"{v} ", ConsoleColor.Yellow);
            foreach (int to in graph[v])
            {
                if (!visited[to])
                {
                    DFS(graph, to, visited);
                }
            }
        }
    }
}
