using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllerMulti1 : MonoBehaviour
{
    public GameObject imagePanel;
    public GameObject itemsCollected;
    private static RawImage collectibleImage;
    private static Text itemsCollectedText;
    private static int numOfItemsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        collectibleImage = imagePanel.GetComponentInChildren<RawImage>();
        itemsCollectedText = itemsCollected.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfItemsCollected == 1)
        {
            Debug.Log("Next Level");
            Destroy(GameObject.Find("Charlie"));
            SceneManager.LoadScene("WinSceneMulti1");
        }
        if (collectibleImage.texture != null)
        {
            imagePanel.SetActive(true);
            PauseGame();
        }
        if (Input.GetKey(KeyCode.Mouse0) && collectibleImage.texture != null)
        {
            imagePanel.SetActive(false);
            collectibleImage.texture = null;
            ResumeGame();
        }
    }

    public static void SetImage(Texture texture)
    {
        collectibleImage.texture = texture;
        numOfItemsCollected++;
        itemsCollectedText.text = numOfItemsCollected.ToString() + "/7";
    }

    void PauseGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        GameObject.Find("Charlie").GetComponent<CharlieController>().enabled = false;
    }
    void ResumeGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("Charlie").GetComponent<CharlieController>().enabled = true;
    }
}
