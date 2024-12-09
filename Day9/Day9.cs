namespace Advent_of_Code_2024.Day9
{
    public static class Day9
    {
        internal class FileBlock(int id)
        {
            public int Id { get; set; } = id;
        }

        private static string[] _input;
        private static List<int> _disk = [];

        static Day9() => Init();

        public static long Part1() {
            var newLength = CompactSpace();
            long result = 0;
            for (int i = 0; i <= newLength; i++)
            {
                result += i * _disk[i];
            }
            return result;
        }

        private static int CompactSpace() {
            var last = _disk.Count() - 1;
            for (int i = 0; i < _disk.Count(); i++)
            {
                if (_disk[i] != -1) continue;
                while (_disk[last] == -1)
                {
                    last--;
                }
                if (last < i)
                    break;
                _disk[i] = _disk[last];
                last--;
            }
            return last;
        }

        public static long Part2() {
            var result = 0;
            return result;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day9).Name);
            for (var j = 0; j < _input[0].Length; j++)
            {
                var counter = j % 2 == 0 ? j / 2 : -1;
                for (int i = 0; i < char.GetNumericValue(_input[0][j]); i++)
                {
                    _disk = [.. _disk, counter];
                }
            }
        }
    }
}
