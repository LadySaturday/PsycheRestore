using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionMenuController : MonoBehaviour
{
    public GameObject OptionMenu;
    public GameObject Instructions;

    bool isOMOpen = false;
    bool isInstructionOpen = false;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (!isOMOpen && !isInstructionOpen)
            {
                isOMOpen = true;
                OptionMenu.SetActive(true);
                EnableOrDisableGO();
            }
            else if(isOMOpen && !isInstructionOpen)
            {
                isOMOpen = false;
                isInstructionOpen = false;
                OptionMenu.SetActive(false);
                EnableOrDisableGO();
            }
        }

        Debug.Log(isOMOpen);
    }

    void EnableOrDisableGO()
    {
        if(sceneName == "Level_1" && isOMOpen)
        {
            GameObject.Find("Charlie").GetComponent<CharlieController>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        }
        else if(sceneName == "Level_1" && !isOMOpen)
        {
            GameObject.Find("Charlie").GetComponent<CharlieController>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        }
    }

    public void OpenInstruction()
    {
        if (isOMOpen)
        {
            isInstructionOpen = true;
            isOMOpen = false;
            Instructions.SetActive(true);
            OptionMenu.SetActive(false);
        }
    }

    public void CloseInstruction()
    {
        if (!isOMOpen)
        {
            isInstructionOpen = false;
            isOMOpen = true;
            Instructions.SetActive(false);
            OptionMenu.SetActive(true);
        }
    }

    public void RestartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Resume()
    {
        isOMOpen = false;
        isInstructionOpen = false;
        OptionMenu.SetActive(false);
        EnableOrDisableGO();
    }

    public void MainManu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void TestGoToLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void TestGoToLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void TestGoToLevel3()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void TestGoToMultiLevel1()
    {
        SceneManager.LoadScene("Multi_Level_1");
    }

    public void TestGoToMultiLevel2()
    {
        SceneManager.LoadScene("Multi_Level_2");
    }

    public void TestGoToMultiLevel3()
    {
        SceneManager.LoadScene("Multi_Level_3");
    }
}
