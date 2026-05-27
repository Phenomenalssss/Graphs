using Graphs;
using System.Globalization;

namespace ProgramGraphs
{
    public class Program
    {
        public static void ColorPrint(object text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text.ToString());
            Console.ResetColor();
        }

        public static void RepeatMain()
        {
            Console.Write("Ещё раз? (2 - да, 1 - с очисткой консоли, 0 - нет)\n>> ");
            int repeat = Convert.ToInt32(Console.ReadLine());
            switch (repeat)
            {
                case 2:
                    {
                        Main();
                        break;
                    }
                case 1:
                    {
                        Console.Clear();
                        Main();
                        break;
                    }
                case 0:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public static void MatrixPrint(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ColorPrint($"{matrix[i, j]} ", ConsoleColor.Yellow);
                }
                Console.WriteLine();
            }
        }

        public static void Main()
        {
            try
            {
                Console.Write("Выберете задание, введя его номер:\n" +
                    "1. Проверка матрицы на неориентированность\n" +
                    "2. Полный граф\n" +
                    "3. Рекурсивный алгоритм обхода графа в глубину\n" +
                    "4. Проверить существование пути от A до B\n" +
                    "5. Минимальный путь между двумя вершинами в неориентированном графе\n" +
                    "6. Заправки\n" +
                    "7. Алгоритм Форда-Беллмана\n" +
                    "8. Алгоритм Флойда\n" +
                    ">> ");
                int exercise = Convert.ToInt32(Console.ReadLine());
                switch (exercise)
                {
                    case 1:
                        {
                            Console.Write("Введите N = ");
                            int N = Convert.ToInt32(Console.ReadLine());
                            int[,] matrix = new int[N, N];
                            for (int i = 0; i < N; i++)
                            {
                                Console.Write($"Введите {N} чисел через пробел {i + 1}-й строки матрицы: ");
                                string[] numbers = Console.ReadLine().Split();
                                for (int j = 0; j < numbers.Length; j++)
                                {
                                    matrix[i, j] = Convert.ToInt32(numbers[j]);
                                }
                            }
                            Console.WriteLine("Матрица:");
                            MatrixPrint(matrix);
                            Graph.CheckForDisorientation(matrix);
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите количество вершин = ");
                            int vertexCount = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Введите количество ребёр = ");
                            int edgeCount = Convert.ToInt32(Console.ReadLine());
                            var edges = Graph.CreateListOfEdges(vertexCount, edgeCount);
                            Console.WriteLine("Список ребёр:");
                            Graph.PrintListOfEdges(edges);
                            if (Graph.IsComplete(vertexCount, edges))
                            {
                                ColorPrint($"YES\n", ConsoleColor.Green);
                            }
                            else
                            {
                                ColorPrint($"NO\n", ConsoleColor.Red);
                            }
                            break;
                        }
                    case 3:
                        {
                            string path = @"D:\Phenomenals\University\Построение и анализ алгоритмов\forExerciseThreeAndFourIn15LAB.txt";
                            if (File.Exists(path))
                            {
                                string[] edges = File.ReadAllLines(path);
                                var graph = Graph.CreateAdjacencyList(edges);
                                Console.WriteLine("Список смежности:");
                                Graph.PrintAdjacencyList(graph);
                                HashSet<int> visited = new HashSet<int>();
                                Console.Write("Обход в глубину: ");
                                foreach (var vertex in graph.Keys)
                                {
                                    if (!visited.Contains(vertex))
                                    {
                                        Graph.DFS(graph, vertex, visited);
                                    }
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                    case 4:
                        {
                            string path = @"D:\Phenomenals\University\Построение и анализ алгоритмов\forExerciseThreeAndFourIn15LAB.txt";
                            if (File.Exists(path))
                            {
                                string[] edges = File.ReadAllLines(path);
                                var graph = Graph.CreateAdjacencyList(edges);
                                Console.WriteLine("Список смежности:");
                                Graph.PrintAdjacencyList(graph);
                                Console.Write("Введите вершину A = ");
                                int A = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Введите вершину B = ");
                                int B = Convert.ToInt32(Console.ReadLine());
                                List<int> pathFromAToB = Graph.GetPathDFS(graph, A, B);
                                if (pathFromAToB.Count > 0)
                                {
                                    ColorPrint($"Путь от {A} до {B} существует\n", ConsoleColor.Green);
                                    ColorPrint($"Путь: {string.Join(" -> ", pathFromAToB)}\n", ConsoleColor.Yellow);
                                }
                                else
                                {
                                    ColorPrint($"Путь от {A} до {B} не существует\n", ConsoleColor.Red);

                                }
                            }
                            break;
                        }
                    case 5:
                        {
                            string path = @"D:\Phenomenals\University\Построение и анализ алгоритмов\forExerciseFiveIn15LAB.txt";
                            if (File.Exists(path))
                            {
                                string[] tempEdges = File.ReadAllLines(path);
                                string[] pathFromTo = tempEdges.LastOrDefault().Split(" ");
                                int[,] matrix = new int[Convert.ToInt32(tempEdges.FirstOrDefault()), Convert.ToInt32(tempEdges.FirstOrDefault())];
                                for (int i = 1; i < tempEdges.Length - 1; i++)
                                {
                                    string[] numbers = tempEdges[i].Split();
                                    for (int j = 0; j < numbers.Length; j++)
                                    {
                                        matrix[i - 1, j] = Convert.ToInt32(numbers[j]);
                                    }
                                }
                                Console.WriteLine("Матрица:");
                                MatrixPrint(matrix);
                                var (dist, from) = Graph.BFS(matrix, Convert.ToInt32(pathFromTo[0]) - 1);
                                Console.Write("Обход в ширину: ");
                                ColorPrint($"{string.Join(" ", dist)}\n", ConsoleColor.Yellow);
                                var pathFromAToB = Graph.GetPathBFS(from, Convert.ToInt32(pathFromTo[0]) - 1, Convert.ToInt32(pathFromTo[1]) - 1);
                                if (pathFromAToB.Count > 0)
                                {
                                    ColorPrint($"Путь от {pathFromTo[0]} до {pathFromTo[1]} существует\n", ConsoleColor.Green);
                                    ColorPrint($"Минимальный путь от {pathFromTo[0]} до {pathFromTo[1]} = {pathFromAToB.Count} ({string.Join(" -> ", pathFromAToB.Select(x => x + 1))})\n", ConsoleColor.Yellow);
                                }
                                else
                                {
                                    ColorPrint($"Путь от {pathFromTo[0]} до {pathFromTo[1]} не существует\n", ConsoleColor.Red);
                                }
                            }
                            break;
                        }
                    case 6:
                        {
                            string path = @"D:\Phenomenals\University\Построение и анализ алгоритмов\forExerciseOneIn16LAB.txt";
                            if (File.Exists(path))
                            {
                                string[] file = File.ReadAllLines(path);
                                int countCity = Convert.ToInt32(file.FirstOrDefault());
                                int[] fuelCost = new int[countCity];
                                string[] numbers = file[1].Split(" ");
                                for (int i = 0; i < fuelCost.Length; i++)
                                {
                                    fuelCost[i] = Convert.ToInt32(numbers[i]);
                                }
                                List<int>[] graph = new List<int>[countCity];
                                for (int i = 0; i < graph.Length; i++)
                                {
                                    graph[i] = new List<int>();
                                }
                                int roads = Convert.ToInt32(file[2]);
                                for (int i = 0, k = 3; i < roads; i++)
                                {
                                    string digits = file[k];
                                    string[] row = digits.Split(" ");
                                    k++;

                                    int a = Convert.ToInt32(row[0]) - 1;
                                    int b = Convert.ToInt32(row[1]) - 1;

                                    graph[a].Add(b);
                                    graph[b].Add(a);
                                }
                                int[] dist = Graph.Dijkstra(graph, fuelCost);
                                Console.Write("Стоимость маршрутов до каждого города: ");
                                ColorPrint($"{string.Join(" ", dist)}\n", ConsoleColor.Yellow);
                                string resultText = "\n";
                                if (dist[countCity - 1] == int.MaxValue || dist[countCity - 1] < 0)
                                {
                                    ColorPrint("Добраться невозможно\n", ConsoleColor.Red);
                                    ColorPrint($"Суммарная стоимость маршрута = -1\n", ConsoleColor.Yellow);
                                    resultText += $"Добраться невозможно\nСуммарная стоимость маршрута = -1\n";
                                }
                                else
                                {
                                    ColorPrint("Добраться возможно\n", ConsoleColor.Green);
                                    ColorPrint($"Суммарная стоимость маршрута = {dist[countCity - 1]}\n", ConsoleColor.Yellow);
                                    resultText += $"Добраться возможно\nСуммарная стоимость маршрута = {dist[countCity - 1]}\n";
                                }
                                File.AppendAllText(path, resultText);
                            }
                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 8:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                RepeatMain();
            }
            catch (Exception e)
            {
                ColorPrint($"Ошибка: {e.Message}\n", ConsoleColor.Red);
                RepeatMain();
            }
        }
    }
}