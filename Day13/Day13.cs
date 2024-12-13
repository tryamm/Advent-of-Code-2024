namespace Advent_of_Code_2024.Day13
{
    public static class Day13
    {
        private static string[] _input;
        private static List<Machine> _machines = [];

        public static long Part1() {
            Init();
            long result = 0;
            foreach (var machine in _machines)
            {
                (var a, var b) = GetSystemResult(machine);
                result += a * 3 + b;
            }

            return result;
        }

        public static long Part2() {
            Init();
            long result = 0;
            foreach (var machine in _machines)
            {
                machine.targetX += 10000000000000;
                machine.targetY += 10000000000000;
                (var a, var b) = GetSystemResult(machine);
                result += a * 3 + b;
            }

            return result;
        }

        private static (long, long) GetSystemResult(Machine machine) {
            var delta = ((machine.buttonB.y * machine.buttonA.x) - (machine.buttonB.x * machine.buttonA.y));
            var deltaA = (machine.buttonB.y * machine.targetX) - (machine.buttonB.x * machine.targetY);
            var deltaB = (machine.buttonA.x * machine.targetY) - (machine.buttonA.y * machine.targetX);
            if (delta != 0)
            {
                var a = deltaA / delta;
                var b = deltaB / delta;
                if ((a * machine.buttonA.y) + (b * machine.buttonB.y) == machine.targetY && (a * machine.buttonA.x) + (b * machine.buttonB.x) == machine.targetX)
                    return (a, b);
            }

            return (0, 0);
        }

        private static void Init() {
            _input = Utilities.GetInputData(typeof(Day13).Name);
            _machines = [];
            for (int i = 0; i < _input.Length; ++i)
            {
                var machine = new Machine();
                machine.buttonA = GetButton(_input[i++], machine, 3);
                machine.buttonB = GetButton(_input[i++], machine, 1);
                var targets = _input[i++].Split(':')[1].Split(',').Select(x => x.Split('=')[1]).Select(n => int.Parse(n)).ToArray();
                machine.targetX = targets[0];
                machine.targetY = targets[1];

                _machines.Add(machine);
            }
        }

        private static Button GetButton(string input, Machine machine, int price) {
            var buttons = input.Split(':')[1].Split(',').Select(x => x.Split('+')[1]).Select(n => int.Parse(n)).ToArray();
            return new Button(buttons[0], buttons[1], price);
        }

        // first part solution
        private static long CalculateTokensToWin(Button buttonA, Button buttonB, long targetX, long targetY) {
            long currentY = 0;
            long currentX = 0;
            long pushesA = 0;
            long pushesB = 0;
            if ((targetX / buttonB.x) > (targetY / buttonB.y))
            {
                pushesB = Math.Min((targetX / buttonB.x), 200);
                currentX = pushesB * buttonB.x;
                currentY = pushesB * buttonB.y;
            }
            else
            {
                pushesB = Math.Min((targetY / buttonB.y), 200);
                currentX = pushesB * buttonB.x;
                currentY = pushesB * buttonB.y;
            }


            while (pushesB > 0)
            {
                if (currentX == targetX && currentY == targetY)
                {
                    return pushesA * 3 + pushesB;
                }

                if (currentX >= targetX || currentY >= targetY)
                {
                    --pushesB;
                }
                else
                {
                    ++pushesA;
                }

                currentX = (pushesA * buttonA.x + pushesB * buttonB.x);
                currentY = (pushesA * buttonA.y + pushesB * buttonB.y);

                if (currentX > targetX || currentY > targetY) continue;
            }

            return 0;
        }
    }

    public class Machine()
    {
        public Button buttonA;
        public Button buttonB;

        public long targetX;
        public long targetY;

        public Machine(Button item1, Button item2, int targetX, int targetY) : this() {
            this.buttonA = item1;
            this.buttonB = item2;
            this.targetX = targetX;
            this.targetY = targetY;
        }
    }

    public class Button()
    {
        public int x;
        public int y;

        public Button(int x, int y, int price) : this() {
            this.x = x;
            this.y = y;
        }
    }
}
