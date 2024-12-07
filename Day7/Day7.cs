namespace Advent_of_Code_2024.Day7
{
    public static class Day7
    {
        private static string[] _input;
        private static Dictionary<long, List<int>> _rules = [];

        public static long Part1() {
            Init();
            long result = 0;
            foreach (var rule in _rules)
            {
                if (IsValid(rule.Key, rule.Value, 0, rule.Value[0]))
                    result += rule.Key;
            }
            return result;
        }

        public static int Part2() {
            //Init();
            var result = 0;
            return result;
        }

        private static bool IsValid(long target, List<int> sequence, int i, long result) {
            i++;
            if ((sequence.Count()) == i) return result == target;

            return IsValid(target, sequence, i, result + sequence[i]) || IsValid(target, sequence, i, result * sequence[i]);
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day7).Name);
            foreach (var line in _input)
            {
                var data = line.Split(": ");
                _rules.Add(long.Parse(data[0]), data[1].Split(' ').Select(x => int.Parse(x)).ToList());
            }
        }
    }
}
