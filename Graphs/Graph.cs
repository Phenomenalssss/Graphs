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

            for (int i = 0; i < edgeCount; i++)
            {
                string[] numbers = Console.ReadLine().Split();
                edges.Add(Tuple.Create(Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1])));
            }

            return edges;
        }

        public static bool IsComplete(int vertexCount, List<Tuple<int, int>> edges)
        {
            HashSet<string> setOfEdges = new HashSet<string>();
            for (int i = 0; i < edges.Count; i++)
            {
                setOfEdges.Add($"{edges[i].Item1}-{edges[i].Item2}");
            }
            if (setOfEdges.Count == vertexCount * (vertexCount - 1) / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void CheckingForDisorientation(int[,] matrix)
        {
            bool flag = true;
            for (int i = 0; i < matrix.GetLength(0) && flag; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) && flag; j++)
                {
                    if (matrix[i, j] != matrix[j, i])
                    {
                        flag = false;
                    }
                    if (matrix[i, i] != 0)
                    {
                        flag = false;
                    }
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
    }
}
