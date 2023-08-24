using Assembly_CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class Menu : MonoBehaviour
{

    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject ExitButton;
    [SerializeField] GameObject ApplyButton;
    [SerializeField] GameObject OptionsButton;
    [SerializeField] GameObject BackButton;
    [SerializeField] GameObject LoadButton;
    [SerializeField] GameObject SaveButton;
    [SerializeField] GameObject BackFromLoadButton;
    [SerializeField] GameObject BackFromSaveButton;
    [SerializeField] GameObject ConfirmLoadButton;
    [SerializeField] GameObject ConfirmSaveButton;
    [SerializeField] GameObject resolutionDropdown;
    [SerializeField] Dropdown resolutionDropdownDROPDOWN;
    public Toggle fullscreenToggle;
    [SerializeField] GameObject ToggleFullScreen;
    [SerializeField] GameObject LoadGameDropdownObject;
    [SerializeField] GameObject SaveGameFieldObject;
    [SerializeField] Dropdown LoadGameDropdown;
    [SerializeField] InputField SaveGameField;
    [SerializeField] GameObject AllSettingsObject;
    [SerializeField] GameObject MainMenuObject;
    [SerializeField] GameObject AllLoadObject;
    [SerializeField] GameObject AllSaveObject;
    [SerializeField] Text LoadDropdownLabel;


    public Text govno1;
    public Text govno2;
    public Text govno3;


    // Use this for initialization
    void Start()
    {
        ShowMainMenu();
    }


    void ShowMainMenu()
    {

        MainMenuObject.SetActive(true);
        if (SLscript.currentGameData == null)
        {
            SaveButton.SetActive(false);
            ExitButton.transform.localPosition = new Vector3((float)-273.8728, (float)-90.65463, (float)62.41916);
            OptionsButton.transform.localPosition = new Vector3((float)-273.8728, (float)70.5, (float)62.41916);
        }
        else
        {
            SaveButton.SetActive(true);
            ExitButton.transform.localPosition = new Vector3((float)-273.8728, (float)-246.6546, (float)62.41916);
            OptionsButton.transform.localPosition = new Vector3((float)-273.8728, (float)-90.65463, (float)62.41916);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (File.Exists(Application.persistentDataPath + "/currentGame.gd"))
            {
                SLscript.Load(-1); //����� ������ ���� - ��� ����������� � ������� ����
            }
        }
    }
    public void Starts()
    {
        Debug.Log("QWE");
        SLscript.isNewGame = true;
        SceneManager.LoadScene("Prologue");
    }
    public void Exit()
    {
        File.Delete(Application.persistentDataPath + "/currentGame.gd");
        Application.Quit();
    }

    public void OptionsClick()
    {
        SettingsScript.previousResolution = Screen.currentResolution;

        MainMenuObject.SetActive(false);
        AllSettingsObject.SetActive(true);
    }

    public void GoBackSettings()
    {
        ShowMainMenu();
        AllSettingsObject.SetActive(false);
    }

    public void BackClick()
    {
        LoadButton.SetActive(true);
        Screen.SetResolution(SettingsScript.previousResolution.width, SettingsScript.previousResolution.height, Screen.fullScreen);
        GoBackSettings();
        Screen.fullScreen = SettingsScript.isFullscreen;
        fullscreenToggle.isOn = SettingsScript.isFullscreen;
        resolutionDropdownDROPDOWN.value = SettingsScript.currentResolutionIndex;



    }

    public void ConfirmLoadClick()
    {
        if (SLscript.savedGames.Count > 0 && SLscript.savedGames.Count > LoadGameDropdown.value)
        {
            SLscript.Load(LoadGameDropdown.value);
        }
    }

    public void BackFromLoadClick()
    {
        AllLoadObject.SetActive(false);
        ShowMainMenu();
        
    }

    public void SaveClick()
    {
        MainMenuObject.SetActive(false);
        AllSaveObject.SetActive(true);
    }

    public void ConfirmSaveButtonClick()
    {

        string dataName;
        if (SaveGameField.text != "")
            dataName = SaveGameField.text;
        else dataName = "����� ����";



        GameData currentGameData = new GameData(dataName);
        currentGameData.name = dataName + " (���� " + Convert.ToString(currentGameData.today) + ")";

        bool flg = false;
        int count = 0;

        foreach(GameData data in SLscript.savedGames)
        {
            if(data.name == currentGameData.name)
            {
                SLscript.savedGames.RemoveAt(count);
                SLscript.savedGames.Insert(count, currentGameData);
                flg = true;
                break;
            }
            count++;
        }

        if(!flg)
            SLscript.savedGames.Add(currentGameData);
        Debug.Log("� ���� ����� ������ ���: " + currentGameData.AllCases.Count);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SLscript.savedGames);
        file.Close();


        BackFromSaveClick();
    }

    public void BackFromSaveClick()
    {
        MainMenuObject.SetActive(true);
        AllSaveObject.SetActive(false);
    }


    public void LoadClick()
    {
        MainMenuObject.SetActive(false);
        AllLoadObject.SetActive(true);
        ShowSavedGames();
    }

    public void ShowSavedGames()
    {
        LoadGameDropdown.ClearOptions();


        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            //�������� �������� ������ ���������� �� �����
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SLscript.savedGames = (List<GameData>)bf.Deserialize(file);
            file.Close(); //�� �������� ������� ��������� ����
            FormLoadDropdown();
            Debug.Log("GOVNOVO2");

        }
        else
        {
            LoadGameDropdown.AddOptions(new List<string> { "��� ����������" });
            LoadGameDropdown.RefreshShownValue(); //���������
        }

        Debug.Log("GOVNOVOZ3");

    }

    public void FormLoadDropdown()
    {
        List<string> savedgames = new List<string>();

        foreach (GameData game in SLscript.savedGames)
        {
            string option = game.name;
            savedgames.Add(option); //��������� �� �����, ������� ������ ��� ������������ � ������ �����
        }
        Debug.Log(savedgames.Count + " - ������� ����� ������");
        if (SLscript.savedGames.Count == 0)
        {
            //LoadGameDropdown.RefreshShownValue(); //���������
            Debug.Log("������ ����");
          
            LoadGameDropdown.AddOptions(new List<string> { "��� ����������" });
            Debug.Log("GOVNOVOZ");

        }
        else
        {
            LoadGameDropdown.AddOptions(savedgames); //�������� ������ ����� � dropdown
            LoadGameDropdown.RefreshShownValue(); //���������
        }
       
    }

    public void DeleteClick()
    {
        


        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            //�������� �������� ������ ���������� �� �����
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SLscript.savedGames = (List<GameData>)bf.Deserialize(file);
            file.Close(); //�� �������� ������� ��������� ����
            Debug.Log("������� ���������� ����� " + LoadGameDropdown.value);
            if(SLscript.savedGames.Count > 0 && SLscript.savedGames.Count > LoadGameDropdown.value)
            {
                SLscript.savedGames.RemoveAt(LoadGameDropdown.value); //������� �� ������ ���� ����������, ����� ���� ��������� ������ ������� � ����
                file = File.Create(Application.persistentDataPath + "/savedGames.gd");
                bf.Serialize(file, SLscript.savedGames);
                file.Close();
            }
            LoadGameDropdown.ClearOptions();
            FormLoadDropdown();
        }
        else
        {
            LoadGameDropdown.ClearOptions();
           
            LoadGameDropdown.AddOptions(new List<string> { "��� ����������" });
            LoadGameDropdown.RefreshShownValue(); //���������

            Debug.Log("FAIL SDOX");
        }
        

    }



}
