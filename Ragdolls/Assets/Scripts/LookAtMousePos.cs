using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------
// Class:
//      LookAtMousePos: This class is used to ray cast from the mouse's position.
//-------------------------------------------------------------------------------------
public class LookAtMousePos : MonoBehaviour {
    
	// Update is called once per frame
	void Update ()
    {
        // Do this when the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            // Ray cast at the centre of the window.
            Ray raycast = Camera.main.ScreenPointToRay(new Vector3 (Camera.main.scaledPixelWidth * 0.5f, Camera.main.scaledPixelHeight * 0.5f, 0));

            // Get the info of what the ray hit.
            RaycastHit hitInfo;
            
            // Creating a new layer mask for ragdolls.
            LayerMask layerMask =
                ~(LayerMask.NameToLayer("Ragdoll"));

            // If the raycasst hit a ragdoll, do this.
            if (Physics.Raycast(raycast, out hitInfo, 100.0f, layerMask.value))
            {
                // Turn into a ragdoll.
                Ragdoll ragdoll = hitInfo.transform.gameObject.GetComponentInParent<Ragdoll>();
                if (ragdoll != null)
                    ragdoll.RagdollOn = true;
            }
        }
	}
}
