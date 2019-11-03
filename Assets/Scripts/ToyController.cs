using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (GameObject.Find("Anna@tpose").GetComponent<AnnaController>().playerHasToy == false)
        {
            Destroy(gameObject);
            GameObject.Find("Anna@tpose").GetComponent<AnnaController>().playerHasToy = true;
        }
    }
}
