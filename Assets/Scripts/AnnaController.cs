using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnnaController : MonoBehaviour
{
    public enum States { Idle, Chase, Attack };
    States currentState = States.Idle;
    private int counterValue = 0;
    public int countDownDisplay;
    public int totalTime;
    private Transform player;
    public float attackDistance;
    public float chasingSpeed;
    public bool playerHasToy = false;
    public int toysLeft;
    public GameObject bearImage;

    // Yizhi 11/24/2019
    Transform secondaryPlayer;
    float stopDistance = 2;
    float speed = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        countDownDisplay = totalTime;
        StartCoroutine("Counter");

        // Yizhi 11/24/2019
        secondaryPlayer = GameObject.FindGameObjectWithTag("SecondaryPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FSM();

        if (playerHasToy)
        {
            bearImage.SetActive(true);
        }
        else
        {
            bearImage.SetActive(false);
        }
    }

    void FSM()
    {
        switch (currentState)
        {
            case States.Idle:
                Idle();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Attack:
                Attack();
                break;
        }
    }

    void Idle()
    {
        if (counterValue == totalTime)
        {
            ChangeState(States.Chase);
        }
    }

    void Chase()
    {
        Debug.Log("Chasing");
        float d2P = Vector3.Distance(transform.position, player.position);
        if (d2P <= attackDistance)
        {
            ChangeState(States.Attack);
        }
        if (d2P <= 10)
        {
            GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string[] { "BEAR!!!!" });
        }
        Vector3 dir2P = player.position - transform.position;
        float dS = chasingSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + dir2P.normalized * dS;
        transform.position = newPos;

        // Yizhi 11/24/2019
        float d2S = Vector3.Distance(transform.position, secondaryPlayer.position);
        if (d2S <= stopDistance)
        {
            Debug.Log("d2S <= stopDistance");
            StartCoroutine(Stop());
        }
    }

    void Attack()
    {
        Debug.Log("Attacking");
        Destroy(player.gameObject);
        SceneManager.LoadScene("DeathScene");
    }

    // Yizhi 11/10/2019
    IEnumerator Stop()
    {
        Debug.Log("Stopping");
        speed = chasingSpeed;
        chasingSpeed = 0;
        Light light = GetComponent<Light>();
        light.color = Color.cyan;
        countDownDisplay += 10;

        Destroy(secondaryPlayer.gameObject);
        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        
        yield return new WaitForSeconds(10);

        light.color = Color.red;
        chasingSpeed = speed;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator Counter()
    {
        yield return new WaitForSeconds(1f);
        counterValue++;
        countDownDisplay--; 
        if (countDownDisplay == 30)
        {
            GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string[] { "Hurry Up. I WANT my bear..." });
        }
        if (countDownDisplay == 0)
        {
            GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string[] { "Ready or not, here I come!" });
        }
        StartCoroutine("Counter");
    }

    private void ChangeState(States toState)
    {
        currentState = toState;
    }

    private void OnTriggerEnter(Collider collider)
    {
      
        if (collider.tag == "Player" && playerHasToy)
        {
            GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string[] { "Thank you" });
            playerHasToy = false;
            toysLeft--;
            if (toysLeft == 0)
            {
                Destroy(gameObject);
            }
        }
        else if (collider.tag == "Player" && playerHasToy == false)
        {
            GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string []{ "I want...","I want my bear"});
        }
    }
}
