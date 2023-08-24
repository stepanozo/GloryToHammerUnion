using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assembly_CSharp;


public class BaseOfCases : MonoBehaviour
{

    [SerializeField] internal static GameMainScript GameSC; //Это один-единственный экземпляр объекта "Скрипт игры", на котором всё висит что нужно для игры

    // Start is called before the first frame update
    void Start()
    {
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //мб это надо удалить, всё как-то и без него работает
        //ТУТ БУДЕТ ИНИЦИАЛИЗАЦИЯ АБСОЛЮТНО ВСЕХ ДЕЛ.

        // Debug.Log(GameObject.Find("GameplaySystem").GetComponent<GameMainScript>());

    }

    static public void InitializeLetters()
    {
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //надо обязательно это перед инициализацией делать, чтоб удостовериться, что мы уже существующему скрпиту письма добавляем
        Debug.Log("Инициализация писем: ");

        GameSC.AllLetters[21] = new Letter(Case.satisfiedRequirement, name: "Письмо: старый друг", text: "  Здравствуй, дружище! Знаю, мы не общались уже пару лет, но я решил написать, как только узнал, что ты стал мэром нашего родного городка. В общем, ты наверное и сам знаешь, но эта работа очень опасная. Ведь то Правительство подставит, то народ напортачит. Короче, будь осторожен.\n   Может ты не знаешь, но я давно уже переехал в Кладьму, тот строительный городок, что в паре миль от Великого Старгорода. Я бы рассказал побольше, если тебе, конечно, интересно, но сейчас меня лично интересует: как дела у тебя? Зачем ты в принципе согласился на эту работу? Я знаю, пропасть между нами велика, но, если найдешь время, ответь пожалуйста. Буду рад твоим письмам.\n\n   Твой старый друг.",
            spritePath: "Спрайты\\Illustrations\\Старый друг");

        Debug.Log("Первое письмо есть: ");

        GameSC.AllLetters[31] = new Letter(
                new Requirement(new Dictionary<int, int> { [23] = 0 }),
                name: "Письмо: Профессор",
                text: " Добрый день, товарищ управляющий! Хочу еще раз поблагодарить вас за то, что нашли время познакомиться. Я понимаю, что ваша служба нашей державе куда важнее моих просьб, но я вас крайне прошу, чтобы вы следили за уровнем медицины в вашем городе. Как вы знаете, почти всех ресурсов не хватает городам, но если в вашем городе вдруг случится эпидемия, что вы сможете сделать без лекарств? Ну и также, это может помочь моему проекту, о котором я вам говорил. Напишу еще раз позже, когда дело с ним прояснится!\n\n   Профессор биологии, А. Н. Костенко.",
                spritePath: "Спрайты\\Illustrations\\Профессор письмо"
                                            );

        GameSC.AllLetters[41] = new Letter(
                Case.satisfiedRequirement,
                name: "Письмо: старый друг",
                text: " Еще раз здравствуй, дорогой друг. Я все же долго думал, как я могу помочь тебе с твоей тяжкой работой. Я, как ты знаешь, хоть и поэт, но некоторые связи с чинами имею! Я поспрашивал некоторых людей про ситуацию с кризисом и узнал много интересного, но печального.\n   Оказывается, кризис наступил и в других странах! Но как хорошо, что мы с тобой живем именно в СКСМ, а то представляешь, что там творится у этих невеж из Звездной Капиталистической Республики? Мой знакомый сказал мне, что мы, с помощью нашего патриотизма, обязательно переживем всех! Так что мысли позитивно. Слава союзу молота!\n\n   Твой старый друг.",
                spritePath: "Спрайты\\Illustrations\\Старый друг"
                                            );

        GameSC.AllLetters[61] = new Letter(
        Case.satisfiedRequirement,
        name: "Вечер",
        text: " Наступает вечер вашего последнего рабочего дня на этой неделе. На улицах спокойно. Кроны деревьев уже немного пожелтели, и, хоть это еще не заметно, осень расцветает в полной мере. Вы выходите на улицу, чтобы подышать свежим воздухом, перед принятием сложных решений. Уже через два часа вам снова придется рассматривать дела на сегодняшний вечер, но сейчас вы спокойно рассматриваете статую Ленина в парке. Выстоит ли ваша держава в условиях непреодолимого кризиса? И можете ли вы хоть что-то изменить? Вы бы хотели знать ответы на эти вопросы, но пока, увы, их нет.",
        spritePath: "Спрайты\\Illustrations\\Город"
                                    );

        GameSC.AllLetters[71] = new Letter(
              new Requirement(new Dictionary<int, int> { [31] = 0}),
              name: "Письмо: Полковник",
              text: "   Добрый день, товарищ управляющий. Вы меня не знаете, но можете звать меня Полковником. В среду вы прислали к одному из моих отрядов разведчика. Раз уж вы нас видели, хочу вам кое-что сообщить. Поверьте, организация, в которой я работаю, никому вреда причинять не собирается, так что крайне вас прошу не разглашать информацию о нашем появлении никому. Особенно властям. Считайте, что наша организация - З.О.В. - всего лишь расследует одно дело. Спасибо за понимание, до скорого.\n\n   Полковник.",
              spritePath: "Спрайты\\Illustrations\\ЗОВ письмо"
                                          );

        Debug.Log(GameSC.AllLetters.Count + " - столько всего писем показала BaseOfCases");

    }

    static public void InitializeCases()
    {
        GameSC = GameObject.Find("GameplaySystem").GetComponent<GameMainScript>(); //ОЧЕНЬ ВАЖНО В НАЧАЛЕ

        GameSC.AllCases[11] =
           new Case(caseID: 11,
           name: "Страх народа",
           spritePath: "Спрайты\\Illustrations\\Толпа",
           text: "Новость властей об эпохе кризиса прошла по городу как торнадо. Все рабочие города уже собрались у ратуши и пытаются прорваться через патрулирующих солдат, чтобы, видимо, добраться до вас и потребовать ответов, которых у вас нет. Оборона армии еле сдерживает поток людей, если быстро с этим не разобраться - могут начаться беспорядки. Что делать?",
           answersText: new string[2]  {
                                            "Впустить и успокоить народ",
                                            "Сдерживать оборону силой"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(new Dictionary<string, int> { ["square"] = -15 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы впустили народ в ратушу и смогли его успокоить. Чиновники были в ярости, ваши отношения с главной площадью и государством ухудшены.",
                                    reputationAuthoritiesGiven: -10),

                                    new AnswerAction(new Dictionary<string, int> { ["workLeft"] = -10, ["workRight"] = -10, ["stock"] = -10 }, Case.NoPollution, Case.NoStatus,
                                    CanDelay: true,
                                    textDayOfTheDay: "Вчера вы сдержали оборону ратуши. Порядок управления не был потревожен, но рабочие и складской районы теперь относятся к вам хуже.")

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
           name: "Иммигранты",
           spritePath: "Спрайты\\Illustrations\\Поезд",
           text: "Иммигранты из Шитоевска-На-вязьме, напуганные новостями о кризисе и усилении дисциплины в городах, требуют от вас поезда в родной город, чтобы продолжать там работать для своих семей. Но снарядить поезд сейчас будет очень трудно - потребуется собрать персонал и провизию.",
           answersText: new string[2]  {
                                            "Снарядить поезд (10 провизии)",
                                            "Игнорировать"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, StatusIDGiven: new Dictionary<string, string> {
                                                                                                                                                     
                                                                                                                                      },
                                    textDayOfTheDay: "Вчера вы успешно снарядили поезд для мигрантов, а они успешно вернулись в Шитоевск-На-Вязьме" ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы отказались снаряжать поезд для мигрантов (либо не хватило провизии), но в нынешней ситуации никто не обозлился. Возможно в будущем ваше решение повлечет за собой новые события.")

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
           name: "Важные люди",
           spritePath: "Спрайты\\Illustrations\\Письмо",
           text: "По вашему становлению на службу мэра-управляющего Скважинском некотрые чины из более развитых городов предлагают вам дорогое знакомство, которое может в будущем помочь вам. Ведь всегда полезно иметь связи с умными людьми, да?",
           answersText: new string[2]  {
                                            "Устроить встречу (50 бюджета)",
                                            "Игнорировать"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы устроили дорогую встречу с одним из влиятельных чиновников. Он представился как министр Даниил, возможно, ваше знакомство еще пригодится кому-то из вас." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы проигнорировали предложение некоего чиновника о встрече (либо не хватило бюджета). Больше вас никто не тревожил.")

                               },
           reqForAnswers: new Requirement[2]
                               {
                                    new Requirement(new Dictionary<int, int>(), playerBudget: 50),
                                    Case.satisfiedRequirement,
                               },
           reqForCase: Case.satisfiedRequirement

           );

        //День 2

        GameSC.AllCases[21] =
         new Case(caseID: 21,
         DaysDelay: 3,
         name: "Проблема водопровода",
         spritePath: "Спрайты\\Illustrations\\Трубы",
         text: "Патрульные доложили, что утром в складском районе произошла неожиданная поломка водопровода. Если вовремя не решить эту проблему, доставка воды станет затрудннительной, что точно скажется на вашей репутации всех районов.",
         answersText: new string[2]  {
                                            "Чинить (-20 материалов)",
                                            "Ждать (до пятницы)"
                                     },

         answerActions: new AnswerAction[2] {
                                    new AnswerAction(new Dictionary<string, int> {["stock"] = 15}, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера рабочие успешно починили водопровод в складском районе, его народ благодарит вас. Теперь трубы держатся крепко, их хватит еще на долгое время." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    CanDelay: true,
                                    textDayOfTheDay: "Вчера вы решили отложить дело о сломанном водопроводе. Оно будет помещено к остальным на сегодняшний день.")

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
         name: "Подкрепления",
         spritePath: "Спрайты\\Illustrations\\Солдаты СКСМ",
         text: "В столь тяжелые времена важно сохранять порядок. Столица предлагает вашему городу дополнительное подкрепление солдат для патрулей, что может помочь избежать в будущем нарушений законов на улицах. Если вы откажетесь, эти солдаты поедут в другой город.",
         answersText: new string[2]  {
                                            "Принять (-10 провизии)",
                                            "Отказаться"
                                     },

         answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы приняли подкрепление солдат, обеспечив им провизию. Возможно, в будущем это предотвратит какие-то события в городе." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы отказались принять подкрепления солдат. По слухам, это подразделение в итоге отдали строительному городку Кладьме.")

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
         name: "С глазу на глаз",
         spritePath: "Спрайты\\Illustrations\\Профессор",
         text: "Нежданно-негаданно с вами связался некий профессор биологии, который очень хочет с вами встретиться и поговорить наедине. По городским слухам, этот профессор однажды уже сидел в тюрьме.",
         answersText: new string[2]  {
                                            "Встретиться",
                                            "Игнорировать"
                                     },

         answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы устроили дорогую встречу с одним из влиятельных чиновников. Он представился как министр Даниил, возможно, ваше знакомство еще пригодится кому-то из вас." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы проигнорироваали профессора биологии. Больше вас никто не тревожил.")

                             },
         reqForAnswers: new Requirement[2]
                             {
                                    Case.satisfiedRequirement,
                                    Case.satisfiedRequirement,
                             },
         reqForCase: Case.satisfiedRequirement

         );

        //День 3

        GameSC.AllCases[31] =
       new Case(caseID: 31,
       name: "Незнакомцы",
       spritePath: "Спрайты\\Illustrations\\ЗОВ",
       text: "Северный блокпост наутро доложил, что недалеко от границы городских ограждений расположился лагерь неизвестных солдат, которые явно не принадлежат армии Союза Молота. Солдаты пока не нарушили никаких законов, но их появлению стоит придать внимание.",
       answersText: new string[2]  {
                                            "Выслать разведчика:",
                                            "Игнорировать"
                                   },

       answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы отправили разведчика за городские ограждения, чтобы выяснить, кто следил за городом. По словам разведчика, солдаты остались в своём лагере и сказали, что никому мешать не собираются." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы решили не трогать группу солдат у городских ограждений. Впрочем, они также ничего плохого не сделали и позже скрылись. Больше вас никто не тревожил.")

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
        name: "Беспризорники",
        spritePath: "Спрайты\\Illustrations\\ЖД",
        text: "Каким-то образом информация о вашем отказе от подкреплений протекла в народ. Какие-то ненормальные побоялись, что армия все-таки приедет и начнется тотальный контроль, поэтому они взорвали динамитом железную дорогу, ведущую из столицы. Личностей установить не удалось.",
        answersText: new string[2]  {
                                            "Чинить (-20 материалов):",
                                            "Ждать (до сл. среды):"
                                  },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus, 
                                    textDayOfTheDay: "Вчера вы отремонтировали железную дорогу столицы, отношения с властями не ухудшились." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    CanDelay: true,
                                    textDayOfTheDay: "Вчера вы решили отложить дело о взорванных рельсах. Оно будет помещено к остальным на сегодняшний день.")

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
        name: "Новые пути",
        spritePath: "Спрайты\\Illustrations\\Шитоевск",
        text: "Министр торговли сообщает, что его отдел находит выгодной возможность построить новую железную дорогу от города до Шитоевска-На-Вязьме, т. к. это позволит торговать куда быстрее и большими поставками.",
        answersText: new string[2]  {
                                            "Строить (-40 материалов):",
                                            "Игнорировать"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы одобрили постройку новой железной дороги до Шитоевска-На-Вязьме. Отныне некоторые покупки из этого города будут обходиться вам дешевле, но не стоит забывать, что поезда иногда приносят не только ресурсы." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы отказались строить новую железную дорогу до Шитоевска-На-Вязьме. Кто знает, может, оно и к лучшему.")

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
        name: "Традиция",
        spritePath: "Спрайты\\Illustrations\\Рынок",
        text: "Сегодня в городе начинают готовиться к завтрашнему празднику хлеба. Чиновники считают, что следует отменить традицию приготовления пирогов, так как это плохо скажется на запасах еды. Все ждут вашего решения.",
        answersText: new string[2]  {
                                           "Оставить (-20 провизии):",
                                           "Отменить:"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven: new Dictionary<string, int> { ["square"] = 10, ["workLeft"] = 10, ["workRight"] = 10, ["stock"] = 10, ["factory"] = 10 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы решили оставить традицию печь пироги. Народ всех улиц крайне благодарен вам, все говорят, что вы сохранили то немногое, что вдохновляет людей каждый день." ),

                                    new AnswerAction(SpiritsIDgiven: new Dictionary<string, int> { ["square"] = -5, ["workLeft"] = -15, ["workRight"] = -15, ["stock"] = -5, ["factory"] = -5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы решили убрать традицию печь пироги. Люди были подавлены вашим решением, но поняли ситуацию. Хотя рабочие районы перенесли ваше решение довольно тяжело.")

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
        name: "Аресты",
        spritePath: "Спрайты\\Illustrations\\Арест",
        text: "Какие-то люди нечаянно подожгли деревянное чучело, являющееся символом надвигающегося праздника хлеба. Патрульные тут же их арестовали. По закону этим людям надлежит выписать штраф, но суровые времена явно и так измотали население. Что делать?",
        answersText: new string[2]  {
                                           "Штраф (+20 бюджета):",
                                           "Отпустить:"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus, budgetGiven: 20, reputationSunriseGiven: -10,
                                    textDayOfTheDay: "Вчера вы взяли штраф с людей, которые случайно подожгли чучело. Никто не пошёл против ваших действий, но одна из группировок, что вам ещё не известна, не одобрила вашего решения." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы не стали брать штраф с бедных людей, за что они были вам очень благодарны. Ваше решение почти ни на что не повлияло.")

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
        name: "Костюмы",
        spritePath: "Спрайты\\Illustrations\\Рынок",
        text: "Грядущий праздник хлеба требует, чтобы особые люди носили традиционные костюмы чучел, но в городе их сейчас нет. Народ предлагает закупить костюмы из Шитоевска-На-Вязьме.",
        answersText: new string[2]  {
                                           "Закупить (-20 бюджета):",
                                           "Игнорировать"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven: new Dictionary<string, int> { ["square"] = 5, ["workLeft"] = 5, ["workRight"] = 5, ["stock"] = 5, ["factory"] = 5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы закупили костюмы из Шитоевска для праздника. Все районы были рады." ),

                                    new AnswerAction(SpiritsIDgiven: new Dictionary<string, int> { ["square"] = -5, ["workLeft"] = -5, ["workRight"] = -5, ["stock"] = -5, ["factory"] = -5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы приняли решение не закупать праздничные костюмы из Шитоевска. Народу это пришлось не по вкусу, хоть некоторые и поняли ситуацию с бюджетом.")

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
        name: "Костюмы",
        spritePath: "Спрайты\\Illustrations\\Рынок",
        text: "Грядущий праздник хлеба требует, чтобы особые люди носили традиционные костюмы чучел, но в городе их сейчас нет. Народ предлагает закупить костюмы из Шитоевска-На-Вязьме. Поскольку недавно Вы построили туда железную дорогу, это решение может оказаться особенно выгодным.",
        answersText: new string[2]  {
                                           "Закупить (-5 бюджета):",
                                           "Игнорировать"
                                    },

        answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["square"] = 5,["workLeft"] = 5,["workRight"] = 5,["stock"] = 5,["factory"] = 5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы закупили костюмы из Шитоевска для праздника. Все районы были рады." ),

                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["square"] = -5,["workLeft"] = -5,["workRight"] = -5,["stock"] = -5,["factory"] = -5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы приняли решение не закупать праздничные костюмы из Шитоевска. Народу это пришлось не по вкусу, хоть некоторые и поняли ситуацию с бюджетом.")

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
              name: "Патриотизм первичен",
              spritePath: "Спрайты\\Illustrations\\Рынок",
              text: "Наступил праздник хлеба. Люди сбрались на площадях, на рынке и на улочках. Правительство хочет укрепить веру народа в великую державу, поэтому требует, чтобы вы организовали выступление на главной площади с речью, восхваляющей власти и народ Союза Молота.",
              answersText: new string[2]  {
                                           "Организовать",
                                           "Игнорировать"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    reputationAuthoritiesGiven: 10,
                                    reputationSunriseGiven: -10,
                                    textDayOfTheDay: "Вчера вы организовали выступление человека из столицы, который произнес воодушевляющую речь. Союз Молота гордится вами! А вот неизвестная вам группировка - нет." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    reputationAuthoritiesGiven: -15,
                                    textDayOfTheDay: "Вчера вы отказали правительству в воодушевляющей речи. Правительство понимает, что само наградило вас полномочиями решать такие вопросы, но вряд ли ваш авторитет укрепился.")

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
              name: "Опять аресты",
              spritePath: "Спрайты\\Illustrations\\Арест",
              text: "Несколько пьянчуг посрывали плакаты Союза Молота, пока были в нетрезвом состоянии. Патрульные тут же их арестовали. По закону этим людям надлежит выписать штраф, но суровые времена явно и так измотали население. С другой стороны, плакаты - это государственная собственность. Что делать?",
              answersText: new string[2]  {
                                           "Штраф (+20 бюджета)",
                                           "Отпустить"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    budgetGiven: 20,
                                    reputationSunriseGiven: -10,
                                    textDayOfTheDay: "Вчера вы взяли штраф с людей, которые посрывали плакаты. Никто не пошел против ваших действий, но одна из группировок, что вам еще не известна, не одобрила вашего решения." ),

                                    new AnswerAction (Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    reputationAuthoritiesGiven: -10,
                                    textDayOfTheDay: "Вчера вы не стали брать штраф с бедных людей, за что они были очень вам благодарны. Но вот власти вас выругали за то, что вы отпустили людей, нарушивших патриотический порядок города, сорвав плакаты.")

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
              name: "Спектакль",
              spritePath: "Спрайты\\Illustrations\\Рынок",
              text: "Небольшое творческое объединение города просит разрешение на организацию спектакля, являющегося сатирой на характеры чиновников. Народу очень хочется посмотреть этот спектакль. Конечно же, чиновники такое не одобрят, но решение за вами.",
              answersText: new string[2]  {
                                           "Разрешить",
                                           "Запретить"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["square"] = -20,["workLeft"] = 5,["workRight"] = 5,["stock"] = 5,["factory"] = 5 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы разрешили поставить сатирический спектакль про чиновников. Народ был рад, а вот чиновники - нет. " ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы запретили устраивать спектакль про чиновников, из-за чего немногие люди долго негодовали. Зато чиновники и слова плохого не сказали.")

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
              name: "Плакаты",
              spritePath: "Спрайты\\Illustrations\\Рынок",
              text: "Власти огласили указ, гласящий, что каждому управляющему города надлежит выпустить новую серию патриотичных плакатов, чтобы \"дух союза не опускался более, а люди не подвергались сомнениям наших действий\".",
              answersText: new string[2]  {
                                           "Плакаты для рабочих",
                                           "Плакаты для чиновников",
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["workLeft"] = 10,["workRight"] = 10 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы решили, что новая серия патриотичных плакатов будет про тяжкую работу на заводе. Рабочие районы вдохновлены!" ),

                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["square"] = 20 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы решили, что новая серия патриотичных плакатов будет про дисциплину среди чиновников. Главная площадь города вдохновлена!")

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
              name: "Несчастье",
              spritePath: "Спрайты\\Illustrations\\Плакаты",
              text: "Утром, работая на заводе, простой рабочий Юрий Семецкий погиб, упав в трубу отходов по нелепой случайности. В его гибели никто не виновен, а по закону ратуша должна выплатить его семье небольшую сумму денег.",
              answersText: new string[2]  {
                                           "Выплатить (-20 бюджета)",
                                           "Игнорировать"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы выделили бюджет семье погибшего рабочего. Семья была очень вам благодарна." ),

                                    new AnswerAction(SpiritsIDgiven : new Dictionary < string, int > {["workLeft"] = -10,["workRight"] = -10 }, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы не выделили денег семье погибшего рабочего. Рабочие районы негодуют и говорят, что вам на них плевать.")

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
              name: "Проверка совести",
              spritePath: "Спрайты\\Illustrations\\Проверяющий",
              text: "Сегодня в город прибыл проверяющий из правительства, чтобы узнать обстановку в городе после первой недели кризиса. Он спрашивает вас, какова по вашему мнению ситуация?",
              answersText: new string[2]  {
                                           "Все хорошо",
                                           "Все плохо"
                                          },

              answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы сказали проверяющему, что у вас в городе все хорошо." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы сказали проверяющему, что у вас в городе все довольно плохо.")

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
      name: "Дорогие запросы",
      spritePath: "Спрайты\\Illustrations\\Письмо",
      text: "Уже известный вам винистр Даниил, с которым вы познакомились в понедельник, просит у вас еще немного денег. Он говорит, что если вы ему дадите эти деньги, это может очень помочь вам завтра, в воскресенье.",
      answersText: new string[2]  {
                                           "Дать деньги (-50 бюджета)",
                                           "Игнорировать"
                                  },

      answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы прислали министру Даниилу нужную сумму денег. Теперь надейтесь, что не зря." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы проигнорировали запрос министра Даниила. Он не ответил. Больше вас никто не тревожил.")

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
           name: "Дорогие поносы 1",
           spritePath: "Спрайты\\Illustrations\\Письмо",
           text: "Уже известный вам винистр Даниил, с которым вы познакомились в понедельник, просит у вас еще немного денег. Он говорит, что если вы ему дадите эти деньги, это может очень помочь вам завтра, в воскресенье.",
           answersText: new string[2]  {
                                           "Дать деньги (-50 бюджета)",
                                           "Игнорировать"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы прислали министру Даниилу нужную сумму денег. Теперь надейтесь, что не зря." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы проигнорировали запрос министра Даниила. Он не ответил. Больше вас никто не тревожил.")

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
           name: "Дорогие поносы 2",
           spritePath: "Спрайты\\Illustrations\\Письмо",
           text: "Уже известный вам винистр Даниил, с которым вы познакомились в понедельник, просит у вас еще немного денег. Он говорит, что если вы ему дадите эти деньги, это может очень помочь вам завтра, в воскресенье.",
           answersText: new string[2]  {
                                           "Дать деньги (-50 бюджета)",
                                           "Игнорировать"
                                       },

           answerActions: new AnswerAction[2] {
                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы прислали министру Даниилу нужную сумму денег. Теперь надейтесь, что не зря." ),

                                    new AnswerAction(Case.NoSpirits, Case.NoPollution, Case.NoStatus,
                                    textDayOfTheDay: "Вчера вы проигнорировали запрос министра Даниила. Он не ответил. Больше вас никто не тревожил.")

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
