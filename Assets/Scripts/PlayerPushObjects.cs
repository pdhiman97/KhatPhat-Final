using UnityEngine;

public class PlayerPushObjects : MonoBehaviour
{
    public float pushForce = 2f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.AddForce(pushDir * pushForce, ForceMode.Impulse);
    }
}

