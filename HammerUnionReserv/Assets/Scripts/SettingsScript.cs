using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Menu MainMenu;
    //public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    //public static Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    //public Slider volumeSlider;
    float currentVolume;
    Resolution[] resolutions;
    public static Resolution previousResolution;
    public static int currentResolutionIndex;
    public static bool isFullscreen;

    public Text GOVNISCHE;



    // Start is called before the first frame update
    void Start()
    {
        isFullscreen = false ;
        //Screen.SetResolution(1920, 1080, Screen.fullScreen);

        MainMenu = GameObject.Find("Canvas").GetComponent<Menu>();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions; //Просто все разрешения, которые поддерживает экран
        currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = " " + resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz ";
            options.Add(option); //Добавляем ту опцию, которую только что сформировали в список опций
            if (resolutions[i].width == Screen.currentResolution.width
                  && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i; //Просто определили индекс того разрешения, которое у нас сейчас есть
        }

       
        resolutionDropdown.AddOptions(options); //Добавили список опций в dropdown

        resolutionDropdown.RefreshShownValue(); //Обновляем

        LoadSettings();




        Debug.Log("primenil");
    }

    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("Volume", volume);
        currentVolume = volume;
    }
    public void SetFullscreen()
    {
       Screen.fullScreen = !Screen.fullScreen;
      
    }

    public void SetResolution()
    {
        //Dropdown.value используй.
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width,
                  resolution.height, Screen.fullScreen);
    }


    public void SetQuality(int qualityIndex)
    {

       // QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void ExitGame()
    {
      //  SceneManager.LoadScene("Level");
    }


    public void SaveSettings()
    {
        
        PlayerPrefs.SetInt("ResolutionPreference", //Запоминаем, какое разрешение игрок препочитает.
                   resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference",
                   System.Convert.ToInt32(Screen.fullScreen));
        isFullscreen = Screen.fullScreen;
        currentResolutionIndex = resolutionDropdown.value;



        //PlayerPrefs.SetFloat("VolumePreference",
        //           currentVolume);


        //По сути дальше дублируется backClick из Menu.cs



        MainMenu.GoBackSettings();

        
       


    }

    public void LoadSettings()
    {
       
        if (PlayerPrefs.HasKey("FullscreenPreference"))
        {
            isFullscreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));

            Screen.fullScreen = isFullscreen;

            fullscreenToggle.isOn = isFullscreen;
         /*   GOVNISCHE.text = "НАЙДЕНЫ ПРЕДПОЧТЕНИЯ" + Convert.ToString(System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"))) + " а поставили почему-то " + Convert.ToString(isFullscreen) +
                "а экран почему-то " + Convert.ToString(Screen.fullScreen);*/


        }
        else
        {

            Screen.fullScreen = true;
            fullscreenToggle.isOn = true;
            isFullscreen = true;
            PlayerPrefs.SetInt("FullscreenPreference",System.Convert.ToInt32(Screen.fullScreen));
          //  GOVNISCHE.text = "НЕ НАЙДЕНЫ ПРЕДПОЧТЕНИЯ";
        }

        fullscreenToggle.isOn = Screen.fullScreen;
        isFullscreen = Screen.fullScreen;


        if (PlayerPrefs.HasKey("ResolutionPreference")) //Если игрок уже ранее выбирал разрешение?
        {
            resolutionDropdown.value =
                         PlayerPrefs.GetInt("ResolutionPreference");
            currentResolutionIndex = resolutionDropdown.value;

        }
        else
        {
            resolutionDropdown.value = currentResolutionIndex;
            PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value); //Запоминаем, какое разрешение игрок препочитает.

        }

        //  if (PlayerPrefs.HasKey("VolumePreference"))
        //     volumeSlider.value =
        //                PlayerPrefs.GetFloat("VolumePreference");
        //else
        //   volumeSlider.value =
        //             PlayerPrefs.GetFloat("VolumePreference");
    }
}

