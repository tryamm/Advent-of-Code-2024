using System.Collections.Generic;

namespace Advent_of_Code_2024.Day6
{
    public static class Day6
    {
        private static string[] _input;
        private static bool[,] _path;
        private static Guard _guard;

        private const char _obstacle = '#';

        private static int _maxX;
        private static int _maxY;
        private static int _minX;
        private static int _minY;

        public static int Part1() {
            Init();
            Move();
            return CalculatePath();
        }

        public static int Part2() {
            Init();
            var result = 0;
            Move();
            return result;
        }

        private static void Move() {
            _path[_guard.y, _guard.x] = true;
            if (!CanMove()) return;

            if (_input[_guard.y][_guard.x] == _obstacle)
            {
                _guard.TurnRight();
            }

            switch (_guard.direction)
            {
                case Direction.Up:
                    _guard.y--;
                    if (_input[_guard.y][_guard.x] == _obstacle)
                    {
                        _guard.TurnRight();
                        _guard.y++;
                    }
                    break;
                case Direction.Right:
                    _guard.x++;
                    if (_input[_guard.y][_guard.x] == _obstacle)
                    {
                        _guard.TurnRight();
                        _guard.x--;
                    }
                    break;
                case Direction.Down:
                    _guard.y++;
                    if (_input[_guard.y][_guard.x] == _obstacle)
                    {
                        _guard.TurnRight();
                        _guard.y--;
                    }
                    break;
                case Direction.Left:
                    _guard.x--;
                    if (_input[_guard.y][_guard.x] == _obstacle)
                    {
                        _guard.TurnRight();
                        _guard.x++;
                    }
                    break;
            }
            Move();

        }

        private static bool CheckLoop() {
            int x = _guard.x;
            int y = _guard.y;
            
        }

        private static bool CanMove() {
            return _guard.x < _maxX && _guard.y < _maxY && _guard.x > _minX && _guard.y > _minY;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day6).Name);
            _maxX = _input[0].Length - 1;
            _maxY = _input.Length - 1;
            _minX = 0;
            _minY = 0;
            _path = new bool[_maxY + 1, _maxX + 1];

            LocateGuard();
        }

        private static void LocateGuard() {
            for (int i = 0; i < _input.Length; i++)
            {
                if (_input[i].IndexOf('^') > 0)
                {
                    _guard = new Guard(_input[i].IndexOf('^'), i);
                    break;
                }
            }
        }

        private static int CalculatePath() {
            var result = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[0].Length; j++)
                {
                    if (_path[i, j]) ++result;
                }
            }

            return result;
        }
    }

    public struct Guard()
    {
        public int x;
        public int y;
        public Direction direction;

        public Guard(int v, int i) : this() {
            x = v;
            y = i;
            direction = Direction.Up;
        }

        public void TurnRight() {
            if (direction == Direction.Left)
            {
                direction = Direction.Up;
            }
            else direction++;
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
