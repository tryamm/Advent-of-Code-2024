namespace Advent_of_Code_2024.Day8
{
    public static class Day8
    {
        private static string[] _input;
        private static List<Antenna> _antennaList = [];
        private static int _maxX;
        private static int _maxY;
        private static int _minX;
        private static int _minY;
        private static List<(int x, int y)> _antiNodes = [];

        private const char _empty = '.';

        static Day8() => Init();

        public static long Part1() {
            var dict = _antennaList.GroupBy(x => x.frequency);
            foreach (var item in dict)
            {
                for (int i = 0; i < item.Count(); i++)
                {
                    var items = item.ToList();
                    for (int j = i + 1; j < item.Count(); j++)
                    {
                        GetAntinodes(items[i], items[j]);
                    }
                }
            }

            return _antiNodes.Count();
        }

        public static long Part2() {
            var dict = _antennaList.GroupBy(x => x.frequency);
            foreach (var item in dict)
            {
                for (int i = 0; i < item.Count(); i++)
                {
                    var items = item.ToList();
                    for (int j = i + 1; j < item.Count(); j++)
                    {
                        GetAntinodes(items[i], items[j]);
                    }
                }
            }

            return _antiNodes.Count();
        }

        public static void GetReccurentAntinodes(Antenna a1, Antenna a2) {
            var modX = a1.x - a2.x;
            var modY = a1.y - a2.y;
            if (IsValid(a1.x + modX, a1.y + modY)) _antiNodes.Add((a1.x + modX, a1.y + modY));
            if (IsValid(a2.x - modX, a2.y - modY)) _antiNodes.Add((a2.x - modX, a2.y - modY));
        }

        public static void GetAntinodes(Antenna a1, Antenna a2) {
            var modX = a1.x - a2.x;
            var modY = a1.y - a2.y;
            if (IsValid(a1.x + modX, a1.y + modY)) _antiNodes.Add((a1.x + modX, a1.y + modY));
            if (IsValid(a2.x - modX, a2.y - modY)) _antiNodes.Add((a2.x - modX, a2.y - modY));
        }

        public static bool IsValid(int x, int y) {
            return !_antiNodes.Any(a => a.x == x && a.y == y) && x >= _minX && x <= _maxX && y >= _minY && y <= _maxY;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day8).Name);
            _maxX = _input[0].Length - 1;
            _maxY = _input.Length - 1;
            _minX = 0;
            _minY = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[i].Length; j++)
                {
                    if (_input[i][j] != _empty)
                    {
                        _antennaList.Add(new Antenna(i, j, _input[i][j]));
                    }
                }
            }
        }
    }

    public class Antenna()
    {
        public int x;
        public int y;
        //public Direction direction;
        public char frequency;

        public Antenna(int y, int x) : this() {
            this.x = x;
            this.y = y;
        }

        public Antenna(int y, int x, char f) : this() {
            this.x = x;
            this.y = y;
            this.frequency = f;
        }

        //public bool Equals(Antenna obj) {
        //    return obj.x == this.x && obj.y == this.y && obj.direction == this.direction;
        //}
    }
}
