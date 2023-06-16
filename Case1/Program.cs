using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static readonly string[] operators = { "+", "-", "*", "/" };
    private static readonly int[] numbers = { 1, 7, 17, 24, 41 };
    private static readonly int expectedResult = 208;

    public static void Main()
    {
        var permutations = GetPermutations(numbers, numbers.Length);
        bool solutionFound = false;

        foreach (var perm in permutations)
        {
            if (TestPermutation(perm.ToList()))
            {
                solutionFound = true;
                break;
            }
        }

        if (!solutionFound)
        {
            Console.WriteLine("No solution found.");
        }
    }


    private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private static bool TestPermutation(List<int> perm)
    {
        for (int i = 0; i < Math.Pow(4, numbers.Length - 1); i++)
        {
            var ops = GetOperatorsForIndex(i);

            double result = perm[0];

            for (int j = 0; j < ops.Length; j++)
            {
                switch (ops[j])
                {
                    case "+":
                        result += perm[j + 1];
                        break;
                    case "-":
                        result -= perm[j + 1];
                        break;
                    case "*":
                        result *= perm[j + 1];
                        break;
                    case "/":
                        result /= perm[j + 1];
                        break;
                }
            }

            if (Math.Abs(result - expectedResult) < 0.000001)
            {
                Console.WriteLine($"Found solution: {string.Join(" ", perm.Zip(ops, (n, op) => n + " " + op).Concat(new[] { perm.Last().ToString() }))} = {expectedResult}");
                return true;
            }
        }

        return false;
    }

    private static string[] GetOperatorsForIndex(int index)
    {
        string[] result = new string[numbers.Length - 1];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = operators[index % 4];
            index /= 4;
        }

        return result;
    }
}
