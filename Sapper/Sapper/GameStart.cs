namespace Sapper;

public class GameStart
{
    public static void StartSapper()
    {
        Console.Write("Enter the plane size: ");
        Plane plane = Generator.GeneratePlane(Byte.Parse(Console.ReadLine() ?? "0"));
        while (GameLoop.Loop(plane))
        {
            plane.Print();
        }
    }
}