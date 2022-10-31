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
        Byte[] stepPosition = ScanStepPosition();
        return Console.ReadLine() switch
        {
            "0" => plane.Step(stepPosition[0], stepPosition[1]),
            "1" => plane.PlaceFlag(stepPosition[0], stepPosition[1]),
            _ => throw new Exception()
        };
    }
    
    public static Boolean Loop(Plane plane)
    {
        if (!Move(plane))
        {
            plane.PrintDeath();
            return false;
        }

        if (!plane.CheckEnd()) return true;
        Console.WriteLine("Congratulation");
        return false;

    }
}