using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assembly_CSharp;


public class BaseOfCases : MonoBehaviour
{

    [SerializeField] internal static GameMainScript GameSC; //��� ����-������������ ��������� ������� "������ ����", �� ������� �� ����� ��� ����� ��� ����

    // Start is called before the first frame update
    void Start()
    {
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //�� ��� ���� �������, �� ���-�� � ��� ���� ��������
        //��� ����� ������������� ��������� ���� ���.

        // Debug.Log(GameObject.Find("GameplaySystem").GetComponent<GameMainScript>());

    }

    static public void InitializeLetters()
    {
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //���� ����������� ��� ����� �������������� ������, ���� ��������������, ��� �� ��� ������������� ������� ������ ���������
        Debug.Log("������������� �����: ");

        GameSC.AllLetters[21] = new Letter(Case.satisfiedRequirement, name: "������: ������ ����", text: "  ����������, �������! ����, �� �� �������� ��� ���� ���, �� � ����� ��������, ��� ������ �����, ��� �� ���� ����� ������ ������� �������. � �����, �� �������� � ��� ������, �� ��� ������ ����� �������. ���� �� ������������� ���������, �� ����� ����������. ������, ���� ���������.\n   ����� �� �� ������, �� � ����� ��� �������� � �������, ��� ������������ �������, ��� � ���� ���� �� �������� ����������. � �� ��������� ��������, ���� ����, �������, ���������, �� ������ ���� ����� ����������: ��� ���� � ����? ����� �� � �������� ���������� �� ��� ������? � ����, �������� ����� ���� ������, ��, ���� ������� �����, ������ ����������. ���� ��� ����� �������.\n\n   ���� ������ ����.",
            spritePath: "�������\\Illustrations\\������ ����");

        Debug.Log("������ ������ ����: ");

        GameSC.AllLetters[31] = new Letter(
                new Requirement(new Dictionary<int, int> { [23] = 0 }),
                name: "������: ���������",
                text: " ������ ����, ������� �����������! ���� ��� ��� ������������� ��� �� ��, ��� ����� ����� �������������. � �������, ��� ���� ������ ����� ������� ���� ������ ���� ������, �� � ��� ������ �����, ����� �� ������� �� ������� �������� � ����� ������. ��� �� ������, ����� ���� �������� �� ������� �������, �� ���� � ����� ������ ����� �������� ��������, ��� �� ������� ������� ��� ��������? �� � �����, ��� ����� ������ ����� �������, � ������� � ��� �������. ������ ��� ��� �����, ����� ���� � ��� ����������!\n\n   ��������� ��������, �. �. ��������.",
                spritePath: "�������\\Illustrations\\��������� ������"
                                            );

        GameSC.AllLetters[41] = new Letter(
                Case.satisfiedRequirement,
                name: "������: ������ ����",
                text: " ��� ��� ����������, ������� ����. � ��� �� ����� �����, ��� � ���� ������ ���� � ����� ������ �������. �, ��� �� ������, ���� � ����, �� ��������� ����� � ������ ����! � ����������� ��������� ����� ��� �������� � �������� � ����� ����� �����������, �� ����������.\n   �����������, ������ �������� � � ������ �������! �� ��� ������, ��� �� � ����� ����� ������ � ����, � �� �������������, ��� ��� �������� � ���� ����� �� �������� ����������������� ����������? ��� �������� ������ ���, ��� ��, � ������� ������ �����������, ����������� ��������� ����! ��� ��� ����� ���������. ����� ����� ������!\n\n   ���� ������ ����.",
                spritePath: "�������\\Illustrations\\������ ����"
                                            );

        GameSC.AllLetters[61] = new Letter(
        Case.satisfiedRequirement,
        name: "�����",
        text: " ��������� ����� ������ ���������� �������� ��� �� ���� ������. �� ������ ��������. ����� �������� ��� ������� ���������, �, ���� ��� ��� �� �������, ����� ���������� � ������ ����. �� �������� �� �����, ����� �������� ������ ��������, ����� ��������� ������� �������. ��� ����� ��� ���� ��� ����� �������� ������������� ���� �� ����������� �����, �� ������ �� �������� �������������� ������ ������ � �����. ������� �� ���� ������� � �������� �������������� �������? � ������ �� �� ���� ���-�� ��������? �� �� ������ ����� ������ �� ��� �������, �� ����, ���, �� ���.",
        spritePath: "�������\\Illustrations\\�����"
                                    );

        GameSC.AllLetters[71] = new Letter(
              new Requirement(new Dictionary<int, int> { [31] = 0}),
              name: "������: ���������",
              text: "   ������ ����, ������� �����������. �� ���� �� ������, �� ������ ����� ���� �����������. � ����� �� �������� � ������ �� ���� ������� ����������. ��� �� �� ��� ������, ���� ��� ���-��� ��������. ��������, �����������, � ������� � �������, ������ ����� ��������� �� ����������, ��� ��� ������ ��� ����� �� ���������� ���������� � ����� ��������� ������. �������� �������. ��������, ��� ���� ����������� - �.�.�. - ����� ���� ���������� ���� ����. ������� �� ���������, �� �������.\n\n   ���������.",
              spritePath: "�������\\Illustrations\\��� ������"
                                          );

        Debug.Log(GameSC.AllLetters.Count + " - ������� ����� ����� �������� BaseOfCases");

    }

    static public void InitializeCases()
    {
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //����� ����� � ������

        GameSC.AllCases[11] =
           new Case(caseID: 11,
           name: "����� ������",
           spritePath: "�������\\Illustrations\\�����",
           text: "������� ������� �� ����� ������� ������ �� ������ ��� �������. ��� ������� ������ ��� ��������� � ������ � �������� ���������� ����� ������������� ������, �����, ������, ��������� �� ��� � ����������� �������, ������� � ��� ���. ������� ����� ��� ���������� ����� �����, ���� ������ � ���� �� ����������� - ����� �������� ����������. ��� ������?",
           answersText: new string[2]  {
                                            "�������� � ��������� �����",
                                            "���������� ������� �����"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(new Dictionary<string, int> { ["square"] = -15 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� ����� � ������ � ������ ��� ���������. ��������� ���� � ������, ���� ��������� � ������� �������� � ������������ ��������.",
                                    reputationAuthoritiesGiven: -10),

                                    new AnswerAction(new Dictionary<string, int> { ["workLeft"] = -10, ["workRight"] = -10, ["stock"] = -10 }, Case.NoPollution, Case.NoStatus,
                                    CanDelay: true,
                                    textDayOfTheDay: "����� �� �������� ������� ������. ������� ���������� �� ��� ����������, �� ������� � ��������� ������ ������ ��������� � ��� ����.")

                               }, 
           reqForAnswers: new Requirement[2]
                               {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                               },
           reqForCase: Case.satisfiedRequirement

           );

        GameSC.AllCases[12] =
           new Case(caseID: 12,
           name: "����������",
           spritePath: "�������\\Illustrations\\�����",
           text: "���������� �� ���������-��-������, ���������� ��������� � ������� � �������� ���������� � �������, ������� �� ��� ������ � ������ �����, ����� ���������� ��� �������� ��� ����� �����. �� ��������� ����� ������ ����� ����� ������ - ����������� ������� �������� � ��������.",
           answersText: new string[2]  {
                                            "��������� ����� (10 ��������)",
                                            "������������"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, StatusIDGiven: new Dictionary<string, string> {
                                                                                                                                                     
                                                                                                                                      },
                                    textDayOfTheDay: "����� �� ������� ��������� ����� ��� ���������, � ��� ������� ��������� � ��������-��-������" ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ���������� ��������� ����� ��� ��������� (���� �� ������� ��������), �� � �������� �������� ����� �� ���������. �������� � ������� ���� ������� �������� �� ����� ����� �������.")

                               },
           reqForAnswers: new Requirement[2]
                               {
                                    new Requirement(new Dictionary<int, int>(), playerProvision: 10),
                                    Case.satisfiedRequirement,
                               },
           reqForCase: Case.satisfiedRequirement

           );

        GameSC.AllCases[13] =
           new Case(caseID: 13,
           name: "������ ����",
           spritePath: "�������\\Illustrations\\������",
           text: "�� ������ ����������� �� ������ ����-������������ ����������� �������� ���� �� ����� �������� ������� ���������� ��� ������� ����������, ������� ����� � ������� ������ ���. ���� ������ ������� ����� ����� � ������ ������, ��?",
           answersText: new string[2]  {
                                            "�������� ������� (50 �������)",
                                            "������������"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� ������� ������� � ����� �� ����������� ����������. �� ������������ ��� ������� ������, ��������, ���� ���������� ��� ���������� ����-�� �� ���." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ��������������� ����������� ������� ��������� � ������� (���� �� ������� �������). ������ ��� ����� �� ��������.")

                               },
           reqForAnswers: new Requirement[2]
                               {
                                    new Requirement(new Dictionary<int, int>(), playerBudget: 50),
                                    Case.satisfiedRequirement,
                               },
           reqForCase: Case.satisfiedRequirement

           );

        //���� 2

        GameSC.AllCases[21] =
         new Case(caseID: 21,
         DaysDelay: 3,
         name: "�������� �����������",
         spritePath: "�������\\Illustrations\\�����",
         text: "���������� ��������, ��� ����� � ��������� ������ ��������� ����������� ������� �����������. ���� ������� �� ������ ��� ��������, �������� ���� ������ ����������������, ��� ����� �������� �� ����� ��������� ���� �������.",
         answersText: new string[2]  {
                                            "������ (-20 ����������)",
                                            "����� (�� �������)"
                                     },

         answerActions: new AnswerAction[2] {
                                    new AnswerAction(new Dictionary<string, int> {["stock"] = 15}, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� ������� ������� �������� ���������� � ��������� ������, ��� ����� ���������� ���. ������ ����� �������� ������, �� ������ ��� �� ������ �����." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    CanDelay: true,
                                    textDayOfTheDay: "����� �� ������ �������� ���� � ��������� �����������. ��� ����� �������� � ��������� �� ����������� ����.")

                             },
         reqForAnswers: new Requirement[2]
                             {
                                    new Requirement(new Dictionary<int, int>(), playerMaterials: 20),
                                    Case.satisfiedRequirement,
                             },
         reqForCase: Case.satisfiedRequirement

         );

        GameSC.AllCases[22] =
         new Case(caseID: 22,
         name: "������������",
         spritePath: "�������\\Illustrations\\������� ����",
         text: "� ����� ������� ������� ����� ��������� �������. ������� ���������� ������ ������ �������������� ������������ ������ ��� ��������, ��� ����� ������ �������� � ������� ��������� ������� �� ������. ���� �� ����������, ��� ������� ������ � ������ �����.",
         answersText: new string[2]  {
                                            "������� (-10 ��������)",
                                            "����������"
                                     },

         answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������� ������������ ������, ��������� �� ��������. ��������, � ������� ��� ������������ �����-�� ������� � ������." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ���������� ������� ������������ ������. �� ������, ��� ������������� � ����� ������ ������������� ������� �������.")

                             },
         reqForAnswers: new Requirement[2]
                             {
                                    new Requirement(new Dictionary<int, int>(), playerProvision: 10),
                                    Case.satisfiedRequirement,
                             },
         reqForCase: Case.satisfiedRequirement

         );

         GameSC.AllCases[23] =
         new Case(caseID: 23,
         name: "� ����� �� ����",
         spritePath: "�������\\Illustrations\\���������",
         text: "��������-��������� � ���� �������� ����� ��������� ��������, ������� ����� ����� � ���� ����������� � ���������� �������. �� ��������� ������, ���� ��������� ������� ��� ����� � ������.",
         answersText: new string[2]  {
                                            "�����������",
                                            "������������"
                                     },

         answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� ������� ������� � ����� �� ����������� ����������. �� ������������ ��� ������� ������, ��������, ���� ���������� ��� ���������� ����-�� �� ���." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ���������������� ���������� ��������. ������ ��� ����� �� ��������.")

                             },
         reqForAnswers: new Requirement[2]
                             {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                             },
         reqForCase: Case.satisfiedRequirement

         );

        //���� 3

        GameSC.AllCases[31] =
       new Case(caseID: 31,
       name: "����������",
       spritePath: "�������\\Illustrations\\���",
       text: "�������� �������� ������ �������, ��� �������� �� ������� ��������� ���������� ������������ ������ ����������� ������, ������� ���� �� ����������� ����� ����� ������. ������� ���� �� �������� ������� �������, �� �� ��������� ����� ������� ��������.",
       answersText: new string[2]  {
                                            "������� ����������:",
                                            "������������"
                                   },

       answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ��������� ���������� �� ��������� ����������, ����� ��������, ��� ������ �� �������. �� ������ ����������, ������� �������� � ���� ������ � �������, ��� ������ ������ �� ����������." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������ �� ������� ������ ������ � ��������� ����������. �������, ��� ����� ������ ������� �� ������� � ����� ��������. ������ ��� ����� �� ��������.")

                           },
       reqForAnswers: new Requirement[2]
                           {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                           },
       reqForCase: Case.satisfiedRequirement

       );


        GameSC.AllCases[32] =
        new Case(caseID: 32,
        DaysDelay: 7,
        name: "�������������",
        spritePath: "�������\\Illustrations\\��",
        text: "�����-�� ������� ���������� � ����� ������ �� ������������ �������� � �����. �����-�� ������������ ���������, ��� ����� ���-���� ������� � �������� ��������� ��������, ������� ��� �������� ��������� �������� ������, ������� �� �������. ��������� ���������� �� �������.",
        answersText: new string[2]  {
                                            "������ (-20 ����������):",
                                            "����� (�� ��. �����):"
                                  },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus, 
                                    textDayOfTheDay: "����� �� ��������������� �������� ������ �������, ��������� � �������� �� ����������." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    CanDelay: true,
                                    textDayOfTheDay: "����� �� ������ �������� ���� � ���������� �������. ��� ����� �������� � ��������� �� ����������� ����.")

                          },
        reqForAnswers: new Requirement[2]
                          {
                                    new Requirement(new Dictionary<int, int>(), playerMaterials: 20),
                                    Case.satisfiedRequirement,
                          },
         reqForCase: new Requirement(new Dictionary<int, int>{ [22] = 1 })

         );

        GameSC.AllCases[33] =
        new Case(caseID: 33,
        name: "����� ����",
        spritePath: "�������\\Illustrations\\��������",
        text: "������� �������� ��������, ��� ��� ����� ������� �������� ����������� ��������� ����� �������� ������ �� ������ �� ���������-��-������, �. �. ��� �������� ��������� ���� ������� � �������� ����������.",
        answersText: new string[2]  {
                                            "������� (-40 ����������):",
                                            "������������"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� ��������� ����� �������� ������ �� ���������-��-������. ������ ��������� ������� �� ����� ������ ����� ���������� ��� �������, �� �� ����� ��������, ��� ������ ������ �������� �� ������ �������." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ���������� ������� ����� �������� ������ �� ���������-��-������. ��� �����, �����, ��� � � �������.")

                            },
        reqForAnswers: new Requirement[2]
                            {
                                    new Requirement(new Dictionary<int, int>(), playerMaterials: 40),
                                    Case.satisfiedRequirement,
                            },
        reqForCase: Case.satisfiedRequirement

        );


        GameSC.AllCases[41] =
        new Case(caseID: 41,
        name: "��������",
        spritePath: "�������\\Illustrations\\�����",
        text: "������� � ������ �������� ���������� � ����������� ��������� �����. ��������� �������, ��� ������� �������� �������� ������������� �������, ��� ��� ��� ����� �������� �� ������� ���. ��� ���� ������ �������.",
        answersText: new string[2]  {
                                           "�������� (-20 ��������):",
                                           "��������:"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven: new Dictionary<string, int> { ["square"] = 10, ["workLeft"] = 10, ["workRight"] = 10, ["stock"] = 10, ["factory"] = 10 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������ �������� �������� ���� ������. ����� ���� ���� ������ ���������� ���, ��� �������, ��� �� ��������� �� ��������, ��� ����������� ����� ������ ����." ),

                                    new AnswerAction(SpiritsIDgiven: new Dictionary<string, int> { ["square"] = -5, ["workLeft"] = -15, ["workRight"] = -15, ["stock"] = -5, ["factory"] = -5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������ ������ �������� ���� ������. ���� ���� ��������� ����� ��������, �� ������ ��������. ���� ������� ������ ��������� ���� ������� �������� ������.")

                            },
        reqForAnswers: new Requirement[2]
                            {
                                    new Requirement(new Dictionary<int, int>(), playerProvision: 20),
                                    Case.satisfiedRequirement,
                            },
        reqForCase: Case.satisfiedRequirement

        );

        GameSC.AllCases[42] =
        new Case(caseID: 42,
        name: "������",
        spritePath: "�������\\Illustrations\\�����",
        text: "�����-�� ���� �������� �������� ���������� ������, ���������� �������� �������������� ��������� �����. ���������� ��� �� �� ����������. �� ������ ���� ����� �������� �������� �����, �� ������� ������� ���� � ��� �������� ���������. ��� ������?",
        answersText: new string[2]  {
                                           "����� (+20 �������):",
                                           "���������:"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus, budgetGiven: 20, reputationSunriseGiven: -10,
                                    textDayOfTheDay: "����� �� ����� ����� � �����, ������� �������� �������� ������. ����� �� ����� ������ ����� ��������, �� ���� �� �����������, ��� ��� ��� �� ��������, �� �������� ������ �������." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �� ����� ����� ����� � ������ �����, �� ��� ��� ���� ��� ����� ����������. ���� ������� ����� �� �� ��� �� ��������.")

                            },
        reqForAnswers: new Requirement[2]
                            {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                            },
        reqForCase: Case.satisfiedRequirement

        );

        GameSC.AllCases[43] =
        new Case(caseID: 43,
        name: "�������",
        spritePath: "�������\\Illustrations\\�����",
        text: "�������� �������� ����� �������, ����� ������ ���� ������ ������������ ������� �����, �� � ������ �� ������ ���. ����� ���������� �������� ������� �� ���������-��-������.",
        answersText: new string[2]  {
                                           "�������� (-20 �������):",
                                           "������������"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven: new Dictionary<string, int> { ["square"] = 5, ["workLeft"] = 5, ["workRight"] = 5, ["stock"] = 5, ["factory"] = 5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� ������� �� ��������� ��� ���������. ��� ������ ���� ����." ),

                                    new AnswerAction(SpiritsIDgiven: new Dictionary<string, int> { ["square"] = -5, ["workLeft"] = -5, ["workRight"] = -5, ["stock"] = -5, ["factory"] = -5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������� ������� �� �������� ����������� ������� �� ���������. ������ ��� �������� �� �� �����, ���� ��������� � ������ �������� � ��������.")

                            },
        reqForAnswers: new Requirement[2]
                            {
                                    new Requirement(new Dictionary<int, int>(), playerBudget: 20),
                                    Case.satisfiedRequirement,
                            },
        reqForCase: new Requirement(new Dictionary<int, int>() { [33] = 1 })

        );

        GameSC.AllCases[44] =
        new Case(caseID: 44,
        name: "�������",
        spritePath: "�������\\Illustrations\\�����",
        text: "�������� �������� ����� �������, ����� ������ ���� ������ ������������ ������� �����, �� � ������ �� ������ ���. ����� ���������� �������� ������� �� ���������-��-������. ��������� ������� �� ��������� ���� �������� ������, ��� ������� ����� ��������� �������� ��������.",
        answersText: new string[2]  {
                                           "�������� (-5 �������):",
                                           "������������"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["square"] = 5,["workLeft"] = 5,["workRight"] = 5,["stock"] = 5,["factory"] = 5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� ������� �� ��������� ��� ���������. ��� ������ ���� ����." ),

                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["square"] = -5,["workLeft"] = -5,["workRight"] = -5,["stock"] = -5,["factory"] = -5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������� ������� �� �������� ����������� ������� �� ���������. ������ ��� �������� �� �� �����, ���� ��������� � ������ �������� � ��������.")

                            },
        reqForAnswers: new Requirement[2]
                            {
                                    new Requirement(new Dictionary<int, int>(), playerBudget: 5),
                                    Case.satisfiedRequirement,
                            },
        reqForCase: new Requirement(new Dictionary<int, int>() { [33] = 0 })

        );

        GameSC.AllCases[51] =
              new Case(caseID: 51,
              name: "���������� ��������",
              spritePath: "�������\\Illustrations\\�����",
              text: "�������� �������� �����. ���� �������� �� ��������, �� ����� � �� �������. ������������� ����� �������� ���� ������ � ������� �������, ������� �������, ����� �� ������������ ����������� �� ������� ������� � �����, ������������ ������ � ����� ����� ������.",
              answersText: new string[2]  {
                                           "������������",
                                           "������������"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    reputationAuthoritiesGiven: 10,
                                    reputationSunriseGiven: -10,
                                    textDayOfTheDay: "����� �� ������������ ����������� �������� �� �������, ������� �������� �������������� ����. ���� ������ �������� ����! � ��� ����������� ��� ����������� - ���." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    reputationAuthoritiesGiven: -15,
                                    textDayOfTheDay: "����� �� �������� ������������� � �������������� ����. ������������� ��������, ��� ���� ��������� ��� ������������ ������ ����� �������, �� ���� �� ��� ��������� ���������.")

                                  },
              reqForAnswers: new Requirement[2]
                                  {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                                  },
              reqForCase: Case.satisfiedRequirement

              );

        GameSC.AllCases[52] =
              new Case(caseID: 52,
              name: "����� ������",
              spritePath: "�������\\Illustrations\\�����",
              text: "��������� ������� ��������� ������� ����� ������, ���� ���� � ��������� ���������. ���������� ��� �� �� ����������. �� ������ ���� ����� �������� �������� �����, �� ������� ������� ���� � ��� �������� ���������. � ������ �������, ������� - ��� ��������������� �������������. ��� ������?",
              answersText: new string[2]  {
                                           "����� (+20 �������)",
                                           "���������"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    budgetGiven: 20,
                                    reputationSunriseGiven: -10,
                                    textDayOfTheDay: "����� �� ����� ����� � �����, ������� ��������� �������. ����� �� ����� ������ ����� ��������, �� ���� �� �����������, ��� ��� ��� �� ��������, �� �������� ������ �������." ),

                                    new AnswerAction (Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    reputationAuthoritiesGiven: -10,
                                    textDayOfTheDay: "����� �� �� ����� ����� ����� � ������ �����, �� ��� ��� ���� ����� ��� ����������. �� ��� ������ ��� �������� �� ��, ��� �� ��������� �����, ���������� �������������� ������� ������, ������ �������.")

                                  },
              reqForAnswers: new Requirement[2]
                                  {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                                  },
              reqForCase: Case.satisfiedRequirement

              );

        GameSC.AllCases[53] =
              new Case(caseID: 53,
              name: "���������",
              spritePath: "�������\\Illustrations\\�����",
              text: "��������� ���������� ����������� ������ ������ ���������� �� ����������� ���������, ����������� ������� �� ��������� ����������. ������ ����� ������� ���������� ���� ���������. ������� ��, ��������� ����� �� �������, �� ������� �� ����.",
              answersText: new string[2]  {
                                           "���������",
                                           "���������"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["square"] = -20,["workLeft"] = 5,["workRight"] = 5,["stock"] = 5,["factory"] = 5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ��������� ��������� ������������ ��������� ��� ����������. ����� ��� ���, � ��� ��������� - ���. " ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ��������� ���������� ��������� ��� ����������, ��-�� ���� �������� ���� ����� ����������. ���� ��������� � ����� ������� �� �������.")

                                  },
              reqForAnswers: new Requirement[2]
                                  {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                                  },
              reqForCase: Case.satisfiedRequirement

              );

        GameSC.AllCases[61] =
              new Case(caseID: 61,
              name: "�������",
              spritePath: "�������\\Illustrations\\�����",
              text: "������ �������� ����, ��������, ��� ������� ������������ ������ �������� ��������� ����� ����� ������������ ��������, ����� \"��� ����� �� ��������� �����, � ���� �� ������������ ��������� ����� ��������\".",
              answersText: new string[2]  {
                                           "������� ��� �������",
                                           "������� ��� ����������",
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["workLeft"] = 10,["workRight"] = 10 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������, ��� ����� ����� ������������ �������� ����� ��� ������ ������ �� ������. ������� ������ �����������!" ),

                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["square"] = 20 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������, ��� ����� ����� ������������ �������� ����� ��� ���������� ����� ����������. ������� ������� ������ �����������!")

                                  },
              reqForAnswers: new Requirement[2]
                                  {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                                  },
              reqForCase: Case.satisfiedRequirement

              );

        GameSC.AllCases[62] =
              new Case(caseID: 62,
              name: "���������",
              spritePath: "�������\\Illustrations\\�������",
              text: "�����, ������� �� ������, ������� ������� ���� �������� �����, ���� � ����� ������� �� ������� �����������. � ��� ������ ����� �� �������, � �� ������ ������ ������ ��������� ��� ����� ��������� ����� �����.",
              answersText: new string[2]  {
                                           "��������� (-20 �������)",
                                           "������������"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� ������ ����� ��������� ��������. ����� ���� ����� ��� ����������." ),

                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["workLeft"] = -10,["workRight"] = -10 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �� �������� ����� ����� ��������� ��������. ������� ������ �������� � �������, ��� ��� �� ��� �������.")

                                  },
              reqForAnswers: new Requirement[2]
                                  {
                                    new Requirement(new Dictionary<int, int>() ,playerBudget: 20),
                                    Case.satisfiedRequirement,
                                  },
              reqForCase: Case.satisfiedRequirement

              );

        GameSC.AllCases[63] =
              new Case(caseID: 63,
              name: "�������� �������",
              spritePath: "�������\\Illustrations\\�����������",
              text: "������� � ����� ������ ����������� �� �������������, ����� ������ ���������� � ������ ����� ������ ������ �������. �� ���������� ���, ������ �� ������ ������ ��������?",
              answersText: new string[2]  {
                                           "��� ������",
                                           "��� �����"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������� ������������, ��� � ��� � ������ ��� ������." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ������� ������������, ��� � ��� � ������ ��� �������� �����.")

                                  },
              reqForAnswers: new Requirement[2]
                                  {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                                  },
              reqForCase: Case.satisfiedRequirement

              );

     GameSC.AllCases[64] =
      new Case(caseID: 64,
      name: "������� �������",
      spritePath: "�������\\Illustrations\\������",
      text: "��� ��������� ��� ������� ������, � ������� �� ������������� � �����������, ������ � ��� ��� ������� �����. �� �������, ��� ���� �� ��� ������ ��� ������, ��� ����� ����� ������ ��� ������, � �����������.",
      answersText: new string[2]  {
                                           "���� ������ (-50 �������)",
                                           "������������"
                                  },

      answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� �������� ������� ������ ����� �����. ������ ���������, ��� �� ���." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ��������������� ������ �������� �������. �� �� �������. ������ ��� ����� �� ��������.")

                          },
      reqForAnswers: new Requirement[2]
                          {
                                    new Requirement(new Dictionary<int, int>(), playerBudget: 50),
                                    Case.satisfiedRequirement,
                          },
      reqForCase: new Requirement(new Dictionary<int, int>() { [13] = 0})

      );

        GameSC.AllCases[81] =
           new Case(caseID: 81,
           name: "������� ������ 1",
           spritePath: "�������\\Illustrations\\������",
           text: "��� ��������� ��� ������� ������, � ������� �� ������������� � �����������, ������ � ��� ��� ������� �����. �� �������, ��� ���� �� ��� ������ ��� ������, ��� ����� ����� ������ ��� ������, � �����������.",
           answersText: new string[2]  {
                                           "���� ������ (-50 �������)",
                                           "������������"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� �������� ������� ������ ����� �����. ������ ���������, ��� �� ���." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ��������������� ������ �������� �������. �� �� �������. ������ ��� ����� �� ��������.")

                               },
           reqForAnswers: new Requirement[2]
                               {
                                    new Requirement(new Dictionary<int, int>(), playerBudget: 50),
                                    Case.satisfiedRequirement,
                               },
           reqForCase: new Requirement(new Dictionary<int, int>() { [13] = 0 })

           );

        GameSC.AllCases[82] =
           new Case(caseID: 82,
           name: "������� ������ 2",
           spritePath: "�������\\Illustrations\\������",
           text: "��� ��������� ��� ������� ������, � ������� �� ������������� � �����������, ������ � ��� ��� ������� �����. �� �������, ��� ���� �� ��� ������ ��� ������, ��� ����� ����� ������ ��� ������, � �����������.",
           answersText: new string[2]  {
                                           "���� ������ (-50 �������)",
                                           "������������"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� �������� �������� ������� ������ ����� �����. ������ ���������, ��� �� ���." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "����� �� ��������������� ������ �������� �������. �� �� �������. ������ ��� ����� �� ��������.")

                               },
           reqForAnswers: new Requirement[2]
                               {
                                    new Requirement(new Dictionary<int, int>(), playerBudget: 50),
                                    Case.satisfiedRequirement,
                               },
           reqForCase: new Requirement(new Dictionary<int, int>() { [13] = 0 })

           );






    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
