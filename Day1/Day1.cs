namespace Advent_of_Code_2024.Day1
{
    public static class Day1
    {
        private static int[] List1 = [];

        private static int[] List2 = [];

        public static int Part1() {
            Init();

            var result = 0;
            for (int i = 0; i < List1.Length; i++)
            {
                result += Math.Abs(List1[i] - List2[i]);
            }

            return result;
        }

        public static int Part2() {
            Init();

            var result = 0;
            for (int i = 0; i < List1.Length; i++)
            {
                result += (List1[i] * List2.Where(x => x == List1[i]).Count());
            }

            return result;
        }

        private static void Init() {
            var input = Utilities.GetInputData(typeof(Day1).Name);
            var tempList1 = Enumerable.Empty<int>();
            var tempList2 = Enumerable.Empty<int>();
            foreach (var line in input)
            {
                var items = line.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
                tempList1 = tempList1.Append(int.Parse(items[0]));
                tempList2 = tempList2.Append(int.Parse(items[1]));
            }
            List1 = tempList1.SortArray().ToArray();
            List2 = tempList2.SortArray().ToArray();
        }


        private static IEnumerable<int> SortArray(this IEnumerable<int> list) {
            return list.OrderBy(x => x).ToArray();
        }
    }
}
