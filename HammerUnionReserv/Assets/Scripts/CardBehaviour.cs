using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assembly_CSharp;
using UnityEngine.UI;
//using TMPro.EditorUtilities;
using System;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public unit u;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Map.GameSC.PlayerState != GameMainScript.states.Fighting)
        {

            GameMainScript.BaseOfUnitsSC.simpleUnitDescription.text = "Здоровье: " + u.HP + "/" + u.maxHP + " \t Урон: " + u.damage + " \t По технике: " + u.techDamage + " \t Кол-во: " + u.quantity + "\n" + u.description;
            GameMainScript.BaseOfUnitsSC.simpleUnitName.text = u.name;
            GameMainScript.BaseOfUnitsSC.simpleUnitPanel.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameMainScript.BaseOfUnitsSC.ActiveReservUnit = u; //Говорим, что теперь u - активный юнит (тот, что отображается сейчас на панели)
        //Настраиваем иллюстрацию
        //GameMainScript.BaseOfUnitsSC.CardOnPanel
        GameObject tempUnitObject = GameMainScript.BaseOfUnitsSC.CardOnPanel;
        GameObject tempChildObject = tempUnitObject.transform.Find("Иллюстрация").gameObject;
        tempChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(u.spritePath);

        //Настраиваем здоровье
        tempUnitObject.transform.Find("Здоровье").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.HP);
        tempUnitObject.transform.Find("Урон").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.damage);
        tempUnitObject.transform.Find("Урон по технике").gameObject.transform.Find("Текст").GetComponent<Text>().text = Convert.ToString(u.techDamage);
        tempUnitObject.transform.Find("Кол-во").GetComponent<Text>().text = Convert.ToString(u.quantity);

        GameMainScript.BaseOfUnitsSC.BigUnitName.text = u.name;
        GameMainScript.BaseOfUnitsSC.BigUnitDescription.text = u.description;

        GameMainScript.BaseOfUnitsSC.buttonAbility.SetActive(false);
        GameMainScript.BaseOfUnitsSC.buttonAttack.SetActive(false);
        GameMainScript.BaseOfUnitsSC.buttonRetreat.SetActive(false);
        GameMainScript.BaseOfUnitsSC.buttonPlayIt.SetActive(true);
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        if (Map.GameSC.PlayerState != GameMainScript.states.Fighting)
        {
            GameMainScript.BaseOfUnitsSC.simpleUnitPanel.SetActive(false);
        }
    }

}
