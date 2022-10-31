namespace Sapper;

public class Plane
{
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
        cracked = new bool[field.Length, field.Length];
        flags = new bool[field.Length, field.Length];
        for (int i = 0; i < field.Length; i++)for (int j = 0; j < field.Length; j++)cracked[i, j] = flags[i, j] = false;
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

    //public Boolean CheckEnd()
    //{
    //    for(int )
    //}
}