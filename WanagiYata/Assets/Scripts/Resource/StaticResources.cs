using Assets.Scripts.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class StaticResources
{
    public static int MapColumn { get; set; }
    public static int MapRow { get; set; }
    public static float CurrentTime { get; set; }
    public static int CurrentDay { get; set; }
    public static DayTime DayTime {get;set;}
    public static string TranslationFolder { get { return string.Concat(Application.dataPath, "/Translations/"); } }
    public static string DialogueFolder { get { return string.Concat(Application.dataPath, "/Translations/Dialogues/", Language, "/"); } }
    public static string Language = "PtBr";
    public static void Initialize()
    {
        Language = "PtBr";
        if (MapColumn.Equals(0))
            MapColumn = 3;
        if (MapRow.Equals(0))
            MapRow = 3;
    }
    public static float TopCameraLimit
    {
        get
        {
            Initialize();
            return UpperLimits[MapRow - 1];
        }
    }
    public static float BotCameraLimit
    {
        get
        {
            Initialize();
            return LowerLimits[MapRow - 1];
        }
    }
    public static float LeftCameraLimit
    {
        get
        {
            Initialize();
            return LeftLimits[MapColumn - 1];
        }
    }
    public static float RightCameraLimit
    {
        get
        {
            Initialize();
            return RightLimits[MapColumn - 1];
        }
    }
    public static string[] SpawnPool
    {
        get
        {
            Initialize();
            return DailySpawns[CurrentDay];
        }
    }
    private static float[] UpperLimits = new float[] { 23.8f, 12.8f, 1.77f, -9.21f, -20.23f };
    private static float[] LowerLimits = new float[] { 20.24f, 9.15f, -1.8f, -12.82f, -23.77f };
    private static float[] LeftLimits = new float[] { -60.86f, -33.16f, -5.8f, 21.43f, 49.19f };
    private static float[] RightLimits = new float[] { -49.1f, -21.5f, 5.8f, 33.1f, 60.78f };
    private static Dictionary<int, string[]> DailySpawns = new Dictionary<int, string[]>
    {
          { 1, new string[] { "Animal","Animal", "Animal", "Animal", "Animal", "Enemy", "Enemy", "Enemy", "Enemy", "Enemy", "Enemy", "Enemy", "Enemy", "Enemy", "Enemy" } },
          { 2, new string[] { "Adam","Gred" } },
          { 3, new string[] { "Adam","Gred" } },
    };
    
}
