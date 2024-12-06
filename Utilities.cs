using System.Reflection;
using System.Text;

namespace Advent_of_Code_2024
{
    internal static class Utilities
    {
        public static string[] GetInputData(string day) {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, @$"{day}\input.txt");
            return File.ReadAllLines(path);
        }

        public static void PrintArray(List<string> input) {
            for (int i = 0; i < input.Count(); i++)
            {
                Console.WriteLine(input[i]);
            }

            Console.WriteLine();
        }

        public static void PrintArray(int[] input) {
            for (int i = 0; i < input.Count(); i++)
            {
                Console.WriteLine(input[i]);
            }

            Console.WriteLine();
        }

        public static List<string> Transpose(List<string> matrix) {
            return matrix
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => new string(g.ToArray()))
                .ToList();
        }

        public static string SubstituteChar(this string input, int index, char c) {
            var sb = new StringBuilder(input);
            sb[index] = c;
            return sb.ToString();
        }
    }
}
