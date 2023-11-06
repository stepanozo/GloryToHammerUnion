using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assembly_CSharp;
using UnityEngine.UI;
//using TMPro.EditorUtilities;
using System;
using Mono.Cecil;
using System.Linq;

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
    public bool gonnaAttack;

    [SerializeField]
    GameObject[] enemyExamples;

    [SerializeField]
    GameObject[] allyExamples;
    public
    GameObject ShpaginPrefab;


    //��� �������


    [SerializeField] GameObject ControlCardRezerv;
    [SerializeField] GameObject ControlCardRezerv2;

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
    public unit ActiveReservUnit;

    private void Start()
    {

        gonnaAttack = false;
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


    public void newTestFunction()
    {

    }

    public void SpawnUnit(unit SpawnedUnit,Vector2 position, string Side, string battlePointTag, int numberOfUnit)
    {

        tempUnitObject = Instantiate(BattleUnitPrefab, position, Quaternion.identity);

        BattleCardScript = tempUnitObject.GetComponent<BattleCardBehaviour>();
        BattleCardScript.u = SpawnedUnit;

       // BattleCardScript.startPosition = tempUnitObject.transform.position;


        tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
        tempUnitObject.transform.SetParent(GameObject.Find("����� ��������").transform);
        tempUnitObject.transform.localScale = new Vector3(1f, 1f, 1f);

        if (Side == "Ally")
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


        /*BattleCardScript = BattleUnitObjectsAlly[0].GetComponent<BattleCardBehaviour>();
        BattleCardScript.timer = 3;
        BattleCardScript.lerp = 0;
        BattleCardScript.startPosition = BattleUnitObjectsAlly[0].transform.position;
        BattleCardScript.endPosition = new Vector2(12, BattleUnitObjectsAlly[0].transform.position.y);
        BattleCardScript.moves = true;*/


        //�������� ��������
        Animator cardAnimator = tempUnitObject.GetComponent<Animator>();

         if(Side == "Ally")
            cardAnimator.SetBool("GoesFromBot", true);
        else
            cardAnimator.SetBool("GoesFromTop", true);
    }


    private void moveUnitX(float x, int numberUnit, bool isEnemy = false)
    {
        GameObject[] arrayUnits;

        arrayUnits = isEnemy ? BattleUnitObjectsEnemy : BattleUnitObjectsAlly;
   

        BattleCardBehaviour unit = arrayUnits[numberUnit].GetComponent<BattleCardBehaviour>();
        unit.timer = 0.15f;
        unit.countTime= 0;
        unit.lerp = 0;
        unit.startPosition = (Vector2)arrayUnits[numberUnit].transform.localPosition;
        unit.endPosition = new Vector2(x, arrayUnits[numberUnit].transform.localPosition.y); //�� ���� ��� �� ���� ������ ������ ������ �� ������� ��� ��???
        unit.moves = true;
        unit.v = Time.deltaTime;
    }

    public void moveUnitsLeft(string battlePointTag, int numberOfAllies, int numberOfEnemies)
    {
        int numberOfUnits = numberOfAllies;
        //numberOfUnits = 2;
        Debug.Log(" rab");
        Debug.Log("���� ������� ��� ���� ���� ������ " + numberOfAllies);
        switch (numberOfUnits)
        {
            case 1:
                moveUnitX(800 - 960, 0);
                break;
            case 2:

                moveUnitX((float)644.5 - 960, 0);
                //if(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count > 1) //������� ����� ������� ������ ���� �� ��� ����.
                if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count > 1 && BattleUnitObjectsAlly[1] != null)
                    moveUnitX((float)962.2 - 960, 1);
                break;
            case 3:

                moveUnitX((float)482.3 - 960, 0);
                moveUnitX((float)800 - 960, 1);
                if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count > 2 && BattleUnitObjectsAlly[2] != null) //���������� ����� ������� ������ ���� �� ��� ����.
                    moveUnitX((float)1117 - 960, 2);
                break;
            case 4:

                moveUnitX((float)326.8 - 960, 0);
                moveUnitX((float)644.5 - 960, 1);
                moveUnitX((float)962.2 - 960, 2);
                if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count > 3 && BattleUnitObjectsAlly[3] != null) //���������� ����� ������� ������ ���� �� ��� ����.
                    moveUnitX((float)1279.9 - 960, 3);
                break;

        }
        numberOfUnits = numberOfEnemies;
       // numberOfUnits = 1;
        switch (numberOfUnits)
        {
            case 1:
                moveUnitX(800 - 960, 0, true);
                break;
            case 2:
                moveUnitX((float)644.5 - 960, 0, true);
                if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits.Count > 1 && BattleUnitObjectsEnemy[1] != null) //���������� ����� ������� ������ ���� �� ��� ����.
                    moveUnitX((float)962.2 - 960, 1, true);
                break;
            case 3:

                moveUnitX((float)482.3 - 960, 0, true);
                moveUnitX((float)800 - 960, 1, true);
                if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits.Count > 2 && BattleUnitObjectsEnemy[2] != null) //���������� ����� ������� ������ ���� �� ��� ����.
                    moveUnitX((float)1117 - 960, 2, true);
                break;
            case 4:

                moveUnitX((float)326.8 - 960, 0, true);
                moveUnitX((float)644.5 - 960, 1, true);
                moveUnitX((float)962.2 - 960, 2, true);
                if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits.Count > 3 && BattleUnitObjectsEnemy[3] != null) //���������� ����� ������� ������ ���� �� ��� ����.
                    moveUnitX((float)1279.9 - 960, 3, true);
                break;
        }
    }

    public void RefreshBattle(string battlePointTag)
    {
        Debug.Log(allyExamples.Length);

        switch (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count)
        {
            case 1:
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[0], allyExamples[3].transform.position, "Ally", battlePointTag, 0);
                break;
            case 2:
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[0], allyExamples[2].transform.position, "Ally", battlePointTag, 0);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[1], allyExamples[4].transform.position, "Ally", battlePointTag, 1);
                break;
            case 3:
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[0], allyExamples[1].transform.position, "Ally", battlePointTag, 0);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[1], allyExamples[3].transform.position , "Ally", battlePointTag, 1);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[2], allyExamples[5].transform.position , "Ally", battlePointTag, 2);
                break;
            case 4:
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[0], allyExamples[0].transform.position, "Ally", battlePointTag, 0);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[1], allyExamples[2].transform.position, "Ally", battlePointTag, 1);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[2], allyExamples[4].transform.position, "Ally", battlePointTag, 2);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits[3], allyExamples[6].transform.position, "Ally", battlePointTag, 3);
                break;
        }

        switch (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits.Count)
        {
            case 1:
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[0], enemyExamples[3].transform.position, "Enemy", battlePointTag, 0);
                break;
            case 2:
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[0], enemyExamples[2].transform.position, "Enemy", battlePointTag, 0);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[1], enemyExamples[4].transform.position, "Enemy", battlePointTag, 1);
                break;
            case 3:
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[0], enemyExamples[1].transform.position, "Enemy", battlePointTag, 0);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[1], enemyExamples[3].transform.position, "Enemy", battlePointTag , 1);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[2], enemyExamples[5].transform.position, "Enemy", battlePointTag, 2);
                break;
            case 4:
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[0], enemyExamples[0].transform.position, "Enemy", battlePointTag, 0);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[1], enemyExamples[2].transform.position, "Enemy", battlePointTag, 1);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[2], enemyExamples[4].transform.position, "Enemy", battlePointTag, 2);
                SpawnUnit(GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits[3], enemyExamples[6].transform.position, "Enemy", battlePointTag, 3);
                break;
        }

    }

    public void attackButtonClick()
    {
        gonnaAttack = true;
        //����� �� ������ ���� �������� �� ������
    }

    public void PlayUnitClick()
    {
        if (ActiveReservUnit.HP == ActiveReservUnit.maxHP && GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits.Count < 4)
        {
            playUnit(ActiveReservUnit, GameMainScript.MapSC.activeBattlePointTag);
            Debug.Log("���������� �������������� ����� ��� " + ActiveReservUnit.quantity);
            RemoveUnit(ActiveReservUnit.name, ActiveReservUnit.quantity, RezervUnits);
            RefreshRezerv( battle:true);
            RefreshAllQuantities();
        }
    }
    
    public void RefreshAllQuantities()
    {
        for (int i = 0; i < 4; i++)
        {
            //������ ��������� ����� ���-��, ��� � ������ �����
            if (BattleUnitObjectsAlly[i] != null && GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits[i] != null)
                 //���� ����� ���� � ����� ������ ������ ����
            {
                BattleUnitObjectsAlly[i].transform.Find("���-��").GetComponent<Text>().text = Convert.ToString(GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits[i].quantity);
                BattleUnitObjectsAlly[i].transform.Find("��������").transform.Find("�����").GetComponent<Text>().text = Convert.ToString(GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].fightAllyUnits[i].HP);
            }

            if(BattleUnitObjectsEnemy[i] != null && GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].fightEnemyUnits[i] != null)
            {
                BattleUnitObjectsEnemy[i].transform.Find("���-��").GetComponent<Text>().text = Convert.ToString(GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].fightEnemyUnits[i].quantity);
                BattleUnitObjectsEnemy[i].transform.Find("��������").transform.Find("�����").GetComponent<Text>().text = Convert.ToString(GameMainScript.MapSC.battlePointsDict[GameMainScript.MapSC.activeBattlePointTag].fightEnemyUnits[i].HP);
            }
        }
    }

    private void playUnit(unit u, string battlePointTag)
    {
        if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count < 4)
        {

         
            int controlNumberOfUnits = GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count; //����������, ������� �������� ��������� ���� �� ��������� �����

            switch (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count)
            {
                case 0:
                    
                    AddUnit(u, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits);
                    if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count > controlNumberOfUnits) //���� ���-�� �������� ����������, �� ����� �������� ������ � ������� ����� ��������. ���� ����� ����� ������ ������������, ����� ������ ������� � �������� �� ����.
                    {
                        moveUnitsLeft(battlePointTag, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits.Count);
                        SpawnUnit(u, allyExamples[3].transform.position, "Ally", battlePointTag, 0);

                    }
                    else
                        RefreshAllQuantities();



                    break;
                case 1:
                    AddUnit(u, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits);
                    if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count > controlNumberOfUnits)
                    {
                        moveUnitsLeft(battlePointTag, controlNumberOfUnits+1, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits.Count);
                        SpawnUnit(u, allyExamples[4].transform.position, "Ally", battlePointTag, 1);
                    }
                    else
                        RefreshAllQuantities();
                    break;
                case 2:
                    Debug.Log("������� �������� �����");
                    AddUnit(u, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits);
                    if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count > controlNumberOfUnits)
                    {
                        moveUnitsLeft(battlePointTag, controlNumberOfUnits+1, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits.Count);
                        SpawnUnit(u, allyExamples[5].transform.position, "Ally", battlePointTag, 2);
                    }
                    else
                        RefreshAllQuantities();
                    break;
                case 3:
                    AddUnit(u, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits);
                    if (GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count > controlNumberOfUnits) 
                    {
                        moveUnitsLeft(battlePointTag, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightAllyUnits.Count, GameMainScript.MapSC.battlePointsDict[battlePointTag].fightEnemyUnits.Count);
                        SpawnUnit(u, allyExamples[6].transform.position, "Ally", battlePointTag, 3);
                    }
                    else
                        RefreshAllQuantities();
                    break;
            }
            RefreshAllQuantities();
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
        RefreshBattle(GameMainScript.MapSC.activeBattlePointTag);
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

        int tempCount = 0;
        foreach(GameObject g in BattleUnitObjectsAlly)
        {
            tempCount++;
            Debug.Log(tempCount + " - ������� ��������� ������");
            if (g!=null) //�������
            {
                cardAnimator = g.GetComponent<Animator>();
                cardAnimator.SetBool("GoesToBot", true);
            }
        }

        foreach (GameObject g in BattleUnitObjectsEnemy)
        {
            Debug.Log("������ ����������");
            if (g != null)
            {
                cardAnimator = g.GetComponent<Animator>();
                cardAnimator.SetBool("GoesToTop", true);
            }
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
       /* if (unitObjects.Count > 0)
        {
            unitObjects[0].transform.SetParent(null);
            //RezervUnitsObjects[0].transform.SetParent(null);
            yCard = unitObjects[0].transform.position.y;
        }
        else*/
      //  {
            yCard = ControlCardRezerv.transform.position.y;
       // }
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
            tempUnitObject.transform.localScale = new Vector3(1f, 1f, 1f);
            Debug.Log("������ �������� " + tempUnitObject.transform.localScale);

            // yCard -= 2715.523f - 2435f;
            yCard -= ControlCardRezerv.transform.position.y - ControlCardRezerv2.transform.position.y;

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
        for (int i = 0; i< UnitList.Count; i++) //���� ������� <= ?
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
            if (u.name == addedUnit.name && u.HP == addedUnit.HP)
            {
                Debug.Log("����� ����� ���� �� ���������� " + u.quantity);
                u.quantity += addedUnit.quantity;
                flg = false;
                break;
            }
        }
        if (flg)
        {
            Debug.Log("���� �� ��������� ������ " + addedUnit.quantity);
            unitList.Add(addedUnit);
        }
    }

   /* public void AddUnit(unit addedUnit, unit[] unitArray)
    {
        bool flg = true;
        int i = 0;

        while (unitArray[i] != null)
        {
            if (unitArray[i].name == addedUnit.name && unitArray[i].HP == addedUnit.HP)
            {
                unitArray[i].quantity += addedUnit.quantity;
                flg = false;
                break;
            }
            i++;
        }
        if (flg)
            unitArray[i] = addedUnit;
    }*/

    void Update()
    {

    }
}
