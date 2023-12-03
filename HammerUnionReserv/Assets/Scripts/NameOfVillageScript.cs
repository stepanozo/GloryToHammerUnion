using Assembly_CSharp; //Это подключили, чтобы Area.cs видеть.
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Добавляем чтобы юзать интерфейс и тексты всякие
using UnityEngine.Video;
using UnityEngine.SceneManagement; //Подключаем обязательно, чтобы иметь возможность сцены переключать.
using System;
using System.Threading;
using Unity.VisualScripting;



public class NameOfVillageScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNextBattlePoint()
    {
        if (GameMainScript.BaseOfUnitsSC.enemyTurn)
        {

            foreach (GameObject g in GameMainScript.BaseOfUnitsSC.BattleUnitObjectsAlly)
            {
                if (g != null)
                    g.GetComponent<Animator>().SetBool("GoesToBot", true);
            }
            foreach (GameObject g in GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy)
            {
                if (g != null)
                    g.GetComponent<Animator>().SetBool("GoesToTop", true);
            }
           


            bool noBattlePoints = true;
            foreach (string key in GameMainScript.MapSC.battlePointsDict.Keys)
            {
                if (GameMainScript.MapSC.battlePointsDict[key].fightEnemyUnits.Count > 0 && GameMainScript.MapSC.battlePointsDict[key].fightAllyUnits.Count > 0 &&
                    !GameMainScript.MapSC.battlePointsDict[key].AllEnemiesWent())
                {
                    noBattlePoints = false;
                    GameMainScript.MapSC.activeBattlePointTag = key;
                    GameMainScript.BaseOfUnitsSC.ToTheBattle();
                    break;
                }
            
                  
            }
            if (noBattlePoints)
            {
                Debug.Log("пПРОДОЛЖИЛИ СЛЕДУЮЩИЙ ДЕНЬ");
                //GameMainScript.BaseOfUnitsSC.exitBattle();
                GameMainScript.BaseOfUnitsSC.BattlePanelAnimator.SetBool("PanelOpened", false);
                GameMainScript.BaseOfUnitsSC.ButtleReservAnimator.SetBool("RezervOpened", false);
                GameMainScript.BaseOfUnitsSC.ButtonsAnimator.SetBool("ButtonsOpened", true);
                GameMainScript.BaseOfUnitsSC.InfoAnimator.SetBool("InfoOpened", true);
                Map.GameSC.PlayerState = GameMainScript.states.WatchingCases;
                Map.GameSC.ContinueNextDay();
            }
        }

    }
        
    

    public void nextEnemyAttacks()
    {

        if (GameMainScript.BaseOfUnitsSC.enemyTurn)
        {
            //Короче надо найти целевого юнита среди союзников. Можно искать с самым минимальным числом хп. Для него вызываем attackThisUnit, делая каждый раз нового активного юнита, который
            //и будет атаковать. Если юнит умер, то меняем целевого юнита, если юниты ещё вообще остались.


            //Поиск целевого юнита
            int minHP = 99999999;
            int numberOfMin = 0;
            string key = GameMainScript.MapSC.activeBattlePointTag;

            for (int i = 0; i < GameMainScript.MapSC.battlePointsDict[key].fightAllyUnits.Count; i++)
            {
                unit u = GameMainScript.MapSC.battlePointsDict[key].fightAllyUnits[i];
                if (u.maxHP * (u.quantity - 1) + u.HP < minHP)
                {
                    numberOfMin = i;
                }
            }
            BattleCardBehaviour aimUnitScript = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsAlly[numberOfMin].GetComponent<BattleCardBehaviour>(); //типа нашли целевого юнита

            //foreach (unit u in GameMainScript.MapSC.battlePointsDict[key].fightEnemyUnits)
            for(int i = 0; i< GameMainScript.MapSC.battlePointsDict[key].fightEnemyUnits.Count; i++)
            {
                unit u = GameMainScript.MapSC.battlePointsDict[key].fightEnemyUnits[i];
                //if(u!=null)
                if (u.isActive)
                {
                   
                        BattleCardBehaviour.activeBattleUnit = u;
                        BattleCardBehaviour.activeBattleUnitTransform = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[i].transform;

                        aimUnitScript.attackUnit();
              

                    break;
                }

            }



            /*
            //foreach(unit u in GameMainScript.MapSC.battlePointsDict[key].fightEnemyUnits)
            foreach (GameObject card in GameMainScript.BaseOfUnits.BattleUnitObjectsEnemy)
            {
                unitScript = card.GetComponent<BattleCardBehaviour>();
                if (GameMainScript.MapSC.battlePointsDict[key].fightAllyUnits.Count > 0 && unitScript.isActive)
                {
                    unitScript.attackUnit();
                }
            }
            */
        }
    }
}
