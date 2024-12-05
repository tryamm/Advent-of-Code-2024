using System.Text;

namespace Advent_of_Code_2024.Day4
{
    public static partial class Day4
    {
        private static string[] _input;
        private static int _maxX;
        private static int _maxY;
        private static int _minX;
        private static int _minY;
        private static string _word = string.Empty;

        public static int Part1() {
            _word = "XMAS";
            Init();
            var result = 0;
            for (int i = 0; i < _input.Count(); i++)
            {
                for (int j = 0; j < _input[i].Count(); j++)
                {
                    if (_input[i][j] == _word[0])
                    {
                        result += FindWord(i, j);
                    }
                }
            }


            return result;
        }

        public static int Part2() {
            _word = "MAS";
            Init();
            var result = 0;
            for (int i = 0; i < _input.Count(); i++)
            {
                for (int j = 0; j < _input[i].Count(); j++)
                {
                    if (_input[i][j] == _word[0])
                    {
                        result += FindDiagonalWord(i, j);
                    }
                }
            }

            return result / 2;
        }

        public static int FindWord(int y, int x) {
            var result = 0;
            if (_input[y][x] == _word[0])
            {
                if (y <= _maxY)
                {
                    if (HasVerticalWord(y, x, true)) result++;
                    if (x <= _maxX)
                    {
                        if (HasDiagonalWord(y, x, true, true)) result++;
                    }
                    if (x >= _minX)
                    {
                        if (HasDiagonalWord(y, x, false, true)) result++;
                    }
                }

                if (y >= _minY)
                {
                    if (HasVerticalWord(y, x, false)) result++;
                    if (x <= _maxX)
                    {
                        if (HasDiagonalWord(y, x, true, false)) result++;
                    }
                    if (x >= _minX)
                    {
                        if (HasDiagonalWord(y, x, false, false)) result++;
                    }
                }

                if (x <= _maxX)
                {
                    if (HasHorizontalWord(y, x, true)) result++;
                }
                if (x >= _minX)
                {
                    if (HasHorizontalWord(y, x, false)) result++;
                }
            }

            return result;
        }

        public static int FindDiagonalWord(int y, int x) {
            var result = 0;
            if (_input[y][x] == _word[0])
            {
                if (y <= _maxY)
                {
                    if (x <= _maxX)
                    {
                        if (HasDiagonalWord(y, x, true, true))
                        {
                            if (HasDiagonalWord(y + 2, x, true, false) || HasDiagonalWord(y, x + 2, false, true))
                            {
                                result++;
                            }
                        }
                    }
                    if (x >= _minX)
                    {
                        if (HasDiagonalWord(y, x, false, true))
                        {
                            if (HasDiagonalWord(y + 2, x, false, false) || HasDiagonalWord(y, x - 2, true, true))
                            {
                                result++;
                            }
                        }
                    }
                }

                if (y >= _minY)
                {
                    if (x <= _maxX)
                    {
                        if (HasDiagonalWord(y, x, true, false))
                        {
                            if (HasDiagonalWord(y - 2, x, true, true) || HasDiagonalWord(y, x + 2, false, false))
                            {
                                result++;
                            }
                        }
                    }
                    if (x >= _minX)
                    {
                        if (HasDiagonalWord(y, x, false, false))
                        {
                            if (HasDiagonalWord(y - 2, x, false, true) || HasDiagonalWord(y, x - 2, true, false))
                            {
                                result++;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static bool HasHorizontalWord(int y, int x, bool right) {
            var result = right
                ? _input[y].Substring(x, _word.Length)
                : string.Join(string.Empty, _input[y].Substring(x - _word.Length + 1, _word.Length).Reverse().ToArray());

            return _word == result;
        }

        public static bool HasVerticalWord(int y, int x, bool down) {
            var sb = new StringBuilder();
            for (int i = 0; i < _word.Length; i++)
            {
                sb.Append(down ? _input[y + i][x] : _input[y - i][x]);
            }

            return _word == sb.ToString();
        }

        public static bool HasDiagonalWord(int y, int x, bool right, bool down) {
            var sb = new StringBuilder();
            for (int i = 0; i < _word.Length; i++)
            {
                var symbol = right
                    ? down
                        ? _input[y + i][x + i]
                        : _input[y - i][x + i]
                    : down
                        ? _input[y + i][x - i]
                        : _input[y - i][x - i];
                sb.Append(symbol);
            }

            return _word == sb.ToString();
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day4).Name);
            _maxX = _input[0].Length - _word.Length;
            _maxY = _input.Length - _word.Length;
            _minX = _word.Length - 1;
            _minY = _word.Length - 1;
        }

    }
}
