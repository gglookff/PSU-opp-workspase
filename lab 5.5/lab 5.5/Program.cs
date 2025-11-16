using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        // Создаем матрицы с помощью LinkedList
        var matrixA = new LinkedList<LinkedList<int>>();
        matrixA.AddLast(new LinkedList<int>(new List<int> { 1, 2, 3 }));
        matrixA.AddLast(new LinkedList<int>(new List<int> { 4, 5, 6 }));

        var matrixB = new LinkedList<LinkedList<int>>();
        matrixB.AddLast(new LinkedList<int>(new List<int> { 7, 8 }));
        matrixB.AddLast(new LinkedList<int>(new List<int> { 9, 10 }));
        matrixB.AddLast(new LinkedList<int>(new List<int> { 11, 12 }));

        Console.WriteLine("Матрица A:");
        PrintMatrixSimple(matrixA);

        Console.WriteLine("\nМатрица B:");
        PrintMatrixSimple(matrixB);

        try
        {
            var result = MultiplyMatricesSimple(matrixA, matrixB);
            Console.WriteLine("\nРезультат умножения A * B:");
            PrintMatrixSimple(result);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("\nОшибка: " + ex.Message);
        }
    }

    // Упрощенный метод печати матрицы
    public static void PrintMatrixSimple(LinkedList<LinkedList<int>> matrix)
    {
        foreach (var row in matrix)
        {
            foreach (var element in row)
            {
                Console.Write(element + "\t");
            }
            Console.WriteLine();
        }
    }

    // Упрощенный метод умножения матриц
    public static LinkedList<LinkedList<int>> MultiplyMatricesSimple(
        LinkedList<LinkedList<int>> matrixA,
        LinkedList<LinkedList<int>> matrixB)
    {
        if (matrixA == null || matrixB == null)
            throw new ArgumentException("Матрицы не могут быть null");

        // Преобразуем LinkedList в списки для более простого доступа по индексу
        var listA = ConvertTo2DList(matrixA);
        var listB = ConvertTo2DList(matrixB);

        int rowsA = listA.Count;
        int colsA = listA[0].Count;
        int rowsB = listB.Count;
        int colsB = listB[0].Count;

        if (colsA != rowsB)
            throw new ArgumentException("Несовместимые размеры матриц");

        var result = new LinkedList<LinkedList<int>>();

        for (int i = 0; i < rowsA; i++)
        {
            var newRow = new LinkedList<int>();
            for (int j = 0; j < colsB; j++)
            {
                int sum = 0;
                for (int k = 0; k < colsA; k++)
                {
                    sum += listA[i][k] * listB[k][j];
                }
                newRow.AddLast(sum);
            }
            result.AddLast(newRow);
        }

        return result;
    }

    // Метод для преобразования LinkedList<LinkedList<T>> в List<List<T>>
    private static List<List<int>> ConvertTo2DList(LinkedList<LinkedList<int>> matrix)
    {
        var result = new List<List<int>>();
        foreach (var row in matrix)
        {
            var newRow = new List<int>();
            foreach (var element in row)
            {
                newRow.Add(element);
            }
            result.Add(newRow);
        }
        return result;
    }
}