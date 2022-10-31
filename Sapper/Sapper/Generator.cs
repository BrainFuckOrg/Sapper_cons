namespace Sapper;
using System;

public class Generator
{
    private const Single Percent = 0.2f;
    private static Byte[,] _plane;
    private const Byte BombNumber_InTable = 10;
    private static UInt16 BombNumber(Int32 size)
    {
        return (UInt16)(size * size * Percent);
    }
    public static Plane GeneratePlane(Byte size)
    {
        _plane = new Byte[size, size];
        SetBombs();
        return new Plane(_plane,BombNumber(_plane.Length));
    }

    private static void SetBombs()
    {
        Random random = new Random();
        for (UInt16 i = 0; i < BombNumber(_plane.Length); i++)
        {
            Byte rowNum;
            Byte colNum;
            do
            {
                rowNum = (Byte)random.Next(_plane.Length);
                colNum = (Byte)random.Next(_plane.Length);
            } while (CanSetBombTo(rowNum, colNum));

            SetBombTo(rowNum, colNum);
        }
    }

    private static void SetBombTo(Byte rowNum, Byte colNum)
    {
        _plane[rowNum, colNum] = BombNumber_InTable;
        for (Int16 i = -1; i <= 1; i++)
        {
            for (Int16 j = -1; j <= 1; j++)
            {
                if (_plane[rowNum+i,colNum+j]==BombNumber_InTable) continue;
                _plane[rowNum + i, colNum + j]++;
            }
        }
    }

    private static Boolean CanSetBombTo(Byte rowNum, Byte colNum)
    {
        if (rowNum >= _plane.Length || colNum >= _plane.Length)
            return false;
        if (_plane[rowNum, colNum] == BombNumber_InTable)
            return false;
        for (Int16 i = -1; i <= 1; i++)
        {
            for (Int16 j = -1; j <= 1; j++)
            {
                if (rowNum + i >= _plane.Length || colNum + j >= _plane.Length) continue;
                if (_plane[rowNum + i, colNum + j]!=BombNumber_InTable) return true;
            }
        }

        return false;
    }
}