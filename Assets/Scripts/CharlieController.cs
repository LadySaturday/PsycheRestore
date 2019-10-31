using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharlieController : MonoBehaviour
{
    public enum States { ToKitchen, Patrol, Chase, Attack };
    public Transform[] waypoints;
    private Transform curWaypoint;
    private int curWayPointNum = 0;
    private States currentState;
    public Transform knife;
    private float radiusArriving = 1f;
    public float speed;
    public Transform hand;
    public float chaseDistance;
    public float attackDistance;
    public float chasingSpeed;
    private Transform player;
    private float yStart;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        yStart = transform.position.y;
        currentState = States.ToKitchen;
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
        dir.Normalize();
        if (distance < radiusArriving)
        {
            Destroy(knife.gameObject);
            Instantiate(knife, hand.position, this.transform.rotation, hand);
            ChangeState(States.Patrol);
            return;
        }
        Vector3 newPos = transform.position + dir * speed * Time.deltaTime;
        newPos.y = yStart;
        transform.position = newPos;
    }

    void Patrol()
    {
        Vector3 dir = curWaypoint.position - this.transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        if (distance < radiusArriving)
        {
            if (curWayPointNum < 4)
            {
                curWayPointNum++;
            }
            else
            {
                curWayPointNum = 0;
            }
            curWaypoint = waypoints[curWayPointNum];
            return;
        }
        Vector3 newPos = transform.position + dir * speed * Time.deltaTime;
        newPos.y = yStart;
        transform.position = newPos;

        float d2P = Vector3.Distance(transform.position, player.position);
        if (d2P < chaseDistance)
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
        float d2P = Vector3.Distance(transform.position, player.position);
        if (d2P <= attackDistance)
        {
            ChangeState(States.Attack);
        }
        else if (d2P > chaseDistance)
        {
            ChangeState(States.Patrol);
        }
        Vector3 dir2P = player.position - transform.position;
        float dS = chasingSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + dir2P.normalized * dS;
        transform.position = newPos;
    }

    void Attack()
    {
        Destroy(player.gameObject);
    }

    void FSM()
    {
        switch (currentState)
        {
            case States.ToKitchen:
                ToKitchen();
                break;
            case States.Patrol:
                radiusArriving = 2f;
                curWaypoint = waypoints[curWayPointNum];
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
