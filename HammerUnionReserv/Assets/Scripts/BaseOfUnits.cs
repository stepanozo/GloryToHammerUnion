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

    //Анимации для перехода в бой
    public Animator RezervAnimator;
    public Animator ButtonsAnimator;
    public Animator InfoAnimator;

    public Animator BattleCardsAnimator;
    public Animator BattlePanelAnimator;

    //Панель юнита
    public GameObject BigUnitPanel;
    public Text BigUnitName;
    public Text BigUnitDescription;
    public GameObject CardOnPanel;
    public  GameObject buttonAttack;
    public GameObject buttonAbility;
    public GameObject buttonRetreat;
    public GameObject buttonPlayIt;


    //Для боя
    public Animator ButtleReservAnimator;
    public GameObject BattleUnitsRezervObject;
    public GameObject BattleRezervObject;
    [SerializeField] Scrollbar BattleReservScrollbar;
    public List<GameObject> BattleRezervUnitsObjects = new List<GameObject>();
    //Для резерва



    [SerializeField] Scrollbar reservScrollbar;
    [SerializeField] GameObject rezervPanel;



    [SerializeField] internal static GameMainScript GameSC; //Это один-единственный экземпляр объекта "Скрипт игры", на котором всё висит что нужно для игры
    public List<unit> RezervUnits = new List<unit>();
    public List<GameObject> RezervUnitsObjects = new List<GameObject>();
    public GameObject UnitPrefab;
    public GameObject AllUnitsRezervObject;
    public GameObject tempUnitObject;
    public GameObject simpleUnitPanel;
    public Text simpleUnitDescription;
    public Text simpleUnitName;
    private CardBehaviour cardScript;

    private void Start()
    {


        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //мб это надо удалить, всё как-то и без него работает

     

    }


    //Короче тут целая функция не получилась к сожалению

    /*
    public void SetScaleOfRezerv()
    {
        foreach(GameObject obj in RezervUnitsObjects) //Временно прикрепляем к канвасу
        {
            obj.transform.SetParent(GameObject.Find("Canvas").transform);
        }


        float heightOneCard = 1f/ (AllUnitsRezervObject.GetComponent<RectTransform>().rect.height / UnitPrefab.GetComponent<RectTransform>().rect.height);
        Debug.Log(heightOneCard + " ЭТО РАЗМЕР КАРТОЧИК");

        float height;
        

        height = RezervUnits.Count <= 3 ? heightOneCard * 3 : heightOneCard * RezervUnits.Count;


        AllUnitsRezervObject.transform.localScale = new Vector2(1f, height);

        foreach (GameObject obj in RezervUnitsObjects) //Обратно прикрепляем к панели, уже изменив её размер
        {
            obj.transform.SetParent(GameObject.Find("Юниты резерва2").transform);
        }

    }*/



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
        RefreshRezerv(battle: true); //Обновить резерв именно битвы, а не обычный резерв
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

        //Тут определяем в зависимости от параметра, в какую панель резерва спавним карточки

        if (!battle)
        {
            unitObjects = RezervUnitsObjects;
            panel = rezervPanel;
            parentUnits = AllUnitsRezervObject;
           
            Debug.Log("Не файтимся");

            
        }
        else
        {
            panel = BattleRezervObject;
            parentUnits = BattleUnitsRezervObject;
            unitObjects = BattleRezervUnitsObjects;

            Debug.Log("Файтимся");
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
            Debug.Log("позиция резерва " + panel.transform.position.x);
            tempUnitObject = Instantiate(UnitPrefab, new Vector2(panel.transform.position.x, yCard), Quaternion.identity);
            cardScript = tempUnitObject.GetComponent<CardBehaviour>();
            cardScript.u = u;
            tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
            tempUnitObject.transform.SetParent(parentUnits.transform);
            unitObjects.Add(tempUnitObject);

            //Настраиваем иллюстрацию
            tempChildObject = tempUnitObject.transform.Find("Иллюстрация").gameObject;
            tempChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(u.spritePath);

            //Настраиваем здоровье
            tempUnitObject.transform.Find("Здоровье").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.HP);
            tempUnitObject.transform.Find("Урон").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.damage);
            tempUnitObject.transform.Find("Урон по технике").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.techDamage);
            tempUnitObject.transform.Find("Кол-во").GetComponent<Text>().text = Convert.ToString(u.quantity);


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

             //Настраиваем иллюстрацию
             tempChildObject = tempUnitObject.transform.Find("Иллюстрация").gameObject;
             tempChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(u.spritePath);

             //Настраиваем здоровье
             tempUnitObject.transform.Find("Здоровье").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.HP);
             tempUnitObject.transform.Find("Урон").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.damage);
             tempUnitObject.transform.Find("Урон по технике").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.techDamage);
             tempUnitObject.transform.Find("Кол-во").GetComponent<Text>().text = Convert.ToString(u.quantity);


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
        else //Внизу считай не максимум, а минимум уже.
            maxHPOfUnit = 999999999;

        if(NumberOfFirstUnit(name , UnitList) != -1) //если такой юнит существует вообще
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

                    Debug.Log("Убираем юнита номер " + numberOfMaxHP);
                    takenUnit = unit.Copy(UnitList[numberOfMaxHP]);
                    takenUnit.quantity = quantity; //У взятого юнита будет такое кол-во, которое в параметре функции (т.е. сколько и хотели взять)

                    UnitList[numberOfMaxHP].quantity -= quantity;
                    RefreshRezerv();
                    //RezervUnitsObjects[numberOfMaxHP].transform.Find("Кол-во").GetComponent<Text>().text = Convert.ToString(UnitList[numberOfMaxHP].quantity);

                    quantity = 0;
                    takenUnits.Add(takenUnit);
                    
                }
                else
                {
                    //Тут бы удалять ещё и сам объект юнита, чтоб память не захламлял
                    takenUnit = unit.Copy(UnitList[numberOfMaxHP]);
                    quantity -= UnitList[numberOfMaxHP].quantity;
                    UnitList.RemoveAt(numberOfMaxHP);

                    //Здесь бы обновлять резерв ещё раз
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
            tempUnitObject.transform.SetParent(GameObject.Find("Юниты резерва2").transform);
            RezervUnitsObjects.Add(tempUnitObject);
            //tempUnitObject.transform.SetParent(GameObject.Find("Юниты резерва").transform);
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
        Debug.Log(addedUnit.name + "- имя добавленного юнита");
        if(addedUnit.name == "Солдат СКСМ")
        {
            Debug.Log("Должны добавить солдата.");
            foreach (unit u in RezervUnits)
            {
                if(u.name== "Солдат СКСМ")
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
            Debug.Log("Добавили нового юнита");
            RezervUnits.Add(addedUnit);
        }
        */
    }

    void Update()
    {

    }
}
