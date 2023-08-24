using Assembly_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; //Добавляем чтобы юзать интерфейс и тексты всякие

namespace Assets.Scripts
{

    public class Letter
    {
        public string spritePath;
        public string name;
        public string text;
        public Requirement requirement;

        public Letter(Requirement requirement, string name = "Письмо без названия", string text = "Сегодня утром вы получили очередное бессмысленное письмо", string spritePath = "Спрайты\\Illustrations\\Проверяющий")
        {
            this.spritePath = spritePath;
            this.name = name;
            this.text = text;
            this.requirement = requirement;
        }

    }

    public class notice
    {
        public string spritePath;
        public string name;
        public string text;
        public notice( string name = "Сообщение", string text = "Сегодня в городе снова что-то происходит, но вам до этого уже нет дела.", string spritePath = "Спрайты\\Illustrations\\Помойка")
        {
            this.spritePath = spritePath;
            this.name = name;
            this.text = text;
        }
    }


    //Все дела должны храниться в dictionary по ID. Потом, когда наступает день х, наверное все дела у которых день х идут в специальный отдельный массив,
    //с которым уже будет оперировать игра. Там проставляем ответы. Если работает по ссылке, то всё ок. Если не по ссылке, то добавим поле Id и будем копировать ответы в dictoionary
    //Ещё можно сделать dictionary с выполненным делами кстати.

    [Serializable]
    public class Case
    {


        public static Dictionary<string, int> NoSpirits = new Dictionary<string, int>(); //ОДНА ИЗ ЗАГЛУШЕК ДЛЯ КЛАССА ANSWERACTION
        public static Dictionary<string, string> NoStatus = new Dictionary<string, string>();
        public static Dictionary<string, string> NoPollution = new Dictionary<string, string>();
        public static Dictionary<int, int> NoCasesRequire = new Dictionary<int, int>();
        public static Requirement satisfiedRequirement = new Requirement(NoCasesRequire);


        public bool[] answers; //значение ответов
        public string[] answersTexts; //текст ответов
        public AnswerAction[] answerActions; //действия, которые производятся в зависимости от ответов.
        public Requirement[] reqForAnswers;
        public Requirement reqForCase;

        public string name;
        public string text;
        public int caseID;
        public int DaysDelay;

        public string spritePath;
        

        public Case( string[] answersText, AnswerAction[] answerActions, Requirement[] reqForAnswers, Requirement reqForCase, int caseID, int DaysDelay = 0, string name = "Старое дело", string text = "Это дело совсем истёрлось, оно уже не имеет никакого значения.",
            string spritePath = "Спрайты\\Illustrations\\Проверяющий")
        {

            this.caseID = caseID;
            this.DaysDelay = DaysDelay;
            this.answers = new bool[4];
            for (int i = 0; i < 4; i++)
                answers[i] = false;
            answersTexts = answersText;
            this.answerActions = answerActions;

            this.reqForAnswers = reqForAnswers;
            this.reqForCase = reqForCase;
            this.name = name;
            this.text = text;
            this.spritePath = spritePath;

        }


    }
    
    [Serializable] public class AnswerAction
    {

        bool CanDelay;
        public string textDayOfTheDay;
        int budgetGiven;
        int materialsGiven;
        int provisionGiven;
        int medicineGiven;
        int permissionsGiven;

        int reputationAuthoritiesGiven;
        int reputationZOVgiven;
        int reputationOKOgiven;
        int reputationNIMBgiven;
        int reputationSunriseGiven;


        public Dictionary<string, int> SpiritsIDgiven;// = new Dictionary<string, int>();
        public Dictionary<string, string> PollutionIDgiven;// = new Dictionary<string, int>();
        public Dictionary<string, string> StatusIDGiven;// = new Dictionary<string, int>();


                                                        //ОБЯЗАТЕЛЬНЫЕ ПАРАМЕТРЫ, ЕСЛИ НИЧЕГО НЕ ХОТИМ МЕНЯТЬ - ПРИ ВЫЗОВЕ СТАВИМ ПУСТЫЕ СЛОВАРИ-ЗАГЛУШКИ
        public AnswerAction(Dictionary<string, int> SpiritsIDgiven, Dictionary<string, string> PollutionIDGiven, Dictionary<string, string> StatusIDGiven,
            
           
            bool CanDelay = false,
            string textDayOfTheDay = "", int budgetGiven = 0, int materialsGiven = 0, int provisionGiven =0, int medicineGiven =0, int permissionsGiven = 0, int reputationAuthoritiesGiven=0,
            int reputationZOVgiven = 0, int reputationOKOgiven=0, int reputationNIMBgiven =0, int reputationSunriseGiven =0)
        {
            this.CanDelay = CanDelay;
            this.SpiritsIDgiven = SpiritsIDgiven;
            this.StatusIDGiven = StatusIDGiven;
            this.PollutionIDgiven = PollutionIDGiven;
            this.textDayOfTheDay = textDayOfTheDay;
            this.budgetGiven = budgetGiven;
            this.materialsGiven= materialsGiven;
            this.provisionGiven = provisionGiven;
            this.medicineGiven = medicineGiven;
            this.permissionsGiven= permissionsGiven;
            this.reputationAuthoritiesGiven= reputationAuthoritiesGiven;
            this.reputationZOVgiven= reputationZOVgiven;
            this.reputationOKOgiven= reputationOKOgiven;
            this.reputationNIMBgiven= reputationNIMBgiven;
            this.reputationSunriseGiven= reputationSunriseGiven;

        }

        public void Do(int CaseKey)
        {

            if(Map.GameSC.AllCases[CaseKey].DaysDelay > 0 && this.CanDelay)
            {
                int begin = (Map.GameSC.today+1) * 10 + 1;
                for(int i = begin; i< begin + 10; i++)
                {
                    if (!Map.GameSC.AllCases.ContainsKey(i)) //если дела с таким ключом ещё не существует, то кладём туда наше отложенное дело как раз
                    {
                        Debug.Log("Отложили это дело, его ID " + Convert.ToString(i));
                        Map.GameSC.AllCases[i] = Map.GameSC.AllCases[CaseKey]; //вот интересно, по указателю он это делает или нет. ДА, ПО УКАЗАТЕЛЮ. СЛУЧАЙНО ПОЛУЧИЛОСЬ ПРОВЕРИТЬ.
                        Map.GameSC.AllCases[CaseKey].DaysDelay--;
                        for (int j = 0; j < 4; j++)
                            Map.GameSC.AllCases[i].answers[j] = false;
                        break;
                    }
                }
            }

            
            foreach (string tag in SpiritsIDgiven.Keys) //Добавляем всем по ID то, что нужно
            {
                Debug.Log("Ща добавим спиритс району");
                GameMainScript.MapSC.MapAreaDict[tag].AddSpirits(SpiritsIDgiven[tag]);
            }

            foreach (string tag in StatusIDGiven.Keys)
            {
                Debug.Log("Делаем статус для " + tag);
                GameMainScript.MapSC.MapAreaDict[tag].SetStatus(StatusIDGiven[tag]);
            }

            foreach (string tag in PollutionIDgiven.Keys)
            {
                GameMainScript.MapSC.MapAreaDict[tag].SetPollution(PollutionIDgiven[tag]);
            }

            //для текста дня оставляю строчку

            recoursesOfPlayer.Budget += budgetGiven;
            recoursesOfPlayer.Materials += materialsGiven;
            recoursesOfPlayer.Provision += provisionGiven;
            recoursesOfPlayer.Medicine += medicineGiven;
            recoursesOfPlayer.Permissions += permissionsGiven;

            reputation.Authorities += reputationAuthoritiesGiven;
            reputation.ZOV += reputationZOVgiven;
            reputation.OKO += reputationOKOgiven;
            reputation.NIMB += reputationNIMBgiven;
            reputation.Sunrise += reputationSunriseGiven;

            for (int i = 0; i < 5; i++)
                if (reputation.rep[i] < 0)
                    reputation.rep[i] = 0;
                else if (reputation.rep[i] > 100)
                    reputation.rep[i] = 100;
            for (int i = 0; i < 5; i++)
                if (recoursesOfPlayer.Recourses[i] < 0)
                    recoursesOfPlayer.Recourses[i] = 0;
                else if (reputation.rep[i] > 100)
                    reputation.rep[i] = 100;


            //тут ещё можно репутацию настраивать, день откладывать, но это потом как-нибудь сделаем.

        }

    }

    [Serializable]
    public class Requirement
    {
        int playerBudget;
        int playerMaterials;
        int playerProvision;
        int playerMedicine;
        int playerPermissions;

        int reputationAuthorities;
        int reputationZOV;
        int reputationOKO;
        int reputationNIMB;
        int reputationSunrise;

        Dictionary<int, int> IDOfCaseAndAnswer;





        //ОБЯЗАТЕЛЬНЫЕ ПАРАМЕТРЫ, ЕСЛИ НИЧЕГО НЕ ХОТИМ МЕНЯТЬ - ПРИ ВЫЗОВЕ СТАВИМ ПУСТЫЕ СЛОВАРИ-ЗАГЛУШКИ
        public Requirement(Dictionary<int, int> IDOfCaseAndAnswer,


            int playerBudget =0 , int playerMaterials = 0, int playerProvision = 0, int playerMedicine = 0, int playerPermissions = 0, int reputationAuthorities = 0,
            int reputationZOV = 0, int reputationOKO = 0, int reputationNIMB = 0    , int reputationSunrise = 0)
        {
           this.playerBudget = playerBudget;
           this.playerMaterials = playerMaterials;
            this.playerProvision = playerProvision;
            this.playerMedicine = playerMedicine;
            this.playerPermissions = playerPermissions;
            this.reputationAuthorities=reputationAuthorities;
            this.reputationNIMB=reputationNIMB;
            this.reputationOKO=reputationOKO;
            this.reputationSunrise=reputationSunrise;
            this.reputationZOV=reputationZOV;

            this.IDOfCaseAndAnswer= IDOfCaseAndAnswer;

        }

        public bool Satisfied()
        {


            if (playerBudget > recoursesOfPlayer.Budget
                 || playerMaterials > recoursesOfPlayer.Materials || playerMedicine > recoursesOfPlayer.Medicine
                 || playerPermissions > recoursesOfPlayer.Permissions || playerProvision > recoursesOfPlayer.Provision
                 || reputation.Authorities < this.reputationAuthorities || reputation.ZOV < this.reputationZOV || reputation.OKO < this.reputationOKO ||
                 reputation.NIMB < this.reputationNIMB || reputation.Sunrise < this.reputationSunrise)
                 {
                Debug.Log("Требование не соблюдено");
                 return false;
                }

            if(IDOfCaseAndAnswer.Count != 0)
             foreach (int key in IDOfCaseAndAnswer.Keys)
             {
                 if //сравним ответы, которые дал игрок с ответами, которые нужны для соблюдения требования
                 (!Map.GameSC.AllCases[key].answers[IDOfCaseAndAnswer[key]])
                    { return false; }
             }
            return true;


        }

        public void GiveResources()
        {
            recoursesOfPlayer.Provision -= this.playerProvision;
            recoursesOfPlayer.Budget -= this.playerBudget;
            recoursesOfPlayer.Materials -= this.playerMaterials;
            recoursesOfPlayer.Medicine -= this.playerMedicine;
            recoursesOfPlayer.Permissions -= this.playerPermissions;
        }

        public void UngiveResources()
        {
            recoursesOfPlayer.Provision += this.playerProvision;
            recoursesOfPlayer.Budget += this.playerBudget;
            recoursesOfPlayer.Materials += this.playerMaterials;
            recoursesOfPlayer.Medicine += this.playerMedicine;
            recoursesOfPlayer.Permissions += this.playerPermissions;
        }

    }



}
