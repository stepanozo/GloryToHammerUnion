using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI; //Добавляем чтобы юзать интерфейс и тексты всякие
using UnityEngine.Video;
using UnityEngine.SceneManagement; //Подключаем обязательно, чтобы иметь возможность сцены переключать.

public class Prologue : MonoBehaviour
{
    int Time = 0;
    //Теперь мы можем перетащить в это поле наш объект Text, но поскольку здесь указан тип Text, то PrologueText сразу задастся не как объект, а как КОМПОНЕНТ "Текст" объекта, в котором мы сразу же сможем непосредственно менять поле text.
    [SerializeField] Text PrologueText; //Надо зайти в канвас, на котором висит скрипт, и в в это поле засунуть вручную текст.
    [SerializeField] Sprite streetSprite;
    [SerializeField] Sprite forestSprite;
    [SerializeField] GameObject ImagesObject;
    [SerializeField] GameObject prologueVideo;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (Time)
            {
                case 0: //Тот самый прологтекст, в который мы вручную засунули в канвасе текст.
                    PrologueText.text = "Почти всю Северную и почти всю Южную Америки заняла страна ЗКР. Звездная Капиталистическая Республика. Ее основой являлась когда-то США, а сейчас они владеют практически лучшими технологиями.";
                    break;
                case 1:
                    PrologueText.text = "Страны Африки же объединились для общего блага, став страной под названием ОСА - Объединенные Страны Африки. Наплевав на невзгоды, они выдвинули себя на первый план и превзошли многие страны Европы.";
                    break;
                case 2:
                    PrologueText.text = "Многие страны Европы либо остались самими собою, либо присоединились к странам-гигантам. Но наиболее продвинутые создали свою страну, назвав его Меркурием (MERCuriy). Они стали известны тем, что дают другим странам наемные войска.";
                    break;
                case 3:
                    PrologueText.text = "Страны Дальнего востока, боясь агрессии со стороны ЗКР или ОСЫ, скооперировались и стали самой продвинутой по технологиям страной-гигантом. СОВ - Страны Объединенного Востока. Несмотря на свое технологическое превосходство, СОВ - сторонники мира.";
                    break;
                case 4:
                    PrologueText.text = "И пусть все эти страны важны для понимания общей картины мира, главный гвоздь программы - СКСМ. На протяжении десятилетий СССР захватывала в войнах или присоединяла мирным путем другие страны Евразии.";
                    break;
                case 5:
                    ImagesObject.GetComponent<Image>().sprite = streetSprite;
                    PrologueText.GetComponent<Text>().text = "Все вместе они стремились к светлому будущему коммунизма. Народ трудился до тех самых пор, пока само слово \"коммунизм\" не утратило своего значения, направив державу на скользкую дорожку диктатуры.";
                    break;
                case 6:
                    PrologueText.text = "Никого уже не интересовало кто управляет страной. Важно было, что управляли ради общего дела. Или?..";
                    break;
                case 7:
                    PrologueText.text = "Запутанные люди стали называть социализмом бюрократизм, не понимая, что идет не так. Впрочем, люди жили, и жили счастливо. Страна держалась крепко, и решила назвать себя СКСМ - Социально-Коммунистический Союз Молота.";
                    break;
                case 8:
                    PrologueText.text = "Почему молота? Позже все станет понятно. Может быть. А  пока что страны-гиганты живут и огрызаются друг на друга...";
                    break;
                case 9:
                    ImagesObject.GetComponent<Image>().sprite = forestSprite;
                    PrologueText.text = "И хоть однажды весь мир сломается от неожиданного события в Сибирских лесах, сейчас история о другом. О мире, который разрушается, и разрушение уже не остановить...";
                    break;
                case 10:
                    ImagesObject.SetActive(false); //отключаем объект нахрен
                    PrologueText.text = "";
                    prologueVideo.GetComponent<VideoPlayer>().Play();
                    break;
                case 11:
                    Time = -1; //чтобы больше не срабатывала эта хрень вся
                    SceneManager.LoadScene("Game");
                    break;
                    

                    //в конце пустой текст делаем

            }
            Time++;
        }
        
    }
}
