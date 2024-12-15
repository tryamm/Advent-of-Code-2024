namespace Advent_of_Code_2024.Day15
{
    public static class Day15
    {
        private static string[] _input;
        private static List<string> _warehouse;
        private static List<Point> _goods;
        private static Point _robot;
        private static Queue<Direction> _directions;
        private const char _obstacle = '#';

        public static long Part1() {
            Init();
            while (_directions.Count > 0)
            {
                var d = _directions.Dequeue();
                if (CanMove(_robot, d))
                {
                    Move(_robot, d);
                }
            }

            return _goods.Select(o => o.x + (o.y * 100)).Sum();
        }

        public static long Part2() {
            Init(true);
            while (_directions.Count > 0)
            {
                var d = _directions.Dequeue();
                if (CanMove(_robot, d))
                {
                    Move(_robot, d);
                }
            }

            return _goods.Select(o => Math.Min(o.x, o._linked?.x ?? 0) + (o.y * 100)).Sum() / 2;
        }

        public static bool CanMove(Point point, Direction direction) {
            return direction switch
            {
                Direction.Up => CheckNeighbour(point.x, point.y - 1, direction) && (point._linked == null || CheckNeighbour(point._linked.x, point._linked.y - 1, direction)),
                Direction.Right => CheckNeighbour(point.x + 1, point.y, direction),
                Direction.Down => CheckNeighbour(point.x, point.y + 1, direction) && (point._linked == null || CheckNeighbour(point._linked.x, point._linked.y + 1, direction)),
                Direction.Left => CheckNeighbour(point.x - 1, point.y, direction),
                _ => false,
            };
        }

        private static bool CheckNeighbour(int x, int y, Direction direction) {
            if (_warehouse[y][x] == _obstacle)
            {
                return false;
            }

            var item = _goods.FirstOrDefault(o => o.x == x && o.y == y);
            if (item != null)
            {
                return CanMove(item, direction);
            }

            return true;
        }

        public static void Move(Point point, Direction direction) {
            MoveNeighbour(point, direction);
            _ = direction switch
            {
                Direction.Up => --point.y,
                Direction.Right => ++point.x,
                Direction.Down => ++point.y,
                Direction.Left => --point.x,
            };
            if (point._linked != null && new Direction[] { Direction.Up, Direction.Down }.Contains(direction))
            {
                MoveNeighbour(point._linked, direction);
                _ = direction switch
                {
                    Direction.Up => --point._linked.y,
                    Direction.Right => ++point._linked.x,
                    Direction.Down => ++point._linked.y,
                    Direction.Left => --point._linked.x,
                };
            }
        }

        private static void MoveNeighbour(Point point, Direction direction) {
            (int nx, int ny) = direction switch
            {
                Direction.Up => (point.x, point.y - 1),
                Direction.Right => (point.x + 1, point.y),
                Direction.Down => (point.x, point.y + 1),
                Direction.Left => (point.x - 1, point.y),
            };

            var item = _goods.FirstOrDefault(o => o.x == nx && o.y == ny);
            if (item != null)
            {
                Move(item, direction);
            }
        }

        private static void Init(bool isExpanded = false) {
            _goods = [];
            _directions = [];
            _warehouse = [];
            _input = Utilities.GetInputData(typeof(Day15).Name);
            for (int i = 0; i < _input.Length; i++)
            {
                var line = _input[i];
                if (line.Contains(_obstacle))
                    _warehouse.Add(line);
                for (int j = 0; j < _input[i].Length; j++)
                {
                    switch (line[j])
                    {
                        case _obstacle:
                            {
                                if (isExpanded)
                                    _warehouse[i] = _warehouse[i].Insert(j * 2 + 1, "#");
                                break;
                            }
                        case 'O':
                            {
                                var h2 = new Point(isExpanded ? j * 2 : j, i);
                                _goods.Add(h2);

                                if (isExpanded)
                                {
                                    _warehouse[i] = _warehouse[i].Insert(j * 2 + 1, "]");
                                    _goods.Add(new Point(j * 2 + 1, i, h2));
                                }
                                break;
                            }
                        case '@':
                            {
                                _robot = new Point(isExpanded ? j * 2 : j, i);
                                if (isExpanded)
                                {
                                    _warehouse[i] = _warehouse[i].Insert(j * 2 + 1, ".");
                                }
                                break;
                            }
                        case '<':
                            {
                                _directions.Enqueue(Direction.Left);
                                break;
                            }
                        case '^':
                            {
                                _directions.Enqueue(Direction.Up);
                                break;
                            }
                        case '>':
                            {
                                _directions.Enqueue(Direction.Right);
                                break;
                            }
                        case 'v':
                            {
                                _directions.Enqueue(Direction.Down);
                                break;
                            }
                        case '.':
                            {
                                if (isExpanded)
                                {
                                    _warehouse[i] = _warehouse[i].Insert(j * 2 + 1, ".");
                                }
                                break;
                            }
                    }
                }
            }
        }
    }

    public class Point()
    {
        public int x;
        public int y;

        public Point _linked;

        public Point(int v, int i) : this() {
            x = v;
            y = i;
        }

        public Point(int v, int i, Point p) : this() {
            x = v;
            y = i;
            _linked = p;

            p._linked = this;
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
