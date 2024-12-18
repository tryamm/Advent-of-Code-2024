namespace Advent_of_Code_2024.Day17
{
    public static class Day17
    {
        private static string[] _input;
        private static Register A = new(0);
        private static Register B = new(0);
        private static Register C = new(0);
        private static int _pointer;
        private static List<int> _program;
        private static List<long> _output;

        private static Dictionary<int, Register> _combo = new Dictionary<int, Register>()
        {
            { 0, new Register(0) },
            { 1, new Register(1) },
            { 2, new Register(2) },
            { 3, new Register(3) },
            { 4, A },
            { 5, B },
            { 6, C },
        };

        public static long Part1() {
            Init();
            _pointer = 0;
            while (_pointer < _program.Count)
            {
                var command = _program[_pointer];
                var operand = _program[_pointer + 1];

                switch (command)
                {
                    case 0:
                        {
                            var co = _combo.GetValueOrDefault(operand);
                            A.Val = (long)(A.Val / (Math.Pow(2, co.Val)));
                            break;
                        }
                    case 1:
                        {
                            B.Val = B.Val ^ operand;
                            break;
                        }
                    case 2:
                        {
                            var co = _combo.GetValueOrDefault(operand);
                            B.Val = co.Val % 8;
                            break;
                        }
                    case 3:
                        {
                            if (A.Val != 0)
                            {
                                _pointer = operand;
                                continue;
                            }
                            break;
                        }
                    case 4:
                        {
                            B.Val = B.Val ^ C.Val;
                            break;
                        }
                    case 5:
                        {
                            var co = _combo.GetValueOrDefault(operand);
                            _output.Add(co.Val % 8);
                            break;
                        }
                    case 6:
                        {
                            var co = _combo.GetValueOrDefault(operand);
                            B.Val = (long)(A.Val / (Math.Pow(2, co.Val)));
                            break;
                        }
                    case 7:
                        {
                            var co = _combo.GetValueOrDefault(operand);
                            C.Val = (long)(A.Val / (Math.Pow(2, co.Val)));
                            break;
                        }
                }

                _pointer += 2;
            }

            var result = 0L;
            Console.WriteLine(string.Join(",", _output));
            return result;
        }

        public static long Part2() {
            var result = 0;
            return result;
        }

        private static void Init() {
            _output = [];
            _input = Utilities.GetInputData(typeof(Day17).Name);
            A.Val = int.Parse(_input[0].Split(':')[1]);
            B.Val = int.Parse(_input[1].Split(':')[1]);
            C.Val = int.Parse(_input[2].Split(':')[1]);

            _program = _input[4].Split(':')[1].Split(",").Select(x => int.Parse(x)).ToList();
        }
    }

    class Register(long val)
    {
        public long Val = val;
    }
}
