using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Level1Story : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    private int index = 0;
    public Button cntBtn;
    private bool startCalled = false;
    // Start is called before the first frame update
    void Start()
    {
        cntBtn.gameObject.SetActive(false);
        StartCoroutine(FadeTextToFullAlpha(1f, texts[index]));
    }

    // Update is called once per frame
    void Update()
    {
        if (index == texts.Length - 1 && startCalled == false)
        {
            startCalled = true;
            cntBtn.gameObject.SetActive(false);
            StartCoroutine(StartLevel());
        }
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeTextToZeroAlpha(1f, texts[index]));
        yield return new WaitForSeconds(1f);
        if (MenuController.type.ToString().Equals("Singleplayer"))
        {
            SceneManager.LoadScene("Level_1");
        }
        else if (MenuController.type.ToString().Equals("Multiplayer"))
        {
            SceneManager.LoadScene("Multi_Level_1");
        }
    }

    public void Continue()
    {
        cntBtn.gameObject.SetActive(false);
        StartCoroutine(Transition());
    }

    public IEnumerator Transition()
    {
        StartCoroutine(FadeTextToZeroAlpha(1f, texts[index]));
        yield return new WaitForSeconds(1f);
        texts[index].gameObject.SetActive(false);
        index++;
        texts[index].gameObject.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(1f, texts[index]));
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        if (index != texts.Length - 1)
            cntBtn.gameObject.SetActive(true);
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
