using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhysicsSoundObject : MonoBehaviour
{
    public AudioClip lightImpact;
    public AudioClip hardImpact;

    public float lightThreshold = 1.0f;
    public float hardThreshold = 3.0f;

    private AudioSource audioSource;
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;

        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce > hardThreshold)
        {
            PlayHardImpact();
        }
        else if (impactForce > lightThreshold)
        {
            PlayLightImpact();
        }
    }

    void OnReleased(SelectExitEventArgs args)
    {
        float releaseSpeed = rb.linearVelocity.magnitude;

        if (releaseSpeed > hardThreshold)
        {
            PlayHardImpact();
        }
        else if (releaseSpeed > lightThreshold)
        {
            PlayLightImpact();
        }
    }

    void PlayLightImpact()
    {
        audioSource.PlayOneShot(lightImpact);
    }

    void PlayHardImpact()
    {
        audioSource.PlayOneShot(hardImpact);
    }
}
