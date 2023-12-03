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
    public static Transform activeBattleUnitTransform;


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

      

        if (lerp/ timer >= 1) //����� ���� �������� ��������
           moves = false;

        if(moves)
        {
            //  Debug.Log("it moves");
           // Debug.Log("������� ������ " + endPosition);
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

        if (!GameMainScript.BaseOfUnitsSC.gonnaAttack) //���� �������� �����
        {
            //����������� �����������
            //GameMainScript.BaseOfUnitsSC.CardOnPanel
            GameObject tempUnitObject = GameMainScript.BaseOfUnitsSC.CardOnPanel;
            GameObject tempChildObject = tempUnitObject.transform.Find("�����������").gameObject;
            tempChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(u.spritePath);

            //����������� ��������
            tempUnitObject.transform.Find("��������").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.HP);
            tempUnitObject.transform.Find("����").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.damage);
            tempUnitObject.transform.Find("���� �� �������").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.techDamage);
            tempUnitObject.transform.Find("���-��").GetComponent<Text>().text = Convert.ToString(u.quantity);

            GameMainScript.BaseOfUnitsSC.BigUnitName.text = u.name;
            GameMainScript.BaseOfUnitsSC.BigUnitDescription.text = u.description;

            GameMainScript.BaseOfUnitsSC.buttonAbility.SetActive(true);
            if(u.isActive)
                GameMainScript.BaseOfUnitsSC.buttonAttack.SetActive(true);
            else
                GameMainScript.BaseOfUnitsSC.buttonAttack.SetActive(false);
            GameMainScript.BaseOfUnitsSC.buttonRetreat.SetActive(true);
            GameMainScript.BaseOfUnitsSC.buttonPlayIt.SetActive(false);

            activeBattleUnit = u;
            activeBattleUnitTransform = this.transform;
        }
        else //���� ������� �����
        {

            attackUnit();
            


            GameMainScript.BaseOfUnitsSC.gonnaAttack = false;


            //������ ����� ���������� � �������� ��� ������ ����� �� ���������� ���.
            activeBattleUnit.isActive = false;
            GameMainScript.BaseOfUnitsSC.buttonAttack.SetActive(false);

        }
    }

    public void attackUnit()
    {
        Debug.Log("������� ���");
        int damage = u.isTech ? activeBattleUnit.techDamage : activeBattleUnit.damage;
        damage *= activeBattleUnit.quantity;
        Debug.Log("�������� ����� ����� �� " + activeBattleUnit.quantity);

        Debug.Log("�������� ���� ������ " + activeBattleUnit.name);
        GameObject tempShpagin = Instantiate(GameMainScript.BaseOfUnitsSC.ShpaginPrefab, activeBattleUnitTransform.position, Quaternion.identity, GameObject.Find("����� ��������").transform);//������� �� �� �������� ������� � �������.
                                                                                                                                                                                               //tempShpagin.transform.rotation = Quaternion.Euler(tempShpagin.transform.rotation.eulerAngles.x, tempShpagin.transform.rotation.eulerAngles.y, Mathf.Atan2(this.transform.position.y - tempShpagin.transform.position.y, this.transform.position.x - tempShpagin.transform.position.x) * Mathf.Rad2Deg);//- 90);

        if (tempShpagin.transform.position.x >= this.gameObject.transform.position.x)
        {
            tempShpagin.gameObject.transform.localScale = new Vector3(-2f, 2, 2);
            tempShpagin.transform.rotation = Quaternion.Euler(tempShpagin.transform.rotation.eulerAngles.x, tempShpagin.transform.rotation.eulerAngles.y, Mathf.Atan2(this.transform.position.y - tempShpagin.transform.position.y, this.transform.position.x - tempShpagin.transform.position.x) * Mathf.Rad2Deg - 180);
        }
        else
        {
            tempShpagin.transform.rotation = Quaternion.Euler(tempShpagin.transform.rotation.eulerAngles.x, tempShpagin.transform.rotation.eulerAngles.y, Mathf.Atan2(this.transform.position.y - tempShpagin.transform.position.y, this.transform.position.x - tempShpagin.transform.position.x) * Mathf.Rad2Deg);
        }
        tempShpagin.transform.SetParent(activeBattleUnitTransform);



        if (tempShpagin == null)
            Debug.Log("shpagina NETY");

        ShpaginBehaviour shpaginScript = tempShpagin.GetComponent<ShpaginBehaviour>();
        shpaginScript.aim = this.gameObject;



        if (damage >= u.maxHP * (u.quantity - 1) + u.HP)
        {
            shpaginScript.aimUnit = u;
            shpaginScript.gonnaDestroy = true; //������ ������ ��� ��� �����������
            Debug.Log("���������� ���");


            //����� ���������� ����������� ����������
            /*
            Animator cardAnimator;
            u.quantity = 0;
            for(int i =0; i< 4; i++)
            {
                if (GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightEnemyUnits[i].quantity <= 0) //���� ����� �����, � �������� ��������� ���-��
                {
                    Debug.Log("������ ����������"); ;
                    cardAnimator = GameMainScript.BaseOfUnitsSC.BattleUnitObjectsEnemy[i].GetComponent<Animator>();
                    cardAnimator.SetBool("GoesToTop", true);
                    GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightEnemyUnits.RemoveAt(i); //��� ������ ���� �����, ����� �����

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

                    Debug.Log(GameMainScript.MapSC.VillageDict[GameMainScript.MapSC.activeVillageTag].fightEnemyUnits.Count + " - ������ ������� ������ �������� � ���� �������");
                    //GameMainScript.BaseOfUnitsSC.moveUnitsLeft()

                    //this.DestroyThis();
                    break;
                }
            }*/


            //����� ������������� �����������
        }
        else
        {

            shpaginScript.gonnaDestroy = false;
            Debug.Log("������� ��� ����");
            while (damage > u.maxHP && u.quantity > 1)
            {
                u.quantity--;
                damage -= u.maxHP;
            }
            if (damage >= u.HP)
            {
                u.quantity--;
                damage -= u.HP;
                u.HP = u.maxHP - damage;
            }
            else
                u.HP -= damage;

            //if(u.HP<0)
            //if(u.HP <0)

            GameMainScript.BaseOfUnitsSC.RefreshAllQuantities(); //��� ��������� �� �������� � ���-�� ���� ������


        }
        activeBattleUnit.isActive = false;
    }

    public void DestroyThis()
    {
        Debug.Log("���� ���������� ���");
        Destroy(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

}
