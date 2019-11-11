using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    // Updated 2019-11-06 //
    public float movementSpeed=1;
    public float runSpeed=2;
    bool isOnGround;
    Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
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
        transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X")) * Mathf.Sign(Input.GetAxis("Horizontal")), Space.World);//mouse rotate
        // updated by Yizhi 11/10/2019
       // if (Input.GetKey("right") || Input.GetKey("left"))
        //{
        //    transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);//change to Q and R for rotate
       // }
        if (Input.GetKey("up") || Input.GetKey("down"))
        {
            transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
        }

    }

    void ProcessJumping()
    {
        CheckIfOnGround();
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)//(Input.GetKeyDown(KeyCode.Space) && isOnGround)//removed until network control implememnted
        {
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
        if ((Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightShift))))////temporarily removed until network controls are added. Left keyboard belongs to julie, right keyboard belongs to dot
        {
            //if(Input.GetKeyUp(KeyCode.LeftShift)) anim.SetFloat("Speed_f", 1);
            // IF Shift key is pressed while walking, run
            //anim.SetFloat("Speed_f", 2);

            // Updated 2019-11-06 //
            anim.SetFloat("Speed_f", runSpeed);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            // ELSE, set speed limit to walk speed
            //movementSpeed = 1;
            //anim.SetFloat("Speed_f", 1);

            // Updated 2019-11-06 //
            anim.SetFloat("Speed_f", movementSpeed);
        }
        else
        {
            //ELSE idle
            anim.SetFloat("Speed_f", 0);

        }
    }
}