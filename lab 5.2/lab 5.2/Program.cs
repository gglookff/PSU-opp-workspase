using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Пример использования: умножение двух матриц
        int[,] matrixA = {
            {1, 2, 3},
            {4, 5, 6}
        };

        int[,] matrixB = {
            {7, 8},
            {9, 10},
            {11, 12}
        };

        Console.WriteLine("Матрица A:");
        PrintMatrix(matrixA);

        Console.WriteLine("\nМатрица B:");
        PrintMatrix(matrixB);

        try
        {
            int[,] result = MultiplyMatrices(matrixA, matrixB);
            Console.WriteLine("\nРезультат умножения A * B:");
            PrintMatrix(result);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("\nОшибка: " + ex.Message);
        }
    }

    // Метод печати матрицы
    public static void PrintMatrix(int[,] matrix)
    {
        if (matrix == null)
        {
            Console.WriteLine("Матрица не существует");
            return;
        }

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    // Метод умножения матриц
    public static int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
    {
        if (matrixA == null || matrixB == null)
        {
            throw new ArgumentException("Матрицы не могут быть null");
        }

        int rowsA = matrixA.GetLength(0);
        int colsA = matrixA.GetLength(1);
        int rowsB = matrixB.GetLength(0);
        int colsB = matrixB.GetLength(1);

        // Проверка возможности умножения
        if (colsA != rowsB)
        {
            throw new ArgumentException(
                $"Невозможно умножить матрицы: количество столбцов первой матрицы ({colsA}) " +
                $"не равно количеству строк второй матрицы ({rowsB})");
        }

        // Создаем результирующую матрицу
        int[,] result = new int[rowsA, colsB];

        // Умножение матриц
        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                int sum = 0;
                for (int k = 0; k < colsA; k++)
                {
                    sum += matrixA[i, k] * matrixB[k, j];
                }
                result[i, j] = sum;
            }
        }

        return result;
    }
}