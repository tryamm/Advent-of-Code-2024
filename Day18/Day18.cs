using Advent_of_Code_2024.Day14;

namespace Advent_of_Code_2024.Day18
{
    public static class Day18
    {
        private static string[] _input;
        private static List<List<int>> _map;
        private static List<List<bool>> _path;
        private static (int y, int x) _target;

        private static int _maxX;
        private static int _maxY;
        private static int _minX;
        private static int _minY;
        private const int _size = 70;
        private const int _bytes = 1024;

        public static long Part1() {
            Init();
            _target = (_size + 1, _size + 1);
            var result = 0L;
            var priorityQueue = new SortedSet<(long score, (int x, int y) point)>
            {
                (0, (1, 1))
            };
            Utilities.PrintArray(_path);
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

                if (_path[point.x + 1][point.y] && _map[point.x + 1][point.y] != 1) priorityQueue.Add((score + 1, (point.x + 1, point.y)));
                if (_path[point.x - 1][point.y] && _map[point.x - 1][point.y] != 1) priorityQueue.Add((score + 1, (point.x - 1, point.y)));
                if (_path[point.x][point.y + 1] && _map[point.x][point.y + 1] != 1) priorityQueue.Add((score + 1, (point.x, point.y + 1)));
                if (_path[point.x][point.y - 1] && _map[point.x][point.y - 1] != 1) priorityQueue.Add((score + 1, (point.x, point.y - 1)));

            }

            Console.WriteLine();
            Utilities.PrintArray(_path);
            return result;
        }

        public static long Part2() {
            Init();
            return default;
        }
        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day18).Name);
            _maxX = _maxY = _size + 2;
            _minX = _minY = 0;

            _map = [];
            _path = [];

            _map.Add(Enumerable.Repeat(1, _size + 3).ToList());
            _path.Add(Enumerable.Repeat(false, _size + 3).ToList());
            for (int i = 1; i < _maxX; i++)
            {
                _map.Add(Enumerable.Concat([1], Enumerable.Repeat(0, _size + 1)).Concat([1]).ToList());
                _path.Add(Enumerable.Concat([false], Enumerable.Repeat(true, _size + 1)).Concat([false]).ToList());
            }
            _map.Add(Enumerable.Repeat(1, _size + 3).ToList());
            _path.Add(Enumerable.Repeat(false, _size + 3).ToList());


            //foreach (var line in _input)
            for (int i = 0; i < _bytes; ++i)
            {
                var line = _input[i];
                var coordinates = line.Split(',');
                _map[int.Parse(coordinates[1]) + 1][int.Parse(coordinates[0]) + 1] = 1;
                _path[int.Parse(coordinates[1]) + 1][int.Parse(coordinates[0]) + 1] = false;
            }
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}
