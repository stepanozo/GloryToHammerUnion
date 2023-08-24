using Assembly_CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

internal static class SLscript
{
    //internal static Map MapScript;
    public static List<GameData> savedGames = new List<GameData>();
    public static GameData currentGameData;
    public static int NumberOfLoadedGame;
    public static bool isNewGame = true;

    public static void Save()
    {
        //Берём все данные
        //GameData.current.today = Map.GameSC.today;

        currentGameData= new GameData("Текущая игра");

        Debug.Log("В файл будем класть дел: " + currentGameData.AllCases.Count);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/currentGame.gd");
        bf.Serialize(file,  currentGameData);
        file.Close();
    }

    public static void Load(int NumberOfGame)
    {



        /* BinaryFormatter bf = new BinaryFormatter();
         FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
         SLscript.savedGames = (List<GameData>)bf.Deserialize(file);
         file.Close();
        */

        //ДАЛЬШЕ БУДЕМ ЭКСПЕРЕМЕНТИРОВАТЬ НАД КЕККИЧЕМ

        // Debug.Log("В этой сохранённой игре у нас день " + Convert.ToString(savedGames[NumberOfGame].today)) ;


   
            NumberOfLoadedGame = NumberOfGame; //Нашей глобальной переменной (на которую смотреть будем в игре) присваиваем значение переданного параметра
            SLscript.isNewGame = false;
            SceneManager.LoadScene("Game");
        

        

        
        
    }

}
