using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStates : MonoBehaviour
{

    public enum enumWeaponStates
    {
        Revolver,
        FluidGun,
        RayGun
    };

    public GameObject[] Weapons;
    public GameObject m_GOWeaponPos;
    public GameObject waterParticles;
    public enumWeaponStates currentState = enumWeaponStates.Revolver;

    private Vector3 spawnPoint;
    // Use this for initialization
    void Start()
    {
        spawnPoint = Weapons[0].transform.position;
        setState(enumWeaponStates.Revolver);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setState(enumWeaponStates newState)
    {
        // perhaps get all of the gun meshes on a gameobject and have them under 
        // the world then depending on the state, swap out the model and set
        // the other one to original transform.

        switch (newState)
        {
            case enumWeaponStates.Revolver:
                // revolver model visible
                Weapons[2].transform.position = spawnPoint;
                Weapons[0].transform.position = m_GOWeaponPos.transform.position;
                break;

            case enumWeaponStates.FluidGun:
                //fluidgun
                Weapons[0].transform.position = spawnPoint;
                Weapons[1].transform.position = m_GOWeaponPos.transform.position;
                waterParticles.SetActive(true);
                break;

            case enumWeaponStates.RayGun:
                //raygun
                Weapons[1].transform.position = spawnPoint;
                waterParticles.SetActive(false);
                Weapons[2].transform.position = m_GOWeaponPos.transform.position;

                break;
            default:
                break;
        }
        currentState = newState;
    }
}
