using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class KathController : MonoBehaviour
{
    public Text timer;
    public int startingTime;
    private NavMeshAgent navMeshAgent;
    public Transform player;
    public enum States { Wait, Chase, Attack };
    private States currentState;
    private bool saidChaseTaunt = false;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = startingTime.ToString();
        StartCoroutine(Countdown());
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = States.Wait;
        GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string[] {
                "JULIE: Kath, why are you doing this?",
                "KATHERINE: You won't understand Julie....this is what needs to be done to keep you safe."});
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
            case States.Wait:
                Wait();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Attack:
                Attack();
                break;
        }
    }

    void Wait()
    {
        float d2P = Vector3.Distance(transform.position, player.position);
        if (startingTime == 30)
        {
            if (!GameObject.Find("DialogManager").GetComponent<DialogManager>().dialogPanel.activeInHierarchy)
            {
                GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string[] {
                    "JULIE: Understand what, that you want to kill me just like the rest!?",
                    "KATHERINE: You never understood. Never would have survived without me. I PROTECTED YOU!!" });
            }
        }
        if (startingTime <= 0)
        {
            ChangeState(States.Chase);
        }
        if (d2P < 2f)
        {
            if (!GameObject.Find("DialogManager").GetComponent<DialogManager>().dialogPanel.activeInHierarchy)
            {
                GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string[] {
                "JULIE: This ends here Kath I don't need your 'help' anymore.",
                "KATHERINE: Y..y...you can't do this without me....you can't live! You will die from the reality of your past. ",
                "JULIE: My past is mine and mine alone. You three tought me that I need to deal with my past. The good of it and all the bad. I need to face it as me...as Juile." });
                StartCoroutine(Win());
            }
        }
    }

    void Chase()
    {
        navMeshAgent.SetDestination(player.position);
        float d2P = Vector3.Distance(transform.position, player.position);
        if (!GameObject.Find("DialogManager").GetComponent<DialogManager>().dialogPanel.activeInHierarchy && saidChaseTaunt == false)
        {
            saidChaseTaunt = true;
            GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(new string[] {
                "KATHERINE: I'm sorry Julie. Please don't make this difficult. This is for your own good.",
                "JULIE: Kath....no."});
            StartCoroutine(DeathConversation());
        }
        //if (d2P <= 1f)
        //    ChangeState(States.Attack);
    }

    IEnumerator DeathConversation()
    {
        yield return new WaitForSeconds(10);
        ChangeState(States.Attack);
    }

    void Attack()
    {
        if (MenuController.type.ToString().Equals("Singleplayer"))
            SceneManager.LoadScene("DeathScene3");
        else if (MenuController.type.ToString().Equals("Multiplayer"))
            SceneManager.LoadScene("DeathSceneMulti3");
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(20);
        if (MenuController.type.ToString().Equals("Singleplayer"))
            SceneManager.LoadScene("WinScene3");
        else if (MenuController.type.ToString().Equals("Multiplayer"))
            SceneManager.LoadScene("WinSceneMulti3");
    }

    private void ChangeState(States toState)
    {
        currentState = toState;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);
        timer.text = startingTime--.ToString();
        if (startingTime >= 0)
            StartCoroutine(Countdown());
    }
}

