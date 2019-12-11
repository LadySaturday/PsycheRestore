using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour
{
    public Texture image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("OnTriggerEnter");
        //GetComponent<BoxCollider>().enabled = false;
        //Destroy(gameObject);
        if (col.tag == "Player")
        {
            gameObject.SetActive(false);
            UIControllerLv1.SetImage(image);
        }
    }
}
