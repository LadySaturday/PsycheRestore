using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

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
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        countDownDisplay = totalTime;
        StartCoroutine("Counter");
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        //Vector3 dir2P = player.position - transform.position;
        //float dS = chasingSpeed * Time.deltaTime;
        //Vector3 newPos = transform.position + dir2P.normalized * dS;
        //transform.position = newPos;
        navMeshAgent.SetDestination(player.position);
    }

    void Attack()
    {
        Debug.Log("Attacking");
        Destroy(player.gameObject);
        SceneManager.LoadScene("DeathScene2");
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
                SceneManager.LoadScene("WinScene2");
            }
        }
        else if (collider.tag == "Player" && playerHasToy == false)
        {
            GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string []{ "I want...","I want my bear"});
        }
    }
}
