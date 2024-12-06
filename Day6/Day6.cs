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
            _path = new bool[_maxY + 1, _maxX + 1];
            Move();
            return CalculatePath();
        }

        public static int Part2() {
            Init();
            var result = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[0].Length; j++)
                {
                    if (_path[i, j])
                    {
                        var originalChar = _input[i][j];
                        _input[i] = _input[i].SubstituteChar(j, _obstacle);
                        if (CheckCycle()) result++;
                        _input[i] = _input[i].SubstituteChar(j, originalChar);
                    }
                }
            }
            return result;
        }

        private static void Move() {
            _path[_guard.y, _guard.x] = true;
            if (!CanMove(_guard)) return;

            if (_input[_guard.y][_guard.x] == _obstacle)
            {
                _guard.TurnRight();
            }

            Step(_guard);
            Move();
        }

        private static bool CheckCycle() {
            var fast = new Guard(_guard.x, _guard.y);
            var slow = new Guard(_guard.x, _guard.y);
            Step(fast);

            while (CanMove(fast) && !fast.Equals(slow))
            {
                Step(fast);
                if (CanMove(fast)) Step(fast);
                Step(slow);
            }

            return fast.Equals(slow);
        }

        private static void Step(Guard guard) {
            switch (guard.direction)
            {
                case Direction.Up:
                    guard.y--;
                    if (_input[guard.y][guard.x] == _obstacle)
                    {
                        guard.TurnRight();
                        guard.y++;
                    }
                    break;
                case Direction.Right:
                    guard.x++;
                    if (_input[guard.y][guard.x] == _obstacle)
                    {
                        guard.TurnRight();
                        guard.x--;
                    }
                    break;
                case Direction.Down:
                    guard.y++;
                    if (_input[guard.y][guard.x] == _obstacle)
                    {
                        guard.TurnRight();
                        guard.y--;
                    }
                    break;
                case Direction.Left:
                    guard.x--;
                    if (_input[guard.y][guard.x] == _obstacle)
                    {
                        guard.TurnRight();
                        guard.x++;
                    }
                    break;
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

        private static bool CanMove(Guard guard) {
            return guard.x < _maxX && guard.y < _maxY && guard.x > _minX && guard.y > _minY;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day6).Name);
            _maxX = _input[0].Length - 1;
            _maxY = _input.Length - 1;
            _minX = 0;
            _minY = 0;

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
    }

    public class Guard()
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

        public bool Equals(Guard obj) {
            return obj.x == this.x && obj.y == this.y && obj.direction == this.direction;
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
