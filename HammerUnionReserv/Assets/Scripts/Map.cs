using Assembly_CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Playables;
using UnityEngine.UI; //Добавляем чтобы юзать интерфейс и тексты всякие
using UnityEngine.Video;
//using static UnityEditor.VersionControl.Asset;

internal class Map : MonoBehaviour
{
    internal static GameMainScript GameSC; //Это один-единственный экземпляр объекта "Скрипт игры", на котором всё висит что нужно для игры
    public Text buildingDescription; //Описание здания, которое игрок собирается построить
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
    public Sprite CheckMarkSprite;//спрайт галочки
    public Sprite NoCheckMarkSprite;
    public Text RequireRecoursesLabel;

    public GameObject TOWNMAP;
    public GameObject VILLAGEMAP;
    public GameObject WARMAP;
    public GameObject TownButton;
    public GameObject VillageButton;
    public GameObject WarButton;

    public int typeOfMap;

    //Чё-то для деревни
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


    //Для полиции:

    public GameObject policeObject;
    public GameObject policeMinus;
    public GameObject policePlus;
    public Text policeValueText;
    public Text soldiersVillageValueText;

    //ЗДЕСЬ МАССИВЫ ДЛЯ ПОЖЕРТВОВАНИЙ РАЙОНАМ БУДУТ
    public GameObject[] reqRecForAreaPictures;
    public Text[] reqRecForAreaTexts;
    public GameObject[] minusesForAreas;
    public GameObject[] plusesForAreas;
    public RectTransform[] positionsForPluses;

    //Очень трешовый массив, показывающий, в какой ячейке на данный момент какой ресурс лежит (для пожертвований районам)
    public int[] SlotsRecourses;
    public int[] SlotsRecoursesVillage; //Аналогичные массивы, но для деревень.
    public int[] SlotsRecoursesGivenVillage;



    public string activeAreaTag; //tag того района, который сейчас на экране
    public string activeVillageTag; //tag той деревни, которая сейчас на экране
    public buildings activeBuildingOnScreen;

    public Dictionary<string, mapArea> MapAreaDict = new Dictionary<string, mapArea>(); //Словарь всех районов
    public Dictionary<string, townArea> TownAreaDict = new Dictionary<string, townArea>(); //Словарь обычных районов, у которых можно менять настроение и давать ресурсы.
    public Dictionary<string, emptyArea> EmptyAreaDict = new Dictionary<string, emptyArea>(); //Словарь пустых районов

    public Dictionary<string, village> VillageDict = new Dictionary<string, village>(); //Словарь всех деревень



    public Dictionary<buildings, building> buildingDict = new Dictionary<buildings, building>();

    // Start is called before the first frame update
    void Start()
    {
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //ОЧЕНЬ ВАЖНО В НАЧАЛЕ
        typeOfMap = 0;
        
        if(SLscript.isNewGame)
        {
            InitializeAreas();      //Создание стартовых районов
            InitializeBuildings();  //Задание описания зданий, которые можно построить
            InitializeVillages();    //Создание деревень
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

  


        //Задаём всякую муть для карты в значение "не показывайте мне её на старте игры пожалуйста"
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

        //Строительнство скрываем
        buildButton.SetActive(false);
        for (int i = 0; i < 5; i++)
        {

            GameMainScript.MapSC.reqRecBuldingTexts[i].text = "";
            GameMainScript.MapSC.reqRecBuldingPictures[i].SetActive(false);

        }

        //Задаём ресурсы в слотах
        SlotsRecourses = new int[5];
        SlotsRecoursesVillage = new int[4];
        SlotsRecoursesGivenVillage = new int[4];
        for (int i = 0; i < 5; i++)
            SlotsRecourses[i] = i;

        //Создаём объекты значков-ресурсов, требуемых для постройки зданий
        //requiredRecourseBuilding reqRec1 = new requiredRecourseBuilding();
    }


    void InitializeBuildings()
    {
        buildingDict[buildings.bank] = new building(description: "Банк. Дает ратуше 50 единиц бюджета в начале каждого понедельника.", name: "Банк", requiredMaterials: 20, requiredBudget: 75, requiredPermissions: 1);
        buildingDict[buildings.factory] = new building(description: "Фабрика. Дает ратуше 3 единицы материалов каждый день.", name: "Фабрика", requiredMaterials: 30, requiredPermissions: 2);
        buildingDict[buildings.bakaly] = new building(description: "Бакалея. Дает ратуше 4 единицы провизии каждый второй день недели (вторник, четверг, суббота).", name: "Бакалея", requiredMaterials: 20, requiredPermissions: 1);
        buildingDict[buildings.pharmacy] = new building(description: "Аптека. Дает ратуше 4 единицы медицины каждый третий день недели (среда, суббота)", name: "Аптека", requiredPermissions: 1, requiredMaterials: 20);
    }

    void InitializeAreas()
    {

        MapAreaDict["blokpostTop"] = new blokpost(name: "Блокпост (С)", status: "работает");
        MapAreaDict["blokpostLeft"] = new blokpost(name: "Блокпост (З)", status: "работает");
        MapAreaDict["blokpostRight"] = new blokpost(name: "Блокпост (В)", status: "работает");
        MapAreaDict["blokpostBottom"] = new blokpost(name: "Блокпост (Ю)", status: "работает");

        EmptyAreaDict["emptyTop"] = new emptyArea(name: "Пустырь (В)");
        EmptyAreaDict["emptyBottom"] = new emptyArea(name: "Пустырь (Ю)");

        TownAreaDict["square"] = new townArea(name: "Главная площадь", description: "Основной жилой район, где располагается ваша ратуша, а также другие" +
            "здания чиновниклв. Здесь чаще всего собираются толпы народа для обсуждения политики.", requiredBudget: 20);
        TownAreaDict["workLeft"] = new townArea(name: "Рабочий район (З)", description: "Один из двух рабочих районов города. Здесь живут простые рабочие," +
            "каждый день им нужны материалы, чтобы работать на заводах и фабриках. Чаще всего именно люди этих районов участвуют в городских событиях.", requiredMaterials: 10);
        TownAreaDict["workRight"] = new townArea(name: "Рабочий район (В)", description: "Один из двух рабочих районов города. Здесь живут простые рабочие," +
            "каждый день им нужны материалы, чтобы работать на заводах и фабриках. Чаще всего именно люди этих районов участвуют в городских событиях.", requiredMaterials: 10);
        TownAreaDict["stock"] = new townArea(name: "Складской район", description: "Почти самый важный район города. Здесь хранятся лекарства и провизия" +
            "для всего города. Также именно сюда приезжают все поезда.", requiredMedicine: 5, requiredProvision: 10);
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
        VillageDict[Convert.ToString(0)] = new village(name: "Щукино", population: 2534, provision: 40, provisionIncome: 0.005);
        VillageDict[Convert.ToString(1)] = new village(name: "Верхние пруды", population: 1256, provision: 10, medicine: 5, provisionIncome: 0.008, medicineIncome: 0.007);
        VillageDict[Convert.ToString(2)] = new village(name: "Тяпкино", population: 1806, provision: 35, medicine: 10, provisionIncome: 0.01, medicineIncome: 0.004);
        VillageDict[Convert.ToString(3)] = new village(name: "Выдумки", population: 3451, budget: 20, materials: 10, budgetIncome: 0.003, materialsIncome: 0.004);
        VillageDict[Convert.ToString(4)] = new village(name: "Антоновка", population: 1289, provision: 60, budget: 20, materials: 10, provisionIncome: 0.015, budgetIncome: 0.003, materialsIncome: 0.002);
        VillageDict[Convert.ToString(5)] = new village(name: "Старые Выселки", population: 1891, materials: 20, medicine: 10, materialsIncome: 0.006, medicineIncome: 0.002);
        VillageDict[Convert.ToString(6)] = new village(name: "Большая Ключёвка", population: 3604, materials: 10, budget: 20, provision: 5, medicine: 6, materialsIncome: 0.003, budgetIncome: 0.003, provisionIncome: 0.0015, medicineIncome: 0.001);
        VillageDict[Convert.ToString(7)] = new village(name: "Сосенки", population: 2379, materials: 35, budget: 10, materialsIncome: 0.008, budgetIncome: 0.004);
        VillageDict[Convert.ToString(8)] = new village(name: "Дроздово", population: 1248, provision: 10, materials: 23, provisionIncome: 0.005, materialsIncome: 0.006);
        VillageDict[Convert.ToString(9)] = new village(name: "Ясное", population: 2287, materials: 23, medicine: 8, materialsIncome: 0.002, medicineIncome: 0.002);
        VillageDict[Convert.ToString(10)] = new village(name: "Вёшенка", population: 581, provision: 15, medicine: 10, provisionIncome: 0.02, medicineIncome: 0.01);
        VillageDict[Convert.ToString(11)] = new village(name: "Малое Безымянное", population: 784, provision: 5, medicine: 3, materials: 8, budget: 4, provisionIncome: 0.008, medicineIncome: 0.006, materialsIncome: 0.004, budgetIncome: 0.002);
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
            Debug.Log("ЛУЛЛШСО");
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
            VillageDict[activeVillageTag].hideVillage(); //скрываем тот район, который был активным
        activeVillageTag = Convert.ToString(villageTag);
        villageName.text = VillageDict[Convert.ToString(villageTag)].name;
        VillageDict[Convert.ToString(villageTag)].showVillage();
    }

    public void ShowArea(string areaTag)
    {
        if(activeAreaTag != "")
            MapAreaDict[activeAreaTag].hideArea(); //скрываем тот район, который был активным
        activeAreaTag = areaTag;
        areaName.text = MapAreaDict[areaTag].name;
        areaDescription.text = MapAreaDict[areaTag].description;
        MapAreaDict[areaTag].showArea();
    }

    public void ShowBuilding(int buildingNumber)//показать инфу о здании, которое собираемся строить
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
        buildingButtonsArray[(int)activeBuildingOnScreen].GetComponent<Image>().sprite = CheckMarkSprite; //меняем спрайт на поставленную галочку.

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
