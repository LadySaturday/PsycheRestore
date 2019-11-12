using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    // Updated 2019-11-06 //
    public float movementSpeed = 1;
    public float runSpeed=5;
    public float walkSpeed = 1;
    bool isOnGround;
    Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    private Animator anim;
    public bool mouseRotate = true;
    public float rotationSpeed = 200f;
    public bool isWalking = false;
    public int strafe=0;//0=idle, 1=left, 2=right



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        updateAnim();
        ProcessJumping();

        //moveDirection.y -= 10f * Time.deltaTime;


        if (mouseRotate)
        {
            transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X")) * Mathf.Sign(Input.GetAxis("Horizontal")), Space.World);//mouse rotate

            if (Input.GetKey("up") || Input.GetKey("down"))
            {
                transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey("left"))
            {
                transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, 0,0 );
                strafe = 1;
            }
            else if (Input.GetKey("right"))
            {
                transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, 0, 0);
                strafe = 2;
            }
            else
            {
                
                strafe = 0;
            }
        }
        else//traditional keyboard controls-- can implement menu to choose rotation style
        {
            // updated by Yizhi 11/10/2019
            if (Input.GetKey("up") || Input.GetKey("down"))
            {
                transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
            }
            if (Input.GetKey("right") || Input.GetKey("left"))
            {
                 transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
          
            }
          
        }

        Debug.Log(movementSpeed);
        

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

            anim.SetInteger("strafe", strafe);
      
            if ((Input.GetKey(KeyCode.RightShift))&&(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow)))//(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift) ||//temporarily removed until network controls are added. Left keyboard belongs to julie, right keyboard belongs to dot
            {
                // Updated 2019-11-06 //
                anim.SetFloat("Speed_f", movementSpeed);
                movementSpeed = runSpeed;
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow))
            {
            
            // Updated 2019-11-06 //
            anim.SetFloat("Speed_f", movementSpeed);
                movementSpeed = walkSpeed;
            }
          
            else
            {
                //ELSE idle
                anim.SetFloat("Speed_f", 0);

            }
       
       
    }
}