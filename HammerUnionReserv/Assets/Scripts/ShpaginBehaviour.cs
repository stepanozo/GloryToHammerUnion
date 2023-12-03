using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assembly_CSharp;
using UnityEngine.UI;
//using TMPro.EditorUtilities;
using System;
using UnityEngine.EventSystems;
using System.Runtime;
using System.Linq;

public class ShpaginBehaviour : MonoBehaviour
{
    public GameObject aim;
    public unit aimUnit;
    public bool gonnaDestroy;
    NameOfVillageScript nameOfPoint;

    // Start is called before the first frame update
    void Start()
    {
        nameOfPoint = GameMainScript.BaseOfUnitsSC.nameOfBattlePoint.GetComponent<NameOfVillageScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aim != null)
        {
            if (this.transform.position.x < aim.transform.position.x)
                this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(aim.transform.position.y - transform.position.y, aim.transform.position.x - transform.position.x) * Mathf.Rad2Deg);//- 90);
            else
                this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(aim.transform.position.y - transform.position.y, aim.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 180);//- 90);
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void DestroyEnemy()
    {
        if (gonnaDestroy)
        {
            //ЗДЕСЬ НАЧИНАЕТСЯ УНИЧТОЖЕНИЕ ПРОТИВНИКА

            Animator cardAnimator;
            aimUnit.quantity = 0;

            if(aimUnit.isEnemy)
            for (int i = 0; i < 4; i++)
            {
                if (GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightEnemyUnits[i].quantity <= 0) //ищем этого юнита, у которого кончилось кол-во
                {
                    Debug.Log("Убрали противника"); ;
                    cardAnimator = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[i].GetComponent<Animator>();
                    cardAnimator.SetBool("GoesToTop", true);
                    GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightEnemyUnits.RemoveAt(i); //это раньше было внизу, после цикла

                    int j = i;
                    while (j + 1 < 4)
                    {
                        GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[j] = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[j + 1];
                        j++;
                    }

                    GameMainScript.BaseOfUnitsSC.moveUnitsLeft(GameMainScript.MapSC.activeBattlePointTag,
              GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits.Count,
              GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightEnemyUnits.Count);

                    //GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[j + 1] = null;

                    Debug.Log(GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightEnemyUnits.Count + " - именно столько врагов осталось в этой деревне");
                    //GameMainScript.BaseOfUnitsSC.moveUnitsLeft()

                    //this.DestroyThis();
                    break;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits[i].quantity <= 0) //ищем этого юнита, у которого кончилось кол-во
                    {
                        Debug.Log("Убрали союзника"); ;
                        cardAnimator = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsAlly[i].GetComponent<Animator>();
                        cardAnimator.SetBool("GoesToBot", true);
                        GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits.RemoveAt(i); //это раньше было внизу, после цикла

                        int j = i;
                        while (j + 1 < 4)
                        {
                            GameMainScript.BaseOfUnitsSC.BattleUnitObjectsAlly[j] = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsAlly[j + 1];
                            j++;
                        }

                        GameMainScript.BaseOfUnitsSC.moveUnitsLeft(GameMainScript.MapSC.activeBattlePointTag,
                  GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits.Count,
                  GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightEnemyUnits.Count);

                        //GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[j + 1] = null;

                        Debug.Log(GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits.Count + " - именно столько союзников осталось в этой деревне");
                        //GameMainScript.BaseOfUnitsSC.moveUnitsLeft()

                        //this.DestroyThis();
                        break;
                    }
                }
            }
        }

        //Вот здесь проверим, если все враги неактивны, то нужно запустить исчезновение надписи. В этой анимации в конце вызвать смену деревни, т.е. рефреш для следующей деревни, в которой ещё не все походили.
        if (GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].AllEnemiesWent() || GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits.Count <1)
        {
            if (GameMainScript.BaseOfUnitsSC.enemyTurn)
            {
                GameMainScript.BaseOfUnitsSC.nameOfBattlePoint.GetComponent<Animator>().SetBool("Shown", false);
            }
        }
        else
        {   if (GameMainScript.BaseOfUnitsSC.enemyTurn)
            {
                nameOfPoint.nextEnemyAttacks();
            }
        }


    }
}