namespace Advent_of_Code_2024.Day16
{
    public static class Day16
    {
        private static string[] _input;
        private static List<List<long>> _map;
        private static List<List<bool>> _path;
        private static (int y, int x) _deer;
        private static (int y, int x) _target;
        private static Dictionary<(int y, int x), List<(int y, int x)>> _predecessors = [];
        private static SortedSet<(long score, (int y, int x) point, (int y, int x) prevPoint, Direction d)> _priorityQueue = [];

        private static List<(int y, int x)> _seats;
        private static int _maxX;
        private static int _maxY;
        private static int _minX;
        private static int _minY;

        public static long Part1() {
            Init();
            var result = long.MaxValue;
            _priorityQueue.Add((0, _deer, _deer, Direction.Right));
            while (_priorityQueue.Count > 0)
            {
                var item = _priorityQueue.Min;
                var (score, point, prevPoint, d) = item;
                if (new[] { _minX, _maxX }.Contains(point.x) || new[] { _minY, _maxY }.Contains(point.y))
                {
                    _priorityQueue.Remove(item);
                    continue;
                }
                if (score <= _map[point.y][point.x] || Math.Abs(score - _map[point.y][point.x]) == 1000)
                {
                    if (!_predecessors.TryGetValue((point.y, point.x), out var preds))
                    {
                        preds = [];
                        _predecessors.Add((point.y, point.x), preds);
                    }

                    if (preds.Count > 0 && score < _map[point.y][point.x])
                    {
                        preds.RemoveAll(x => true);
                    }

                    preds.Add(prevPoint);
                    _map[point.y][point.x] = score;
                }

                if (point.x == _target.x && point.y == _target.y)
                {
                    result = Math.Min(result, score);
                    _priorityQueue = [];
                    continue;
                }
                if (_path[point.y][point.x])
                {
                    _priorityQueue.Remove(item);
                    continue;
                }

                switch (d)
                {
                    case Direction.Right:
                        {
                            ProcessPoint(score + 1, point.y, point.x + 1, point, d);
                            ProcessPoint(score + 1001, point.y + 1, point.x, point, d.TurnRight());
                            ProcessPoint(score + 1001, point.y - 1, point.x, point, d.TurnLeft());
                            break;
                        }
                    case Direction.Down:
                        {
                            ProcessPoint(score + 1, point.y + 1, point.x, point, d);
                            ProcessPoint(score + 1001, point.y, point.x - 1, point, d.TurnRight());
                            ProcessPoint(score + 1001, point.y, point.x + 1, point, d.TurnLeft());
                            break;
                        }
                    case Direction.Left:
                        {
                            ProcessPoint(score + 1, point.y, point.x - 1, point, d);
                            ProcessPoint(score + 1001, point.y - 1, point.x, point, d.TurnRight());
                            ProcessPoint(score + 1001, point.y + 1, point.x, point, d.TurnLeft());
                            break;
                        }
                    case Direction.Up:
                        {
                            ProcessPoint(score + 1, point.y - 1, point.x, point, d);
                            ProcessPoint(score + 1001, point.y, point.x + 1, point, d.TurnRight());
                            ProcessPoint(score + 1001, point.y, point.x - 1, point, d.TurnLeft());
                            break;
                        }
                }

                _path[point.y][point.x] = true;
                _priorityQueue.Remove(item);
            }
            return result;
        }

        private static void ProcessPoint(long score, int y, int x, (int y, int x) point, Direction d) {
            _priorityQueue.Add((score, (y, x), point, d));
        }

        public static long Part2() {
            _predecessors = _predecessors.OrderBy(x => x.Key).ToDictionary();
            _seats = [];
            var result = FindAllSeats(_target.y, _target.x);
            return _seats.Count;
        }

        static int FindAllSeats(int y, int x) {
            if (_seats.Contains((y, x))) return 0;
            _seats.Add((y, x));

            var point = _predecessors.GetValueOrDefault((y, x));
            if (point == null)
                return 0;

            return point.Select(p => FindAllSeats(p.y, p.x)).Count();
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
            _maxX = _input[0].Length - 1;
            _maxY = _input.Length - 1;
            _minX = _minY = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                _map.Add([]);
                _path.Add([]);
                for (int j = 0; j < _input[i].Length; j++)
                {
                    _map[i].Add(long.MaxValue);
                    if (_input[i][j] == 'S')
                        _deer = (i, j);
                    if (_input[i][j] == 'E')
                        _target = (i, j);

                    _path[i].Add(_input[i][j] == '#');
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
