﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectibesController : MonoBehaviour
{
    public Texture image;

    // Yizhi 11/10/2019
    Transform secondaryPlayer;
    float awareDistance = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        // Yizhi 11/10/2019
        secondaryPlayer = GameObject.FindGameObjectWithTag("SecondaryPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Yizhi 11/10/2019
        float d2P = Vector3.Distance(transform.position, secondaryPlayer.position);
        if(d2P <= awareDistance)
        {
            Debug.Log("d2P <= awareDistance");
            transform.GetChild(0).gameObject.SetActive(true);
            //gameObject.GetComponent<Renderer>().material.SetColor("color", Color.blue);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        UIController.SetImage(image);
        Destroy(gameObject);
    }
}
