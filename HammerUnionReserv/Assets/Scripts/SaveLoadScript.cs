using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Assembly_CSharp;
using System;
using Assets.Scripts;


/*internal static class SaveLoadScript
{
    public static Map MapScript;
    public static List<GameData> savedGames = new List<GameData>();

    public static void Save()
    {



        savedGames.Add(GameData.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SaveLoadScript.savedGames);
        file.Close();
    }
}
*/
[Serializable] internal  class GameData
{
    //public static GameData current;
    public int today;

    public Dictionary<int, int> IDOfCaseAndAnswer = new Dictionary<int, int>();
    public Dictionary<string, mapArea> MapAreaDict = new Dictionary<string, mapArea>(); //Словарь всех районов
    public Dictionary<string, townArea> TownAreaDict = new Dictionary<string, townArea>(); //Словарь обычных районов, у которых можно менять настроение и давать ресурсы.
    public Dictionary<string, emptyArea> EmptyAreaDict = new Dictionary<string, emptyArea>(); //Словарь пустых районов
    public Dictionary<string, village> VillageDict = new Dictionary<string, village>(); //Словарь всех деревень
    public Dictionary<buildings, building> buildingDict = new Dictionary<buildings, building>();
    public Dictionary<int, Case> AllCases = new Dictionary<int, Case>();
    public List<unit> RezervUnits = new List<unit>();

    public int[] Recourses;
    public int soldiers;
    public int[] rep;
    public string name;

    public GameData(string name)
    {
        this.name = name;
        today = Map.GameSC.today;

        IDOfCaseAndAnswer = Map.GameSC.IDOfCaseAndAnswer;
        MapAreaDict = GameMainScript.MapSC.MapAreaDict;
        TownAreaDict = GameMainScript.MapSC.TownAreaDict;
        EmptyAreaDict = GameMainScript.MapSC.EmptyAreaDict;
        VillageDict = GameMainScript.MapSC.VillageDict;
        buildingDict = GameMainScript.MapSC.buildingDict;
        AllCases = Map.GameSC.AllCases;
        RezervUnits = GameMainScript.BaseOfUnitsSC.RezervUnits;

        Recourses = recoursesOfPlayer.Recourses;
        soldiers = recoursesOfPlayer.soldiers;
        rep = reputation.rep;

    }
}
