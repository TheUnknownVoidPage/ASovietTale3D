using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollToggle : MonoBehaviour
{
    //protected Animator Animator;
    protected Rigidbody Rigidbody;
    protected CapsuleCollider CapsuleCollider;

    protected Collider[] ChildrenCollider;
    protected Rigidbody[] ChildrenRigidbody;

    void Awake()
    {
        //Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();

        ChildrenCollider = GetComponentsInChildren<Collider>();
        ChildrenRigidbody = GetComponentsInChildren<Rigidbody>();


    }
    void Start()
    {
        RagdollActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void RagdollActive(bool active)
    {
        foreach(var collider in ChildrenCollider)
        {
            collider.enabled = active;
        }
        foreach (var rigidbody in ChildrenRigidbody)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        //Animator.enabled = !active;
        Rigidbody.detectCollisions = !active;
        Rigidbody.isKinematic = active;
        CapsuleCollider.enabled = !active;
        
    }
}
