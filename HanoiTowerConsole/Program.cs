using HanoiTowerSolver;

namespace HanoiTowerConsole;

internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Optinos:\n 1 - Solve\n 2 - Exit\nR:");
            string? answer = Console.ReadLine();
            answer = answer?.ToLower();

            switch (answer)
            {
                case "1":
                case "s":
                case "solve":
                    if (AskDisksCount(out uint diskCount))
                        Solve(diskCount);
                    break;
                case "2":
                case "e":
                case "exit":
                    return;
                default:
                    Console.WriteLine("Not an option.");
                    break;
            }
        }
    }

    private static bool AskDisksCount(out uint diskCount)
    {
        while (true)
        {
            Console.Write("Type how many disks you want or [R/r] to return: ");
            string? answer = Console.ReadLine();
            if (answer == null)
            {
                Console.WriteLine("You must enter a valid positive integer.");
                continue;
            }
            
            answer = answer.ToLower();
            if (answer == "r")
            {
                diskCount = 0;
                return false;
            }

            if (uint.TryParse(answer, out diskCount))
                return true;
            else
            {
                Console.WriteLine("You must enter a valid positive integer.");
                continue;
            }
        }
    }

    private static void Solve(uint diskCount)
    {
        Step[] solution = HanoiTower.Solve(diskCount);

        uint[] firstPole = new uint[diskCount];
        for (uint i = 0; i < firstPole.Length;)
            firstPole[i] = ++i;

        uint[][] board = new uint[HanoiTower.POLES_COUNT][];
        board[0] = firstPole;

        for (int i = 1; i < board.Length; i++)
            board[i] = new uint[diskCount];

        Console.WriteLine();
        DrawBoard(board);
        for (uint i = 0; i < solution.Length; i++)
        {
            Step step = solution[i];

            uint[] pole = board[step.Source];
            uint disk = 0;
            for (int j = 0; j < pole.Length; j++)
                if (pole[j] > 0)
                {
                    disk = pole[j];
                    pole[j] = 0;
                    break;
                }

            pole = board[step.Destination];
            for (int j = pole.Length - 1; j > -1; j--)
                if (pole[j] == 0)
                {
                    pole[j] = disk;
                    break;
                }

            Console.WriteLine($"\n{step.Source} => {step.Destination}\n");
            DrawBoard(board);
        }
        Console.WriteLine();
    }

    private static void DrawBoard(uint[][] board)
    {
        int count = board[0].Length;
        for (int k = 0; k < count; k++)
        {
            Console.Write("| ");
            for (uint i = 0; ;)
            {
                uint[] pole = board[i];
                if (pole[k] == 0)
                    Console.Write(" ");
                else
                    Console.Write(pole[k]);

                if (++i < board.Length)
                    Console.Write(" | ");
                else
                    break;
            }
            Console.WriteLine(" |");
        }
    }
}