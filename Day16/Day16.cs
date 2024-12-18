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
        private static SortedSet<(long score, (int y, int x) point, Direction d)> _priorityQueue = [];

        private static List<(int y, int x)> _seats;

        public static long Part1() {
            Init();
            var result = long.MaxValue;
            _priorityQueue.Add((0, _deer, Direction.Right));
            while (_priorityQueue.Count > 0)
            {
                var item = _priorityQueue.Min;
                var (score, point, d) = item;
                //if (score < _map[point.y][point.x])
                //{
                //    if (!_predecessors.TryGetValue((point.y, point.x), out var preds))
                //    {
                //        preds = [];
                //        _predecessors.Add((point.y, point.x), preds);
                //    }

                //    preds.Add(point);
                //}
                if (point.x == _target.x && point.y == _target.y)
                {
                    //if (result <= score) 
                    result = Math.Min(result, score);
                    //_priorityQueue = [];
                }
                if (_path[point.y][point.x])
                {
                    _priorityQueue.RemoveWhere(q => q.point.Equals(point));
                    continue;
                }
                _map[point.y][point.x] = Math.Min(score, _map[point.y][point.x]);
                //if (_path[point.y][point.x]) continue;
                switch (d)
                {
                    case Direction.Right:
                        {
                            //if (!_path[point.y][point.x + 1])
                            //    if (!_path[point.y + 1][point.x])
                            //        if (!_path[point.y - 1][point.x])
                            ProcessPoint(score + 1, point.y, point.x + 1, point, d);
                            ProcessPoint(score + 1, point.y, point.x + 1, point, d);
                            ProcessPoint(score + 1001, point.y + 1, point.x, point, d.TurnRight());
                            ProcessPoint(score + 1001, point.y - 1, point.x, point, d.TurnLeft());
                            break;
                        }
                    case Direction.Down:
                        {
                            //if (!_path[point.y + 1][point.x])
                            //    if (!_path[point.y][point.x - 1])
                            //        if (!_path[point.y][point.x + 1])
                            ProcessPoint(score + 1, point.y + 1, point.x, point, d);
                            ProcessPoint(score + 1001, point.y, point.x - 1, point, d.TurnRight());
                            ProcessPoint(score + 1001, point.y, point.x + 1, point, d.TurnLeft());
                            break;
                        }
                    case Direction.Left:
                        {
                            //if (!_path[point.y][point.x - 1])
                            //    if (!_path[point.y - 1][point.x])
                            //        if (!_path[point.y + 1][point.x])
                            ProcessPoint(score + 1, point.y, point.x - 1, point, d);
                            ProcessPoint(score + 1001, point.y - 1, point.x, point, d.TurnRight());
                            ProcessPoint(score + 1001, point.y + 1, point.x, point, d.TurnLeft());
                            break;
                        }
                    case Direction.Up:
                        {
                            //    if (!_path[point.y - 1][point.x])
                            //        if (!_path[point.y][point.x + 1])
                            //            if (!_path[point.y][point.x - 1])
                            ProcessPoint(score + 1, point.y - 1, point.x, point, d);
                            ProcessPoint(score + 1001, point.y, point.x + 1, point, d.TurnRight());
                            ProcessPoint(score + 1001, point.y, point.x - 1, point, d.TurnLeft());
                            break;
                        }
                }

                _path[point.y][point.x] = true;
                _priorityQueue.RemoveWhere(q => q.point.Equals(point));
            }
            return result;
        }

        private static void ProcessPoint(long score, int y, int x, (int y, int x) point, Direction d) {
            if (!_predecessors.TryGetValue((y, x), out var preds))
            {
                preds = [];
                _predecessors.Add((y, x), preds);
            }

            preds.Add(point);
            var min = preds.Select(p => _map[p.y][p.x]).OrderBy(p => p).First();
            if (min > 0)
            {
                preds = preds.Where(p => _map[p.y][p.x] == min).ToList();
            }
            _priorityQueue.Add((score, (y, x), d));
        }

        public static long Part2() {
            //Init();
            Utilities.PrintArray(_map);
            _predecessors = _predecessors.OrderBy(x => x.Key).ToDictionary();
            _predecessors.Remove(_deer);
            _seats = [];
            _ = FindAllSeats(_target.y, _target.x);
            return _seats.Count();

            //517 high
            // 150
        }

        static int FindAllSeats(int y, int x) {
            if (_seats.Contains((y, x))) return 0;
            _seats.Add((y, x));

            var point = _predecessors.GetValueOrDefault((y, x));
            if (point == null)
                return 0;

            return point.Select(p => Math.Abs(_map[y][x] - _map[p.y][p.x]) <= 1001 ? FindAllSeats(p.y, p.x) : 0).Count();
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
                    _map[i].Add(long.MaxValue);
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
