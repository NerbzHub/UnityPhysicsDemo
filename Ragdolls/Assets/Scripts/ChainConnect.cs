using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainConnect : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        gameObject.GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }

}
