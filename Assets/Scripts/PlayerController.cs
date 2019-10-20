using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed ;
    public float rotationSpeed = 200.0f;

    Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;


    // reference to character's Animator component
    private Animator anim;

    void Start()
    {
   
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        updateAnim();


            moveDirection.y -= 10f * Time.deltaTime;
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
            transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);   

    }


    void updateAnim()
    {
        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.RightShift)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)))
        {
            // IF Shift key is pressed while walking, run
            movementSpeed = 2;
            anim.SetFloat("Speed_f", 2);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            // ELSE, set speed limit to walk speed
            movementSpeed = 1;
            anim.SetFloat("Speed_f", 1);
        }
        else
        {
            //ELSE idle
            anim.SetFloat("Speed_f", 0);

        }
    }
}