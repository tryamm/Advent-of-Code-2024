namespace Advent_of_Code_2024.Day9
{
    public static class Day9
    {
        private static string[] _input;
        private static List<int> _disk;
        private static List<int> _diskStructure;

        public static long Part1() {
            Init();
            CompactSpace();
            return CalculateChecksum();
        }

        public static long Part2() {
            Init();
            ReorganizeSpace();
            return CalculateChecksum();
        }

        private static long CalculateChecksum() {
            long result = 0;
            for (int i = 0; i < _disk.Count; i++)
            {
                result += _disk[i] != -1 ? i * _disk[i] : 0;
            }

            return result;
        }

        private static void CompactSpace() {
            var last = _disk.Count - 1;
            for (int i = 0; i < _disk.Count; i++)
            {
                if (_disk[i] != -1) continue;
                while (_disk[last] == -1)
                {
                    _disk[last] = -1;
                    last--;
                }
                if (last < i)
                    break;
                _disk[i] = _disk[last];
                _disk[last] = -1;
                last--;
            }
        }

        private static void ReorganizeSpace() {
            var endCounter = _disk.Count;
            for (int i = _diskStructure.Count - 2; i > 1; i--)
            {
                endCounter -= _diskStructure[i];
                if (i % 2 == 1) continue;
                var startCounter = 0;
                for (int j = 1; j < i; j++)
                {
                    startCounter += _diskStructure[j - 1];
                    if (j % 2 == 0) continue;
                    if (_diskStructure[j] >= _diskStructure[i])
                    {
                        _disk.InsertRange(startCounter, Enumerable.Repeat(_disk[endCounter], _diskStructure[i]));
                        _disk.RemoveRange(startCounter + _diskStructure[i], _diskStructure[i]);
                        _disk.InsertRange(endCounter, Enumerable.Repeat(-1, _diskStructure[i]));
                        _disk.RemoveRange(endCounter + _diskStructure[i], _diskStructure[i]);
                         
                        var valI = _diskStructure[i];
                        var valJ = _diskStructure[j];

                        _diskStructure.Insert(j, 0);
                        _diskStructure[j + 1] = valI;
                        _diskStructure.Insert(j + 2, valJ - valI);
                        
                        i += 2;
                        break;
                    }
                }
            }
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day9).Name);
            _input[0] += '0';
            _disk = [];
            _diskStructure = [];
            for (var j = 0; j < _input[0].Length; j++)
            {
                _diskStructure = _diskStructure.Append((int)char.GetNumericValue(_input[0][j])).ToList();
                var counter = j % 2 == 0 ? j / 2 : -1;
                for (int i = 0; i < char.GetNumericValue(_input[0][j]); i++)
                {
                    _disk = [.. _disk, counter];
                }
            }
        }
    }
}
