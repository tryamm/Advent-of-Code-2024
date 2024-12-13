namespace Advent_of_Code_2024.Day11
{
    public static class Day11
    {
        private static string[] _input;
        private static List<long> _stones;
        private static int _blinks;
        private static Dictionary<(long stone, long blinks), long> _cache = [];

        public static long Part1() {
            Init();
            long result = 0;
            _blinks = 25;
            foreach (var stone in _stones)
            {
                result += BlinkResult(stone, 0, 0);
            }
            return result;
        }

        public static long Part2() {
            Init();
            long result = 0;
            _blinks = 75;

            foreach (var stone in _stones)
            {
                result += BlinkResult(stone, 0, 0);
            }
            return result;
        }

        private static long BlinkResult(long stone, long count, int blinks) {
            if (_cache.TryGetValue((stone, blinks), out var result)) {
                return result;
            }

            if (blinks == _blinks) return 1;

            if (stone == 0)
            {
                var x = BlinkResult(1, 0, blinks + 1);
                count += x;
                _cache.TryAdd((1, blinks + 1), x);
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                var value = stone.ToString();
                var x = BlinkResult(long.Parse(value[..(value.Length / 2)]), 0, blinks + 1);
                var y = BlinkResult(long.Parse(value.Substring(value.Length / 2, value.Length / 2)), 0, blinks + 1);
                count += x + y;
                _cache.TryAdd((long.Parse(value[..(value.Length / 2)]), blinks + 1), x);
                _cache.TryAdd((long.Parse(value.Substring(value.Length / 2, value.Length / 2)), blinks + 1), y);
            }
            else
            {
                var x = BlinkResult(stone * 2024, 0, blinks + 1);
                count += x;
                _cache.TryAdd((stone * 2024, blinks + 1), x);
            }

            return count;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day11).Name);
            _stones = _input[0].Split(' ').Select(x => long.Parse(x)).ToList();
            _cache = [];
        }
    }
}
