using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerLv2 : MonoBehaviour
{
    public GameObject InstructionLvl2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Canvas/InstructionLvl2"))
        {
            StartCoroutine(ShowInstruction8Sec());
        }
    }

    // Yizhi 11/28/2019
    IEnumerator ShowInstruction8Sec()
    {
        yield return new WaitForSeconds(8);
        InstructionLvl2.gameObject.SetActive(false);
    }
}
