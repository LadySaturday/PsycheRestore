using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharlieController : MonoBehaviour
{
    public enum States { ToKitchen, Patrol, Chase, Attack };
    States currentState = States.ToKitchen;
    public Transform knife;
    private float radiusArriving = 1;
    private float radiusArriving_Stop = 1f;
    public float maxSpeed = 5;
    public float speed = 0;
    public Transform hand;
    public float chaseDistance = 10;
    private float attackDistance = 2;
    private bool isPlayerHidden = false;
    public float chasingSpeed = 2;
    Transform player;
    // Start is called before the first frame update
    private float yStart;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        yStart = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
    }

    void ToKitchen()
    {
        Vector3 dir = knife.position - this.transform.position;
        float distance = dir.magnitude;
        if (distance < radiusArriving_Stop)
        {
            Destroy(knife.gameObject);
            Instantiate(knife, hand.position, this.transform.rotation, hand);
            ChangeState(States.Patrol);
            return;
        }
        dir.Normalize();
        if (distance < radiusArriving)
        {
            speed = maxSpeed * (distance - radiusArriving_Stop) / (radiusArriving - radiusArriving_Stop);
        }
        else
        {
            speed = maxSpeed;
        }
        Vector3 newPos = transform.position + dir * speed * Time.deltaTime;
        newPos.y = yStart;
        transform.position = newPos;
    }

    void Patrol()
    {
        Debug.Log("Patrolling");
        // teleport to different rooms and patrol around
        float d2P = Vector3.Distance(transform.position, player.position);
        if (d2P < chaseDistance /* && looking at player*/)
        {
            ChangeState(States.Chase);
        }
    }

    private void ChangeState(States toState)
    {
        currentState = toState;
    }

    void Chase()
    {
        Debug.Log("Chasing");
        float d2P = Vector3.Distance(transform.position, player.position);
        if (d2P <= attackDistance)
        {
            ChangeState(States.Attack);
        }
        else if (d2P > chaseDistance)
        {
            ChangeState(States.Patrol);
        }
        else
        {
            CheckPlayerHidden();
            if (isPlayerHidden)
            {
                ChangeState(States.Patrol);
            }
        }
        Vector3 dir2P = player.position - transform.position;
        float dS = chasingSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + dir2P.normalized * dS;
        transform.position = newPos;
        //Vector3 rot2P = transform.LookAt(player)

    }

    void Attack()
    {
        Debug.Log("Attacking");
        Destroy(player.gameObject);
        // kill player
    }

    private void CheckPlayerHidden()
    {
        Vector3 e2P = player.position - transform.position;
        e2P.Normalize();
        float cosPhi = Vector3.Dot(e2P, transform.forward);
        isPlayerHidden = (cosPhi < 0);
    }

    void FSM()
    {
        switch (currentState)
        {
            case States.ToKitchen:
                ToKitchen();
                break;
            case States.Patrol:
                Patrol();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Attack:
                Attack();
                break;
        }
    }
}
