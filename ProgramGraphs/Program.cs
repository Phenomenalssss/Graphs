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
                    "4. Проверить существование пути от A в B\n" +
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
                                Console.WriteLine("Данные из файла:");
                                foreach(var edge in edges)
                                {
                                    ColorPrint($"{edge}\n", ConsoleColor.Yellow);
                                }
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
                                var graph = Graph.CreateAdjacencyList(edges);
                                Console.WriteLine("Список смежности:");
                                Graph.PrintAdjacencyList(graph);
                                bool[] visited = new bool[maxVertex];
                                Console.Write("Обход в глубину: ");
                                Graph.DFS(graph, Convert.ToInt32(edges[0].Split("-")[0]), visited);
                            }
                            break;
                        }
                    case 4:
                        {
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