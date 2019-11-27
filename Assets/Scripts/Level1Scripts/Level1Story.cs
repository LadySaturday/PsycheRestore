using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1Story : MonoBehaviour
{
    public Text[] texts;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        if (index == texts.Length - 1)
        {
            string chosenLevelType = MenuController.chosenLevelType;
            if (chosenLevelType == "singleplayer")
            {
                SceneManager.LoadScene("Level_1");
            }
            else if (chosenLevelType == "multiplayer")
            {
                SceneManager.LoadScene("Multi_Level_1");
            }
        }
        else
        {
            texts[index].gameObject.SetActive(false);
            index++;
            texts[index].gameObject.SetActive(true);
        }
        
    }
}
