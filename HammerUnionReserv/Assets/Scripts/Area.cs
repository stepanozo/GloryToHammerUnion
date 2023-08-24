using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; //Добавляем чтобы юзать интерфейс и тексты всякие

namespace Assembly_CSharp
{


    internal static class recoursesOfPlayer
    {

        public static int[] Recourses;
        public static int soldiers;

        public static int Budget
        {
            get { return Recourses[0]; }
            set { Recourses[0] = value; }
        }

        public static int Materials
        {
            get { return Recourses[1]; }
            set { Recourses[1] = value; }
        }

        public static int Provision
        {
            get { return Recourses[2]; }
            set { Recourses[2] = value; }
        }

        public static int Medicine
        {
            get { return Recourses[3]; }
            set { Recourses[3] = value; }
        }

        public static int Permissions
        {
            get { return Recourses[4]; }
            set { Recourses[4] = value; }
        }

        
    }

    internal static class reputation
    {

        public static int[] rep;

        public static int Authorities
        {
            get { return rep[0]; }
            set { rep[0] = value; }
        }

        public static int ZOV
        {
            get { return rep[1]; }
            set { rep[1] = value; }
        }

        public static int OKO
        {
            get { return rep[2]; }
            set { rep[2] = value; }
        }

        public static int NIMB
        {
            get { return rep[3]; }
            set { rep[3] = value; }
        }

        public static int Sunrise
        {
            get { return rep[4]; }
            set { rep[4] = value; }
        }

    }

    public enum buildings
    {
        noBuilding = -1,
        bank = 0,
        factory = 1,
        bakaly = 2,
        pharmacy = 3

    }

    [Serializable]
    internal class building
    {
        public int RequiredBudget
        {
            get { return requiredRecourses[0]; }
            set { requiredRecourses[0] = value; }         
        }
        public int RequiredMaterials
        {
            get { return requiredRecourses[1]; }
            set { requiredRecourses[1] = value; }
        }
        public int RequiredProvision
        {
            get { return requiredRecourses[2]; }
            set { requiredRecourses[2] = value; }
        }
        public int RequiredMedicine
        {
            get { return requiredRecourses[3]; }
            set { requiredRecourses[3] = value; }
        }
        public int RequiredPermissions
        {
            get { return requiredRecourses[4]; }
            set { requiredRecourses[4] = value; }
        }

        public int[] requiredRecourses;

        public string name;
        public string description;
        
        public building(int requiredBudget = 0, int requiredMaterials = 0, int requiredProvision = 0, int requiredMedicine = 0,int requiredPermissions = 0, string description = "Какое-то здание", string name = "Здание")
        {
            this.description = description;
            this.name = name;

            requiredRecourses = new int[5];
            requiredRecourses[0] = requiredBudget;
            requiredRecourses[1] = requiredMaterials;
            requiredRecourses[2] = requiredProvision;
            requiredRecourses[3] = requiredMedicine;
            requiredRecourses[4] = requiredPermissions;
        }

        public void showBuilding(int buildingNumber)
        {
            GameMainScript.MapSC.hideBuilding();
            int numberOfPic = 0;
            if(!GameMainScript.MapSC.MapAreaDict[GameMainScript.MapSC.activeAreaTag].buildings[(int)GameMainScript.MapSC.activeBuildingOnScreen]) //Если такого здания в районе нет, 
                for(int i=0; i<5; i++)                              //тогда выводим на экран требуемые ресурсы, как бы предлагая его построить.
                {
                    GameMainScript.MapSC.RequireRecoursesLabel.text = "Нужно ресурсов:";
                    if (requiredRecourses[i] > 0)
                    {
                        GameMainScript.MapSC.reqRecBuldingTexts[numberOfPic].text = Convert.ToString(requiredRecourses[i]);
                        GameMainScript.MapSC.reqRecBuldingPictures[numberOfPic].GetComponent<Image>().sprite = GameMainScript.MapSC.spritesForRecourses[i];
                   
                        GameMainScript.MapSC.reqRecBuldingPictures[numberOfPic].SetActive(true);

                        numberOfPic++;
                    }

                }

            GameMainScript.MapSC.buildingDescription.text = this.description;
            if (recoursesOfPlayer.Budget >= GameMainScript.MapSC.buildingDict[(buildings)buildingNumber].RequiredBudget &&
                recoursesOfPlayer.Materials >= GameMainScript.MapSC.buildingDict[(buildings)buildingNumber].RequiredMaterials &&
                recoursesOfPlayer.Provision >= GameMainScript.MapSC.buildingDict[(buildings)buildingNumber].RequiredProvision &&
                recoursesOfPlayer.Medicine >= GameMainScript.MapSC.buildingDict[(buildings)buildingNumber].RequiredMedicine &&
                recoursesOfPlayer.Permissions >= GameMainScript.MapSC.buildingDict[(buildings)buildingNumber].RequiredPermissions

                && !GameMainScript.MapSC.MapAreaDict[GameMainScript.MapSC.activeAreaTag].buildings[(int)GameMainScript.MapSC.activeBuildingOnScreen]) //если такого здания нет
            {
                GameMainScript.MapSC.buildButton.SetActive(true);
            }

                
        }
       

    }




    [Serializable]
    internal class village
    {
        public double[] incomeCoefficient;
        public int[] totalResources;
        public int[] givenResources;
        public string name;
        public int population;
        public int police;
        public double revolutionChance;


        public int SumRes
        {
            get
            {
                int sumRes = 0;
                for (int i = 0; i < 4; i++)
                {
                    sumRes += totalResources[i];
                }
                return sumRes;
            }
        }

        public int SumGivenRes
        {
            get
            {
                int sumRes = 0;
                for (int i = 0; i < 4; i++)
                {
                    sumRes += givenResources[i];
                }
                return sumRes;
            }
        }

        public village(string name = "Безымянная деревня", int population = 1250,
            int budget = 0, int materials = 0, int provision = 0, int medicine = 0, double revolutionChance = 3, double budgetIncome = 0, double materialsIncome = 0, double provisionIncome = 0, double medicineIncome = 0)
        {
            this.name = name;
            this.population = population;

            totalResources = new int[4];
            totalResources[0] = budget;
            totalResources[1] = materials;
            totalResources[2] = provision;
            totalResources[3] = medicine;

            givenResources = new int[4];
            givenResources[0] = 0;
            givenResources[1] = 0;
            givenResources[2] = 0;
            givenResources[3] = 0;

            incomeCoefficient = new double[4];
            incomeCoefficient[0] = budgetIncome;
            incomeCoefficient[1] = materialsIncome;
            incomeCoefficient[2] = provisionIncome;
            incomeCoefficient[3] = medicineIncome;


            this.revolutionChance = revolutionChance;
            police = 0;
            
        }

        public void ShowGivenRes()
        {


            //ТУТ ВЫВОДИМ ЭТУ ХРЕНЬ НА ЭКРАН
            int numberOfPic = 0;
            bool noRes = true;
            for (int i = 0; i < 4; i++)
            {
                if (givenResources[i] > 0)
                {
                    noRes = false;
                    GameMainScript.MapSC.villageResGivenTexts[numberOfPic].text = Convert.ToString(givenResources[i]);
                    GameMainScript.MapSC.villageResGivenPictures[numberOfPic].GetComponent<Image>().sprite = GameMainScript.MapSC.spritesForRecourses[i];

                    GameMainScript.MapSC.villageResGivenPictures[numberOfPic].SetActive(true);


                    numberOfPic++;
                }
                else
                {
                    GameMainScript.MapSC.villageResGivenPictures[numberOfPic].SetActive(false);
                }
            }
            GameMainScript.MapSC.villageResoursesGivenText.text = noRes ? "Не приносит ресурсов." : "Ресурсов приносит: ";
        }

        public void RefreshGivenRes() //ИЩЕМ ТУТ ОШИБКУ КОТОРАЯ ВСЁ ЛОМАЕТ
        {
            int soldiersCount = police;

            for (int i = 0; i < 4; i++)
            {
                givenResources[i] = 0;
            }

            //Настраиваем новыые значения
     
            while (soldiersCount > 0 && SumGivenRes < SumRes)
                for (int i = 0; i < 4; i++)
                {
                    if (totalResources[i] - givenResources[i] > 0)
                    {
                        givenResources[i]++;
                        soldiersCount--;
                    }
                    if (soldiersCount == 0 || SumGivenRes >= SumRes) //ТУТ НАКРИНЖИЛ С УСЛОВИЕМ???
                        break;
                }


          
        }

        public void RefreshPopulation()
        {
            GameMainScript.MapSC.villagePopulationText.text = "Население: " + Convert.ToString(population);
        }

        public void RefreshResourses()
        {
            int numberOfPic = 0;
            bool HoldsResources = false;
            bool givesResourses = false;
            for (int i = 0; i < 4; i++)
            {
                if (totalResources[i] > 0)
                {
                    GameMainScript.MapSC.SlotsRecoursesVillage[numberOfPic] = i; //ЭТО ТРЕШ, ЕСЛИ НЕ РАБОТАЕТ ПЕРЕДЕЛАЙ
                    HoldsResources = true;
                    GameMainScript.MapSC.villageResTexts[numberOfPic].text = Convert.ToString(totalResources[i]);
                    GameMainScript.MapSC.villageResPictures[numberOfPic].GetComponent<Image>().sprite = GameMainScript.MapSC.spritesForRecourses[i];

                    GameMainScript.MapSC.villageResPictures[numberOfPic].SetActive(true);

                    if (police > 0)
                    {
                        givesResourses = true;
                        GameMainScript.MapSC.villageResGivenTexts[numberOfPic].text = Convert.ToString(givenResources[i]);
                        GameMainScript.MapSC.villageResGivenPictures[numberOfPic].GetComponent<Image>().sprite = GameMainScript.MapSC.spritesForRecourses[i];

                        GameMainScript.MapSC.villageResGivenPictures[numberOfPic].SetActive(true);
                    }

                    numberOfPic++;
                }

            }
            if (HoldsResources)
            {
                GameMainScript.MapSC.villageResoursesText.text = "Ресурсов имеется:";
                if (givesResourses)
                    GameMainScript.MapSC.villageResoursesGivenText.text = "Ресурсов приносит:";
                else
                    GameMainScript.MapSC.villageResoursesGivenText.text = "Не приносит ресурсов.";
            }
            else
                GameMainScript.MapSC.villageResoursesText.text = "Ресурсы отсутствуют.";
        }

        public void GiveSoldiers()
        {

            Debug.Log("Сейчас в деревне " + Convert.ToString(SumRes) + "ресурсов, она приносит " + Convert.ToString(SumGivenRes) + "ресурсов");


            if (police < 20 && recoursesOfPlayer.soldiers > 0)// && SumGivenRes < SumRes) //(Здесь будет проверка: если у игрока есть полицейские и если полицейских на районе меньше 10-ти)
            {
                recoursesOfPlayer.soldiers--;//Тут надо отнимать у игрока одного полицейского
                police++;
                RefreshGivenRes();
                ShowGivenRes();
            }


            RefreshSoldiers();


        }

        public void RefreshSoldiers()
        {

            GameMainScript.MapSC.soldiersVillageValueText.text = Convert.ToString(police) + "/20";
        }

        public void UngiveSoldiers()
        {
            if (police > 0) 
            {
                recoursesOfPlayer.soldiers++;//Тут надо отнимать у игрока одного полицейского
                police--;

                if (SumGivenRes > 0)
                {
                    RefreshGivenRes();
                    ShowGivenRes();
                }

            }


            RefreshSoldiers();


        }

        public void hideVillage()
        {
            GameMainScript.MapSC.villageName.text = "";
            GameMainScript.MapSC.villagePopulationText.text = "";
            GameMainScript.MapSC.revolutionChanceText.text = "";
            GameMainScript.MapSC.RequireRecoursesLabel.text = "";
            for(int i=0; i<4; i++)
            {
                GameMainScript.MapSC.villageResPictures[i].SetActive(false);
                GameMainScript.MapSC.villageResGivenPictures[i].SetActive(false);
            }
        }

        public void showVillage()
        {
            hideVillage();
            

            GameMainScript.MapSC.villageName.text = name;
            GameMainScript.MapSC.villagePopulationText.text = "Население: " + Convert.ToString(population);
            GameMainScript.MapSC.revolutionChanceText.text = "Шанс восстания: " + Convert.ToString(revolutionChance) + "%";
            GameMainScript.MapSC.RequireRecoursesLabel.text = "Нужно ресурсов:";
            GameMainScript.MapSC.soldiersVillageValueText.text = Convert.ToString(police) + "/20";
            //GameMainScript.MapSC..text = "Нужно ресурсов:"; КОРОЧЕ СДЕЛАЙ ТУТ ПОКАЗ СОЛДАТИКОВ

            RefreshResourses();
         


        }

        public void weeklyIncome()
        {
            System.Random rnd = new System.Random();
            for (int i = 0; i<4; i++)
            {
                totalResources[i] += (int)(population * incomeCoefficient[i] * rnd.Next(5,15)/10);
            }
            Debug.Log("Теперь популяция деревни вместо " + Convert.ToString(population));
            float mnoj = (float)rnd.Next(100-(int)revolutionChance/2, 103) / 100;
            population = (int)(population * mnoj);
            Debug.Log("Равна " + Convert.ToString(population));

            Debug.Log("Множитель популяции был равен " + Convert.ToString(mnoj));


        }

        public void dailyIncome()
        {
            for (int i = 0; i < 4; i++)
            {
                totalResources[i] -= givenResources[i];
                recoursesOfPlayer.Recourses[i] += givenResources[i];
               
            }
            revolutionChance += (double)SumGivenRes / 10;

            if (SumGivenRes == 0 && revolutionChance >= 5)
                revolutionChance -= 2;
            else
                revolutionChance += (double)SumGivenRes / 5;

            RefreshGivenRes(); //ЯВНО В ЭТОЙ ПРОЦЕДУРЕ КАКАЯ-ТО ОШИБКА
            RefreshResourses();
        }

        public void dailyRevolution()
        {
            System.Random rnd = new System.Random();
            int randomNumber = rnd.Next(0, 100);

            if (randomNumber <= revolutionChance) //Если это условие соблюдено, тогда начинается революция в деревне
            {

            }
        }
    }

    [Serializable]
    internal abstract class mapArea
    {
        public bool[] buildings = new bool[4];
        public string name;
        public string description;

        public mapArea(string name, string description)
        {
            this.name = name;
            this.description = description;

        }

        public virtual void AddMess(int mess)
        {

        }
        public virtual void  GiveRecourses(int numberOfPicture)
        {

        }

        public virtual void UngiveRecourses(int numberOfPicture)
        {

        }

        public virtual void GivePolice()
        {

        }

        public virtual void UngivePolice()
        {

        }

        public virtual void AddSpirits(int spirits)
        {

        }

        public virtual void SetStatus(string spirits)
        {

        }

        public virtual void SetPollution(string pollution)
        {

        }

        public abstract void showArea();
       
   
        public abstract void hideArea();

    }

    [Serializable]
    internal class emptyArea : mapArea
    {

        public emptyArea(string name = "Пустырь", string description =
            "Пустырь, расчищенный когда-то рабочими. Здесь никто не живет, но это место можно застроить.")
            : base(name, description)
        {
            for (int i = 0; i < 4; i++)
                this.buildings[i] = false;
        }

        public bool noBuildings
        {
            get 
            {
                bool flag = false;
                for (int i = 0; !flag && i < 4; i++)
                    if (this.buildings[i])
                        flag = true;
                return !flag;
            }
        }

        public override void showArea()
        {
           //Показываем кнопки зданий
           for(int i=0; i<4; i++)
           {
                GameMainScript.MapSC.buildingButtonsArray[i].SetActive(true);
           }
           GameMainScript.MapSC.buildText.text = "Строить:";
           GameMainScript.MapSC.buildingDescription.text = "Выберите здание.";

            //Галочки выставляем там, где надо

            for (int i = 0; i < 4; i++)
            {
                if (buildings[i])
                    GameMainScript.MapSC.buildingButtonsArray[i].GetComponent<Image>().sprite = GameMainScript.MapSC.CheckMarkSprite;
                else
                    GameMainScript.MapSC.buildingButtonsArray[i].GetComponent<Image>().sprite = GameMainScript.MapSC.NoCheckMarkSprite;

            }
        }

        public override void hideArea()
        {
            for (int i = 0; i < 4; i++)
                GameMainScript.MapSC.buildingButtonsArray[i].SetActive(false);
 
            GameMainScript.MapSC.buildText.text = "";
            GameMainScript.MapSC.buildingDescription.text = "";
            GameMainScript.MapSC.buildButton.SetActive(false);
            GameMainScript.MapSC.hideBuilding();
            GameMainScript.MapSC.RequireRecoursesLabel.text = "";

        }

        public override void AddMess(int mess)
        {
        }
    }

    [Serializable]
    internal abstract class usualArea : mapArea
    {
        public int[] requiredRecourses;
        public int[] currentRecourses;
        public int police;

        public int RequiredBudget
        {
            get { return requiredRecourses[0]; }
            set { requiredRecourses[0] = value; }
        }
        public int RequiredMaterials
        {
            get { return requiredRecourses[1]; }
            set { requiredRecourses[1] = value; }
        }
        public int RequiredProvision
        {
            get { return requiredRecourses[2]; }
            set { requiredRecourses[2] = value; }
        }
        public int RequiredMedicine
        {
            get { return requiredRecourses[3]; }
            set { requiredRecourses[3] = value; }
        }
        public int RequiredPermissions
        {
            get { return requiredRecourses[4]; }
            set { requiredRecourses[4] = value; }
        }
        public int CurrentBudget
        {
            get { return currentRecourses[0]; }
            set { currentRecourses[0] = value; }
        }
        public int CurrentMaterials
        {
            get { return currentRecourses[1]; }
            set { currentRecourses[1] = value; }
        }
        public int CurrentProvision
        {
            get { return currentRecourses[2]; }
            set { currentRecourses[2] = value; }
        }
        public int CurrentMedicine
        {
            get { return currentRecourses[3]; }
            set { currentRecourses[3] = value; }
        }
        public int CurrentPermissions
        {
            get { return currentRecourses[4]; }
            set { currentRecourses[4] = value; }
        }


        public usualArea(string name, int requiredBudget, int requiredMaterials, int requiredProvision, int requiredMedicine, int requiredPermissions, string description) : base(name, description)
        {
            requiredRecourses = new int[5];
            currentRecourses= new int[5];

            RequiredBudget = requiredBudget;
            RequiredMaterials = requiredMaterials;
            RequiredProvision = requiredProvision;
            RequiredMedicine = requiredMedicine;
            RequiredPermissions = requiredPermissions;

            CurrentBudget = 0;
            CurrentMaterials = 0;
            CurrentProvision = 0;
            CurrentMedicine = 0;
            CurrentPermissions = 0;
        }

        public override void GiveRecourses(int numberOfPicture)
        {
            int numberOfRecourse = GameMainScript.MapSC.SlotsRecourses[numberOfPicture]; //задаём ресурс такой, какой лежит в слоте, на который тыкнули

            if (currentRecourses[numberOfRecourse] < requiredRecourses[numberOfRecourse] && recoursesOfPlayer.Recourses[numberOfRecourse] >= 5)
            {
                recoursesOfPlayer.Recourses[numberOfRecourse] -= 5;
                currentRecourses[numberOfRecourse] += 5;
                GameMainScript.MapSC.reqRecForAreaTexts[numberOfPicture].text = Convert.ToString(currentRecourses[numberOfRecourse]) + "/" + Convert.ToString(requiredRecourses[numberOfRecourse]);
            }

        }

        public override void UngiveRecourses(int numberOfPicture)
        {
            int numberOfRecourse = GameMainScript.MapSC.SlotsRecourses[numberOfPicture]; //задаём ресурс такой, какой лежит в слоте, на который тыкнули

            if (currentRecourses[numberOfRecourse] > 0)
            {
               
                currentRecourses[numberOfRecourse] -= 5;
                recoursesOfPlayer.Recourses[numberOfRecourse] += 5;
                GameMainScript.MapSC.reqRecForAreaTexts[numberOfPicture].text = Convert.ToString(currentRecourses[numberOfRecourse]) + "/" + Convert.ToString(requiredRecourses[numberOfRecourse]);
            }

        }

        public override void GivePolice()
        {
            if(police < 10 && recoursesOfPlayer.soldiers > 0) //(Здесь будет проверка: если у игрока есть полицейские и если полицейских на районе меньше 10-ти)
            {
                recoursesOfPlayer.soldiers--;//Тут надо отнимать у игрока одного полицейского
                police ++;
                GameMainScript.MapSC.policeValueText.text = Convert.ToString(police) + "/10";
            }

        }

        public override void UngivePolice()
        {
            if (police > 0) //(Здесь будет проверка: если у игрока есть полицейские и если полицейских на районе меньше 10-ти)
            {
                recoursesOfPlayer.soldiers++;//Тут надо отнимать у игрока одного полицейского
                police --;
                GameMainScript.MapSC.policeValueText.text = Convert.ToString(police) + "/10";
            }

        }
        public override void showArea()
        {
            this.hideArea();
            int numberOfPic = 0;


            GameMainScript.MapSC.RequireRecoursesLabel.text = "Нужно ресурсов:";
            bool reallyNeedRecourses = false;
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Здание хочет " + Convert.ToString(requiredRecourses[i]) + " ресурса под номером " + Convert.ToString(i));

                if (requiredRecourses[i] > 0)
                {
                    GameMainScript.MapSC.SlotsRecourses[numberOfPic] = i; //ЭТО ТРЕШ, ЕСЛИ НЕ РАБОТАЕТ ПЕРЕДЕЛАЙ
                    reallyNeedRecourses = true;
                    GameMainScript.MapSC.reqRecForAreaTexts[numberOfPic].text = Convert.ToString(currentRecourses[i]) + "/" + Convert.ToString(requiredRecourses[i]);
                    GameMainScript.MapSC.reqRecForAreaPictures[numberOfPic].GetComponent<Image>().sprite = GameMainScript.MapSC.spritesForRecourses[i];

                    GameMainScript.MapSC.reqRecForAreaPictures[numberOfPic].SetActive(true);
                    GameMainScript.MapSC.minusesForAreas[numberOfPic].SetActive(true);
                    GameMainScript.MapSC.plusesForAreas[numberOfPic].SetActive(true);

                    numberOfPic++;
                }

            }
            if (reallyNeedRecourses)
                GameMainScript.MapSC.RequireRecoursesLabel.text = "Нужно ресурсов:";
            else
                GameMainScript.MapSC.RequireRecoursesLabel.text = "Ресурсы не требуются.";
        }

        public override void hideArea()
        {
            for (int i = 0; i < 5; i++)
            {
                GameMainScript.MapSC.RequireRecoursesLabel.text = "";
                GameMainScript.MapSC.reqRecForAreaTexts[i].text = "";
                GameMainScript.MapSC.reqRecForAreaPictures[i].SetActive(false);
                GameMainScript.MapSC.minusesForAreas[i].SetActive(false);
                GameMainScript.MapSC.plusesForAreas[i].SetActive(false);
            }
        }

    }

    [Serializable]
    internal class blokpost : usualArea
    {
        string status;
        public blokpost(string name = "Блокпост", string status = "Неизвестен", int requiredBudget = 0, int requiredMaterials = 0, int requiredProvision = 0, int requiredMedicine = 0, int requiredPermissions = 0, string description =
            "Армейский блокпост. Охраняет город и ведет патруль местности. Каждый блокпост сразу свяжется с ратушей в случае наблюдения подозрительных или опасных вещей.")
            : base(name, requiredBudget, requiredMaterials, requiredProvision, requiredMedicine, requiredPermissions, description)
        {
            this.status = status;
        }
        public override void showArea()
        {
            base.showArea();
            GameMainScript.MapSC.statusText.text = "Статус: " + status;
          
        }
        public override void hideArea()
        {
            base.hideArea();
            GameMainScript.MapSC.statusText.text = "";
            
        }

        public override void SetStatus(string status)
        {
            this.status = status;
        }

        public override void AddMess(int mess)
        {

        }
    }
    [Serializable]
    internal class townArea : usualArea
    {
        public int spirits;
        public int mess;
        public bool messCame;

        public townArea(string name = "Жилой район", int spirits = 70, int requiredBudget = 0, int requiredMaterials = 0, int requiredProvision = 0, int requiredMedicine = 0, int requiredPermissions=0,string description =
            "Этот район не играет ключевой роли в жизни города: люди возвращаются сюда лишь для того, чтобы переночевать, а утром вновь отправляются на работу.")
            : base(name, requiredBudget, requiredMaterials, requiredProvision, requiredMedicine, requiredPermissions, description)
        {
            {
                this.spirits = spirits;
                mess = 0;
                police = 0; 
                messCame = false;
            }
        }


        public override void hideArea()
        {
            base.hideArea();
            GameMainScript.MapSC.statusText.text = "";
            GameMainScript.MapSC.UpperStatusText.text = "";
            GameMainScript.MapSC.policeObject.SetActive(false);

        }

        public override void showArea()
        {
            base.hideArea();
            base.showArea();

            if (messCame)
            {
                GameMainScript.MapSC.UpperStatusText.text = "Настроение: " + Convert.ToString(spirits) + "/100";
                GameMainScript.MapSC.statusText.text = "Беспорядки: " + Convert.ToString(mess) + "/50";
                GameMainScript.MapSC.policeObject.SetActive(true);
                GameMainScript.MapSC.policeValueText.text = Convert.ToString(police) + "/10";
            }
            else
            {
                GameMainScript.MapSC.statusText.text = "Настроение: " + Convert.ToString(spirits) + "/100";
            }
            Debug.Log("Беспорядки " + Convert.ToString(mess));

        }

        public override void AddSpirits(int spirits)
        {
            this.spirits += spirits;
            if (this.spirits <= 0)
            {
                this.spirits = 0;
                string nameForNotice = "В одном из районов";
                if (this.name == "Рабочий район (В)")
                    nameForNotice = "В рабочем районе (В)";
                else if (this.name == "Рабочий район (З)")
                    nameForNotice = "В рабочем районе (З)";
                else if (this.name == "Заводской район")
                    nameForNotice = "В заводском районе";
                else if (this.name == "Складской район")
                    nameForNotice = "В складском районе";
                else if (this.name == "Главная площадь")
                    nameForNotice = "На главной площади";

                if (!messCame)
                {
                    //ДОБАВЛЯЕМ УВЕДОМЛЕНИЕ О БЕСПОРЯДКАХ
                    Map.GameSC.todayNotices.Add(new notice("Беспорядки!", nameForNotice + " начались беспорядки. Повысьте настроение района до 20, либо отправьте в район солдат, чтобы остановить рост беспорядков.", this.name == "Главная площадь" ? "Спрайты\\Illustrations\\Толпа" : "Спрайты\\Illustrations\\Молотов"));
                    
                    messCame = true;
                }
            }
            else if (this.spirits > 100)
                this.spirits = 100;
        }

        public override void AddMess(int plusMess)
        {
            if (plusMess > 0)
            {
                this.mess += plusMess;
                if (this.mess > 50)
                    this.mess = 50;
                else if (this.mess < 0)
                    this.mess = 0;
            }

        }


    }

    [Serializable]
    internal class factoryArea : usualArea
    {
        string pollution;
        public int mess;
        public bool messCame;
        public factoryArea(string name = "Заводской район", string pollution = "неизвестно", int requiredBudget = 0, int requiredMaterials = 0, int requiredProvision = 0, int requiredMedicine = 0, int requiredPermissions = 0, string description =
            "Здесь никого нет, но город живёт засчет этого района. Именно здесь рабочие трудятся, чтобы производить городские товары или выполнять заказы страны.")
            : base(name, requiredBudget, requiredMaterials, requiredProvision, requiredMedicine, requiredPermissions,  description)
        {
            {
                this.pollution = pollution;
                mess = 0;
                police = 0;
                messCame = false;
            }
        }

        public override void showArea()
        {
            base.showArea();
            if (messCame)
            {
                GameMainScript.MapSC.statusText.text = "Загрязнения: " + pollution;
                GameMainScript.MapSC.statusText.text = "Беспорядки: " + Convert.ToString(mess) + "/50";
                GameMainScript.MapSC.policeObject.SetActive(true);
                GameMainScript.MapSC.policeValueText.text = Convert.ToString(police) + "/10";
            }
            else
            {
                GameMainScript.MapSC.statusText.text = "Загрязнения: " + pollution;
            }
        }
        public override void hideArea()
        {
            base.hideArea();
            GameMainScript.MapSC.statusText.text = "";
            GameMainScript.MapSC.UpperStatusText.text = "";
            GameMainScript.MapSC.policeObject.SetActive(false);
        }

        public override void SetPollution(string pollution)
        {
            this.pollution = pollution;
        }

        public override void AddMess(int mess)
        { 

            this.mess += mess;
            if (this.mess > 50)
                this.mess = 50;
            else if (this.mess < 0)
                this.mess = 0;

        }
    }
}
