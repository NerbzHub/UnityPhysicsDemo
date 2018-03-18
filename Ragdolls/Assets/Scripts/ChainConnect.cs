using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-------------------------------------------------------------------------------------
// Class:
//      ChainConnect is used for setting up the chain on awake.
//-------------------------------------------------------------------------------------
public class ChainConnect : MonoBehaviour {

    //-------------------------------------------------------------------------------------
    // Awake runs when the gameobject becomes awake.
    //-------------------------------------------------------------------------------------
    void Awake()
    {
        gameObject.GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }

}
