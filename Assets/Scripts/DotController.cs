using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class DotController : MonoBehaviour
{


    public static float movementSpeed =3;
    public float rotationSpeed = 200.0f;



    Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {  
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDirection.y -= 10f * Time.deltaTime;

        // updated by Yizhi 11/10/2019
        if (Input.GetKey("right") || Input.GetKey("left"))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);//change to Q and R for rotate
        }
        else if (Input.GetKey("up") || Input.GetKey("down"))
        {
            transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
        }

    }



}