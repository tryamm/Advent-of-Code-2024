namespace Advent_of_Code_2024.Day10
{
    public static class Day10
    {
        private static string[] _input;
        private static int[,] _map;
        private static List<(int x, int y)> _trailheads = [];
        private static bool _countUnique;
        private static int _maxX;
        private static int _maxY;
        private static int _minX;
        private static int _minY;

        static Day10() => Init();

        public static long Part1() {
            var result = 0;
            _countUnique = true;
            for (int i = 0; i <= _maxX; i++)
            {
                for (int j = 0; j <= _maxY; j++)
                {
                    if (_map[i, j] == 0)
                    {
                        _trailheads = [];
                        result += CalculateScore(i, j);
                    }
                }
            }
            return result;
        }

        public static long Part2() {
            var result = 0;
            _countUnique = false;
            for (int i = 0; i <= _maxX; i++)
            {
                for (int j = 0; j <= _maxY; j++)
                {
                    if (_map[i, j] == 0)
                    {
                        result += CalculateScore(i, j);
                    }
                }
            }
            return result;
        }

        public static int CalculateScore(int x, int y) {
            if (_map[x, y] == 9)
            {
                if (_countUnique && _trailheads.Any(t => t.x == x && t.y == y))
                {
                    return 0;
                }
                _trailheads.Add((x, y));
                return 1;
            }

            var expected = _map[x, y] + 1;
            return
                (IsValid(x + 1, y) && (_map[x + 1, y] == expected) ? CalculateScore(x + 1, y) : 0) +
                (IsValid(x - 1, y) && _map[x - 1, y] == expected ? CalculateScore(x - 1, y) : 0) +
                (IsValid(x, y + 1) && _map[x, y + 1] == expected ? CalculateScore(x, y + 1) : 0) +
                (IsValid(x, y - 1) && _map[x, y - 1] == expected ? CalculateScore(x, y - 1) : 0);
        }

        public static bool IsValid(int x, int y) {
            return x >= _minX && x <= _maxX && y >= _minY && y <= _maxY;
        }


        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day10).Name);
            _maxX = _input.Length - 1;
            _maxY = _input[0].Length - 1;
            _minX = 0;
            _minY = 0;
            _map = new int[_maxX + 1, _maxY + 1];
            for (int i = 0; i <= _maxX; i++)
            {
                for (int j = 0; j <= _maxY; j++)
                {
                    _map[i, j] = _input[i][j] - 48;
                }
            }
        }
    }
}
