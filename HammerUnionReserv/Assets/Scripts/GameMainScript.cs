using Assembly_CSharp; //Это подключили, чтобы Area.cs видеть.
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Добавляем чтобы юзать интерфейс и тексты всякие
using UnityEngine.Video;
using UnityEngine.SceneManagement; //Подключаем обязательно, чтобы иметь возможность сцены переключать.
using System;
using System.Threading;
using Unity.VisualScripting;

public class GameMainScript : MonoBehaviour
{
    public Text[] numberOfCase;
    public Text[] nameOfCase;
    public Text[] descriptionOfCase;
    public GameObject[] DeloObject;
    public Text[] textDayOfWeek;
    public Text[] textDayOfMonth;
    public Text TodayText;

    [SerializeField] Image[] DeloSprite;
    
    [SerializeField] GameObject[] answerObjectsD1;
    [SerializeField] GameObject[] answerGalochkiD1;
    [SerializeField] GameObject[] answerObjectsD2;
    [SerializeField] GameObject[] answerGalochkiD2;

 

    //Для писем
    [SerializeField] GameObject letterObject;
    [SerializeField] Image letterImage;
    public Text LetterName;
    public Text LetterDescription;
    public Text MagazineNumber;
    public Text MagazineTextLeft;
    public Text MagazineTextRight;

    //Для уведомлений
    public GameObject noticeObject;
    public Text noticeName;
    public Text noticeDescription;
    [SerializeField] Image noticeImage;


    public Sprite GalochkaSprite;
    public Sprite NoGalochkaSprite;
    public int today; //Переменная, отвечающая за сегодняшний день

    //public AllCases Cases;

    public int ActiveNotice;
    public int ActiveLetter;
    public int ActiveCaseObject;
    public int ActiveCaseID;
    public int ActiveCaseNumber; //номер дела из тех дел, которые сегодня доступны (от 1 до 10)
    public Dictionary<int, Case> AllCases = new Dictionary<int, Case>();
    public Dictionary<int, int> IDOfCaseAndAnswer = new Dictionary<int, int>();

    public Dictionary<int, Letter> AllLetters = new Dictionary<int, Letter>();


    public List<Case> todayCases;
    public List<Letter> todayLetters;
    public List<notice> todayNotices;



    //public static Dictionary<int, Case> AllCases;


    public Text[] recoursesTable;
    public Text[] reputationTable;
    public GameObject soldiersWeHave;
    public Text soldiersValue;

    public Animator mapAnimator; //Аниматор карты, просто карту надо перетащить
    public Animator villageMapAnimator;
    public Animator warMapAnimator;
    [SerializeField] Animator NextDayButtonAnimator;
    [SerializeField] Animator delo1Animator;
    [SerializeField] Animator delo2Animator;
    [SerializeField] Animator magazineAnimator;

    states PlayerState = states.WatchingCases;
    //[SerializeField] GameObject soundObject;
    public AudioSource SoundSource;// = soundObject.GetComponent<AudioSource>(); //то, что будет вызывать звуки
    public AudioClip PaperSound;
    [SerializeField] AudioClip PencilSound;
    [SerializeField] AudioClip ButtonNextDaySound;
    [SerializeField] AudioClip ClockSound;

    internal static Map MapSC; //Это один-единственный экземпляр объекта "Скрипт карты", на котором всё висит что нужно для карты
    internal static BaseOfUnits BaseOfUnitsSC;

    enum states //Состояния, в которых может находиться пользователь
    {
        WatchingCases = 0,
        WatchingMap = 1,
        ReadingMagazine = 2,
        ReadingLetters = 3
    }

    public enum recoursesEnum
    {
        budget = 0,
        materials = 1,
        provision = 2,
        medicine = 3,
        permissions = 4

    }
 

    public void RefreshRecourses()
    {
        for (int i = 0; i < 5; i++)
            recoursesTable[i].text = Convert.ToString(recoursesOfPlayer.Recourses[i]);
        soldiersValue.text = Convert.ToString(recoursesOfPlayer.soldiers);

    }

    public void RefreshReputation()
    {
        reputationTable[0].text = Convert.ToString(reputation.Authorities) + "/100";
    }

    public void MapGet() //Функция, которая открывает карту
    {

        
        if (PlayerState == states.WatchingMap)
        {
            SoundSource.PlayOneShot(PaperSound);
            PlayerState = states.WatchingCases;
            mapAnimator.SetBool("MapOpened", false);

        }
        else if (PlayerState == states.WatchingCases)
        {
            SoundSource.PlayOneShot(PaperSound);
            PlayerState = states.WatchingMap;

            mapAnimator.SetBool("MapOpened", true);   
 
        }

        if (MapSC.activeVillageTag != "")
            MapSC.VillageDict[MapSC.activeVillageTag].showVillage();
        if (MapSC.activeAreaTag != "")
        {
            MapSC.MapAreaDict[MapSC.activeAreaTag].showArea();
        }
    }   



    // Start is called before the first frame update
    void Start()
    {
        // Cases = new AllCases();

        MapSC = GameObject.Find("MapScript").GetComponent<Map>();
        BaseOfUnitsSC = GameObject.Find("UnitsScript").GetComponent<BaseOfUnits>();


        if (!SLscript.isNewGame) //если загружаем игру, а не начинаем новую
        {
            GameData GD;
            if (SLscript.NumberOfLoadedGame == -1)
                GD = SLscript.currentGameData;
            else
                GD = SLscript.savedGames[SLscript.NumberOfLoadedGame];
            
                today = GD.today;
                IDOfCaseAndAnswer = GD.IDOfCaseAndAnswer;

                AllCases = GD.AllCases;
                recoursesOfPlayer.Recourses = GD.Recourses;
                recoursesOfPlayer.soldiers = GD.soldiers;
                reputation.rep = GD.rep;

            BaseOfUnitsSC.RezervUnits = GD.RezervUnits;

            //Debug.Log("Из файла достали дел: " + SLscript.savedGames[SLscript.savedGames.Count - 1].AllCases.Count);
            //Debug.Log("Всего дел в список загрузилось: " + AllCases.Count);


        }
        else
        {
            //Инициализируем ресурсы

            recoursesOfPlayer.Recourses = new int[5];

            recoursesOfPlayer.Budget = 300;
            recoursesOfPlayer.Materials = 120;
            recoursesOfPlayer.Provision = 60;
            recoursesOfPlayer.Medicine = 30;
            recoursesOfPlayer.Permissions = 1;
            recoursesOfPlayer.soldiers = 20;

            reputation.rep = new int[5];
            reputation.OKO = 100; //ПОЧЕМУ ОН ЗДЕСЬ ОШИБКУ ВЫДАЁТ, НУ ПОЧЕМУ
            reputation.Authorities = 80;
            reputation.NIMB = 100;
            reputation.Sunrise = 100;
            reputation.ZOV = 100;

            BaseOfCases.InitializeCases();
            IDOfCaseAndAnswer.Add(0, 0);
            letterObject.SetActive(true);

            today = 1;

            //Здесь добавить юнита попробуем
            BaseOfUnitsSC.AddUnit(new unit(name: "Солдат СКСМ", damage: 4, hP: 3, maxHP: 3, techDamage: 4, spritePath: "Спрайты\\Illustrations\\Солдаты СКСМ", description: "ASD", quantity: 2), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "БТР", damage: 10, hP: 50, maxHP: 50, techDamage: 10, spritePath: "Спрайты\\Illustrations\\БТР колонна", description: "ASD", quantity: 1), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "Солдат СКСМ", damage: 4, hP: 3, maxHP: 3, techDamage: 4, spritePath: "Спрайты\\Illustrations\\Солдаты СКСМ", description: "ASD", quantity: 2), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "БТР", damage: 10, hP: 50, maxHP: 50, techDamage: 10, spritePath: "Спрайты\\Illustrations\\БТР колонна", description: "ASD", quantity: 1), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "БТР", damage: 10, hP: 35, maxHP: 50, techDamage: 10, spritePath: "Спрайты\\Illustrations\\БТР колонна", description: "ASD", quantity: 1), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "Солдат СКСМ", damage: 4, hP: 1, maxHP: 3, techDamage: 4, spritePath: "Спрайты\\Illustrations\\Солдаты СКСМ", description: "ASD", quantity: 3), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.RefreshRezerv();

        }

        
        RefreshRecourses();
        RefreshReputation();
        RefreshCalendar();

        ActiveCaseObject = 0;

        //УВЕДОМЛЕНИЯ
        todayNotices = new List<notice>();
        ActiveNotice = 0;


        //ПИСЬМА

        todayLetters = new List<Letter>();
        Debug.Log("Сейчас будет инициализация писем");
        BaseOfCases.InitializeLetters();
       
        ActiveLetter = -1; //Делаем -1, т.к. сначала всегда будет минус первое письмо - последствия дня.
        Debug.Log("Всего писем " + AllLetters.Count);


        foreach (int key in AllLetters.Keys)
            Debug.Log("Ключ письма " + key);


        if (!SLscript.isNewGame)
        {

            Debug.Log("GOVNICO");

            if (today != 1)
            {
                Debug.Log("Ща добавим писем");
                AddTodayLetters();
                Debug.Log("ПИСЕМ СЕГОДНЯ" + todayLetters.Count);
                if(todayLetters.Count > 0 && SLscript.NumberOfLoadedGame != -1)
                OKletter();
            }
            else
                Debug.Log("СЕГОДНЯ ДЕНЬ 1");

            if ((today == 1 || todayLetters.Count > 0) && SLscript.NumberOfLoadedGame != -1)
            letterObject.SetActive(true);

        }




        //ЗДЕСЬ НАЧИНАЮТСЯ ДЕЛА
        todayCases = new List<Case>();


       
        todayCases.Clear();
        ActiveCaseNumber = 0;

      
        for (int i = 10 * today + 1; i < 10 * today + 10; i++)
        {

            if (AllCases.ContainsKey(i) && AllCases[i].reqForCase.Satisfied())
            {
             //   Debug.Log("ХОТИМ ДОБАВИТЬ НОВОЕ ДЕЛО ПОД НОМЕРОМ " + i );
                todayCases.Add(AllCases[i]);
               // Debug.Log("ДОБАВИЛИ НОВОЕ ДЕЛО");

            }
        }


        if (AllCasesDone())
        {
            NextDayButtonAnimator.SetBool("AllCasesDone", true);
            // Debug.Log("Здесь мы закончили");
        }

        ShowActiveCase();



    }
    
    string ConvertToDayOfWeek(int day, int month)
    {
        if (day == 5)
            return "Пятница: праздник хлеба";

        if (month == 9)
        {
            int temp = day % 7;
            
            switch (temp)
            {
                case 1:
                    return "Понедельник";
                case 2:
                    return "Вторник";
                case 3:
                    return "Среда";
                case 4:
                    return "Четверг";
                case 5:
                    return "Пятница";
                case 6:
                    return "Суббота: поставки";
                case 0:
                    return "Воскресенье: выходной";
                default:
                    return "нубас";
            }
        }
        else return "нубас";

    }

    void HideActiveCase()
    {
        if (ActiveCaseObject == 0)
        {
            for (int i = 0; i < 4; i++)
                answerObjectsD1[i].SetActive(false);

        }
        else if (ActiveCaseObject == 1)
        {
            for (int i = 0; i < 4; i++)
                answerObjectsD2[i].SetActive(false);
        }
    }
    void ShowActiveCase()
    {
        HideActiveCase();



        //  Debug.Log("ActiveCaseObject = " + Convert.ToString(ActiveCaseObject));
        //  Debug.Log("ActiveCaseNumber = " + Convert.ToString(ActiveCaseNumber));

        DeloSprite[ActiveCaseObject].sprite = Resources.Load<Sprite>(todayCases[ActiveCaseNumber].spritePath); //Ищешь объект в папочке
        numberOfCase[ActiveCaseObject].text = "Дело 1";
        //  Debug.Log("АКТИВНОЕ ДЕЛО " + Convert.ToString(ActiveCaseNumber));
        // Debug.Log("АКТИВНОЕ ОБЪЕКТ ДЕЛА " + Convert.ToString(ActiveCaseObject));
        // Debug.Log("Размер списка " + Convert.ToString(todayCases.Count));
        nameOfCase[ActiveCaseObject].text = todayCases[ActiveCaseNumber].name;
        descriptionOfCase[ActiveCaseObject].text = todayCases[ActiveCaseNumber].text;
        numberOfCase[ActiveCaseObject].text = "ДЕЛО #" + Convert.ToString(ActiveCaseNumber + 1);

        /* for(int i =0; i< 4; i++)
         {
             Debug.Log("Ответ номер " + Convert.ToString(i) + " это " + Convert.ToString(todayCases[ActiveCaseNumber].answers[i]));
         }*/

        if (ActiveCaseObject == 0) //Если на экране первый объект дела
        {

            for (int i = 0; i < todayCases[ActiveCaseNumber].answersTexts.Length; i++)
            {
                answerObjectsD1[i].GetComponent<Text>().text = todayCases[ActiveCaseNumber].answersTexts[i];
                if (todayCases[ActiveCaseNumber].answers[i]) //Обновляем галочки, чтобы показать их только там, где они реально должны стоять на этом деле
                {
                    answerGalochkiD1[i].GetComponent<Image>().sprite = GalochkaSprite;
                }
                else
                {
                    answerGalochkiD1[i].GetComponent<Image>().sprite = NoGalochkaSprite;
                }
                answerObjectsD1[i].SetActive(true);
            }

        }
        else if (ActiveCaseObject == 1) //Если на экране второй объект дела
        {
            //Debug.Log("НА ЭКРАНЕ ВТОРОЙ ОБЪЕКТ КРИНДЖА");

            for (int i = 0; i < todayCases[ActiveCaseNumber].answersTexts.Length; i++)
            {
                answerObjectsD2[i].GetComponent<Text>().text = todayCases[ActiveCaseNumber].answersTexts[i];
                if (todayCases[ActiveCaseNumber].answers[i]) //Обновляем галочки, чтобы показать их только там, где они реально должны стоять на этом деле
                {
                    answerGalochkiD2[i].GetComponent<Image>().sprite = GalochkaSprite;
                }
                else
                {
                    answerGalochkiD2[i].GetComponent<Image>().sprite = NoGalochkaSprite;
                }
                answerObjectsD2[i].SetActive(true);
            }
        }
    }

    public bool AllCasesDone()
    {
        bool[] CaseDone = new bool[todayCases.Count];
        for(int i = 0; i < todayCases.Count; i++)
        {
            CaseDone[i] = false;
            for(int j=0; j < 4; j++)
            {
                if (todayCases[i].answers[j])
                {
                    CaseDone[i] = true;
                    break;
                }
            }
            if (CaseDone[i] == false)
            {
                return false;
            }
        }
        return true;

    }

    public void ChooseAnswer(int numberDelo_Answer)
    {
        Debug.Log("GOVNO ! ! !");

        //Сделай только если сатисфайд
        int numberDelo = numberDelo_Answer / 10;
        int numberAnswer = numberDelo_Answer % 10;


        if (todayCases[ActiveCaseNumber].answers[numberAnswer] == false) //Если ответ этот не выбран ещё
        {
            if (todayCases[ActiveCaseNumber].reqForAnswers[numberAnswer].Satisfied())
            {
                SoundSource.PlayOneShot(PencilSound);
                if (numberDelo == 0) //Если на экране 1 объект дела
                {
                    answerGalochkiD1[numberAnswer].GetComponent<Image>().sprite = GalochkaSprite;
                    for (int i = 0; i < 4; i++) //Остальные ответы отменяем
                    {
                        if (i != numberAnswer)
                        {
                            answerGalochkiD1[i].GetComponent<Image>().sprite = NoGalochkaSprite;
                            if (todayCases[ActiveCaseNumber].answers[i]) //Возвращаем игроку ресурсы за отменённый ответ, который ранее был проставлен
                                todayCases[ActiveCaseNumber].reqForAnswers[i].UngiveResources(); 
                            todayCases[ActiveCaseNumber].answers[i] = false;
                          //  Debug.Log("Отменили ответ ноер " + Convert.ToString(i));
                        }
                    }
                }
                else //Если на экране 2 объект дела
                {
                    answerGalochkiD2[numberAnswer].GetComponent<Image>().sprite = GalochkaSprite;
                    for (int i = 0; i < 4; i++) //Остальные ответы отменяем
                    {
                        if (i != numberAnswer)
                        {
                            answerGalochkiD2[i].GetComponent<Image>().sprite = NoGalochkaSprite;
                            if (todayCases[ActiveCaseNumber].answers[i]) //Возвращаем игроку ресурсы за отменённый ответ, который ранее был проставлен
                                todayCases[ActiveCaseNumber].reqForAnswers[i].UngiveResources();
                            todayCases[ActiveCaseNumber].answers[i] = false;
                        //    Debug.Log("Отменили ответ ноер " + Convert.ToString(i));
                        }
                    }
                }
                todayCases[ActiveCaseNumber].reqForAnswers[numberAnswer].GiveResources(); //Даём делу нужные ресурсы
                RefreshRecourses();
                todayCases[ActiveCaseNumber].answers[numberAnswer] = true;
            }
            //else Debug.Log("Не удовлетворено");
        }            
        else //если этот ответ уже выбран
        {
                if (numberDelo == 0)
                {
                    answerGalochkiD1[numberAnswer].GetComponent<Image>().sprite = NoGalochkaSprite;
                }
                else
                {
                    answerGalochkiD2[numberAnswer].GetComponent<Image>().sprite = NoGalochkaSprite;
                }
            todayCases[ActiveCaseNumber].reqForAnswers[numberAnswer].UngiveResources(); //Возвращаем игроку ресурсы за отменённый ответ
            RefreshRecourses();
            todayCases[ActiveCaseNumber].answers[numberAnswer] = false;

        }

        //ПОПРОБУЕМ ВЫВЕСТИ КНОПКУ СЛЕДУЮЩЕГО ДНЯ, ЕСЛИ ВСЕ ДЕЛА СДЕЛАНЫ

        if (AllCasesDone())
        {
            NextDayButtonAnimator.SetBool("AllCasesDone", true);
           // Debug.Log("Здесь мы закончили");
        }
        else
        {
            NextDayButtonAnimator.SetBool("AllCasesDone", false);
           // Debug.Log("Здесь мы НЕ закончили");
        }

    }


    public void AddTodayLetters()
    {
        //Добавляем новые сегодняшние письма
        todayLetters.Clear();
        for (int i = 10 * today + 1; i < 10 * today + 10; i++)
        {

            if (!AllLetters.ContainsKey(i))
                Debug.Log("ПИСЬМА НЕТУ");
            else
            {
                Debug.Log("Письмо есть");
            }

            if (AllLetters.ContainsKey(i) && AllLetters[i].requirement.Satisfied())
            {
                Debug.Log("Письмо ДОБАВЛЕНО");
                todayLetters.Add(AllLetters[i]);
            }
        }



    }

    public void NewDayProcedures()
    {

        today++;
        Debug.Log("Сегодня день номер " + Convert.ToString(today));
        //Здесь, когда день уже перевалил на следующий, мы добавляем игроку положенные ресурсы за построенные здания
        foreach (string key in MapSC.EmptyAreaDict.Keys)
        {

            if (MapSC.EmptyAreaDict[key].buildings[(int)buildings.bank] && today % 7 == 1)
            {
                recoursesOfPlayer.Budget += 50;
            }

            if (MapSC.EmptyAreaDict[key].buildings[(int)buildings.factory])
            {
                recoursesOfPlayer.Materials += 3;
            }

            if (MapSC.EmptyAreaDict[key].buildings[(int)buildings.bakaly] && (today % 7 == 2 || today % 7 == 4 || today % 7 == 6))
            {
                recoursesOfPlayer.Provision += 4;
            }
            if (MapSC.EmptyAreaDict[key].buildings[(int)buildings.pharmacy] && (today % 7 == 3 || today % 7 == 6))
            {
                recoursesOfPlayer.Medicine += 4;
            }

        }

        RefreshCalendar();


        //Тут разбираемся со всеми деревнями
        //НАДО ПОСТАВИТЬ УСЛОВИЕ КАЖДУЮ НЕДЕЛЮ, А НЕ КАЖДЫЙ ДЕНЬ
        if(today % 7 == 1) //По понедельникам
            foreach (string key in MapSC.VillageDict.Keys)
            {
                MapSC.VillageDict[key].weeklyIncome();

            }
        foreach (string key in MapSC.VillageDict.Keys)
        {
            MapSC.VillageDict[key].dailyIncome();

        }

        if (MapSC.activeVillageTag != "")
            MapSC.VillageDict[MapSC.activeVillageTag].showVillage();
        if (MapSC.activeAreaTag != "")
        {
            MapSC.MapAreaDict[MapSC.activeAreaTag].showArea();
        }



        AddTodayLetters();

    

        RefreshRecourses();
        RefreshReputation();

        ActiveLetter = -1;
        letterObject.SetActive(true);


        //НОВЫЕ ДЕЛА


        todayCases.Clear();
        ActiveCaseNumber = 0;
        ActiveCaseObject = 0;

        for (int i = 10 * today + 1; i < 10 * today + 10; i++)
        {

            if (AllCases.ContainsKey(i) && AllCases[i].reqForCase.Satisfied())
            {
                Debug.Log("ХОТИМ ДОБАВИТЬ НОВОЕ ДЕЛО");
                todayCases.Add(AllCases[i]);
                Debug.Log("ДОБАВИЛИ НОВОЕ ДЕЛО");

            }
        }

        delo1Animator.SetInteger("Condition", 3);
        ShowActiveCase();

    }

    void UpdateConsequences()
    {
        //СНАЧАЛА ПОКАЖЕМ ПИСЬМО НОМЕР -1 - последствия прошедшего дня

        letterImage.sprite = Resources.Load<Sprite>("Спрайты\\Illustrations\\Город");
        //Здесь в зависимости ото дня даём конкретные последствия в письме

        string DayConsequences;
        if (today % 7 == 6)
            DayConsequences = "Закончился последний рабочий день этой недели. Началось воскресенье. Вчера в 11 часов вечера вашему городу доставили новые ресурсы.";
        else
            DayConsequences = "\tНачался новый день. Город продолжает жить. Порядок в государстве стабилен.\n";

        foreach (Case delo in todayCases)
        {
            DayConsequences += "\t" + delo.answerActions[AnswerOfCase(delo)].textDayOfTheDay + "\n";
        }
        DayConsequences += "\tВсе ваши отношения с районами и группировками были пересчитаны соотвественно потраченным ресурсам и совершенным делам. \n";
        LetterDescription.text = DayConsequences;
        LetterName.text = "Последствия";
    }

    public void NextDayClick()
    {

        SoundSource.PlayOneShot(ClockSound);
        //ПИСЬМА

        UpdateConsequences();



        //ЗДЕСЬ У РАЙОНОВ ОТНИМАЮТ НАСТРОЕНИЕ И ПРИБАВЛЯЮТ БЕСПОРЯДКИ

        foreach (string key in MapSC.TownAreaDict.Keys)
        {



            if (MapSC.TownAreaDict[key].messCame)
            {
                int plusMess = 20 - MapSC.TownAreaDict[key].spirits;
                Debug.Log("Прибавляем беспорядков без учёта полиции " + Convert.ToString(plusMess));
                Debug.Log("А полиции в районе при этом " + Convert.ToString(MapSC.TownAreaDict[key].policeUnits.Count));
                MapSC.TownAreaDict[key].AddMess(plusMess - MapSC.TownAreaDict[key].policeUnits.Count * 2); //Добавляем беспорядки

                //Сюда можно наверное условие поражения или хз

            }


            for (int i = 0; i < 5; i++)
            {
                MapSC.TownAreaDict[key].AddSpirits((-1) * (MapSC.TownAreaDict[key].requiredRecourses[i] - MapSC.TownAreaDict[key].currentRecourses[i]));
                MapSC.TownAreaDict[key].currentRecourses[i] = 0;

            }
        }


        delo1Animator.SetInteger("Condition", 1);
        delo2Animator.SetInteger("Condition", 1);
        if (AllCasesDone())
        {
            HideActiveCase();
            for (int i = 0; i < todayCases.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (todayCases[i].answers[j])
                    {
                        Debug.Log("Сейчас будем ДЕЛАТЬ");

                        todayCases[i].answerActions[j].Do(todayCases[i].caseID);
                    }

                }
            }
            NextDayButtonAnimator.SetBool("AllCasesDone", false);
            if (PlayerState == states.WatchingMap)
                MapSC.ShowArea(MapSC.activeAreaTag);

        }

      

        NewDayProcedures(); //Здесь все процедуры, которые так же можно применять и в воскресенье.


    }

    void RefreshCalendar()
    {
        TodayText.text = Convert.ToString(today) + " сентября, " + ConvertToDayOfWeek(today, 9);
        for(int i=0; i< 3; i++)
        {
            textDayOfMonth[i].text = Convert.ToString(today + 1 + i) + "\nсентября";
            textDayOfWeek[i].text = ConvertToDayOfWeek(today + 1 + i, 9);
        }
    }

    public int AnswerOfCase(Case delo)
    {
        for (int i = 0; i < 4; i++)
        {
            if (delo.answers[i])
                return i;
        }
        return -1;
    }

    public void NewMagazineWeekly()
    {
        int week = today / 7 + 1;
        switch (week)
        {
            case 2:
                MagazineTextLeft.text = "вы слон";
                MagazineTextRight.text = "димон";
                MagazineNumber.text = "Серия #11; выпуск #25\r\n";
                break;
        }
    }

    void FirstSundayLetter()
    {
        letterImage.sprite = Resources.Load<Sprite>("Спрайты\\Illustrations\\Правительство");
        LetterName.text = "Конец первой недели: Власти";

        int sumSpirits = 0;
        foreach (string key in MapSC.TownAreaDict.Keys)
            sumSpirits += MapSC.TownAreaDict[key].spirits;

        if (sumSpirits / 4 > 75)
        {
            LetterDescription.text = "   Мы должны признать, что ваша работа нас впечатлила. В первую же неделю кризиса вы смогли удержать в городе дисциплину и настроение граждан на нужном уровне, что не может не радовать. Продолжайте в том же духе и тогда, быть может, мы наградим ваш город чем-нибудь особенным.";
            reputation.Authorities += 15;
        }
        else if (sumSpirits/4 > 50)
        {
            LetterDescription.text = "  Вы неплохо справились с вашей работой! Хоть кое-где дисциплина и просела, город стоит ровно, что уже хорошо. Вам все же стоит поработать над порядком и иногда радовать граждан, иначе последствия могут быть в следующий раз куда серьезнее. Надеемся, вы нас услышали.";
            reputation.Authorities += 5;
        }
        else if (sumSpirits/4 <= 50)
        {
            if (AllCases[64].answers[0]) //если дали взятку Даниилу
            {
                LetterDescription.text = "   Мы заметили много недочетов в вашей работе, но, скажем так, один наш общий знакомый вчера вечером замолвил за вас словечко. К вам претензий у нас нет, но постарайтесь уж в следующий раз сохранить состояние города!";
            }
            else if(sumSpirits/4 > 25)
            { 
                LetterDescription.text = "  Город еле вынес эту неделю, что крайне плохо, ведь кризис может длиться месяца или года! Если говорить честно, вы довольно плохо следили за городом: дисциплины почти нет, граждане чуть ли не бунтуют. Если вы так продолжите, мы найдем кого-то более подходящего на ваше место, зарубите себе на носу!";
                reputation.Authorities -= 15;
            }
            else
            {
                LetterDescription.text = "   Вы, похоже, даже не старались! Город в ужасном состоянии! Дисциплины и порядка нет совсем, граждане уже лезут в окна домов чиновников! Город не выдержит ни дня кризиса более, если вы за него не возьметесь! Мы делаем вам исключение и даем еще один шанс, только потому, что вы работаете первую неделю. Надеемся, вам все понятно.";
                reputation.Authorities -= 25;
            }    
            

        }
        LetterDescription.text = LetterDescription.text + "\n   Но хватит разговоров, вам следует сегодня хорошенько отдохнуть, чтобы завтра продолжить работать. Слава союзу молота!";




    }

    public void OKletter()
    {
        ActiveLetter++;
        if (today % 7 == 0 && ActiveLetter == 0) // Если ещё не приступили к основным письмам и если сегодня воскресенье, то надо показать письмо проверяющего.
        {
            FirstSundayLetter();
        }

        else
        if (ActiveLetter < todayLetters.Count)
        {
            letterImage.sprite = Resources.Load<Sprite>(todayLetters[ActiveLetter].spritePath);
            LetterDescription.text = todayLetters[ActiveLetter].text;
            LetterName.text = todayLetters[ActiveLetter].name;
        }
        else
        {


           
            letterObject.SetActive(false);

            //ЗДЕСЬ ЗАПУСКАЕМ УВЕДОМЛЕНИЯ, ЕСЛИ ОНИ ЕСТЬ
            if (todayNotices.Count > 0)
            {   
                noticeObject.SetActive(true);
                noticeImage.sprite = Resources.Load<Sprite>(todayNotices[ActiveNotice].spritePath);
                noticeDescription.text = todayNotices[ActiveNotice].text;
                noticeName.text = todayNotices[ActiveNotice].name;

            }
            else if(today % 7 == 0) //Если сегодня воскресенье (выходной), то сразу же запускаем следующий день, т.к. в воскресенье чел не работает.
            {
                //ЗАПУСКАЕМ СЛЕДУЮЩИЙ ДЕНЬ, НО БЕЗ ПЕРЕРАСПРЕДЕЛЕНИЯ РЕПУТАЦИИ
                NewDayProcedures();
                //МЕНЯЕМ ГАЗЕТУ. Почему именно здесь? Да я не знаю если честно.
                NewMagazineWeekly();

            }
           

        }
    }

    public void OKNotice()
    {
        ActiveNotice++;

        if (ActiveNotice < todayNotices.Count)
        {
            noticeImage.sprite = Resources.Load<Sprite>(todayNotices[ActiveNotice].spritePath);
            noticeDescription.text = todayNotices[ActiveNotice].text;
            noticeName.text = todayNotices[ActiveNotice].name;
        }
        else
        {
            if (today % 7 == 0) //Если сегодня воскресенье (выходной), то сразу же запускаем следующий день, т.к. в воскресенье чел не работает.
            {
                //ЗАПУСКАЕМ СЛЕДУЮЩИЙ ДЕНЬ, НО БЕЗ ПЕРЕРАСПРЕДЕЛЕНИЯ РЕПУТАЦИИ
                NewDayProcedures();
                //МЕНЯЕМ ГАЗЕТУ. Почему именно здесь? Да я не знаю если честно.
                NewMagazineWeekly();

            }
            todayNotices.Clear();
            ActiveNotice = 0;
            noticeObject.SetActive(false);
        }
    }

    public void MagazineButtonClick()
    {
        if (PlayerState == states.WatchingCases)
        {
            SoundSource.PlayOneShot(PaperSound);
            PlayerState = states.ReadingMagazine;
            magazineAnimator.SetBool("ActiveMagazine", true);
        }
        else if(PlayerState == states.ReadingMagazine)
        {
            SoundSource.PlayOneShot(PaperSound);
            PlayerState = states.WatchingCases;
            magazineAnimator.SetBool("ActiveMagazine", false);
        }
    }

    public void forwardCase()
    {
        //Debug.Log(todayCases[ActiveCaseNumber].spritePath);
        if (ActiveCaseObject == 0)  //Это номер физического объекта дела, всегда равен 0 или 1
        {
            ActiveCaseObject = 1;
            Debug.Log("Дело 1 убираем");
            delo1Animator.SetInteger("Condition", 1);
            delo2Animator.SetInteger("Condition", 3);

        }
        else
        {
            Debug.Log("Дело 2 убираем");
            ActiveCaseObject = 0;
            delo1Animator.SetInteger("Condition", 3);
            delo2Animator.SetInteger("Condition", 1);
        }
        SoundSource.PlayOneShot(PaperSound);


        if (ActiveCaseNumber == todayCases.Count - 1)
        {
            ActiveCaseNumber = 0;   //Это номер в нашем списке сегодняшних дел
        }
        else
        {
            ActiveCaseNumber++;
        }

        ShowActiveCase();

    }

    public void backwardCase()
    {
        if (ActiveCaseObject == 0)  //Это номер физического объекта дела, всегда равен 0 или 1
        {
            ActiveCaseObject = 1;

            delo1Animator.SetInteger("Condition", 1);
            delo2Animator.SetInteger("Condition", 3);

        }
        else
        {
            ActiveCaseObject = 0;
            delo1Animator.SetInteger("Condition", 3);
            delo2Animator.SetInteger("Condition", 1);
        }
        SoundSource.PlayOneShot(PaperSound);



        if (ActiveCaseNumber == 0)
        {
            ActiveCaseNumber = todayCases.Count - 1;
        }
        else
        {
            ActiveCaseNumber--;
        }

        ShowActiveCase();
    }
    

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
            SLscript.Save();
        }

           
    }
}
