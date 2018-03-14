using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMousePos : MonoBehaviour {

    //public float lookSpeed;

	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

            Ray raycast = Camera.main.ScreenPointToRay(new Vector3 (Camera.main.scaledPixelWidth * 0.5f, Camera.main.scaledPixelHeight * 0.5f, 0));

            RaycastHit hitInfo;

            //screnwidth.width * 0.5
            //screenwidth.height * 0.5

            LayerMask layerMask =
                // targets/ragdolls
                ~(LayerMask.NameToLayer("Ragdoll"));


            if (Physics.Raycast(raycast, out hitInfo, 100.0f, layerMask.value))
            {
                //do hit stuff
                
                // other needs to be what i hit(Ragdoll)
                Ragdoll ragdoll = hitInfo.transform.gameObject.GetComponentInParent<Ragdoll>();
                if (ragdoll != null)
                    ragdoll.RagdollOn = true;
            }
        }
	}
}
