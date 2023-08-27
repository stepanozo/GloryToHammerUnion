using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assembly_CSharp;
using UnityEngine.UI;
//using TMPro.EditorUtilities;
using System;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        Debug.Log("��¨� �����");

        GameMainScript.BaseOfUnitsSC.simpleUnitDescription.text = "��������: " + u.HP + "/" + u.maxHP + " \t ����: " + u.damage + " \t �� �������: " + u.techDamage + " \t ���-��: " + u.quantity + "\n" + u.description;
        GameMainScript.BaseOfUnitsSC.simpleUnitName.text = u.name;
        GameMainScript.BaseOfUnitsSC.simpleUnitPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("�¨� �����");
        GameMainScript.BaseOfUnitsSC.simpleUnitPanel.SetActive(false);
    }

}
