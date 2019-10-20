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
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            // IF Shift key is pressed, run
            movementSpeed = 2;
            anim.SetFloat("Speed_f", 2);
        }
        else 
        {
            // ELSE, set speed limit to walk speed
            movementSpeed = 1;
            anim.SetFloat("Speed_f", 1);

        }





       // if (rb.isGrounded)
        //    {
         //       moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
         //       moveDirection = moveDirection * movementSpeed;
         //   }
            //Gravity
            moveDirection.y -= 10f * Time.deltaTime;
       //     rb.Move(moveDirection * Time.deltaTime);

            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
            transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);

        
     
        
        

    }
}