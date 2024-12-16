using Advent_of_Code_2024.Day14;

namespace Advent_of_Code_2024.Day16
{
    public static class Day16
    {
        private static string[] _input;
        private static List<List<int>> _map;
        private static List<List<bool>> _path;
        private static (int y, int x) _deer;
        private static (int y, int x) _target;

        public static long Part1() {
            Init();
            var result = long.MaxValue;
            //var queue = new Queue<((int y, int x) point, Direction d, long score)>();
            var priorityQueue = new SortedSet<(long score, (int y, int x) point, Direction d)>();
            priorityQueue.Add((0, _deer, Direction.Right));
            while (priorityQueue.Count > 0)
            {
                var item = priorityQueue.Min;
                var (score, point, d) = item;
                if (point.x == _target.x && point.y == _target.y)
                {
                    result = Math.Min(result, score);
                }
                if (_path[point.y][point.x])
                {
                    priorityQueue.Remove(item);
                    continue;
                }
                if (_path[point.y][point.x]) continue;
                switch (d)
                {
                    case Direction.Right:
                        {
                            if (!_path[point.y][point.x + 1]) priorityQueue.Add((score + 1, (point.y, point.x + 1), d));
                            if (!_path[point.y + 1][point.x]) priorityQueue.Add((score + 1001, (point.y + 1, point.x), d.TurnRight()));
                            if (!_path[point.y - 1][point.x]) priorityQueue.Add((score + 1001, (point.y - 1, point.x), d.TurnLeft()));
                            break;
                        }
                    case Direction.Down:
                        {
                            if (!_path[point.y + 1][point.x]) priorityQueue.Add((score + 1, (point.y + 1, point.x), d));
                            if (!_path[point.y][point.x - 1]) priorityQueue.Add((score + 1001, (point.y, point.x - 1), d.TurnRight()));
                            if (!_path[point.y][point.x + 1]) priorityQueue.Add((score + 1001, (point.y, point.x + 1), d.TurnLeft()));
                            break;
                        }
                    case Direction.Left:
                        {
                            if (!_path[point.y][point.x - 1]) priorityQueue.Add((score + 1, (point.y, point.x - 1), d));
                            if (!_path[point.y - 1][point.x]) priorityQueue.Add((score + 1001, (point.y - 1, point.x), d.TurnRight()));
                            if (!_path[point.y + 1][point.x]) priorityQueue.Add((score + 1001, (point.y + 1, point.x), d.TurnLeft()));
                            break;
                        }
                    case Direction.Up:
                        {
                            if (!_path[point.y - 1][point.x]) priorityQueue.Add((score + 1, (point.y - 1, point.x), d));
                            if (!_path[point.y][point.x + 1]) priorityQueue.Add((score + 1001, (point.y, point.x + 1), d.TurnRight()));
                            if (!_path[point.y][point.x - 1]) priorityQueue.Add((score + 1001, (point.y, point.x - 1), d.TurnLeft()));
                            break;
                        }
                }

                _path[point.y][point.x] = true;
                priorityQueue.Remove(item);
            }
            return result;
        }

        public static long Part2() {
            Init();
            return default;
        }

        private static Direction TurnRight(this Direction direction) {
            if (direction == Direction.Left)
            {
                return Direction.Up;
            }
            else return ++direction;
        }

        private static Direction TurnLeft(this Direction direction) {
            if (direction == Direction.Up)
            {
                return Direction.Left;
            }
            else return --direction;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day16).Name);
            _map = [];
            _path = [];
            for (int i = 0; i < _input.Length; i++)
            {
                _map.Add([]);
                _path.Add([]);
                for (int j = 0; j < _input[i].Length; j++)
                {
                    _map[i].Add(_input[i][j] == '#' ? -1 : 1);
                    if (_input[i][j] == 'S')
                        _deer = (i, j);
                    if (_input[i][j] == 'E')
                        _target = (i, j);

                    _path[i].Add(_input[i][j] == '#' ? true : false);
                }
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
