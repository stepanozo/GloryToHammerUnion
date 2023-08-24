using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assembly_CSharp;
using UnityEngine.UI;
using TMPro.EditorUtilities;
using System;

public class BaseOfUnits : MonoBehaviour
{
    //Для резерва

    [SerializeField] Scrollbar reservScrollbar;
    [SerializeField] GameObject rezervPanel;



    [SerializeField] internal static GameMainScript GameSC; //Это один-единственный экземпляр объекта "Скрипт игры", на котором всё висит что нужно для игры
    public List<unit> RezervUnits = new List<unit>();
    public List<GameObject> RezervUnitsObjects = new List<GameObject>();
    public GameObject UnitPrefab;
    public GameObject AllUnitsRezervObject;
    public GameObject tempUnitObject;

    private void Start()
    {


        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //мб это надо удалить, всё как-то и без него работает

        //Здесь добавить юнита попробуем
        AddUnitReserv(new unit(name: "Солдат СКСМ", damage: 4, hP: 3, maxHP: 3, techDamage: 4, spritePath: "Спрайты\\Illustrations\\Солдаты СКСМ", description: "ASD", quantity: 2));
        AddUnitReserv(new unit(name: "БТР", damage: 10, hP: 50, maxHP: 50, techDamage: 10, spritePath: "Спрайты\\Illustrations\\БТР колонна", description: "ASD", quantity: 1));
        AddUnitReserv(new unit(name: "Солдат СКСМ", damage: 4, hP: 3, maxHP: 3, techDamage: 4, spritePath: "Спрайты\\Illustrations\\Солдаты СКСМ", description: "ASD", quantity: 2));
        AddUnitReserv(new unit(name: "БТР", damage: 10, hP: 50, maxHP: 50, techDamage: 10, spritePath: "Спрайты\\Illustrations\\БТР колонна", description: "ASD", quantity: 1));
        AddUnitReserv(new unit(name: "БТР", damage: 10, hP: 35, maxHP: 50, techDamage: 10, spritePath: "Спрайты\\Illustrations\\БТР колонна", description: "ASD", quantity: 1));
        AddUnitReserv(new unit(name: "Солдат СКСМ", damage: 4, hP: 1, maxHP: 3, techDamage: 4, spritePath: "Спрайты\\Illustrations\\Солдаты СКСМ", description: "ASD", quantity: 3));
        RefreshRezerv();
        //AddUnitReserv(new unit("Солдат СКСМ", 23, 12, 5, "Спрайты\\Illustrations\\Солдаты СКСМ", "ASD", 3));

        //Тут экспериментально добавим несколько юнитов в резерв

        //Instantiate(UnitPrefab, new Vector3(1.9662f, 2715.523f, 0f), Quaternion.identity);



        //SetScaleOfRezerv(); 

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


    public void InitializeUnits()
    {

    }

    public void HideRezerv()
    {
        rezervPanel.SetActive(false);
    }


    public void RefreshRezerv()
    {
        ClearRezerv();
        reservScrollbar.value = 1;
        float yCard = 911.2f;
        GameObject tempChildObject;

        foreach (unit u in RezervUnits)
        {

            tempUnitObject = Instantiate(UnitPrefab, new Vector2(1691.37f, yCard), Quaternion.identity);
            tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
            tempUnitObject.transform.SetParent(GameObject.Find("Юниты резерва2").transform);
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
            /*
            RezervUnits.Add(new unit("s", 2, 3, 4, "sd", "asd", 123));
            tempUnitObject = Instantiate(UnitPrefab, new Vector2(1691.37f, yCard), Quaternion.identity);
            tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
            tempUnitObject.transform.SetParent(GameObject.Find("Юниты резерва2").transform);
            RezervUnitsObjects.Add(tempUnitObject);
            //tempUnitObject.transform.SetParent(GameObject.Find("Юниты резерва").transform);
            //tempUnitObject.transform.SetParent(AllUnitsRezervObject.transform);
            //tempUnitObject.transform.position = new Vector2(1.9662f, yCard);
            yCard -= 2715.523f - 2435f;*/
        }
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

    public int NumberOfFirstUnit(string name, List<unit> UnitList)
    {
        for (int i = 0; i<= UnitList.Count; i++)
        {
            if (UnitList[i].name == name)
                return i;
        }
        return -1;
    }



    public List<unit> RemoveUnit(string name, int quantity, List<unit> UnitList)
    {
        List<unit> takenUnits = new List<unit>();
        int numberOfMaxHP;
        int maxHPOfUnit = 1;

        if(NumberOfFirstUnit(name , UnitList) != -1) //если такой юнит существует вообще
        {
            numberOfMaxHP = NumberOfFirstUnit(name, UnitList);
            unit takenUnit;
            while (quantity > 0)
            {

                for (int i = 0; i < UnitList.Count; i++)
                {
                    if (UnitList[i].name == name && UnitList[i].HP > maxHPOfUnit)
                    {
                        numberOfMaxHP = i;
                    }
                }

                if (UnitList[numberOfMaxHP].quantity > quantity)
                {

                    Debug.Log("Убираем юнита номер " + numberOfMaxHP);
                    takenUnit = unit.Copy(UnitList[numberOfMaxHP]);

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
    
    public void ClearRezerv()
    {
        foreach (GameObject obj in RezervUnitsObjects)
        {
            Destroy(obj);
        }
    }

    public void ShowRezerv()
    {

        float yCard = 911.2f;

       
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

    public void AddUnitReserv(unit addedUnit)
    {
        bool flg = true;
        foreach (unit u in RezervUnits)
        {
            if(u.name == addedUnit.name && u.HP == addedUnit.HP)
            {
                u.quantity += addedUnit.quantity;
                flg = false;
                break;
            }
        }
        if (flg)
            RezervUnits.Add(addedUnit);


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
