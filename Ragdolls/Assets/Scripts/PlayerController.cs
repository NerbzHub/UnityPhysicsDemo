using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject cameraHolder;
    public GameObject m_Projectile;
    
    public Rigidbody m_rigidBody;
    public Collider m_headCollider;
    public Collider m_groundCollider;
    public Cloth targetCloth;

    public GameObject[] ProjectileArray;
    public bool reloading = false;

    [HideInInspector]
    public int bulletCounter = 0;

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

    public float moveSpeed;
    public float maxSpeed = 12.0f;

    //private WeaponStates weaponStates;
    private WeaponStates.enumWeaponStates currentWeapon;

    // Use this for initialization
    void Start()
    {
        if (gameObject.GetComponent<Rigidbody>())
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        //weaponStates = gameObject.GetComponent<WeaponStates>();
        //currentWeapon = WeaponStates.enumWeaponStates.Revolver;
        gameObject.GetComponent<WeaponStates>().currentState = WeaponStates.enumWeaponStates.Revolver;
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

        if (bulletCounter > 6)
        {
            for (int i = 1; i < bulletCounter; i++)
            {
                targetCloth.capsuleColliders[i] = null;
            }

            bulletCounter = 1;
        }

        if (Input.GetKey(KeyCode.R) && !reloading)
        {
            reloading = true;
            Invoke("Reload", 1.5f);

        }

        //-------------------------------------------------------------------------------------------
        //                                          Mouse
        //
        if (Input.GetMouseButtonDown(0))
        {
            //Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

            Ray raycast = Camera.main.ScreenPointToRay(new Vector3(Camera.main.scaledPixelWidth * 0.5f, Camera.main.scaledPixelHeight * 0.5f, 0));

            RaycastHit hitInfo;

            switch (gameObject.GetComponent<WeaponStates>().currentState)
            {
                case WeaponStates.enumWeaponStates.Revolver:
                    // all of the mouse stuff other than ragdolls.
                    break;
                case WeaponStates.enumWeaponStates.FluidGun:
                    // Fluid sim from gun
                    break;
                case WeaponStates.enumWeaponStates.RayGun:
                    // visible ray cast
                    break;
                default:
                    break;
            }

            ///screnwidth.width * 0.5
            ///screenwidth.height * 0.5

            LayerMask layerMaskRagdoll =
                ~(LayerMask.NameToLayer("Ragdoll"));

            LayerMask layerMaskCloth =
               // targets/ragdolls
               ~(LayerMask.NameToLayer("Cloth"));

            /// if layer mask is cloth layer then fire projectile.

            // Shoot

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
                //GameObject go = Instantiate(m_Projectile, newVec3, transform.rotation);
                //
                //targetCloth.capsuleColliders[1] = go.GetComponent<CapsuleCollider>();
                //++bulletCounter;
                //go.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);

                ProjectileArray[bulletCounter].transform.position =
                gameObject.GetComponent<WeaponStates>().m_GOWeaponPos.transform.position + gameObject.GetComponent<WeaponStates>().m_GOWeaponPos.transform.forward;
                //ProjectileArray[bulletCounter].transform.position = transform.position + m_ProjectileSpawnGO.transform.position;
                ProjectileArray[bulletCounter].GetComponent<Rigidbody>().isKinematic = true;
                ProjectileArray[bulletCounter].GetComponent<Rigidbody>().isKinematic = false;
                ProjectileArray[bulletCounter].GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
                ++bulletCounter;
            }

        }
        //-------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------
        //                                   Keyboard Input Movement
        //
        if (Input.GetKey(KeyCode.W))
        {
            m_rigidBody.AddForce(new Vector3(transform.forward.x * moveSpeed, 0, transform.forward.z * moveSpeed), ForceMode.Acceleration);

        }

        if (Input.GetKey(KeyCode.S))
        {
            m_rigidBody.AddForce(new Vector3(-transform.forward.x * moveSpeed, 0, -transform.forward.z * moveSpeed), ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_rigidBody.AddForce(new Vector3(transform.right.x * moveSpeed, 0, transform.right.z * moveSpeed), ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_rigidBody.AddForce(new Vector3(-transform.right.x * moveSpeed, 0, -transform.right.z * moveSpeed), ForceMode.Acceleration);
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

        //toggle weapon
        // todo: it is staying on fluid gun for some reason.
        if (Input.GetKeyDown(KeyCode.U))
        {
            switch (gameObject.GetComponent<WeaponStates>().currentState)
            {
                case WeaponStates.enumWeaponStates.Revolver:
                    gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.FluidGun);
                    // revolver model visible
                    break;

                case WeaponStates.enumWeaponStates.FluidGun:
                    //fluidgun
                    gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.RayGun);
                    break;

                case WeaponStates.enumWeaponStates.RayGun:
                    //raygun
                    gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.Revolver);
                    break;
                default:
                    break;
            }

            //if(currentWeapon == WeaponStates.enumWeaponStates.Revolver)
            //    weaponStates.setState(WeaponStates.enumWeaponStates.FluidGun);

            //else if (currentWeapon == WeaponStates.enumWeaponStates.FluidGun)
            //    weaponStates.setState(WeaponStates.enumWeaponStates.RayGun);

            //else if (currentWeapon == WeaponStates.enumWeaponStates.RayGun)
            //    weaponStates.setState(WeaponStates.enumWeaponStates.Revolver);
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
            if (m_canStand)
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

        if (other.tag == "Ground")
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

    private void Reload()
    {
        bulletCounter = 0;
        reloading = false;
    }

}



/// 15 shots in the array.
/// 6 bullets in the gun.
/// bullets dissappear after 2 seconds.
/// reload time makes it so that the array can never stuff up.
/// THUS ALLOWING FOR THE ARRAY TO SEEM LIKE ITS NOT SHIT.

/// use the scroll wheel to swap between 
/// fluid gun, normal gun or laser gun.(Somehow draw the ray)