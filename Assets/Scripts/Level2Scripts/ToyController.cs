using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyController : MonoBehaviour
{
    public Text dollCount;
    private static int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        dollCount.text = (count.ToString() + "/7");
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
            count++;
            dollCount.text = (count.ToString() + "/7");

        }
    }
}
