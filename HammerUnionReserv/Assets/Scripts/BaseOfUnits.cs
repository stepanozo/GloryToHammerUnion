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
    //��� �������

    [SerializeField] Scrollbar reservScrollbar;
    [SerializeField] GameObject rezervPanel;
    


    [SerializeField] internal static GameMainScript GameSC; //��� ����-������������ ��������� ������� "������ ����", �� ������� �� ����� ��� ����� ��� ����
    public List<unit> RezervUnits = new List<unit>();
    public List<GameObject> RezervUnitsObjects = new List<GameObject>();
    public GameObject UnitPrefab;
    public GameObject AllUnitsRezervObject;
    public GameObject tempUnitObject;

    private void Start()
    {

        
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //�� ��� ���� �������, �� ���-�� � ��� ���� ��������

        //����� �������� ����� ���������
        AddUnitReserv(new unit(name: "������ ����", damage: 4, hP: 3, techDamage: 4, spritePath: "�������\\Illustrations\\������� ����", description: "ASD", quantity: 20));
        AddUnitReserv(new unit(name: "���", damage: 10, hP: 50, techDamage: 10, spritePath: "�������\\Illustrations\\��� �������", description: "ASD", quantity: 1));
        AddUnitReserv(new unit(name: "������ ����", damage: 4, hP: 3, techDamage: 4, spritePath: "�������\\Illustrations\\������� ����", description: "ASD", quantity: 15));
        AddUnitReserv(new unit(name: "���", damage: 10, hP: 50, techDamage: 10, spritePath: "�������\\Illustrations\\��� �������", description: "ASD", quantity: 1));
        AddUnitReserv(new unit(name: "���", damage: 10, hP: 35, techDamage: 10, spritePath: "�������\\Illustrations\\��� �������", description: "ASD", quantity: 1));
        AddUnitReserv(new unit(name: "������ ����", damage: 4, hP: 1, techDamage: 4, spritePath: "�������\\Illustrations\\������� ����", description: "ASD", quantity: 12));
        //AddUnitReserv(new unit("������ ����", 23, 12, 5, "�������\\Illustrations\\������� ����", "ASD", 3));

        //��� ���������������� ������� ��������� ������ � ������

        //Instantiate(UnitPrefab, new Vector3(1.9662f, 2715.523f, 0f), Quaternion.identity);



        //SetScaleOfRezerv(); 

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
        float yCard = 911.2f;
        GameObject tempChildObject;

        foreach (unit u in RezervUnits)
        {

            tempUnitObject = Instantiate(UnitPrefab, new Vector2(1691.37f, yCard), Quaternion.identity);
            tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
            tempUnitObject.transform.SetParent(GameObject.Find("����� �������2").transform);
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
            /*
            RezervUnits.Add(new unit("s", 2, 3, 4, "sd", "asd", 123));
            tempUnitObject = Instantiate(UnitPrefab, new Vector2(1691.37f, yCard), Quaternion.identity);
            tempUnitObject.transform.SetParent(GameObject.Find("Canvas").transform);
            tempUnitObject.transform.SetParent(GameObject.Find("����� �������2").transform);
            RezervUnitsObjects.Add(tempUnitObject);
            //tempUnitObject.transform.SetParent(GameObject.Find("����� �������").transform);
            //tempUnitObject.transform.SetParent(AllUnitsRezervObject.transform);
            //tempUnitObject.transform.position = new Vector2(1.9662f, yCard);
            yCard -= 2715.523f - 2435f;*/
        }
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
            tempUnitObject.transform.SetParent(GameObject.Find("����� �������2").transform);
            RezervUnitsObjects.Add(tempUnitObject);
            //tempUnitObject.transform.SetParent(GameObject.Find("����� �������").transform);
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
