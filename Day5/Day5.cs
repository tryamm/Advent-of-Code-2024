using System.Collections.Generic;

namespace Advent_of_Code_2024.Day5
{
    public static class Day5
    {
        private static string[] _input;
        private static Dictionary<int, IEnumerable<int>> _rules = [];
        private static string[] _updates = [];

        public static int Part1() {
            Init();
            var result = 0;

            foreach (var update in _updates)
            {
                var sequence = update.Split(',').Select(x => int.Parse(x)).ToList();
                if (sequence.IsValid())
                {
                    result += sequence[sequence.Count / 2];
                }
            }

            return result;
        }

        public static int Part2() {
            Init();
            var result = 0;
            foreach (var update in _updates)
            {
                var sequence = update.Split(',').Select(x => int.Parse(x)).ToList();
                if (!sequence.IsValid())
                {
                    var newSequence = sequence.Order(new NumberComparer()).ToList();
                    result += newSequence[newSequence.Count / 2];
                }
            }

            return result;
        }

        private static bool IsValid(this List<int> list) {
            for (int i = 0; i < list.Count; i++)
            {
                if (_rules.TryGetValue(list[i], out var record))
                {
                    if (list.Take(i).Any(x => record.Contains(x)))
                    {
                        return false;
                    }
                };

            }
            return true;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day5).Name);
            var split = Array.IndexOf(_input, _input.FirstOrDefault(x => string.IsNullOrEmpty(x)));
            foreach (var line in _input.Take(split))
            {
                var rule = line.Split('|');
                var key = int.Parse(rule[0]);
                var value = int.Parse(rule[1]);

                if (_rules.TryGetValue(key, out var array))
                {
                    array = array.Append(value);
                }
                else
                {
                    _rules.Add(key, [value]);
                }
            }

            _updates = _input.Skip(split + 1).Take(_input.Length - split).ToArray();
        }


        class NumberComparer : IComparer<int>
        {
            public int Compare(int x, int y) {
                if (_rules.TryGetValue(x, out var value) && value.Contains(y))
                {
                    return 1;
                }
                if (_rules.TryGetValue(y, out value) && value.Contains(x))
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}
