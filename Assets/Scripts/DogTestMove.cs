using UnityEngine;
using UnityEngine.AI;

public class DogTestMove : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            if (agent.velocity.magnitude > 0.1f)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }
}
