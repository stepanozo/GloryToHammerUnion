using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI; //��������� ����� ����� ��������� � ������ ������
using UnityEngine.Video;
using UnityEngine.SceneManagement; //���������� �����������, ����� ����� ����������� ����� �����������.

public class Prologue : MonoBehaviour
{
    int Time = 0;
    //������ �� ����� ���������� � ��� ���� ��� ������ Text, �� ��������� ����� ������ ��� Text, �� PrologueText ����� �������� �� ��� ������, � ��� ��������� "�����" �������, � ������� �� ����� �� ������ ��������������� ������ ���� text.
    [SerializeField] Text PrologueText; //���� ����� � ������, �� ������� ����� ������, � � � ��� ���� �������� ������� �����.
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
                case 0: //��� ����� �����������, � ������� �� ������� �������� � ������� �����.
                    PrologueText.text = "����� ��� �������� � ����� ��� ����� ������� ������ ������ ���. �������� ����������������� ����������. �� ������� �������� �����-�� ���, � ������ ��� ������� ����������� ������� ������������.";
                    break;
                case 1:
                    PrologueText.text = "������ ������ �� ������������ ��� ������ �����, ���� ������� ��� ��������� ��� - ������������ ������ ������. �������� �� ��������, ��� ��������� ���� �� ������ ���� � ��������� ������ ������ ������.";
                    break;
                case 2:
                    PrologueText.text = "������ ������ ������ ���� �������� ������ �����, ���� �������������� � �������-��������. �� �������� ����������� ������� ���� ������, ������ ��� ��������� (MERCuriy). ��� ����� �������� ���, ��� ���� ������ ������� ������� ������.";
                    break;
                case 3:
                    PrologueText.text = "������ �������� �������, ����� �������� �� ������� ��� ��� ���, ���������������� � ����� ����� ����������� �� ����������� �������-��������. ��� - ������ ������������� �������. �������� �� ���� ��������������� �������������, ��� - ���������� ����.";
                    break;
                case 4:
                    PrologueText.text = "� ����� ��� ��� ������ ����� ��� ��������� ����� ������� ����, ������� ������ ��������� - ����. �� ���������� ����������� ���� ����������� � ������ ��� ������������ ������ ����� ������ ������ �������.";
                    break;
                case 5:
                    ImagesObject.GetComponent<Image>().sprite = streetSprite;
                    PrologueText.GetComponent<Text>().text = "��� ������ ��� ���������� � �������� �������� ����������. ����� �������� �� ��� ����� ���, ���� ���� ����� \"���������\" �� �������� ������ ��������, �������� ������� �� ��������� ������� ���������.";
                    break;
                case 6:
                    PrologueText.text = "������ ��� �� ������������ ��� ��������� �������. ����� ����, ��� ��������� ���� ������ ����. ���?..";
                    break;
                case 7:
                    PrologueText.text = "���������� ���� ����� �������� ����������� �����������, �� �������, ��� ���� �� ���. �������, ���� ����, � ���� ���������. ������ ��������� ������, � ������ ������� ���� ���� - ���������-���������������� ���� ������.";
                    break;
                case 8:
                    PrologueText.text = "������ ������? ����� ��� ������ �������. ����� ����. �  ���� ��� ������-������� ����� � ���������� ���� �� �����...";
                    break;
                case 9:
                    ImagesObject.GetComponent<Image>().sprite = forestSprite;
                    PrologueText.text = "� ���� ������� ���� ��� ��������� �� ������������ ������� � ��������� �����, ������ ������� � ������. � ����, ������� �����������, � ���������� ��� �� ����������...";
                    break;
                case 10:
                    ImagesObject.SetActive(false); //��������� ������ ������
                    PrologueText.text = "";
                    prologueVideo.GetComponent<VideoPlayer>().Play();
                    break;
                case 11:
                    Time = -1; //����� ������ �� ����������� ��� ����� ���
                    SceneManager.LoadScene("Game");
                    break;
                    

                    //� ����� ������ ����� ������

            }
            Time++;
        }
        
    }
}
