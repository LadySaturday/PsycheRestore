using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private bool soundIsOn = false;
    private bool musicIsOn = false;
    private float soundVolume = 0;

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

    public void LOAD_LEVEL(string levelName) // Enter level name through Unity
    {
        SceneManager.LoadScene(levelName);
    }

    public void TOGGLE_SOUND()
    {
        soundIsOn = !soundIsOn;
        Debug.Log("soundsIsOn: " + soundOnOff.isOn);
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
