using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_Network : NetworkBehaviour
{
    public GameObject main_camera;
    //public GameObject[] characterModel;

    public override void OnStartLocalPlayer()
    {
        if(gameObject == GameObject.FindGameObjectWithTag("Julie"))
        {
            GetComponent<PlayerController>().enabled = true;
        }
        else
        {
            GetComponent<DotController>().enabled = true;
        }
       
        main_camera.SetActive(true);

        //foreach (GameObject go in characterModel)
        //{
        //    go.SetActive(false);
        //}
    }
}
