using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharlieControllerMulti : MonoBehaviour
{
    public enum States { ToKitchen, Patrol, Chase, Attack };
    public AudioClip chaseMusic;
    public AudioClip patrolMusic;
    public Transform[] nodes;
    private Transform[] points;
    private int destPoint = 0;
    private States currentState;
    public Transform knife;
    public float speed;
    public Transform hand;
    public float chaseDistance;
    public float attackDistance;
    public float chasingSpeed;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Graph2 g;
    private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // Yizhi 11/10/2019
    Transform secondaryPlayer;
    float stopDistance = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = States.ToKitchen;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoBraking = false;
        navMeshAgent.speed = speed;
        points = new Transform[0];

        g = new Graph2(HeuristicStrategy.EuclideanDistance);
        g.add_vertex_AStar('A', nodes[0].transform.position, new Dictionary<char, float>() { { 'B', findDistance(nodes[0], nodes[1]) }, { 'F', findDistance(nodes[0], nodes[5]) }, { 'G', findDistance(nodes[0], nodes[6]) } });

        g.add_vertex_AStar('B', nodes[1].transform.position, new Dictionary<char, float>() { { 'A', findDistance(nodes[1], nodes[0]) }, { 'C', findDistance(nodes[1], nodes[2]) }, { 'E', findDistance(nodes[1], nodes[4]) }, { 'F', findDistance(nodes[1], nodes[5]) } });

        g.add_vertex_AStar('C', nodes[2].transform.position, new Dictionary<char, float>() { { 'B', findDistance(nodes[2], nodes[1]) }, { 'D', findDistance(nodes[2], nodes[3]) }, { 'E', findDistance(nodes[2], nodes[4]) } });

        g.add_vertex_AStar('D', nodes[3].transform.position, new Dictionary<char, float>() { { 'E', findDistance(nodes[3], nodes[4]) }, { 'L', findDistance(nodes[3], nodes[11]) }, { 'M', findDistance(nodes[3], nodes[12]) } });

        g.add_vertex_AStar('E', nodes[4].transform.position, new Dictionary<char, float>() { { 'B', findDistance(nodes[4], nodes[1]) }, { 'C', findDistance(nodes[4], nodes[2]) }, { 'D', findDistance(nodes[4], nodes[3]) }, { 'F', findDistance(nodes[4], nodes[5]) }, { 'K', findDistance(nodes[4], nodes[10]) }, { 'L', findDistance(nodes[4], nodes[11]) } });

        g.add_vertex_AStar('F', nodes[5].transform.position, new Dictionary<char, float>() { { 'A', findDistance(nodes[5], nodes[0]) }, { 'B', findDistance(nodes[5], nodes[1]) }, { 'E', findDistance(nodes[5], nodes[4]) }, { 'G', findDistance(nodes[5], nodes[6]) }, { 'K', findDistance(nodes[5], nodes[10]) } });

        g.add_vertex_AStar('G', nodes[6].transform.position, new Dictionary<char, float>() { { 'F', findDistance(nodes[6], nodes[5]) }, { 'H', findDistance(nodes[6], nodes[7]) } });

        g.add_vertex_AStar('H', nodes[7].transform.position, new Dictionary<char, float>() { { 'G', findDistance(nodes[7], nodes[6]) }, { 'I', findDistance(nodes[7], nodes[8]) }, { 'J', findDistance(nodes[7], nodes[9]) } });

        g.add_vertex_AStar('I', nodes[8].transform.position, new Dictionary<char, float>() { { 'H', findDistance(nodes[8], nodes[7]) }, { 'J', findDistance(nodes[8], nodes[9]) }, { 'Q', findDistance(nodes[8], nodes[16]) } });

        g.add_vertex_AStar('J', nodes[9].transform.position, new Dictionary<char, float>() { { 'H', findDistance(nodes[9], nodes[7]) }, { 'I', findDistance(nodes[9], nodes[8]) }, { 'K', findDistance(nodes[9], nodes[10]) }, { 'P', findDistance(nodes[9], nodes[15]) } });

        g.add_vertex_AStar('K', nodes[10].transform.position, new Dictionary<char, float>() { { 'H', findDistance(nodes[10], nodes[7]) }, { 'L', findDistance(nodes[10], nodes[11]) }, { 'O', findDistance(nodes[10], nodes[14]) }, { 'J', findDistance(nodes[10], nodes[9]) }, { 'E', findDistance(nodes[10], nodes[4]) }, { 'F', findDistance(nodes[10], nodes[5]) } });

        g.add_vertex_AStar('L', nodes[11].transform.position, new Dictionary<char, float>() { { 'K', findDistance(nodes[11], nodes[10]) }, { 'D', findDistance(nodes[11], nodes[3]) }, { 'E', findDistance(nodes[11], nodes[4]) }, { 'N', findDistance(nodes[11], nodes[13]) }, { 'M', findDistance(nodes[11], nodes[12]) }, { 'O', findDistance(nodes[11], nodes[14]) } });

        g.add_vertex_AStar('M', nodes[12].transform.position, new Dictionary<char, float>() { { 'D', findDistance(nodes[12], nodes[3]) }, { 'L', findDistance(nodes[12], nodes[11]) }, { 'N', findDistance(nodes[12], nodes[13]) } });

        g.add_vertex_AStar('N', nodes[13].transform.position, new Dictionary<char, float>() { { 'M', findDistance(nodes[13], nodes[12]) }, { 'L', findDistance(nodes[13], nodes[11]) }, { 'O', findDistance(nodes[13], nodes[14]) }, { 'U', findDistance(nodes[13], nodes[20]) }, { 'V', findDistance(nodes[13], nodes[21]) } });

        g.add_vertex_AStar('O', nodes[14].transform.position, new Dictionary<char, float>() { { 'K', findDistance(nodes[14], nodes[10]) }, { 'L', findDistance(nodes[14], nodes[11]) }, { 'N', findDistance(nodes[14], nodes[13]) }, { 'P', findDistance(nodes[14], nodes[15]) }, { 'J', findDistance(nodes[14], nodes[9]) }, { 'U', findDistance(nodes[14], nodes[20]) } });

        g.add_vertex_AStar('P', nodes[15].transform.position, new Dictionary<char, float>() { { 'J', findDistance(nodes[15], nodes[9]) }, { 'O', findDistance(nodes[15], nodes[14]) }, { 'Q', findDistance(nodes[15], nodes[16]) }, { 'S', findDistance(nodes[15], nodes[18]) }, { 'T', findDistance(nodes[15], nodes[19]) }, { 'U', findDistance(nodes[15], nodes[20]) } });

        g.add_vertex_AStar('Q', nodes[16].transform.position, new Dictionary<char, float>() { { 'J', findDistance(nodes[16], nodes[9]) }, { 'I', findDistance(nodes[16], nodes[8]) }, { 'R', findDistance(nodes[16], nodes[17]) }, { 'P', findDistance(nodes[16], nodes[15]) }, { 'S', findDistance(nodes[16], nodes[18]) } });

        g.add_vertex_AStar('R', nodes[17].transform.position, new Dictionary<char, float>() { { 'Q', findDistance(nodes[17], nodes[16]) }, { 'W', findDistance(nodes[17], nodes[22]) }, { 'X', findDistance(nodes[17], nodes[23]) }, { 'S', findDistance(nodes[17], nodes[18]) } });

        g.add_vertex_AStar('S', nodes[18].transform.position, new Dictionary<char, float>() { { 'Q', findDistance(nodes[18], nodes[16]) }, { 'R', findDistance(nodes[18], nodes[17]) }, { 'W', findDistance(nodes[18], nodes[22]) }, { 'X', findDistance(nodes[18], nodes[23]) }, { 'P', findDistance(nodes[18], nodes[15]) }, { 'Y', findDistance(nodes[18], nodes[24]) } });

        g.add_vertex_AStar('T', nodes[19].transform.position, new Dictionary<char, float>() { { 'Q', findDistance(nodes[19], nodes[16]) }, { 'P', findDistance(nodes[19], nodes[15]) }, { 'S', findDistance(nodes[19], nodes[18]) }, { 'Y', findDistance(nodes[19], nodes[24]) }, { 'Z', findDistance(nodes[19], nodes[25]) }, { 'U', findDistance(nodes[19], nodes[20]) } });

        g.add_vertex_AStar('U', nodes[20].transform.position, new Dictionary<char, float>() { { 'V', findDistance(nodes[20], nodes[21]) }, { 'N', findDistance(nodes[20], nodes[13]) }, { 'L', findDistance(nodes[20], nodes[11]) }, { 'O', findDistance(nodes[20], nodes[14]) }, { 'T', findDistance(nodes[20], nodes[19]) }, { 'Z', findDistance(nodes[20], nodes[25]) } });

        g.add_vertex_AStar('V', nodes[21].transform.position, new Dictionary<char, float>() { { 'N', findDistance(nodes[21], nodes[13]) }, { 'U', findDistance(nodes[21], nodes[20]) } });

        g.add_vertex_AStar('W', nodes[22].transform.position, new Dictionary<char, float>() { { 'R', findDistance(nodes[22], nodes[17]) }, { 'X', findDistance(nodes[22], nodes[23]) } });

        g.add_vertex_AStar('X', nodes[23].transform.position, new Dictionary<char, float>() { { 'R', findDistance(nodes[23], nodes[17]) }, { 'W', findDistance(nodes[23], nodes[22]) }, { 'S', findDistance(nodes[23], nodes[18]) }, { 'Y', findDistance(nodes[23], nodes[24]) } });

        g.add_vertex_AStar('Y', nodes[24].transform.position, new Dictionary<char, float>() { { 'S', findDistance(nodes[24], nodes[18]) }, { 'X', findDistance(nodes[24], nodes[23]) }, { 'T', findDistance(nodes[24], nodes[19]) }, { 'Z', findDistance(nodes[24], nodes[25]) } });

        g.add_vertex_AStar('Z', nodes[25].transform.position, new Dictionary<char, float>() { { 'U', findDistance(nodes[25], nodes[20]) }, { 'T', findDistance(nodes[25], nodes[19]) }, { 'Y', findDistance(nodes[25], nodes[24]) } });

        // Yizhi 11/10/2019
        secondaryPlayer = GameObject.FindGameObjectWithTag("SecondaryPlayer").transform;
    }

    void Update()
    {
        FSM();

        // Yizhi 11/10/2019
        float d2P = Vector3.Distance(transform.position, secondaryPlayer.position);
        if (d2P <= stopDistance)
        {
            Debug.Log("d2P <= stopDistance");
            StartCoroutine(Stop());
        }
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

    private void ChangeState(States toState)
    {
        currentState = toState;

        if (toState == States.Chase)
        {
            player.GetComponentInChildren<AudioSource>().Pause();
            player.GetComponentInChildren<AudioSource>().clip = chaseMusic;
            player.GetComponentInChildren<AudioSource>().Play();
        }
        if (toState == States.Patrol)
        {
            player.GetComponentInChildren<AudioSource>().Pause();
            player.GetComponentInChildren<AudioSource>().clip = patrolMusic;
            player.GetComponentInChildren<AudioSource>().Play();
        }

    }

    void ToKitchen()
    {
        navMeshAgent.SetDestination(knife.transform.position);
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 1f)
        {
            Vector3 pos = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z);
            Destroy(knife.gameObject);
            Instantiate(knife, pos, this.transform.rotation, hand);
            ChangeState(States.Patrol);
        }
    }

    void Patrol()
    {
        GetComponent<Animator>().SetBool("isChasing", false);
        navMeshAgent.speed = speed;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            if (destPoint < points.Length)
                GotoNextPoint();
            else
                MakeNewPath();

        float d2P = Vector3.Distance(transform.position, player.position);
        Vector3 myPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Vector3 targetDir = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(targetDir, transform.forward);
        RaycastHit hitInfo;
        Physics.Raycast(myPosition, targetDir, out hitInfo, chaseDistance);

        if (d2P < chaseDistance && hitInfo.collider.gameObject.tag == "Player" && angleToPlayer >= -60 && angleToPlayer <= 60)
            ChangeState(States.Chase);
        else if (d2P <= attackDistance)
            ChangeState(States.Attack);
        else if (Input.GetKeyDown(KeyCode.RightShift))
            InvestigateNoise();
    }

    void InvestigateNoise()
    {
        Debug.Log("Investigate");
        navMeshAgent.SetDestination(player.transform.position);
    }

    void Chase()
    {
        GetComponent<Animator>().SetBool("isChasing", true);
        Vector3 myPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        RaycastHit hitInfo;
        Vector3 targetDir = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(targetDir, transform.forward);
        float d2P = Vector3.Distance(transform.position, player.position);
        Physics.Raycast(myPosition, targetDir, out hitInfo, chaseDistance);
        navMeshAgent.speed = chasingSpeed;
        navMeshAgent.SetDestination(player.transform.position);
        if (d2P <= attackDistance)
            ChangeState(States.Attack);
        else if (d2P >= chaseDistance || hitInfo.collider.gameObject.tag != "Player" || angleToPlayer <= -60 || angleToPlayer >= 60)
            ChangeState(States.Patrol);
    }

    void Attack()
    {
        Destroy(player.gameObject);//causes camera to be destroyed
        SceneManager.LoadScene("DeathSceneMulti1");
    }

    // Yizhi 11/10/2019
    IEnumerator Stop()
    {
        Debug.Log("Stopping");
        navMeshAgent.speed = 0;
        transform.GetChild(0).gameObject.SetActive(true);
        Destroy(secondaryPlayer.gameObject);
        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        transform.GetChild(0).gameObject.SetActive(false);
        navMeshAgent.speed = speed;
    }

    private float findDistance(Transform a, Transform b)
    {
        return Vector3.Distance(a.position, b.position);
    }

    void GotoNextPoint()
    {
        navMeshAgent.SetDestination(points[destPoint].position);
        Debug.Log("Travelling to: " + points[destPoint].gameObject.name);
        destPoint++;
    }

    void MakeNewPath()
    {
        Debug.Log("Making New Path");
        destPoint = 0;

        char s = chars[Random.Range(0, chars.Length)];
        char f = chars[Random.Range(0, chars.Length)];

        List<char> shortest_path = g.shortest_path_via_AStar_algo(s, f);
        points = new Transform[shortest_path.Count];
        for (int x = 0; x < shortest_path.Count; x++)
            points[x] = GameObject.Find(shortest_path[x].ToString()).transform;
    }
}
