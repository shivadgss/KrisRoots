using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    public Transform[] followPath;
    [SerializeField]
    private int length;
    [SerializeField]
    private int currentPosition = 0;
    public float noiseDistance;
    public float leaveDistance;

    public enum EnemyState {patrolling, Checking, Chasing };
    [SerializeField]
    private EnemyState state = EnemyState.patrolling;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        length = followPath.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.patrolling)
        {
            agent.SetDestination(followPath[currentPosition].position);
        }

        NoiseCheck();

        if (state == EnemyState.Chasing)
        {
            agent.SetDestination(player.position);
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    switchPosition();
                }
            }
        }
    }

    void switchPosition()
    {
        currentPosition++;

        if (currentPosition > length - 1)
        {
            currentPosition = 0;
        }
    }

    void NoiseCheck()
    {
        float distance = Vector3.Distance(player.position, transform.position);
         
        if (distance <= noiseDistance && Input.GetMouseButton(0) && state != EnemyState.Chasing)
        {
            state = EnemyState.Checking;
            agent.SetDestination(player.position);
        }

        if (distance >= leaveDistance && state == EnemyState.Chasing)
        {
            state = EnemyState.Checking;
            StartCoroutine(waitBeforePatrolEnable());
        }

        if (!agent.pathPending && state == EnemyState.Checking)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    StartCoroutine(waitBeforePatrolEnable());
                }
            }
        }
    }

    IEnumerator waitBeforePatrolEnable()
    {
        yield return new WaitForSeconds(5f);
        state = EnemyState.patrolling;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            state = EnemyState.Chasing;
        }
    }

}