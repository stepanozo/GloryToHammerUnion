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

public class BattleCardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public unit u;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public bool moves = false;
    public float timer = 3.0f;
    public float lerp = 0;
    public float v;
    public float countTime;

    public static unit activeBattleUnit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    double distanceToDestination()
    {
        return Math.Sqrt(Math.Pow(transform.position.x - endPosition.x, 2) + Math.Pow(transform.position.y - endPosition.y, 2));
    }

    double fullDistance()
    {
        return Math.Sqrt(Math.Pow(startPosition.x - endPosition.x, 2) + Math.Pow(startPosition.y - endPosition.y, 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (moves)
        {
            countTime += Time.deltaTime;
            if(lerp <0.5f)
                lerp = 0.4f * countTime*countTime;
        }

      

        if (lerp/ timer >= 1) //когда надо окончить движение
           moves = false;

        if(moves)
        {
            //  Debug.Log("it moves");
           // Debug.Log("Эндовая позишн " + endPosition);
            this.transform.localPosition = Vector2.Lerp(startPosition, endPosition, lerp/timer);
        }
        
    }

    public void MoveUnit()
    {
       // startPosition = this.transform.position;
       // endPosition = (Vector2)transform.position + new Vector2(100, 100);
       // moves = true;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (!GameMainScript.BaseOfUnitsSC.gonnaAttack)
        {
            //Настраиваем иллюстрацию
            //GameMainScript.BaseOfUnitsSC.CardOnPanel
            GameObject tempUnitObject = GameMainScript.BaseOfUnitsSC.CardOnPanel;
            GameObject tempChildObject = tempUnitObject.transform.Find("Иллюстрация").gameObject;
            tempChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(u.spritePath);

            //Настраиваем здоровье
            tempUnitObject.transform.Find("Здоровье").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.HP);
            tempUnitObject.transform.Find("Урон").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.damage);
            tempUnitObject.transform.Find("Урон по технике").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.techDamage);
            tempUnitObject.transform.Find("Кол-во").GetComponent<Text>().text = Convert.ToString(u.quantity);

            GameMainScript.BaseOfUnitsSC.BigUnitName.text = u.name;
            GameMainScript.BaseOfUnitsSC.BigUnitDescription.text = u.description;

            GameMainScript.BaseOfUnitsSC.buttonAbility.SetActive(true);
            GameMainScript.BaseOfUnitsSC.buttonAttack.SetActive(true);
            GameMainScript.BaseOfUnitsSC.buttonRetreat.SetActive(true);
            GameMainScript.BaseOfUnitsSC.buttonPlayIt.SetActive(false);

            activeBattleUnit = u;
        }
        else
        {
            Debug.Log("Атакуем его");
            int damage = u.isTech ? activeBattleUnit.techDamage : activeBattleUnit.damage;
            damage *= activeBattleUnit.quantity;
            Debug.Log("Умножаем дамаг юнита на " + activeBattleUnit.quantity);
            if(damage >= u.maxHP * (u.quantity-1) + u.HP)
            {
                Debug.Log("Уничтожили его");


                //ЗДЕСЬ НАЧИНАЕТСЯ УНИЧТОЖЕНИЕ ПРОТИВНИКА
                Animator cardAnimator;
                u.quantity = 0;
                for(int i =0; i< 4; i++)
                {
                    if (GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightEnemyUnits[i].quantity <= 0) //ищем этого юнита, у которого кончилось кол-во
                    {
                        Debug.Log("Убрали противника"); ;
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
               

                //ЗДЕСЬ ЗАКАНЧИВАЕТСЯ УНИЧТОЖЕНИЕ
            }
            else 
            {
                Debug.Log("Наносим ему урон");
                while (damage > u.maxHP && u.quantity > 1)
                {
                    u.quantity--;
                    damage -= u.maxHP;
                }
                if(damage >= u.HP)
                {
                    u.quantity--;
                    damage -= u.HP;
                    u.HP = u.maxHP - damage;
                }
                else
                    u.HP -= damage;

                //if(u.HP<0)
                //if(u.HP <0)

                GameMainScript.BaseOfUnitsSC.RefreshAllQuantities(); //тут обновляем всё здоровье и кол-во всех юнитов
                

            }
            GameMainScript.BaseOfUnitsSC.gonnaAttack = false;
        }
    }


    public void DestroyThis()
    {
        Debug.Log("Надо уничтожить это");
        Destroy(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

}
