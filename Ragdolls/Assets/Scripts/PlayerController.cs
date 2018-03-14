using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_Force;

    public GameObject m_Projectile;
    public Rigidbody m_rigidBody;
    public Collider m_headCollider;
    public Collider m_groundCollider;

    private bool m_canStand = true;
    private bool m_grounded = false;
    private bool bOnce = true;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;

    // Use this for initialization
    void Start()
    {
        if (gameObject.GetComponent<Rigidbody>())
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
      
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Mouse look at
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }


        //-------------------------------------------------------------------------------------------
        //                                          Mouse
        //
        if (Input.GetMouseButtonDown(0))
        {
            //Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

            Ray raycast = Camera.main.ScreenPointToRay(new Vector3(Camera.main.scaledPixelWidth * 0.5f, Camera.main.scaledPixelHeight * 0.5f, 0));

            RaycastHit hitInfo;

            ///screnwidth.width * 0.5
            ///screenwidth.height * 0.5

            LayerMask layerMaskRagdoll =
                ~(LayerMask.NameToLayer("Ragdoll"));

            LayerMask layerMaskCloth =
               // targets/ragdolls
               ~(LayerMask.NameToLayer("Cloth"));

            /// if layer mask is cloth layer then fire projectile.





            if (Physics.Raycast(raycast, out hitInfo, 100.0f, layerMaskRagdoll.value))
            {
                //do hit stuff for ragdoll

                /// other needs to be what i hit(Ragdoll)
                Ragdoll ragdoll = hitInfo.transform.gameObject.GetComponentInParent<Ragdoll>();
                if (ragdoll != null)
                    ragdoll.RagdollOn = true;
            }

            /// Fire Projectile

            if (Physics.Raycast(raycast, out hitInfo, 100.0f, layerMaskCloth.value))
            {
                Transform newTrans = gameObject.transform;
                Vector3 newVec3 = newTrans.position;
                newVec3 += transform.forward;
                //newTrans.position = newVec3;
                //do hit stuff for cloth.
                GameObject go =  Instantiate(m_Projectile, newVec3, transform.rotation);
                go.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
            }

        }
        //-------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------
        //                                      Keyboard Input
        //
        if (Input.GetKey(KeyCode.W))
        {
            m_rigidBody.AddForce(transform.forward, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_rigidBody.AddForce(-transform.forward, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_rigidBody.AddForce(transform.right, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_rigidBody.AddForce(-transform.right, ForceMode.Impulse);
        }
        //-------------------------------------------------------------------------------------------
        //                                          Jump
        //
        if ((Input.GetKeyDown(KeyCode.Space) && m_grounded))
        {
            m_rigidBody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
        //-------------------------------------------------------------------------------------------
        if ((Input.GetAxis("Mouse ScrollWheel") > 0f) && m_grounded && bOnce)
        {
            m_rigidBody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            bOnce = false;
            Invoke("TimedbOnce", 0.5f);
        }
        //-------------------------------------------------------------------------------------------
        //                                         Crouch
        //
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_headCollider.isTrigger = true;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if(m_canStand)
            m_headCollider.isTrigger = false;
        }
        //-------------------------------------------------------------------------------------------



        //if(m_canStand)
        //{
        //    m_headCollider.isTrigger = false;
        //}

        // grounded trigger underneath the normal collider and if other.tag is ground then grounded.

        
        // add list for any rigidbodies.
        // if 
    }

    private void OnTriggerEnter(Collider other)
    {
        m_canStand = false;

        if(other.tag == "Ground")
        {
            m_grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_canStand = true;

        if (other.tag == "Ground")
        {
            m_grounded = false;
        }
    }

    private void TimedbOnce()
    {
        bOnce = true;
    }
}
