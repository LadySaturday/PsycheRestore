using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Initializing variables
    private bool soundIsOn = false;
    private bool musicIsOn = false;
    private float soundVolume = 0;
    public static string chosenLevelType;

    public Toggle soundOnOff;
    public Toggle musicOnOff;
    public Slider soundVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        soundOnOff.isOn = soundIsOn;
        musicOnOff.isOn = musicIsOn;
        soundVolumeSlider.value = soundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enter level name through Unity
    public void LOAD_LEVEL(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SinglePlayer()
    {
        chosenLevelType = "singleplayer";
        SceneManager.LoadScene("Level_1_Story");
    }

    public void MultiPlayer()
    {
        chosenLevelType = "multiplayer";
        SceneManager.LoadScene("Level_1_Story");
    }


    // Reset scene
    public void REVIVE(string levelName)
    {
        SceneManager.LoadScene(levelName);

        /*
        Scene level1 = SceneManager.GetSceneByName("Level1");
        Scene level2 = SceneManager.GetSceneByName("Level2");
        Scene level3 = SceneManager.GetSceneByName("Level3");
        Scene activeScene = SceneManager.GetActiveScene();

        if (level1.Equals(activeScene))
        {
            SceneManager.LoadScene(levelName);
        }
        else if (level2.Equals(activeScene))
        {
            SceneManager.LoadScene(levelName);
        }
        else if (level3.Equals(activeScene))
        {
            SceneManager.LoadScene(levelName);
        }
        */
    }

    // Sound, Music and Volume
    public void TOGGLE_SOUND()
    {
        soundIsOn = !soundIsOn;
        Debug.Log("soundsIsOn: " + soundOnOff.isOn);
        UpdateUI();
    }

    public void TOGGLE_MUSIC(bool musicON)
    {
        musicIsOn = !musicIsOn;
        Debug.Log("musicIsOn: " + musicOnOff.isOn);
        UpdateUI();
    }

    public void CHANGE_VOLUME(float newValue)
    {
        soundVolumeSlider.value = newValue;
        Debug.Log("soundVolume: " + soundVolume);
        UpdateUI();
    }

    private void UpdateUI()
    {
        soundOnOff.isOn = soundIsOn;
        musicOnOff.isOn = musicIsOn;
        soundVolumeSlider.value = soundVolume;
    }

}
