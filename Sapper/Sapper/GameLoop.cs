using System.Diagnostics;

namespace Sapper;

public class GameLoop
{
    private static Byte[] ScanStepPosition()
    {
        Byte[] stepPosition = new Byte[2];
        Console.Write("Enter row for move");
        stepPosition[0] = Byte.Parse(Console.ReadLine() ?? "0");
        Console.Write("Enter column for move");
        stepPosition[1] = Byte.Parse(Console.ReadLine() ?? "0");
        return stepPosition;
    }

    private static Boolean Move(Plane plane)
    {
        Console.WriteLine("0 - make move\n1 - place flag");
        String bufer = Console.ReadLine();
        Byte[] stepPosition = ScanStepPosition();
        return bufer switch
        {
            "0" => plane.Step(stepPosition[0], stepPosition[1]),
            "1" => plane.PlaceFlag(stepPosition[0], stepPosition[1]),
            _ => throw new Exception()
        };
    }
    
    public static Boolean Loop(Plane plane)
    {
        PrintGameInfo(plane);
        if (!Move(plane))
        {
            plane.PrintDeath();
            return false;
        }

        if (!plane.CheckEnd()) return true;
        Console.WriteLine("Congratulation");
        return false;

    }

    private static void PrintGameInfo(Plane plane)
    {
        Console.WriteLine("Bomb number: {0}  Flag left: {1}", plane.BombNumber, plane.BombNumber - plane.Placedflags);
    }
}