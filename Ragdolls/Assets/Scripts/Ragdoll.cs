using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------------
// Class:
//      Ragdoll: This class is used for a humanoid object that when triggered turns into a ragdoll.
//-------------------------------------------------------------------------------------------
[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{

    // This stores the animation of the walk.
    private Animator animator = null;
    // Stores the rigidBody list.
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    // What happens when the ragdoll gets turned on.
    public bool RagdollOn
    {
        get { return !animator.enabled; }
        set
        {
            animator.enabled = !value;
            foreach (Rigidbody r in rigidbodies)
                r.isKinematic = !value;
        }
    }

    //-------------------------------------------------------------------------------------------
    // Use this for initialization.
    //-------------------------------------------------------------------------------------------
    void Start()
    {
        // Allocate the animator into the holder.
        animator = GetComponent<Animator>();

        // Sets all of the rigidbodies to kinematic.
        foreach (Rigidbody r in rigidbodies)
            r.isKinematic = true;
    }

    //-------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //-------------------------------------------------------------------------------------------
    void Update()
    {

    }

}
