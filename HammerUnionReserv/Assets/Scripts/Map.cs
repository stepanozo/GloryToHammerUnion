using Assembly_CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Playables;
using UnityEngine.UI; //��������� ����� ����� ��������� � ������ ������
using UnityEngine.Video;
//using static UnityEditor.VersionControl.Asset;

internal class Map : MonoBehaviour
{
    internal static GameMainScript GameSC; //��� ����-������������ ��������� ������� "������ ����", �� ������� �� ����� ��� ����� ��� ����
    public Text buildingDescription; //�������� ������, ������� ����� ���������� ���������
    public Text areaDescription;
    public Text areaName;
    public Text UpperStatusText;
    public Text statusText;
    public Text buildText;
    public GameObject[] buildingButtonsArray;
    public GameObject[] reqRecBuldingPictures;
    public Text[] reqRecBuldingTexts;
    public Sprite[] spritesForRecourses;
    public GameObject buildButton;
    public Sprite CheckMarkSprite;//������ �������
    public Sprite NoCheckMarkSprite;
    public Text RequireRecoursesLabel;

    public GameObject TOWNMAP;
    public GameObject VILLAGEMAP;
    public GameObject WARMAP;
    public GameObject TownButton;
    public GameObject VillageButton;
    public GameObject WarButton;

    public int typeOfMap;

    //׸-�� ��� �������
    public Text villageName;
    public Text villagePopulationText;
    public Text revolutionChanceText;
    public Text villageResoursesText;
    public Text villageResoursesGivenText;

    public GameObject[] villageResPictures;
    public GameObject[] villageResGivenPictures;
    public Text[] villageResTexts;
    public Text[] villageResGivenTexts;
    public GameObject villageSoldiers;
    public Text villageSoldiersNumberText;


    //��� �������:

    public GameObject policeObject;
    public GameObject policeMinus;
    public GameObject policePlus;
    public Text policeValueText;
    public Text soldiersVillageValueText;

    //����� ������� ��� ������������� ������� �����
    public GameObject[] reqRecForAreaPictures;
    public Text[] reqRecForAreaTexts;
    public GameObject[] minusesForAreas;
    public GameObject[] plusesForAreas;
    public RectTransform[] positionsForPluses;

    //����� �������� ������, ������������, � ����� ������ �� ������ ������ ����� ������ ����� (��� ������������� �������)
    public int[] SlotsRecourses;
    public int[] SlotsRecoursesVillage; //����������� �������, �� ��� ��������.
    public int[] SlotsRecoursesGivenVillage;



    public string activeAreaTag; //tag ���� ������, ������� ������ �� ������
    public string activeVillageTag; //tag ��� �������, ������� ������ �� ������
    public buildings activeBuildingOnScreen;

    public Dictionary<string, mapArea> MapAreaDict = new Dictionary<string, mapArea>(); //������� ���� �������
    public Dictionary<string, townArea> TownAreaDict = new Dictionary<string, townArea>(); //������� ������� �������, � ������� ����� ������ ���������� � ������ �������.
    public Dictionary<string, emptyArea> EmptyAreaDict = new Dictionary<string, emptyArea>(); //������� ������ �������

    public Dictionary<string, village> VillageDict = new Dictionary<string, village>(); //������� ���� ��������



    public Dictionary<buildings, building> buildingDict = new Dictionary<buildings, building>();

    // Start is called before the first frame update
    void Start()
    {
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //����� ����� � ������
        typeOfMap = 0;
        
        if(SLscript.isNewGame)
        {
            InitializeAreas();      //�������� ��������� �������
            InitializeBuildings();  //������� �������� ������, ������� ����� ���������
            InitializeVillages();    //�������� ��������
        }
        else
        {
            GameData GD;
            if (SLscript.NumberOfLoadedGame == -1)
                GD = SLscript.currentGameData;
            else
                GD = SLscript.savedGames[SLscript.NumberOfLoadedGame];

            MapAreaDict = GD.MapAreaDict;
            TownAreaDict = GD.TownAreaDict;
            EmptyAreaDict = GD.EmptyAreaDict;
            VillageDict = GD.VillageDict;
            buildingDict = GD.buildingDict;
        }

  


        //����� ������ ���� ��� ����� � �������� "�� ����������� ��� � �� ������ ���� ����������"
        activeBuildingOnScreen = buildings.noBuilding;
        buildingDescription.text = "";
        activeAreaTag = "";
        statusText.text = "";
        UpperStatusText.text = "";
        buildText.text = "";
        for (int i = 0; i < 4; i++)
            GameMainScript.MapSC.buildingButtonsArray[i].SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            GameMainScript.MapSC.RequireRecoursesLabel.text = "";
            GameMainScript.MapSC.reqRecForAreaTexts[i].text = "";
            GameMainScript.MapSC.reqRecForAreaPictures[i].SetActive(false);
            GameMainScript.MapSC.minusesForAreas[i].SetActive(false);
            GameMainScript.MapSC.plusesForAreas[i].SetActive(false);
        }

        //�������������� ��������
        buildButton.SetActive(false);
        for (int i = 0; i < 5; i++)
        {

            GameMainScript.MapSC.reqRecBuldingTexts[i].text = "";
            GameMainScript.MapSC.reqRecBuldingPictures[i].SetActive(false);

        }

        //����� ������� � ������
        SlotsRecourses = new int[5];
        SlotsRecoursesVillage = new int[4];
        SlotsRecoursesGivenVillage = new int[4];
        for (int i = 0; i < 5; i++)
            SlotsRecourses[i] = i;

        //������ ������� �������-��������, ��������� ��� ��������� ������
        //requiredRecourseBuilding reqRec1 = new requiredRecourseBuilding();
    }


    void InitializeBuildings()
    {
        buildingDict[buildings.bank] = new building(description: "����. ���� ������ 50 ������ ������� � ������ ������� ������������.", name: "����", requiredMaterials: 20, requiredBudget: 75, requiredPermissions: 1);
        buildingDict[buildings.factory] = new building(description: "�������. ���� ������ 3 ������� ���������� ������ ����.", name: "�������", requiredMaterials: 30, requiredPermissions: 2);
        buildingDict[buildings.bakaly] = new building(description: "�������. ���� ������ 4 ������� �������� ������ ������ ���� ������ (�������, �������, �������).", name: "�������", requiredMaterials: 20, requiredPermissions: 1);
        buildingDict[buildings.pharmacy] = new building(description: "������. ���� ������ 4 ������� �������� ������ ������ ���� ������ (�����, �������)", name: "������", requiredPermissions: 1, requiredMaterials: 20);
    }

    void InitializeAreas()
    {

        MapAreaDict["blokpostTop"] = new blokpost(name: "�������� (�)", status: "��������");
        MapAreaDict["blokpostLeft"] = new blokpost(name: "�������� (�)", status: "��������");
        MapAreaDict["blokpostRight"] = new blokpost(name: "�������� (�)", status: "��������");
        MapAreaDict["blokpostBottom"] = new blokpost(name: "�������� (�)", status: "��������");

        EmptyAreaDict["emptyTop"] = new emptyArea(name: "������� (�)");
        EmptyAreaDict["emptyBottom"] = new emptyArea(name: "������� (�)");

        TownAreaDict["square"] = new townArea(name: "������� �������", description: "�������� ����� �����, ��� ������������� ���� ������, � ����� ������" +
            "������ ����������. ����� ���� ����� ���������� ����� ������ ��� ���������� ��������.", requiredBudget: 20);
        TownAreaDict["workLeft"] = new townArea(name: "������� ����� (�)", description: "���� �� ���� ������� ������� ������. ����� ����� ������� �������," +
            "������ ���� �� ����� ���������, ����� �������� �� ������� � ��������. ���� ����� ������ ���� ���� ������� ��������� � ��������� ��������.", requiredMaterials: 10);
        TownAreaDict["workRight"] = new townArea(name: "������� ����� (�)", description: "���� �� ���� ������� ������� ������. ����� ����� ������� �������," +
            "������ ���� �� ����� ���������, ����� �������� �� ������� � ��������. ���� ����� ������ ���� ���� ������� ��������� � ��������� ��������.", requiredMaterials: 10);
        TownAreaDict["stock"] = new townArea(name: "��������� �����", description: "����� ����� ������ ����� ������. ����� �������� ��������� � ��������" +
            "��� ����� ������. ����� ������ ���� ��������� ��� ������.", requiredMedicine: 5, requiredProvision: 10);
        MapAreaDict["factory"] = new factoryArea();

        MapAreaDict["square"] = TownAreaDict["square"];
        MapAreaDict["workLeft"] = TownAreaDict["workLeft"];
        MapAreaDict["workRight"] = TownAreaDict["workRight"];
        MapAreaDict["stock"] = TownAreaDict["stock"];
        MapAreaDict["emptyTop"] = EmptyAreaDict["emptyTop"];
        MapAreaDict["emptyBottom"] = EmptyAreaDict["emptyBottom"];
    }

    public void InitializeVillages()
    {
        VillageDict[Convert.ToString(0)] = new village(name: "������", population: 2534, provision: 40, provisionIncome: 0.005);
        VillageDict[Convert.ToString(1)] = new village(name: "������� �����", population: 1256, provision: 10, medicine: 5, provisionIncome: 0.008, medicineIncome: 0.007);
        VillageDict[Convert.ToString(2)] = new village(name: "�������", population: 1806, provision: 35, medicine: 10, provisionIncome: 0.01, medicineIncome: 0.004);
        VillageDict[Convert.ToString(3)] = new village(name: "�������", population: 3451, budget: 20, materials: 10, budgetIncome: 0.003, materialsIncome: 0.004);
        VillageDict[Convert.ToString(4)] = new village(name: "���������", population: 1289, provision: 60, budget: 20, materials: 10, provisionIncome: 0.015, budgetIncome: 0.003, materialsIncome: 0.002);
        VillageDict[Convert.ToString(5)] = new village(name: "������ �������", population: 1891, materials: 20, medicine: 10, materialsIncome: 0.006, medicineIncome: 0.002);
        VillageDict[Convert.ToString(6)] = new village(name: "������� ��������", population: 3604, materials: 10, budget: 20, provision: 5, medicine: 6, materialsIncome: 0.003, budgetIncome: 0.003, provisionIncome: 0.0015, medicineIncome: 0.001);
        VillageDict[Convert.ToString(7)] = new village(name: "�������", population: 2379, materials: 35, budget: 10, materialsIncome: 0.008, budgetIncome: 0.004);
        VillageDict[Convert.ToString(8)] = new village(name: "��������", population: 1248, provision: 10, materials: 23, provisionIncome: 0.005, materialsIncome: 0.006);
        VillageDict[Convert.ToString(9)] = new village(name: "�����", population: 2287, materials: 23, medicine: 8, materialsIncome: 0.002, medicineIncome: 0.002);
        VillageDict[Convert.ToString(10)] = new village(name: "¸�����", population: 581, provision: 15, medicine: 10, provisionIncome: 0.02, medicineIncome: 0.01);
        VillageDict[Convert.ToString(11)] = new village(name: "����� ����������", population: 784, provision: 5, medicine: 3, materials: 8, budget: 4, provisionIncome: 0.008, medicineIncome: 0.006, materialsIncome: 0.004, budgetIncome: 0.002);
    }

    public void ChangeMap(int newTypeMap)
    {
        if (newTypeMap == 1)
        {
            typeOfMap = 1;
            TOWNMAP.SetActive(false);
            WARMAP.SetActive(false);
            VILLAGEMAP.SetActive(true);

            TownButton.SetActive(true);
            WarButton.SetActive(true);
            VillageButton.SetActive(false);

            if (activeVillageTag != "")
            {
                VillageDict[activeVillageTag].showVillage();
            }


        }
        else if (newTypeMap == 2)
        {
            typeOfMap = 2;

            TOWNMAP.SetActive(false);
            WARMAP.SetActive(true);
            VILLAGEMAP.SetActive(false);

            TownButton.SetActive(true);
            WarButton.SetActive(false);
            VillageButton.transform.position = WarButton.transform.position;
            VillageButton.SetActive(true);
            
        }
        else if (newTypeMap == 0)
        {
            Debug.Log("�������");
            typeOfMap = 0;

            TOWNMAP.SetActive(true);
            WARMAP.SetActive(false);
            VILLAGEMAP.SetActive(false);

            TownButton.SetActive(false);
            WarButton.SetActive(true);
            VillageButton.transform.position = TownButton.transform.position;
            VillageButton.SetActive(true);

            if (activeAreaTag != "")
            {
                MapAreaDict[activeAreaTag].showArea();
            }
        }
    }



    public void ShowVillage(int villageTag)
    {
        if (activeVillageTag != "")
            VillageDict[activeVillageTag].hideVillage(); //�������� ��� �����, ������� ��� ��������
        activeVillageTag = Convert.ToString(villageTag);
        villageName.text = VillageDict[Convert.ToString(villageTag)].name;
        VillageDict[Convert.ToString(villageTag)].showVillage();
    }

    public void ShowArea(string areaTag)
    {
        if(activeAreaTag != "")
            MapAreaDict[activeAreaTag].hideArea(); //�������� ��� �����, ������� ��� ��������
        activeAreaTag = areaTag;
        areaName.text = MapAreaDict[areaTag].name;
        areaDescription.text = MapAreaDict[areaTag].description;
        MapAreaDict[areaTag].showArea();
    }

    public void ShowBuilding(int buildingNumber)//�������� ���� � ������, ������� ���������� �������
    {
        activeBuildingOnScreen = (buildings)buildingNumber;
        buildingDict[activeBuildingOnScreen].showBuilding((int)activeBuildingOnScreen);


        
   
    }

    public void GiveSoldiersVillage()
    {
        VillageDict[activeVillageTag].GiveSoldiers();
        GameSC.RefreshRecourses();
    }

    public void UngiveSoldiersVillage()
    {
        VillageDict[activeVillageTag].UngiveSoldiers();
        GameSC.RefreshRecourses();
    }

    public void hideBuilding()
    {
        GameMainScript.MapSC.buildButton.SetActive(false);
        for (int i = 0; i < 5; i++)
        {

            GameMainScript.MapSC.reqRecBuldingTexts[i].text = "";
            GameMainScript.MapSC.reqRecBuldingPictures[i].SetActive(false);

        }
        RequireRecoursesLabel.text = "";
    }

    public void AddBuilding()
    {
        MapAreaDict[activeAreaTag].buildings[(int)activeBuildingOnScreen] = true;
        GameMainScript.MapSC.buildButton.SetActive(false);
        buildingButtonsArray[(int)activeBuildingOnScreen].GetComponent<Image>().sprite = CheckMarkSprite; //������ ������ �� ������������ �������.

        for (int i = 0; i < 5; i++)
            recoursesOfPlayer.Recourses[i] -= buildingDict[activeBuildingOnScreen].requiredRecourses[i];
        buildingDict[activeBuildingOnScreen].showBuilding((int)activeBuildingOnScreen);
        GameSC.RefreshRecourses();

    }

    public void GivePolice()
    {
        MapAreaDict[activeAreaTag].GivePolice();
        GameSC.RefreshRecourses();
    }

    public void UngivePolice()
    {
        MapAreaDict[activeAreaTag].UngivePolice();
        GameSC.RefreshRecourses();
    }

    public void GiveRecourses(int numberOfPicture)
    {
       MapAreaDict[activeAreaTag].GiveRecourses(numberOfPicture);
        GameSC.RefreshRecourses();
    }

    public void UngiveRecourses(int numberOfPicture)
    {
        MapAreaDict[activeAreaTag].UngiveRecourses(numberOfPicture);
        GameSC.RefreshRecourses();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
