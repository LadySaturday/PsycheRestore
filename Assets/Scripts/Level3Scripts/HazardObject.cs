using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardObject : MonoBehaviour
{
    // Yizhi 11/30/2019
    Transform secondaryPlayer;
    float extinguishDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Yizhi 11/30/2019
        //if (MenuController.type.ToString().Equals("Multiplayer"))
            secondaryPlayer = GameObject.FindGameObjectWithTag("SecondaryPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Yizhi 11/30/2019
        //if (MenuController.type.ToString().Equals("Multiplayer"))
        //{
            float d2P = Vector3.Distance(transform.position, secondaryPlayer.position);
            Debug.Log(d2P);
            if (d2P <= extinguishDistance)
            {
                Debug.Log("d2P <= extinguishDistance");
                gameObject.active = false;
            }
        //}
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
