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

        public static void CreateAdjacencyMatrix(int vertexCount, int edgeCount)
        {
            vertexCount = Convert.ToInt32(Console.ReadLine());
            edgeCount = Convert.ToInt32(Console.ReadLine());
            // Создаем двумерный массив для матрицы смежности
            int[,] graph = new int[vertexCount, vertexCount];
            // Добавляем вершины в матрицу смежности
            for (int i = 0; i < edgeCount; i++)
            {
                int a = Convert.ToInt32(Console.ReadLine());
                int b = Convert.ToInt32(Console.ReadLine());
                a--;
                b--;
                graph[a, b] = 1;
                graph[b, a] = 1;
            }
            // Выводим матрицу смежности
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    Console.Write(" {0}", graph[i, j]);
                }
                Console.WriteLine();
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
