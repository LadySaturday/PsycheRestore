using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (MenuController.type.ToString().Equals("Singleplayer"))
                SceneManager.LoadScene("DeathScene3");
            else if (MenuController.type.ToString().Equals("Multiplayer"))
                SceneManager.LoadScene("DeathSceneMulti3");
        }
    }
}
