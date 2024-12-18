namespace Advent_of_Code_2024.Day18
{
    public static class Day18
    {
        private static string[] _input;
        private static List<List<bool>> _path;
        private static (int y, int x) _target;

        private static int _maxX;
        private static int _maxY;
        private static int _minX;
        private static int _minY;
        private static int _bytes = 1024;
        private const int _size = 70;

        public static long Part1() {
            Init();
            _target = (_size + 1, _size + 1);

            var result = FindPathLength();
            return result;
        }

        public static string Part2() {
            var result = 1L;
            _target = (_size + 1, _size + 1);

            while (result != 0)
            {
                ++_bytes;
                Init();
                result = FindPathLength();
            }
            return _input[_bytes - 1];
        }

        private static long FindPathLength() {
            var result = 0L;
            var priorityQueue = new SortedSet<(long score, (int x, int y) point)>
            {
                (0, (1, 1))
            };
            while (priorityQueue.Count > 0)
            {
                var item = priorityQueue.Min;
                var (score, point) = item;

                if (point.x == _target.x && point.y == _target.y)
                {
                    result = score;
                    priorityQueue = [];
                    continue;
                }
                if (!_path[point.x][point.y])
                {
                    continue;
                }
                _path[point.x][point.y] = false;
                priorityQueue.Remove(item);

                if (_path[point.x + 1][point.y]) priorityQueue.Add((score + 1, (point.x + 1, point.y)));
                if (_path[point.x - 1][point.y]) priorityQueue.Add((score + 1, (point.x - 1, point.y)));
                if (_path[point.x][point.y + 1]) priorityQueue.Add((score + 1, (point.x, point.y + 1)));
                if (_path[point.x][point.y - 1]) priorityQueue.Add((score + 1, (point.x, point.y - 1)));

            }
            return result;
        }


        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day18).Name);
            _maxX = _maxY = _size + 2;
            _minX = _minY = 0;

            _path = [];
            _path.Add(Enumerable.Repeat(false, _size + 3).ToList());
            for (int i = 1; i < _maxX; i++)
            {
                _path.Add(Enumerable.Concat([false], Enumerable.Repeat(true, _size + 1)).Concat([false]).ToList());
            }
            _path.Add(Enumerable.Repeat(false, _size + 3).ToList());


            for (int i = 0; i < _bytes; ++i)
            {
                var line = _input[i];
                var coordinates = line.Split(',');
                _path[int.Parse(coordinates[1]) + 1][int.Parse(coordinates[0]) + 1] = false;
            }
        }
    }
}
