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

    // Start is called before the first frame update
    void Start()
    {

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
            //«ƒ≈—№ Ќј„»Ќј≈“—я ”Ќ»„“ќ∆≈Ќ»≈ ѕ–ќ“»¬Ќ» ј

            Animator cardAnimator;
            aimUnit.quantity = 0;
            for (int i = 0; i < 4; i++)
            {
                if (GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightEnemyUnits[i].quantity <= 0) //ищем этого юнита, у которого кончилось кол-во
                {
                    Debug.Log("”брали противника"); ;
                    cardAnimator = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[i].GetComponent<Animator>();
                    cardAnimator.SetBool("GoesToTop", true);
                    GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightEnemyUnits.RemoveAt(i); //это раньше было внизу, после цикла

                    int j = i;
                    while (j + 1 < 4)
                    {
                        GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[j] = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[j + 1];
                        j++;
                    }

                    GameMainScript.BaseOfUnitsSC.moveUnitsLeft(GameMainScript.MapSC.activeVillageTag,
              GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightAllyUnits.Count,
              GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightEnemyUnits.Count);

                    //GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[j + 1] = null;

                    Debug.Log(GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightEnemyUnits.Count + " - именно столько врагов осталось в этой деревне");
                    //GameMainScript.BaseOfUnitsSC.moveUnitsLeft()

                    //this.DestroyThis();
                    break;
                }
            }
        }
    }
}