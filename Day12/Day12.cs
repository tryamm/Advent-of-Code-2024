namespace Advent_of_Code_2024.Day12
{
    public static class Day12
    {
        private static string[] _input;
        private static bool[,] _gardenPath;
        private static Dictionary<char, List<Region>> _plants;

        private static int _maxX;
        private static int _maxY;
        private static int _minX;
        private static int _minY;

        static Day12() => Init();
        
        public static long Part1() {
            return _plants.SelectMany(x => x.Value.Select(y => y.points.Count * y.perimeter)).Sum();
        }

        public static long Part2() {
            return _plants.SelectMany(x => x.Value.Select(y => y.points.Count * y.corners)).Sum();
        }

        private static void IdentifyFigureSides(char plant, int x, int y, Region region) {
            if (_input[x][y] != plant) return;
            if (_gardenPath[x, y]) return;

            region.points.Add(new Point(x, y));
            CheckCorners(plant, x, y, region);
            _gardenPath[x, y] = true;

            CheckPoint(plant, x + 1, y, region);
            CheckPoint(plant, x, y + 1, region);
            CheckPoint(plant, x - 1, y, region);
            CheckPoint(plant, x, y - 1, region);

        }

        private static void CheckCorners(char plant, int x, int y, Region region) {
            if (_gardenPath[x, y]) return;
            //check regular corners
            if (IsValid(x + 1, y) && IsValid(x, y + 1) && _input[x + 1][y] != plant && _input[x][y + 1] != plant) ++region.corners;
            if (IsValid(x - 1, y) && IsValid(x, y + 1) && _input[x][y + 1] != plant && _input[x - 1][y] != plant) ++region.corners;
            if (IsValid(x - 1, y) && IsValid(x, y - 1) && _input[x - 1][y] != plant && _input[x][y - 1] != plant) ++region.corners;
            if (IsValid(x + 1, y) && IsValid(x, y - 1) && _input[x][y - 1] != plant && _input[x + 1][y] != plant) ++region.corners;

            if ((!IsValid(x + 1, y) && IsValid(x, y + 1) && _input[x][y + 1] != plant) || (!IsValid(x, y + 1) && IsValid(x + 1, y) && _input[x + 1][y] != plant)) ++region.corners;
            if ((!IsValid(x - 1, y) && IsValid(x, y + 1) && _input[x][y + 1] != plant) || (!IsValid(x, y + 1) && IsValid(x - 1, y) && _input[x - 1][y] != plant)) ++region.corners;
            if ((!IsValid(x - 1, y) && IsValid(x, y - 1) && _input[x][y - 1] != plant) || (!IsValid(x, y - 1) && IsValid(x - 1, y) && _input[x - 1][y] != plant)) ++region.corners;
            if ((!IsValid(x + 1, y) && IsValid(x, y - 1) && _input[x][y - 1] != plant) || (!IsValid(x, y - 1) && IsValid(x + 1, y) && _input[x + 1][y] != plant)) ++region.corners;

            //check irregular corners
            if (IsValid(x + 1, y) && IsValid(x, y + 1) && _input[x + 1][y] == plant && _input[x][y + 1] == plant && _input[x + 1][y + 1] != plant) ++region.corners;
            if (IsValid(x - 1, y) && IsValid(x, y + 1) && _input[x][y + 1] == plant && _input[x - 1][y] == plant && _input[x - 1][y + 1] != plant) ++region.corners;
            if (IsValid(x - 1, y) && IsValid(x, y - 1) && _input[x - 1][y] == plant && _input[x][y - 1] == plant && _input[x - 1][y - 1] != plant) ++region.corners;
            if (IsValid(x + 1, y) && IsValid(x, y - 1) && _input[x][y - 1] == plant && _input[x + 1][y] == plant && _input[x + 1][y - 1] != plant) ++region.corners;

            if ((!IsValid(x + 1, y) || !IsValid(x - 1, y)) && (!IsValid(x, y + 1) || !IsValid(x, y - 1)))
            {
                ++region.corners;
            }
        }

        private static void CheckPoint(char plant, int x, int y, Region region) {
            if (IsValid(x, y))
            {
                if (_input[x][y] != plant)
                {
                    ++region.perimeter;
                }
                else
                {
                    IdentifyFigureSides(plant, x, y, region);
                }
            }
            else ++region.perimeter;
        }

        public static bool IsValid(int x, int y) {
            return x >= _minX && x <= _maxX && y >= _minY && y <= _maxY;
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day12).Name);
            _maxX = _input[0].Length - 1;
            _maxY = _input.Length - 1;
            _minX = 0;
            _minY = 0;
            _gardenPath = new bool[_maxY + 1, _maxX + 1];
            _plants = [];

            InitializeField();
        }

        private static void InitializeField() {
            for (int i = 0; i < _input.Length; ++i)
            {
                for (int j = 0; j < _input[0].Length; ++j)
                {
                    if (_gardenPath[i, j]) continue;
                    if (!_plants.TryGetValue(_input[i][j], out var regions))
                    {
                        regions = new List<Region>();
                        _plants.Add(_input[i][j], regions);
                    }

                    var currentRegion = regions.FirstOrDefault(x => x.IsContainPoint(i, j));
                    if (currentRegion is null)
                    {
                        currentRegion = new Region();
                        regions.Add(currentRegion);
                    }

                    IdentifyFigureSides(_input[i][j], i, j, currentRegion);
                }
            }
        }
    }

    public class Region()
    {
        public List<Point> points = [];
        public int perimeter;
        public int corners;

        public bool IsContainPoint(int x, int y) {
            return points.Any(p => p.x == x && p.y == y);
        }
    }

    public class Point()
    {
        public int x;
        public int y;

        public Point(int x, int y) : this() {
            this.x = x;
            this.y = y;
        }
    }
}
