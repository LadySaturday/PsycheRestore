using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public string[] sentences;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(sentences);
            Destroy(gameObject);
        }
    }
}
