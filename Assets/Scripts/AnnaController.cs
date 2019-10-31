using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaController : MonoBehaviour
{
    public enum States { Idle, Chase, Attack };
    States currentState = States.Idle;
    private int counterValue = 0;
    public int totalTime;
    private Transform player;
    public float attackDistance;
    public float chasingSpeed;
    public bool playerHasToy = false;
    public int toysLeft;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("Counter");
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
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
        Vector3 dir2P = player.position - transform.position;
        float dS = chasingSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + dir2P.normalized * dS;
        transform.position = newPos;
    }

    void Attack()
    {
        Debug.Log("Attacking");
        Destroy(player.gameObject);
    }

    IEnumerator Counter()
    {
        yield return new WaitForSeconds(1f);
        counterValue++;
        StartCoroutine("Counter");
    }

    private void ChangeState(States toState)
    {
        currentState = toState;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && playerHasToy == true)
        {
            playerHasToy = false;
            toysLeft--;
            if (toysLeft == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
