using Graphs;

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
                            string path = @"D:\Phenomenals\University\Построение и анализ алгоритмов\forExerciseThreeIn15LAB.txt";
                            if (File.Exists(path))
                            {
                                string[] edges = File.ReadAllLines(path);
                                var graph = Graph.CreateAdjacencyList(edges);
                                Console.WriteLine("Список смежности:");
                                Graph.PrintAdjacencyList(graph);
                                HashSet<int> visited = new HashSet<int>();
                                Console.Write("Обход в глубину: ");
                                foreach(var vertex in graph.Keys)
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
                            string path = @"D:\Phenomenals\University\Построение и анализ алгоритмов\forExerciseThreeIn15LAB.txt";
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
                                List<int> pathFromAToB = Graph.GetPath(graph, A, B);
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