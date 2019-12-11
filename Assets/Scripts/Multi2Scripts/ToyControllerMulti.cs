using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyControllerMulti : MonoBehaviour
{
    public Text dollCount;
    private static int count;

    // Yizhi 11/24/2019
    Transform secondaryPlayer;
    float awareDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        dollCount.text = (count.ToString() + "/7");

        // Yizhi 11/24/2019
        secondaryPlayer = GameObject.FindGameObjectWithTag("SecondaryPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Yizhi 11/24/2019
        float d2P = Vector3.Distance(transform.position, secondaryPlayer.position);
        if (d2P <= awareDistance)
        {
            Debug.Log("d2P <= awareDistance");
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (GameObject.Find("Anna@tpose").GetComponent<AnnaControllerMulti>().playerHasToy == false)
        {
            Destroy(gameObject);
            GameObject.Find("Anna@tpose").GetComponent<AnnaControllerMulti>().playerHasToy = true;
            count++;
            dollCount.text = (count.ToString() + "/7");

        }
    }
}
