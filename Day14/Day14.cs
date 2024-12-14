using System.Reflection;

namespace Advent_of_Code_2024.Day14
{
    public static class Day14
    {
        private static string[] _input;
        private static List<Robot> _robots = [];
        private static int _width = 101;
        private static int _height = 103;
        private static int[,] _hall = new int[_width, _height];

        static Day14() => Init();

        public static long Part1() {
            var seconds = 100;
            long q1 = 0;
            long q2 = 0;
            long q3 = 0;
            long q4 = 0;
            var borderX = _width / 2;
            var borderY = _height / 2;

            Move(seconds);
            CalculateQuadrants(ref q1, ref q2, ref q3, ref q4, borderX, borderY);
            return q1 * q2 * q3 * q4;
        }

        public static long Part2() {
            var seconds = 0;
            var borderX = _width / 2;
            var borderY = _height / 2;
            var dangerous = new Dictionary<long, long>();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, @$"{typeof(Day14).Name}\FindingATree.txt");
            using (StreamWriter writetext = new(path))
            {
                while (seconds < 10000)
                {
                    ++seconds;
                    Move();

                    for (int j = 0; j < _height; j++)
                    {
                        for (int i = 0; i < _width; i++)
                        {
                            if (_hall[i, j] > 0)
                                writetext.Write(_hall[i, j]);
                            else
                                writetext.Write('.');
                        }
                        writetext.WriteLine();
                    }
                    writetext.WriteLine(seconds);
                }
            }

            return seconds;
        }

        private static void CalculateQuadrants(ref long q1, ref long q2, ref long q3, ref long q4, int borderX, int borderY) {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_hall[i, j] > 0)
                    {
                        if (i < borderX && j < borderY) q1 += _hall[i, j];
                        if (i > borderX && j < borderY) q2 += _hall[i, j];
                        if (i < borderX && j > borderY) q3 += _hall[i, j];
                        if (i > borderX && j > borderY) q4 += _hall[i, j];
                    }
                }
            }
        }

        private static void Move(int seconds) {
            foreach (var robot in _robots)
            {
                var newX = ((robot.stepX * seconds) + robot.x) % _width;
                var newY = ((robot.stepY * seconds) + robot.y) % _height;
                newX = newX < 0 ? _width + newX : newX;
                newY = newY < 0 ? _height + newY : newY;
                if (_hall[newX, newY] < 9) _hall[newX, newY] += 1;
            }
        }

        private static void Move() {
            foreach (var robot in _robots)
            {
                if (_hall[robot.x, robot.y] > 0) _hall[robot.x, robot.y] -= 1;
                var newX = (robot.stepX + robot.x) % _width;
                var newY = (robot.stepY + robot.y) % _height;
                newX = newX < 0 ? _width + newX : newX;
                newY = newY < 0 ? _height + newY : newY;
                robot.x = newX;
                robot.y = newY;
                _hall[newX, newY] += 1;
            }
        }



        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day14).Name);
            foreach (var line in _input)
            {
                var startCoordination = line.Split(' ')[0].TrimStart('p', '=').Split(',').Select(x => int.Parse(x)).ToArray();
                var speed = line.Split(' ')[1].TrimStart('v', '=').Split(',').Select(x => int.Parse(x)).ToArray();
                var robot = new Robot(startCoordination[0], startCoordination[1], speed[0], speed[1]);
                _robots.Add(robot);
            }
        }
    }

    public class Robot()
    {
        public int x;
        public int y;

        public int stepX;
        public int stepY;

        public Robot(int x, int y, int stepX, int stepY) : this() {
            this.x = x;
            this.y = y;
            this.stepX = stepX;
            this.stepY = stepY;
        }
    }
}
