namespace Sapper;

public class Plane
{
    private ConsoleColor[] colors = { ConsoleColor.DarkBlue, ConsoleColor.Blue,ConsoleColor.Cyan,ConsoleColor.DarkCyan,ConsoleColor.DarkGreen,ConsoleColor.Yellow,ConsoleColor.Red,ConsoleColor.DarkRed,ConsoleColor.Magenta};
    private byte[,] field;
    private bool[,] cracked;
    private bool[,] flags;
    public UInt16 bombNumber { get; private set; }
    public UInt16 placedflags { get; private set; }

    public Plane(byte[,] Field, UInt16 BombNumber)
    {
        field = Field;
        bombNumber = BombNumber;
        placedflags = 0;
        cracked = new bool[field.GetLength(0),field.GetLength(0)];
        flags = new bool[field.Length, field.Length];
        for (int i = 0; i < field.GetLength(0); i++)for (int j = 0; j < field.GetLength(0); j++)cracked[i, j] = flags[i, j] = false;
    }

    public bool Step(byte x, byte y)//true - nice, false - ur ded
    {
        if (field[x,y] == 10) return false;
        cracked[x,y] = true;
        if (flags[x, y]) placedflags--;
        flags[x, y] = false;
        return true;
    }

    public bool PlaceFlag(byte x, byte y)
    {
        if (placedflags == bombNumber) return false;
        if (!flags[x, y]) placedflags++;
        return flags[x, y] = true;
    }

    public Boolean CheckEnd()
    {
        bool result = true;
        foreach (bool b in cracked)
        foreach (bool b2 in flags)
            result &= b || b2;
        return result;
    }

    public void Print()
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(0); j++)
            {
                if (flags[i, j])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("F");
                }
                else if (cracked[i, j])
                {
                    Console.ForegroundColor = colors[field[i, j]];
                    Console.Write(field[i,j]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("#");
                }
            }
            Console.WriteLine();
        }
    }

    public void PrintDeath()
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(0); j++)
            {
                if (field[i, j] == 10)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("¤");
                }
                else
                {
                    Console.ForegroundColor = colors[field[i, j]];
                    Console.Write(field[i,j]);
                }
            }
            Console.WriteLine();
        }
    }
}