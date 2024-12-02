namespace Advent_of_Code_2024.Day1
{
    public static class Day2
    {
        private static string[] _input;

        private const int _min = 1;
        private const int _max = 3;

        public static int Part1() {
            Init();

            var result = 0;
            foreach (var line in _input)
            {
                var items = line.Split(' ').Select(x => int.Parse(x)).ToList();
                var isSafe = items.IsSafe();
                result = isSafe ? ++result : result;
            }

            return result;
        }

        public static int Part2() {
            Init();

            var result = 0;
            foreach (var line in _input)
            {
                var items = line.Split(' ').Select(x => int.Parse(x)).ToList();
                var isSafe = items.IsSafe();
                if (!isSafe)
                {
                    isSafe = IsSafeUnstrict(items);
                }
                result = isSafe ? ++result : result;
            }

            return result;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day2).Name);
        }

        private static bool IsSafe(this List<int> input) {
            var sign = Math.Sign(input[1] - input[0]);
            return !input.Skip(1).Zip(input, (curr, prev) => Math.Abs(curr - prev) < _min || Math.Abs(curr - prev) > _max || Math.Sign(curr - prev) != sign).Any(x => x);
        }

        private static bool IsSafeUnstrict(this List<int> input) {
            for (int i = 1; i <= input.Count; i++)
            {
                var sequence = input.Take(i - 1).Concat(input.Skip(i).Take(input.Count - i)).ToList();
                if (sequence.IsSafe())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
