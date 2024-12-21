namespace Advent_of_Code_2024.Day19
{
    public static class Day19
    {
        private static string[] _input;
        private static List<string> _parts;
        private static List<string> _patterns;

        public static int Part1() {
            Init();

            var result = 0;
            foreach (var p in _patterns)
            {
                if (IsPossiblePattern(p))
                {
                    ++result;
                }
            }
            return result;
        }

        public static long Part2() {
            Init();

            var result = 0L;
            foreach (var p in _patterns)
            {
                result += CountPossiblePattern(p, []);

            }
            return result;
        }

        private static bool IsPossiblePattern(string pattern) {
            if (string.IsNullOrEmpty(pattern)) return true;

            var parts = _parts.Where(p => pattern.StartsWith(p));
            var result = false;
            if (parts.Any())
            {
                foreach (var part in parts)
                {
                    result = result || IsPossiblePattern(pattern[part.Length..]);
                }
            }

            return result;
        }

        private static long CountPossiblePattern(string pattern, Dictionary<string, long> cache) {
            if (string.IsNullOrEmpty(pattern)) return 1L;
            if (cache.TryGetValue(pattern, out var c))
            {
                return c;
            }

            var parts = _parts.Where(p => pattern.StartsWith(p));
            var result = 0L;
            if (parts.Any())
            {
                foreach (var part in parts)
                {
                    var count = CountPossiblePattern(pattern[part.Length..], cache);
                    result += count;
                }
            }
            cache.Add(pattern, result);

            return result;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day19).Name);
            _parts = [.. _input[0].Split(", ")];

            _patterns = [];
            for (int i = 2; i < _input.Length; i++)
            {
                _patterns.Add(_input[i].Trim());
            }
        }
    }
}
