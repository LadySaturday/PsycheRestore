using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController1 : MonoBehaviour
{
    public float PlayerSpeed = 1;
    public float rotationSpeed = 200.0f;
    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()  { }
    // Update is called once per frame
    void Update()
    {
        float mh = Input.GetAxis("Horizontal");
        float mv= Input.GetAxis("Vertical");
        if(mh != 0 || mv != 0)
        {
            Vector3 movement = new Vector3(mh, 0, mv);
            Vector3 newPos = transform.position + movement*PlayerSpeed*Time.deltaTime;
            transform.position = newPos;
            moveDirection.y -= 10f * Time.deltaTime;
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
        }
    }
}
