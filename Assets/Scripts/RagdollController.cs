using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody[] ragdollBodies;
    [SerializeField] private Collider[] ragdollColliders;
    [SerializeField] private CharacterController charController;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        charController = GetComponentInParent<CharacterController>();

        foreach(var rb in ragdollBodies)
            rb.isKinematic = true;

        foreach(var collider in ragdollColliders)
            collider.enabled = true;
    }

    public void EnableRagdoll()
    {
        if(animator != null)
            animator.enabled = false;

        if(charController != null)
            charController.enabled = false;

        foreach (var rb in ragdollBodies)
            rb.isKinematic = false;
    }
}
