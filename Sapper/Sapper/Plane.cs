namespace Sapper;

public class Plane
{
    private ConsoleColor[] _colors = { ConsoleColor.DarkBlue, ConsoleColor.Blue,ConsoleColor.Cyan,ConsoleColor.DarkCyan,ConsoleColor.DarkGreen,ConsoleColor.Yellow,ConsoleColor.Red,ConsoleColor.DarkRed,ConsoleColor.Magenta};
    private byte[,] _field;
    private bool[,] _cracked;
    private bool[,] _flags;
    public UInt16 BombNumber { get; private set; }
    public UInt16 Placedflags { get; private set; }

    public Plane(byte[,] field, UInt16 bombNumber)
    {
        this._field = field;
        this.BombNumber = bombNumber;
        Placedflags = 0;
        _cracked = new bool[this._field.GetLength(0),this._field.GetLength(0)];
        _flags = new bool[this._field.Length, this._field.Length];
        for (int i = 0; i < this._field.GetLength(0); i++)for (int j = 0; j < this._field.GetLength(0); j++)_cracked[i, j] = _flags[i, j] = false;
    }

    public bool Step(byte x, byte y)//true - nice, false - ur ded
    {
        if (_field[x,y] == 10) return false;
        _cracked[x,y] = true;
        if(_field[x,y]==0)
            for (Int16 i = -1; i <= 1; i++)
            {
                for (Int16 j = -1; j <= 1; j++)
                {
                    //Console.WriteLine(x+i + " " + y+);
                    if (x + i >= _field.GetLength(0) || y + j >= _field.GetLength(0) || x + i < 0 || y + j < 0 || _cracked[x+i,y+j]) continue;
                    Step((Byte)(x+i),(Byte)(y+j));
                }
            }
        if (_flags[x, y]) Placedflags--;
        _flags[x, y] = false;
        return true;
    }

    public bool PlaceFlag(byte x, byte y)
    {
        if (Placedflags == BombNumber) return false;
        if (!_flags[x, y]) Placedflags++;
        return _flags[x, y] = true;
    }

    public Boolean CheckEnd()
    {
        bool result = true;
        for(int i=0; i<_field.GetLength(0);i++)
        for (int j = 0; j < _field.GetLength(0); j++)
            result &= _cracked[i, j] || _flags[i, j];
        return result;
    }

    public void Print()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(0); j++)
            {
                if (_flags[i, j])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("F");
                }
                else if (_cracked[i, j])
                {
                    Console.ForegroundColor = _colors[_field[i, j]];
                    Console.Write(_field[i,j]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("#");
                }
            }
            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.White;
    }

    public void PrintDeath()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(0); j++)
            {
                if (_field[i, j] == 10)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("¤");
                }
                else
                {
                    Console.ForegroundColor = _colors[_field[i, j]];
                    Console.Write(_field[i,j]);
                }
            }
            Console.WriteLine();
        }
    }
}