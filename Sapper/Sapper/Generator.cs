namespace Sapper;
using System;

public class Generator
{
    private const Single Percent = 0.2f;
    private static Byte[,] _plane = null!;
    private const Byte BombNumberInTable = 10;

    private static Int32 ArrLength => _plane.GetLength(0);

    private static UInt16 BombNumber => (UInt16)(ArrLength * ArrLength * Percent);
    
    public static Plane GeneratePlane(Byte size)
    {
        _plane = new Byte[size, size];
        SetBombs();
        return new Plane(_plane,BombNumber);
    }

    private static void SetBombs()
    {
        Random random = new Random();
        for (UInt16 i = 0; i < BombNumber; i++)
        {
            Console.WriteLine(i);
            Byte rowNum;
            Byte colNum;
            do
            {
                rowNum = (Byte)random.Next(ArrLength);
                colNum = (Byte)random.Next(ArrLength);
            } while (!CanSetBombTo(rowNum, colNum));

            SetBombTo(rowNum, colNum);
        }
    }

    private static void SetBombTo(Byte rowNum, Byte colNum)
    {
        _plane[rowNum, colNum] = BombNumberInTable;
        for (Int16 i = -1; i <= 1; i++)
        {
            for (Int16 j = -1; j <= 1; j++)
            {
                if ( rowNum + i >= ArrLength || colNum + j >= ArrLength || rowNum + i < 0 || colNum + j < 0 || _plane[rowNum+i,colNum+j]==BombNumberInTable) continue;
                _plane[rowNum + i, colNum + j]++;
            }
        }
    }

    private static Boolean CanSetBombTo(Byte rowNum, Byte colNum)
    {
        if (rowNum >= ArrLength || colNum >= ArrLength)
            return false;
        if (_plane[rowNum, colNum] == BombNumberInTable)
            return false;
        for (Int16 i = -1; i <= 1; i++)
        {
            for (Int16 j = -1; j <= 1; j++)
            {
                if (rowNum + i >= ArrLength || colNum + j >= ArrLength || rowNum + i < 0 || colNum + j < 0) continue;
                if (_plane[rowNum + i, colNum + j]!=BombNumberInTable) return true;
            }
        }

        return false;
    }
}