using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour
{
    public Transform player;

    [Header("Audio")]
    public AudioClip smallZoneSound;
    public AudioClip largeZoneSound;

    private AudioSource audioSource;

    private NavMeshAgent agent;
    private Animator animator;

    private bool inSmallZone = false;
    private bool inLargeZone = false;
    private bool chasingToy = false;
    private bool isDistracted = false;

    private Transform toyTarget;

    // Animation numbers
    private int RUN = 4;
    private int SIT = 7;
    private int ANGRY = 6;
    private int ALERT = 1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        agent.stoppingDistance = 1.8f;
        animator.SetInteger("Animation", SIT);
    }

    void Update()
    {
        // 🔴 TOY HAS HIGHEST PRIORITY
        if (chasingToy && toyTarget != null)
        {
            float toyDistance = Vector3.Distance(transform.position, toyTarget.position);

            if (toyDistance > agent.stoppingDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(toyTarget.position);
                animator.SetInteger("Animation", RUN);
            }
            else
            {
                agent.isStopped = true;
                animator.SetInteger("Animation", SIT);
                chasingToy = false;
            }

            return;
        }

        // 🔴 SMALL PROXIMITY (Only if NOT distracted)
        if (inSmallZone && !isDistracted)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > agent.stoppingDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetInteger("Animation", RUN);
            }
            else
            {
                agent.isStopped = true;
                animator.SetInteger("Animation", ANGRY);
            }

            return;
        }

        // 🔵 LARGE PROXIMITY (Only if NOT distracted)
        if (inLargeZone && !isDistracted)
        {
            agent.isStopped = true;
            animator.SetInteger("Animation", ALERT);
            return;
        }

        // Default
        agent.isStopped = true;
        animator.SetInteger("Animation", SIT);
    }

    // ===== SMALL ZONE =====
    public void EnterSmallZone()
    {
        inSmallZone = true;
        inLargeZone = false;

        // 🔊 Play bark sound
        if (!isDistracted && audioSource != null && smallZoneSound != null)
        {
            audioSource.PlayOneShot(smallZoneSound);
        }
    }

    public void ExitSmallZone()
    {
        inSmallZone = false;
    }

    // ===== LARGE ZONE =====
    public void EnterLargeZone()
    {
        if (!inSmallZone && !isDistracted)
        {
            inLargeZone = true;

            // 🔊 Play growl sound
            if (audioSource != null && largeZoneSound != null)
            {
                audioSource.PlayOneShot(largeZoneSound);
            }
        }
    }

    public void ExitLargeZone()
    {
        inLargeZone = false;
    }

    // ===== TOY =====
    public void ChaseToy(Transform toy)
    {
        chasingToy = true;
        inSmallZone = false;
        inLargeZone = false;

        isDistracted = true;   // distraction stays forever
        toyTarget = toy;

        agent.isStopped = false;
    }
}
