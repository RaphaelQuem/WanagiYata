using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class StaticResources
{
    public static int MapColumn { get; set; }
    public static int MapRow { get; set; }
    public static int CurrentDay { get; set; }
    public static float TopCameraLimit
    {
        get
        {
            return UpperLimits[MapRow - 1];
        }
    }
    public static float BotCameraLimit
    {
        get
        {
            return LowerLimits[MapRow - 1];
        }
    }
    public static float LeftCameraLimit
    {
        get
        {
            return LeftLimits[MapColumn - 1];
        }
    }
    public static float RightCameraLimit
    {
        get
        {
            return RightLimits[MapColumn - 1];
        }
    }
    public static string[] SpawnPool
    {
        get
        {
            return DailySpawns[CurrentDay];
        }
    }
    private static float[] UpperLimits = new float[] { 23.8f, 12.8f, 1.77f, -9.21f, -20.23f };
    private static float[] LowerLimits = new float[] { 20.24f, 9.15f, -1.8f, -12.82f, -23.77f };
    private static float[] LeftLimits = new float[] { -60.86f, -33.16f, -5.8f, 21.43f, 49.19f };
    private static float[] RightLimits = new float[] { -49.1f, -21.5f, 5.8f, 33.1f, 60.78f };
    private static Dictionary<int, string[]> DailySpawns = new Dictionary<int, string[]>
    {
          { 1, new string[] { "Animal","Animal", "Animal", "Animal", "Animal" } },
          { 2, new string[] { "Adam","Gred" } },
          { 3, new string[] { "Adam","Gred" } },
    };

}
