using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HazardObject : MonoBehaviour
{
    // Yizhi 11/30/2019
    public Text txtExtinguishTimes;
    public GameObject DotDiedMessage;
    Transform secondaryPlayer;
    float extinguishDistance = 2;
    static int extinguishTimes = 10;
    static byte g = 255;
    static byte b = 255;

    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Yizhi 11/30/2019
        sceneName = SceneManager.GetActiveScene().name;
        
        if (sceneName == "Multi_Level_3")
        {
            txtExtinguishTimes.text = extinguishTimes.ToString();
            secondaryPlayer = GameObject.FindGameObjectWithTag("SecondaryPlayer").transform;
        }  
    }

    // Update is called once per frame
    void Update()
    {
        // Yizhi 11/30/2019
        //if (MenuController.type.ToString().Equals("Multiplayer"))
        //{
        if(sceneName == "Multi_Level_3")
        {
            float d2P = Vector3.Distance(transform.position, secondaryPlayer.position);
            if (d2P <= extinguishDistance)
            {
                extinguishTimes -= 1;
                txtExtinguishTimes.text = extinguishTimes.ToString();
                g -= 25;
                b -= 25;
                Debug.Log("g: " + g + " | b: " + b);
                GameObject.FindGameObjectWithTag("SecondaryPlayer").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(255, g, b, 255));
                var main = secondaryPlayer.GetChild(0).GetComponent<ParticleSystem>().main;
                Color c = new Color32(255, g, b, 255);
                main.startColor = c;
                if (extinguishTimes == 0)
                {
                    DotDiedMessage.SetActive(true);
                    Destroy(secondaryPlayer.gameObject);
                }
                gameObject.active = false;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (sceneName == "Level_3")
                SceneManager.LoadScene("DeathScene3");
            else if (sceneName == "Multi_Level_3")
                SceneManager.LoadScene("DeathSceneMulti3");
        }
    }
}
