using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControll : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public float radius;
    private Transform player;

    public enum State {patroling,chaising}
    public State state = State.patroling;

    void Start()
    {
        player = GameLogic.I.player;
    }
    void Update()
    {
        float distToPlayer = Vector3.Distance(player.position, transform.position);

        if (state == State.patroling && distToPlayer < radius && OnEyes())
        {
            state = State.chaising;
            animator.SetBool("IsRun",true);
        }
        if (state == State.chaising)
        {
            agent.SetDestination(player.position);
            if (distToPlayer < 1f)
            {
                GameLogic.I.Catched();
                agent.destination = transform.position;
                agent.speed = 0;
                animator.SetBool("IsRun", false);
                enabled = false;
            }
        }

    }
    public bool OnEyes()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, player.position - transform.position, out hit, radius);
        Debug.Log("See " + hit.collider.name);
        if (hit.collider.name == "Player") return true;
        else return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
