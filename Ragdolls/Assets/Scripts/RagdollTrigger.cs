using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------------
//  Class:
//      RagdollTrigger: This class is used for the onTriggerEnter function for when the 
//                      ragdoll object enter one of these triggers.
//-------------------------------------------------------------------------------------------
public class RagdollTrigger : MonoBehaviour {

    //-------------------------------------------------------------------------------------------
    // Use this for initialization.
    //-------------------------------------------------------------------------------------------
    void Start () {
		
	}

    //-------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //-------------------------------------------------------------------------------------------
    void Update ()
    {

    }

    //-------------------------------------------------------------------------------------------
    // This occurrs when the object enters a trigger.
    //-------------------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        // Sets the ragdoll to on when it enters the trigger.
        Ragdoll ragdoll = other.gameObject.GetComponentInParent<Ragdoll>();
        if (ragdoll != null)
            ragdoll.RagdollOn = true;
    }
}
