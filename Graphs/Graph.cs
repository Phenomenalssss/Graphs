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

        public static Dictionary<int, HashSet<int>> CreateAdjacencyList(string[] edges)
        {
            var graph = new Dictionary<int, HashSet<int>>();
            foreach (var edge in edges)
            {
                string[] numbers = edge.Split("-");
                int a = Convert.ToInt32(numbers[0]);
                int b = Convert.ToInt32(numbers[1]);

                if (!graph.ContainsKey(a))
                {
                    graph[a] = new HashSet<int>();
                }
                if (!graph.ContainsKey(b))
                {
                    graph[b] = new HashSet<int>();
                }
                graph[a].Add(b);
                graph[b].Add(a);
            }
            return graph;
        }

        public static void PrintAdjacencyList(Dictionary<int, HashSet<int>> graph)
        {
            foreach (var pair in graph)
            {
                int vertex = pair.Key;
                HashSet<int> neighbors = pair.Value;
                ColorPrint($"Вершина {vertex}:", ConsoleColor.Yellow);
                foreach (var neighbor in neighbors)
                {
                    ColorPrint($" {neighbor}", ConsoleColor.Yellow);
                }
                Console.WriteLine();
            }
        }

        public static void DFS(Dictionary<int, HashSet<int>> graph, int v, HashSet<int> visited)
        {
            visited.Add(v);
            ColorPrint($"{v} ", ConsoleColor.Yellow);
            foreach (int to in graph[v])
            {
                if (!visited.Contains(to))
                {
                    DFS(graph, to, visited);
                }
            }
        }

        private static void _DFS(Dictionary<int, HashSet<int>> graph, int v, HashSet<int> visited)
        {
            visited.Add(v);
            foreach (int to in graph[v])
            {
                if (!visited.Contains(to))
                {
                    _DFS(graph, to, visited);
                }
            }
        }

        public static List<int> GetPath(Dictionary<int, HashSet<int>> graph, int start, int target)
        {
            List<int> path = new List<int>();
            HashSet<int> check = new HashSet<int>();
            _DFS(graph, start, check);
            if (!check.Contains(target))
            {
                return path;
            }
            int current = start;
            path.Add(current);
            while(current != target)
            {
                foreach(int neighbor in graph[current])
                {
                    if (!path.Contains(neighbor))
                    {
                        HashSet<int> visited = new HashSet<int>(path);
                        _DFS(graph, neighbor, visited);
                        if (visited.Contains(target))
                        {
                            current = neighbor;
                            path.Add(current);
                            break;
                        }
                    }
                }
            }
            return path;
        }
    }
}
