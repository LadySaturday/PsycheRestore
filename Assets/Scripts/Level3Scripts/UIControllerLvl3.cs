using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerLvl3 : MonoBehaviour
{
    public GameObject InstructionLvl3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Canvas/InstructionLvl3"))
        {
            StartCoroutine(ShowInstruction8Sec());
        }
    }

    // Yizhi 11/28/2019
    IEnumerator ShowInstruction8Sec()
    {
        yield return new WaitForSeconds(8);
        InstructionLvl3.gameObject.SetActive(false);
    }
}
