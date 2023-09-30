using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assembly_CSharp;
using UnityEngine.UI;
//using TMPro.EditorUtilities;
using System;

public class BaseOfUnits : MonoBehaviour
{

    //�������� ��� �������� � ���
    public Animator RezervAnimator;
    public Animator ButtonsAnimator;
    public Animator InfoAnimator;

    public Animator BattleCardsAnimator;
    public Animator BattlePanelAnimator;

    //������ �����
    public GameObject BigUnitPanel;
    public Text BigUnitName;
    public Text BigUnitDescription;
    public GameObject CardOnPanel;
    public  GameObject buttonAttack;
    public GameObject buttonAbility;
    public GameObject buttonRetreat;
    public GameObject buttonPlayIt;


    //��� ���
    public Animator ButtleReservAnimator;
    public GameObject BattleUnitsRezervObject;
    public GameObject BattleRezervObject;
    [SerializeField] Scrollbar BattleReservScrollbar;
    public List<GameObject> BattleRezervUnitsObjects = new List<GameObject>();
    public GameObject[] BattleUnitObjectsEnemy;
    public GameObject[] BattleUnitObjectsAlly;
    [SerializeField] GameObject parentUnitObject;

    //��� �������



    [SerializeField] Scrollbar reservScrollbar;
    [SerializeField] GameObject rezervPanel;



    [SerializeField] internal static GameMainScript GameSC; //��� ����-������������ ��������� ������� "������ ����", �� ������� �� ����� ��� ����� ��� ����
    public List<unit> RezervUnits = new List<unit>();
    public List<GameObject> RezervUnitsObjects = new List<GameObject>();
    public GameObject UnitPrefab;
    public GameObject BattleUnitPrefab;
    public GameObject AllUnitsRezervObject;
    public GameObject tempUnitObject;
    public GameObject simpleUnitPanel;
    public Text simpleUnitDescription;
    public Text simpleUnitName;
    private CardBehaviour cardScript;
    private BattleCardBehaviour BattleCardScript;

    private void Start()
    {


        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //�� ��� ���� �������, �� ���-�� � ��� ���� ��������
        BattleUnitObjectsAlly = new GameObject[4];
        BattleUnitObjectsEnemy = new GameObject[4];
     

    }


    //������ ��� ����� ������� �� ���������� � ���������

    /*
    public void SetScaleOfRezerv()
    {
        foreach(GameObject obj in RezervUnitsObjects) //�������� ����������� � �������
        {
            obj.transform.SetParent(GameObject.Find("Canvas").transform);
        }


        float heightOneCard = 1f/ (AllUnitsRezervObject.GetComponent<RectTransform>().rect.height / UnitPrefab.GetComponent<RectTransform>().rect.height);
        Debug.Log(heightOneCard + " ��� ������ ��������");

        float height;
        

        height = RezervUnits.Count <= 3 ? heightOneCard * 3 : heightOneCard * RezervUnits.Count;


        AllUnitsRezervObject.transform.localScale = new Vector2(1f, height);

        foreach (GameObject obj in RezervUnitsObjects) //������� ����������� � ������, ��� ������� � ������
        {
            obj.transform.SetParent(GameObject.Find("����� �������2").transform);
        }

    }*/


    public void SpawnUnit(unit SpawnedUnit,Vector2 position, string Side, string villageTag, int numberOfUnit)
    {

        tempUnitObject = Instantiate(BattleUnitPrefab, position, Quaternion.identity);

        BattleCardScript = tempUnitObject.GetComponent<BattleCardBehaviour>();
        BattleCardScript.u = SpawnedUnit;
        tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
        tempUnitObject.transform.SetParent(parentUnitObject.transform);

        if(Side == "Ally")
          BattleUnitObjectsAlly[numberOfUnit] = tempUnitObject;
        else
            BattleUnitObjectsEnemy[numberOfUnit] = tempUnitObject;

        //����������� �����������
        GameObject tempChildObject;
        tempChildObject = tempUnitObject.transform.Find("�����������").gameObject;
        tempChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(SpawnedUnit.spritePath);

        //����������� ��������
        tempUnitObject.transform.Find("��������").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(SpawnedUnit.HP);
        tempUnitObject.transform.Find("����").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(SpawnedUnit.damage);
        tempUnitObject.transform.Find("���� �� �������").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(SpawnedUnit.techDamage);
        tempUnitObject.transform.Find("���-��").GetComponent<Text>().text = Convert.ToString(SpawnedUnit.quantity);

        //�������� ��������
        Animator cardAnimator = tempUnitObject.GetComponent<Animator>();

        if(Side == "Ally")
            cardAnimator.SetBool("GoesFromBot", true);
        else
            cardAnimator.SetBool("GoesFromTop", true);
    }


    public void RefreshBattle(string villageTag)
    {

        switch (GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits.Count)
        {
            case 1:
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[0], new Vector2(800, -277), "Ally", villageTag, 0);
                break;
            case 2:
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[0], new Vector2((float)644.5, -277), "Ally", villageTag, 0);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[1], new Vector2((float)962.2, -277), "Ally", villageTag, 1);
                break;
            case 3:
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[0], new Vector2((float)482.3, -277), "Ally", villageTag, 0);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[1], new Vector2((float)800, -277) , "Ally", villageTag, 1);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[2], new Vector2((float)1117, -277) , "Ally", villageTag, 2);
                break;
            case 4:
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[0], new Vector2((float)326.8, -277), "Ally", villageTag, 0);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[1], new Vector2((float)644.5, -277), "Ally", villageTag, 1);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[2], new Vector2((float)962.2, -277), "Ally", villageTag, 2);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightAllyUnits[3], new Vector2((float)1279.9, -277), "Ally", villageTag, 3);
                break;
        }

        switch (GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits.Count)
        {
            case 1:
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[0], new Vector2(800, (float)1719.38), "Enemy", villageTag, 0);
                break;
            case 2:
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[0], new Vector2((float)644.5, (float)1719.38), "Enemy", villageTag, 0);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[1], new Vector2((float)962.2, (float)1719.38), "Enemy", villageTag, 1);
                break;
            case 3:
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[0], new Vector2((float)482.3, (float)1719.38), "Enemy", villageTag, 0);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[1], new Vector2((float)800, (float)1719.38), "Enemy", villageTag , 1);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[2], new Vector2((float)1117, (float)1719.38), "Enemy", villageTag, 2);
                break;
            case 4:
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[0], new Vector2((float)326.8, (float)1719.38), "Enemy", villageTag, 0);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[1], new Vector2((float)644.5, (float)1719.38), "Enemy", villageTag, 1);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[2], new Vector2((float)962.2, (float)1719.38), "Enemy", villageTag, 2);
                SpawnUnit(GameMainScript.MapSC.VillageDict[villageTag].fightEnemyUnits[3], new Vector2((float)1279.9, (float)1719.38), "Enemy", villageTag, 3);
                break;
        }

    }

    public void ToTheBattle()
    {
        BattleReservScrollbar.value = 1;
        GameSC.PlayerState = GameMainScript.states.Fighting;
        GameSC.SoundSource.PlayOneShot(GameSC.PaperSound);
        RezervAnimator.SetBool("RezervOpened", false); 
        ButtonsAnimator.SetBool("ButtonsOpened", false);
        InfoAnimator.SetBool("InfoOpened", false);
        simpleUnitPanel.SetActive(false);
        GameSC.mapAnimator.SetBool("MapOpened", false);

        if(GameSC.ActiveCaseNumber == 0)
            GameSC.delo1Animator.SetInteger("Condition", 1);
        else
            GameSC.delo2Animator.SetInteger("Condition", 1);

        BattlePanelAnimator.SetBool("PanelOpened", true);
        ButtleReservAnimator.SetBool("RezervOpened", true);
        RefreshRezerv(battle: true); //�������� ������ ������ �����, � �� ������� ������
        RefreshBattle(GameMainScript.MapSC.activeVillageTag);
    }

    public void exitBattle()
    {
        
        GameSC.PlayerState = GameMainScript.states.WatchingMap;
        GameSC.SoundSource.PlayOneShot(GameSC.PaperSound);
        RezervAnimator.SetBool("RezervOpened", true);
        ButtonsAnimator.SetBool("ButtonsOpened", true);
        InfoAnimator.SetBool("InfoOpened", true);
        GameSC.mapAnimator.SetBool("MapOpened", true);

        if (GameSC.ActiveCaseNumber == 0)
            GameSC.delo1Animator.SetInteger("Condition", 3);
        else
            GameSC.delo2Animator.SetInteger("Condition", 3);

        BattlePanelAnimator.SetBool("PanelOpened", false);
        ButtleReservAnimator.SetBool("RezervOpened", false);
        RezervAnimator.SetBool("RezervOpened", true);

        rezervPanel.SetActive(false);
        RefreshRezerv();

        Animator cardAnimator;

        foreach(GameObject g in BattleUnitObjectsAlly)
        {
            cardAnimator = g.GetComponent<Animator>();
            cardAnimator.SetBool("GoesToBot", true);
        }

        foreach (GameObject g in BattleUnitObjectsEnemy)
        {
            cardAnimator = g.GetComponent<Animator>();
            cardAnimator.SetBool("GoesToTop", true);
        }

    }

    public void InitializeUnits()
    {

    }

    public void HideRezerv()
    {
        rezervPanel.SetActive(false);
    }


    public void RefreshRezerv(bool battle = false)
    {
        GameObject panel;
        GameObject parentUnits;
        List<GameObject> unitObjects;

        //��� ���������� � ����������� �� ���������, � ����� ������ ������� ������� ��������

        if (!battle)
        {
            unitObjects = RezervUnitsObjects;
            panel = rezervPanel;
            parentUnits = AllUnitsRezervObject;
           
            Debug.Log("�� ��������");

            
        }
        else
        {
            panel = BattleRezervObject;
            parentUnits = BattleUnitsRezervObject;
            unitObjects = BattleRezervUnitsObjects;

            Debug.Log("��������");
        }

        float yCard;
        if (unitObjects.Count > 0)
        {
            unitObjects[0].transform.SetParent(null);
            //RezervUnitsObjects[0].transform.SetParent(null);
            yCard = unitObjects[0].transform.position.y;
        }
        else
        {
            yCard = 911.2f;
        }
        ClearRezerv(battle);
        GameObject tempChildObject;


        foreach (unit u in RezervUnits)
        {
            //1691.37f
            //1691-730 = 961
            Debug.Log("������� ������� " + panel.transform.position.x);
            tempUnitObject = Instantiate(UnitPrefab, new Vector2(panel.transform.position.x, yCard), Quaternion.identity);
            cardScript = tempUnitObject.GetComponent<CardBehaviour>();
            cardScript.u = u;
            tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
            tempUnitObject.transform.SetParent(parentUnits.transform);
            unitObjects.Add(tempUnitObject);

            //����������� �����������
            tempChildObject = tempUnitObject.transform.Find("�����������").gameObject;
            tempChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(u.spritePath);

            //����������� ��������
            tempUnitObject.transform.Find("��������").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.HP);
            tempUnitObject.transform.Find("����").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.damage);
            tempUnitObject.transform.Find("���� �� �������").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.techDamage);
            tempUnitObject.transform.Find("���-��").GetComponent<Text>().text = Convert.ToString(u.quantity);


            yCard -= 2715.523f - 2435f;

        }


        /* ClearRezerv();
         reservScrollbar.value = 1;
         float yCard = 911.2f;
         GameObject tempChildObject;


         foreach (unit u in RezervUnits)
         {

             tempUnitObject = Instantiate(UnitPrefab, new Vector2(1691.37f, yCard), Quaternion.identity);
             tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
             tempUnitObject.transform.SetParent(AllUnitsRezervObject.transform);
             RezervUnitsObjects.Add(tempUnitObject);

             //����������� �����������
             tempChildObject = tempUnitObject.transform.Find("�����������").gameObject;
             tempChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(u.spritePath);

             //����������� ��������
             tempUnitObject.transform.Find("��������").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.HP);
             tempUnitObject.transform.Find("����").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.damage);
             tempUnitObject.transform.Find("���� �� �������").gameObject.transform.Find("�����").GetComponent<Text>().text = Convert.ToString(u.techDamage);
             tempUnitObject.transform.Find("���-��").GetComponent<Text>().text = Convert.ToString(u.quantity);


             yCard -= 2715.523f - 2435f;

         }
         */
    }

    public int CountUnit(string name, List<unit> UnitList)
    {
        int count = 0;
        foreach(unit u in UnitList)
        {
            if (u.name == name)
                count+= u.quantity;
        }
        return count;
    }

    public int CountUnit(List<unit> UnitList)
    {
        int count = 0;
        foreach (unit u in UnitList)
        {
                count += u.quantity;
        }
        return count;
    }

    public int NumberOfFirstUnit(string name, List<unit> UnitList)
    {
        for (int i = 0; i<= UnitList.Count; i++)
        {
            if (UnitList[i].name == name)
                return i;
        }
        return -1;
    }



    public List<unit> RemoveUnit(string name, int quantity, List<unit> UnitList, bool MaxHPFirst = true)
    {
        

        List<unit> takenUnits = new List<unit>();
        int numberOfMaxHP;

        int maxHPOfUnit;

        if (MaxHPFirst)
            maxHPOfUnit = 1;
        else //����� ������ �� ��������, � ������� ���.
            maxHPOfUnit = 999999999;

        if(NumberOfFirstUnit(name , UnitList) != -1) //���� ����� ���� ���������� ������
        {
            numberOfMaxHP = NumberOfFirstUnit(name, UnitList);
            unit takenUnit;
            while (quantity > 0)
            {

                for (int i = 0; i < UnitList.Count; i++)
                {
                    if ((UnitList[i].name == name && UnitList[i].HP > maxHPOfUnit && MaxHPFirst) || (UnitList[i].name == name && UnitList[i].HP < maxHPOfUnit && !MaxHPFirst))
                    {
                        numberOfMaxHP = i;
                    }
                }

                if (UnitList[numberOfMaxHP].quantity > quantity)
                {

                    Debug.Log("������� ����� ����� " + numberOfMaxHP);
                    takenUnit = unit.Copy(UnitList[numberOfMaxHP]);
                    takenUnit.quantity = quantity; //� ������� ����� ����� ����� ���-��, ������� � ��������� ������� (�.�. ������� � ������ �����)

                    UnitList[numberOfMaxHP].quantity -= quantity;
                    RefreshRezerv();
                    //RezervUnitsObjects[numberOfMaxHP].transform.Find("���-��").GetComponent<Text>().text = Convert.ToString(UnitList[numberOfMaxHP].quantity);

                    quantity = 0;
                    takenUnits.Add(takenUnit);
                    
                }
                else
                {
                    //��� �� ������� ��� � ��� ������ �����, ���� ������ �� ���������
                    takenUnit = unit.Copy(UnitList[numberOfMaxHP]);
                    quantity -= UnitList[numberOfMaxHP].quantity;
                    UnitList.RemoveAt(numberOfMaxHP);

                    //����� �� ��������� ������ ��� ���
                    if(AllUnitsRezervObject.activeInHierarchy)
                        RefreshRezerv();
                    takenUnits.Add(takenUnit);
                    
                }

            }
        }
        return takenUnits;

       
            
    }
    
    public void ClearRezerv(bool battle = false)
    {
        if (battle)
        {
            foreach (GameObject obj in BattleRezervUnitsObjects)
            {
                Destroy(obj);
            }
            BattleRezervUnitsObjects.Clear();
        }
        else
        {
            foreach (GameObject obj in RezervUnitsObjects)
            {
                Destroy(obj);
            }
            RezervUnitsObjects.Clear();
        }
    }

    public void ShowRezerv()
    {
        rezervPanel.SetActive(true);
        reservScrollbar.value = 1;
        RefreshRezerv();


        /*
        for(int i =0; i<10; i++)
        {
            
            RezervUnits.Add(new unit("s", 2, 3, 4, "sd", "asd", 123));
            tempUnitObject = Instantiate(UnitPrefab, new Vector2(1691.37f, yCard), Quaternion.identity);
            tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
            tempUnitObject.transform.SetParent(GameObject.Find("����� �������2").transform);
            RezervUnitsObjects.Add(tempUnitObject);
            //tempUnitObject.transform.SetParent(GameObject.Find("����� �������").transform);
            //tempUnitObject.transform.SetParent(AllUnitsRezervObject.transform);
            //tempUnitObject.transform.position = new Vector2(1.9662f, yCard);
            yCard -= 2715.523f - 2435f;
        }
        */


    }

    public void AddUnit(unit addedUnit, List<unit> unitList)
    {
        bool flg = true;
        foreach (unit u in unitList)
        {
            if(u.name == addedUnit.name && u.HP == addedUnit.HP)
            {
                u.quantity += addedUnit.quantity;
                flg = false;
                break;
            }
        }
        if (flg)
            unitList.Add(addedUnit);


        /*
        bool flg = true;
        Debug.Log(addedUnit.name + "- ��� ������������ �����");
        if(addedUnit.name == "������ ����")
        {
            Debug.Log("������ �������� �������.");
            foreach (unit u in RezervUnits)
            {
                if(u.name== "������ ����")
                {
                    u.quantity+=addedUnit.quantity;
                    flg = false;
                    break;
                }    
            }
            if (flg)
                RezervUnits.Add(addedUnit);
        }
        else
        {
            Debug.Log("�������� ������ �����");
            RezervUnits.Add(addedUnit);
        }
        */
    }

    void Update()
    {

    }
}
