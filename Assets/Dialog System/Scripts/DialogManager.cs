using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text textDisplay;
    private string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject dialogPanel;
    public float waitTime = 4;
    public Font font;

    public void ShowDialog(string[] sentences)
    {
        
        Reset();
        this.sentences = sentences;
        dialogPanel.SetActive(true);
        StartCoroutine(Type());
    }

    void Reset()
    {
        this.sentences = null;
        textDisplay.text = "";
        index = 0;
  
        dialogPanel.SetActive(false);
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(waitTime);
        NextSentence();
    }

    public void NextSentence()
    {


        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
            Reset();
    }
}
