using Assembly_CSharp; //��� ����������, ����� Area.cs ������.
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //��������� ����� ����� ��������� � ������ ������
using UnityEngine.Video;
using UnityEngine.SceneManagement; //���������� �����������, ����� ����� ����������� ����� �����������.
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

 

    //��� �����
    [SerializeField] GameObject letterObject;
    [SerializeField] Image letterImage;
    public Text LetterName;
    public Text LetterDescription;
    public Text MagazineNumber;
    public Text MagazineTextLeft;
    public Text MagazineTextRight;

    //��� �����������
    public GameObject noticeObject;
    public Text noticeName;
    public Text noticeDescription;
    [SerializeField] Image noticeImage;


    public Sprite GalochkaSprite;
    public Sprite NoGalochkaSprite;
    public int today; //����������, ���������� �� ����������� ����

    //public AllCases Cases;

    public int ActiveNotice;
    public int ActiveLetter;
    public int ActiveCaseObject;
    public int ActiveCaseID;
    public int ActiveCaseNumber; //����� ���� �� ��� ���, ������� ������� �������� (�� 1 �� 10)
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

    public Animator mapAnimator; //�������� �����, ������ ����� ���� ����������
    public Animator villageMapAnimator;
    public Animator warMapAnimator;
    [SerializeField] Animator NextDayButtonAnimator;
    [SerializeField] Animator delo1Animator;
    [SerializeField] Animator delo2Animator;
    [SerializeField] Animator magazineAnimator;

    states PlayerState = states.WatchingCases;
    //[SerializeField] GameObject soundObject;
    public AudioSource SoundSource;// = soundObject.GetComponent<AudioSource>(); //��, ��� ����� �������� �����
    public AudioClip PaperSound;
    [SerializeField] AudioClip PencilSound;
    [SerializeField] AudioClip ButtonNextDaySound;
    [SerializeField] AudioClip ClockSound;

    internal static Map MapSC; //��� ����-������������ ��������� ������� "������ �����", �� ������� �� ����� ��� ����� ��� �����
    internal static BaseOfUnits BaseOfUnitsSC;

    enum states //���������, � ������� ����� ���������� ������������
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

    public void MapGet() //�������, ������� ��������� �����
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


        if (!SLscript.isNewGame) //���� ��������� ����, � �� �������� �����
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

            //Debug.Log("�� ����� ������� ���: " + SLscript.savedGames[SLscript.savedGames.Count - 1].AllCases.Count);
            //Debug.Log("����� ��� � ������ �����������: " + AllCases.Count);


        }
        else
        {
            //�������������� �������

            recoursesOfPlayer.Recourses = new int[5];

            recoursesOfPlayer.Budget = 300;
            recoursesOfPlayer.Materials = 120;
            recoursesOfPlayer.Provision = 60;
            recoursesOfPlayer.Medicine = 30;
            recoursesOfPlayer.Permissions = 1;
            recoursesOfPlayer.soldiers = 20;

            reputation.rep = new int[5];
            reputation.OKO = 100; //������ �� ����� ������ ������, �� ������
            reputation.Authorities = 80;
            reputation.NIMB = 100;
            reputation.Sunrise = 100;
            reputation.ZOV = 100;

            BaseOfCases.InitializeCases();
            IDOfCaseAndAnswer.Add(0, 0);
            letterObject.SetActive(true);

            today = 1;

            //����� �������� ����� ���������
            BaseOfUnitsSC.AddUnit(new unit(name: "������ ����", damage: 4, hP: 3, maxHP: 3, techDamage: 4, spritePath: "�������\\Illustrations\\������� ����", description: "ASD", quantity: 2), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "���", damage: 10, hP: 50, maxHP: 50, techDamage: 10, spritePath: "�������\\Illustrations\\��� �������", description: "ASD", quantity: 1), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "������ ����", damage: 4, hP: 3, maxHP: 3, techDamage: 4, spritePath: "�������\\Illustrations\\������� ����", description: "ASD", quantity: 2), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "���", damage: 10, hP: 50, maxHP: 50, techDamage: 10, spritePath: "�������\\Illustrations\\��� �������", description: "ASD", quantity: 1), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "���", damage: 10, hP: 35, maxHP: 50, techDamage: 10, spritePath: "�������\\Illustrations\\��� �������", description: "ASD", quantity: 1), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.AddUnit(new unit(name: "������ ����", damage: 4, hP: 1, maxHP: 3, techDamage: 4, spritePath: "�������\\Illustrations\\������� ����", description: "ASD", quantity: 3), BaseOfUnitsSC.RezervUnits);
            BaseOfUnitsSC.RefreshRezerv();

        }

        
        RefreshRecourses();
        RefreshReputation();
        RefreshCalendar();

        ActiveCaseObject = 0;

        //�����������
        todayNotices = new List<notice>();
        ActiveNotice = 0;


        //������

        todayLetters = new List<Letter>();
        Debug.Log("������ ����� ������������� �����");
        BaseOfCases.InitializeLetters();
       
        ActiveLetter = -1; //������ -1, �.�. ������� ������ ����� ����� ������ ������ - ����������� ���.
        Debug.Log("����� ����� " + AllLetters.Count);


        foreach (int key in AllLetters.Keys)
            Debug.Log("���� ������ " + key);


        if (!SLscript.isNewGame)
        {

            Debug.Log("GOVNICO");

            if (today != 1)
            {
                Debug.Log("�� ������� �����");
                AddTodayLetters();
                Debug.Log("����� �������" + todayLetters.Count);
                if(todayLetters.Count > 0 && SLscript.NumberOfLoadedGame != -1)
                OKletter();
            }
            else
                Debug.Log("������� ���� 1");

            if ((today == 1 || todayLetters.Count > 0) && SLscript.NumberOfLoadedGame != -1)
            letterObject.SetActive(true);

        }




        //����� ���������� ����
        todayCases = new List<Case>();


       
        todayCases.Clear();
        ActiveCaseNumber = 0;

      
        for (int i = 10 * today + 1; i < 10 * today + 10; i++)
        {

            if (AllCases.ContainsKey(i) && AllCases[i].reqForCase.Satisfied())
            {
             //   Debug.Log("����� �������� ����� ���� ��� ������� " + i );
                todayCases.Add(AllCases[i]);
               // Debug.Log("�������� ����� ����");

            }
        }


        if (AllCasesDone())
        {
            NextDayButtonAnimator.SetBool("AllCasesDone", true);
            // Debug.Log("����� �� ���������");
        }

        ShowActiveCase();



    }
    
    string ConvertToDayOfWeek(int day, int month)
    {
        if (day == 5)
            return "�������: �������� �����";

        if (month == 9)
        {
            int temp = day % 7;
            
            switch (temp)
            {
                case 1:
                    return "�����������";
                case 2:
                    return "�������";
                case 3:
                    return "�����";
                case 4:
                    return "�������";
                case 5:
                    return "�������";
                case 6:
                    return "�������: ��������";
                case 0:
                    return "�����������: ��������";
                default:
                    return "�����";
            }
        }
        else return "�����";

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

        DeloSprite[ActiveCaseObject].sprite = Resources.Load<Sprite>(todayCases[ActiveCaseNumber].spritePath); //����� ������ � �������
        numberOfCase[ActiveCaseObject].text = "���� 1";
        //  Debug.Log("�������� ���� " + Convert.ToString(ActiveCaseNumber));
        // Debug.Log("�������� ������ ���� " + Convert.ToString(ActiveCaseObject));
        // Debug.Log("������ ������ " + Convert.ToString(todayCases.Count));
        nameOfCase[ActiveCaseObject].text = todayCases[ActiveCaseNumber].name;
        descriptionOfCase[ActiveCaseObject].text = todayCases[ActiveCaseNumber].text;
        numberOfCase[ActiveCaseObject].text = "���� #" + Convert.ToString(ActiveCaseNumber + 1);

        /* for(int i =0; i< 4; i++)
         {
             Debug.Log("����� ����� " + Convert.ToString(i) + " ��� " + Convert.ToString(todayCases[ActiveCaseNumber].answers[i]));
         }*/

        if (ActiveCaseObject == 0) //���� �� ������ ������ ������ ����
        {

            for (int i = 0; i < todayCases[ActiveCaseNumber].answersTexts.Length; i++)
            {
                answerObjectsD1[i].GetComponent<Text>().text = todayCases[ActiveCaseNumber].answersTexts[i];
                if (todayCases[ActiveCaseNumber].answers[i]) //��������� �������, ����� �������� �� ������ ���, ��� ��� ������� ������ ������ �� ���� ����
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
        else if (ActiveCaseObject == 1) //���� �� ������ ������ ������ ����
        {
            //Debug.Log("�� ������ ������ ������ �������");

            for (int i = 0; i < todayCases[ActiveCaseNumber].answersTexts.Length; i++)
            {
                answerObjectsD2[i].GetComponent<Text>().text = todayCases[ActiveCaseNumber].answersTexts[i];
                if (todayCases[ActiveCaseNumber].answers[i]) //��������� �������, ����� �������� �� ������ ���, ��� ��� ������� ������ ������ �� ���� ����
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

        //������ ������ ���� ���������
        int numberDelo = numberDelo_Answer / 10;
        int numberAnswer = numberDelo_Answer % 10;


        if (todayCases[ActiveCaseNumber].answers[numberAnswer] == false) //���� ����� ���� �� ������ ���
        {
            if (todayCases[ActiveCaseNumber].reqForAnswers[numberAnswer].Satisfied())
            {
                SoundSource.PlayOneShot(PencilSound);
                if (numberDelo == 0) //���� �� ������ 1 ������ ����
                {
                    answerGalochkiD1[numberAnswer].GetComponent<Image>().sprite = GalochkaSprite;
                    for (int i = 0; i < 4; i++) //��������� ������ ��������
                    {
                        if (i != numberAnswer)
                        {
                            answerGalochkiD1[i].GetComponent<Image>().sprite = NoGalochkaSprite;
                            if (todayCases[ActiveCaseNumber].answers[i]) //���������� ������ ������� �� ��������� �����, ������� ����� ��� ����������
                                todayCases[ActiveCaseNumber].reqForAnswers[i].UngiveResources(); 
                            todayCases[ActiveCaseNumber].answers[i] = false;
                          //  Debug.Log("�������� ����� ���� " + Convert.ToString(i));
                        }
                    }
                }
                else //���� �� ������ 2 ������ ����
                {
                    answerGalochkiD2[numberAnswer].GetComponent<Image>().sprite = GalochkaSprite;
                    for (int i = 0; i < 4; i++) //��������� ������ ��������
                    {
                        if (i != numberAnswer)
                        {
                            answerGalochkiD2[i].GetComponent<Image>().sprite = NoGalochkaSprite;
                            if (todayCases[ActiveCaseNumber].answers[i]) //���������� ������ ������� �� ��������� �����, ������� ����� ��� ����������
                                todayCases[ActiveCaseNumber].reqForAnswers[i].UngiveResources();
                            todayCases[ActiveCaseNumber].answers[i] = false;
                        //    Debug.Log("�������� ����� ���� " + Convert.ToString(i));
                        }
                    }
                }
                todayCases[ActiveCaseNumber].reqForAnswers[numberAnswer].GiveResources(); //��� ���� ������ �������
                RefreshRecourses();
                todayCases[ActiveCaseNumber].answers[numberAnswer] = true;
            }
            //else Debug.Log("�� �������������");
        }            
        else //���� ���� ����� ��� ������
        {
                if (numberDelo == 0)
                {
                    answerGalochkiD1[numberAnswer].GetComponent<Image>().sprite = NoGalochkaSprite;
                }
                else
                {
                    answerGalochkiD2[numberAnswer].GetComponent<Image>().sprite = NoGalochkaSprite;
                }
            todayCases[ActiveCaseNumber].reqForAnswers[numberAnswer].UngiveResources(); //���������� ������ ������� �� ��������� �����
            RefreshRecourses();
            todayCases[ActiveCaseNumber].answers[numberAnswer] = false;

        }

        //��������� ������� ������ ���������� ���, ���� ��� ���� �������

        if (AllCasesDone())
        {
            NextDayButtonAnimator.SetBool("AllCasesDone", true);
           // Debug.Log("����� �� ���������");
        }
        else
        {
            NextDayButtonAnimator.SetBool("AllCasesDone", false);
           // Debug.Log("����� �� �� ���������");
        }

    }


    public void AddTodayLetters()
    {
        //��������� ����� ����������� ������
        todayLetters.Clear();
        for (int i = 10 * today + 1; i < 10 * today + 10; i++)
        {

            if (!AllLetters.ContainsKey(i))
                Debug.Log("������ ����");
            else
            {
                Debug.Log("������ ����");
            }

            if (AllLetters.ContainsKey(i) && AllLetters[i].requirement.Satisfied())
            {
                Debug.Log("������ ���������");
                todayLetters.Add(AllLetters[i]);
            }
        }



    }

    public void NewDayProcedures()
    {

        today++;
        Debug.Log("������� ���� ����� " + Convert.ToString(today));
        //�����, ����� ���� ��� ��������� �� ���������, �� ��������� ������ ���������� ������� �� ����������� ������
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


        //��� ����������� �� ����� ���������
        //���� ��������� ������� ������ ������, � �� ������ ����
        if(today % 7 == 1) //�� �������������
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


        //����� ����


        todayCases.Clear();
        ActiveCaseNumber = 0;
        ActiveCaseObject = 0;

        for (int i = 10 * today + 1; i < 10 * today + 10; i++)
        {

            if (AllCases.ContainsKey(i) && AllCases[i].reqForCase.Satisfied())
            {
                Debug.Log("����� �������� ����� ����");
                todayCases.Add(AllCases[i]);
                Debug.Log("�������� ����� ����");

            }
        }

        delo1Animator.SetInteger("Condition", 3);
        ShowActiveCase();

    }

    void UpdateConsequences()
    {
        //������� ������� ������ ����� -1 - ����������� ���������� ���

        letterImage.sprite = Resources.Load<Sprite>("�������\\Illustrations\\�����");
        //����� � ����������� ��� ��� ��� ���������� ����������� � ������

        string DayConsequences;
        if (today % 7 == 6)
            DayConsequences = "���������� ��������� ������� ���� ���� ������. �������� �����������. ����� � 11 ����� ������ ������ ������ ��������� ����� �������.";
        else
            DayConsequences = "\t������� ����� ����. ����� ���������� ����. ������� � ����������� ��������.\n";

        foreach (Case delo in todayCases)
        {
            DayConsequences += "\t" + delo.answerActions[AnswerOfCase(delo)].textDayOfTheDay + "\n";
        }
        DayConsequences += "\t��� ���� ��������� � �������� � ������������� ���� ����������� ������������� ����������� �������� � ����������� �����. \n";
        LetterDescription.text = DayConsequences;
        LetterName.text = "�����������";
    }

    public void NextDayClick()
    {

        SoundSource.PlayOneShot(ClockSound);
        //������

        UpdateConsequences();



        //����� � ������� �������� ���������� � ���������� ����������

        foreach (string key in MapSC.TownAreaDict.Keys)
        {



            if (MapSC.TownAreaDict[key].messCame)
            {
                int plusMess = 20 - MapSC.TownAreaDict[key].spirits;
                Debug.Log("���������� ����������� ��� ����� ������� " + Convert.ToString(plusMess));
                Debug.Log("� ������� � ������ ��� ���� " + Convert.ToString(MapSC.TownAreaDict[key].policeUnits.Count));
                MapSC.TownAreaDict[key].AddMess(plusMess - MapSC.TownAreaDict[key].policeUnits.Count * 2); //��������� ����������

                //���� ����� �������� ������� ��������� ��� ��

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
                        Debug.Log("������ ����� ������");

                        todayCases[i].answerActions[j].Do(todayCases[i].caseID);
                    }

                }
            }
            NextDayButtonAnimator.SetBool("AllCasesDone", false);
            if (PlayerState == states.WatchingMap)
                MapSC.ShowArea(MapSC.activeAreaTag);

        }

      

        NewDayProcedures(); //����� ��� ���������, ������� ��� �� ����� ��������� � � �����������.


    }

    void RefreshCalendar()
    {
        TodayText.text = Convert.ToString(today) + " ��������, " + ConvertToDayOfWeek(today, 9);
        for(int i=0; i< 3; i++)
        {
            textDayOfMonth[i].text = Convert.ToString(today + 1 + i) + "\n��������";
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
                MagazineTextLeft.text = "�� ����";
                MagazineTextRight.text = "�����";
                MagazineNumber.text = "����� #11; ������ #25\r\n";
                break;
        }
    }

    void FirstSundayLetter()
    {
        letterImage.sprite = Resources.Load<Sprite>("�������\\Illustrations\\�������������");
        LetterName.text = "����� ������ ������: ������";

        int sumSpirits = 0;
        foreach (string key in MapSC.TownAreaDict.Keys)
            sumSpirits += MapSC.TownAreaDict[key].spirits;

        if (sumSpirits / 4 > 75)
        {
            LetterDescription.text = "   �� ������ ��������, ��� ���� ������ ��� ����������. � ������ �� ������ ������� �� ������ �������� � ������ ���������� � ���������� ������� �� ������ ������, ��� �� ����� �� ��������. ����������� � ��� �� ���� � �����, ���� �����, �� �������� ��� ����� ���-������ ���������.";
            reputation.Authorities += 15;
        }
        else if (sumSpirits/4 > 50)
        {
            LetterDescription.text = "  �� ������� ���������� � ����� �������! ���� ���-��� ���������� � �������, ����� ����� �����, ��� ��� ������. ��� ��� �� ����� ���������� ��� �������� � ������ �������� �������, ����� ����������� ����� ���� � ��������� ��� ���� ���������. ��������, �� ��� ��������.";
            reputation.Authorities += 5;
        }
        else if (sumSpirits/4 <= 50)
        {
            if (AllCases[64].answers[0]) //���� ���� ������ �������
            {
                LetterDescription.text = "   �� �������� ����� ��������� � ����� ������, ��, ������ ���, ���� ��� ����� �������� ����� ������� �������� �� ��� ��������. � ��� ��������� � ��� ���, �� ������������ �� � ��������� ��� ��������� ��������� ������!";
            }
            else if(sumSpirits/4 > 25)
            { 
                LetterDescription.text = "  ����� ��� ����� ��� ������, ��� ������ �����, ���� ������ ����� ������� ������ ��� ����! ���� �������� ������, �� �������� ����� ������� �� �������: ���������� ����� ���, �������� ���� �� �� �������. ���� �� ��� ����������, �� ������ ����-�� ����� ����������� �� ���� �����, �������� ���� �� ����!";
                reputation.Authorities -= 15;
            }
            else
            {
                LetterDescription.text = "   ��, ������, ���� �� ���������! ����� � ������� ���������! ���������� � ������� ��� ������, �������� ��� ����� � ���� ����� ����������! ����� �� �������� �� ��� ������� �����, ���� �� �� ���� �� ����������! �� ������ ��� ���������� � ���� ��� ���� ����, ������ ������, ��� �� ��������� ������ ������. ��������, ��� ��� �������.";
                reputation.Authorities -= 25;
            }    
            

        }
        LetterDescription.text = LetterDescription.text + "\n   �� ������ ����������, ��� ������� ������� ���������� ���������, ����� ������ ���������� ��������. ����� ����� ������!";




    }

    public void OKletter()
    {
        ActiveLetter++;
        if (today % 7 == 0 && ActiveLetter == 0) // ���� ��� �� ���������� � �������� ������� � ���� ������� �����������, �� ���� �������� ������ ������������.
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

            //����� ��������� �����������, ���� ��� ����
            if (todayNotices.Count > 0)
            {   
                noticeObject.SetActive(true);
                noticeImage.sprite = Resources.Load<Sprite>(todayNotices[ActiveNotice].spritePath);
                noticeDescription.text = todayNotices[ActiveNotice].text;
                noticeName.text = todayNotices[ActiveNotice].name;

            }
            else if(today % 7 == 0) //���� ������� ����������� (��������), �� ����� �� ��������� ��������� ����, �.�. � ����������� ��� �� ��������.
            {
                //��������� ��������� ����, �� ��� ����������������� ���������
                NewDayProcedures();
                //������ ������. ������ ������ �����? �� � �� ���� ���� ������.
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
            if (today % 7 == 0) //���� ������� ����������� (��������), �� ����� �� ��������� ��������� ����, �.�. � ����������� ��� �� ��������.
            {
                //��������� ��������� ����, �� ��� ����������������� ���������
                NewDayProcedures();
                //������ ������. ������ ������ �����? �� � �� ���� ���� ������.
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
        if (ActiveCaseObject == 0)  //��� ����� ����������� ������� ����, ������ ����� 0 ��� 1
        {
            ActiveCaseObject = 1;
            Debug.Log("���� 1 �������");
            delo1Animator.SetInteger("Condition", 1);
            delo2Animator.SetInteger("Condition", 3);

        }
        else
        {
            Debug.Log("���� 2 �������");
            ActiveCaseObject = 0;
            delo1Animator.SetInteger("Condition", 3);
            delo2Animator.SetInteger("Condition", 1);
        }
        SoundSource.PlayOneShot(PaperSound);


        if (ActiveCaseNumber == todayCases.Count - 1)
        {
            ActiveCaseNumber = 0;   //��� ����� � ����� ������ ����������� ���
        }
        else
        {
            ActiveCaseNumber++;
        }

        ShowActiveCase();

    }

    public void backwardCase()
    {
        if (ActiveCaseObject == 0)  //��� ����� ����������� ������� ����, ������ ����� 0 ��� 1
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
