using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    public static float movementSpeed ;
    public float rotationSpeed = 200.0f;
    public float jumpForce;
    bool isOnGround;

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
        ProcessJumping();

        moveDirection.y -= 10f * Time.deltaTime;
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
            transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);   

    }

    void ProcessJumping()
    {
        CheckIfOnGround();
        if (Input.GetKeyDown(KeyCode.LeftControl) && isOnGround)//(Input.GetKeyDown(KeyCode.Space) && isOnGround)//removed until network control implememnted
        {
            Debug.Log("JumP");
            transform.Translate(0, 0.75f, 0);
            isOnGround = false;
            anim.SetBool("Jump_b", true);
        }


    }


    void CheckIfOnGround()    {
        Ray[] rays = new Ray[3];
        rays[0] = new Ray(transform.position - Vector3.right * .45f, Vector3.down);
        rays[1] = new Ray(transform.position, Vector3.down);
        rays[2] = new Ray(transform.position + Vector3.right * .45f, Vector3.down);

        RaycastHit hit;
        float maxD = .1f;

        foreach (Ray ray in rays)
        {
            if (Physics.Raycast(ray, out hit, maxD))
            {
                if (hit.collider != null)
                {
                    isOnGround = true;
                    //isJumping = false; REMOVED
                }
                else
                {
                    isOnGround = false;
                }
            }
        }
    }


    void updateAnim()
    {
        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)))//|| (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.RightShift))//temporarily removed until network controls are added. Left keyboard belongs to julie, right keyboard belongs to dot
        {
            //if(Input.GetKeyUp(KeyCode.LeftShift)) anim.SetFloat("Speed_f", 1);
            // IF Shift key is pressed while walking, run
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