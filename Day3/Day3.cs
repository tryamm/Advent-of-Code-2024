using System.Text.RegularExpressions;

namespace Advent_of_Code_2024.Day3
{
    public static class Day3
    {
        private static string[] _input;

        private static Regex _mulPattern = new Regex("mul\\(\\d+,\\d+\\)");
        private static Regex _dontPattern = new Regex("don't\\(\\)");
        private static Regex _doPattern = new Regex("do\\(\\)");

        public static int Part1() {
            Init();
            var result = 0;
            foreach (var input in _input)
            {
                var operations = _mulPattern.Matches(input);
                foreach (Match operation in operations)
                {
                    result += Multiply(operation.Value);
                }
            }

            return result;
        }

        public static int Part2() {
            Init();
            var result = 0;
            var isEnabled = true;
            foreach (var input in _input)
            {
                var operations = _mulPattern.Matches(input);
                var dos = _doPattern.Matches(input);
                var donts = _dontPattern.Matches(input);
                var data = operations.Concat(dos).Concat(donts).OrderBy(x => x.Index).Select(x => x.ToString()).ToArray();
                foreach (var d in data)
                {
                    if (_mulPattern.IsMatch(d) && isEnabled)
                    {
                        result += Multiply(d);
                    } else
                    if (_doPattern.IsMatch(d))
                    {
                        isEnabled = true;
                    } else
                    if (_dontPattern.IsMatch(d))
                    {
                        isEnabled = false;
                    }
                }
            }

            return result;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day3).Name);
        }

        private static int Multiply(string input) {
            var parsed = input.TrimStart('m', 'u', 'l', '(').TrimEnd(')').Split(',');
            return int.Parse(parsed[0]) * int.Parse(parsed[1]);

        }
    }
}
