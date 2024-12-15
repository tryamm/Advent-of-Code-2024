using Advent_of_Code_2024.Day6;
using System;

namespace Advent_of_Code_2024.Day15
{
    public static class Day15
    {
        private static string[] _input;
        private static List<Point> _goods;
        private static Point _robot;
        private static Queue<Direction> _directions;
        private const char _obstacle = '#';

        static Day15() => Init();

        public static long Part1() {
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
            return default(long);
        }

        public static bool CanMove(Point point, Direction direction) {
            return direction switch
            {
                Direction.Up => CheckNeighbour(point.x, point.y - 1, direction),
                Direction.Right => CheckNeighbour(point.x + 1, point.y, direction),
                Direction.Down => CheckNeighbour(point.x, point.y + 1, direction),
                Direction.Left => CheckNeighbour(point.x - 1, point.y, direction),
                _ => false,
            };
        }

        private static bool CheckNeighbour(int x, int y, Direction direction) {
            if (_input[y][x] == _obstacle)
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

        private static void Init() {
            _goods = [];
            _directions = [];
            _input = Utilities.GetInputData(typeof(Day15).Name);
            for (int i = 0; i < _input.Length; i++)
            {
                var line = _input[i];
                for (int j = 0; j < _input[i].Length; j++)
                {
                    switch (line[j])
                    {
                        case 'O':
                            {
                                _goods.Add(new Point(j, i));
                                break;
                            }
                        case '@':
                            {
                                _robot = new Point(j, i);
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
                    }
                }
            }
        }
    }

    public class Point()
    {
        public int x;
        public int y;
        //public Direction direction;

        public Point(int v, int i) : this() {
            x = v;
            y = i;
            //direction = Direction.Up;
        }

        //public void TurnRight() {
        //    if (direction == Direction.Left)
        //    {
        //        direction = Direction.Up;
        //    }
        //    else direction++;
        //}

        //public bool Equals(Point obj) {
        //    return obj.x == this.x && obj.y == this.y && obj.direction == this.direction;
        //}
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}
