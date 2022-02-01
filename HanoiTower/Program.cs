//#define STEP_BY_STEP

namespace TowerOfHanoi
{
    internal class Program
    {
        const int DISK_COUNT_INT = (int)DISK_COUNT;
        const uint DISK_COUNT = 5;
        const uint ORIGIN = 0;
        const uint DESTINATION = 2;

        static List<uint>[] board = new List<uint>[] 
        {
            new List<uint>(DISK_COUNT_INT),
            new List<uint>(DISK_COUNT_INT),
            new List<uint>(DISK_COUNT_INT) 
        };

        static void Main(string[] args)
        {
            uint[][] solution = HanoiTower.Solve(DISK_COUNT, ORIGIN, DESTINATION);

            for (uint i = DISK_COUNT; i > 0; i--)
                board[0].Add(i);

            Draw();
            Console.ReadLine();

            for (uint i = 0;i<solution.Length;i++)
            {
                uint[] step = solution[i];

                List<uint> pole = board[step[0]];
                uint disk = pole[pole.Count - 1];
                pole.RemoveAt(pole.Count - 1);

                pole = board[step[1]];
                pole.Add(disk);

                Console.WriteLine($"{step[0]} => {step[1]}\n");
                Draw();

#if STEP_BY_STEP
                Console.ReadLine();
#endif
            }

            Console.WriteLine("Done!!!");
            Console.ReadLine();
        }

        static void Draw()
        {
            for (int j = DISK_COUNT_INT; j > 0; )
            {
                j--;
                for (uint i = 0; i < board.Length; i++)
                {
                    List<uint> pole = board[i];
                    if (j > -1 && j < pole.Count)
                        Console.Write(pole[j]);
                    else
                        Console.Write(" ");

                    if (i + 1 != board.Length)
                        Console.Write(" | ");
                }
                Console.WriteLine();
            }
        }
    }
}