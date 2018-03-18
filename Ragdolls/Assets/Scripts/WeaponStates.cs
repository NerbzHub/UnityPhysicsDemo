using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------------
//  Class:
//      WeaponStates: The weaponstates class is used to stores the different weapons you
//                     are able to use in the demo. It allows for easy changing between 
//                      states.
//-------------------------------------------------------------------------------------------
public class WeaponStates : MonoBehaviour
{
    // ENUM for the different weaponstates.
    public enum enumWeaponStates
    {
        Revolver,
        FluidGun,
        WaterJetpack
    };

    // An array for the different weapon gameobjects.
    public GameObject[] Weapons;
    // The position on the player that they hold the weapons at.
    public GameObject m_GOWeaponPos;
    // Particles for the fluid gun.
    public GameObject waterParticles;
    // Setting the first state to be revolver.
    public enumWeaponStates currentState = enumWeaponStates.Revolver;

    // The spawn point fpr the weapons to be stored when they aren't in use.
    private Vector3 spawnPoint;

    //-------------------------------------------------------------------------------------------
    // Use this for initialization.
    //-------------------------------------------------------------------------------------------
    void Start()
    {
        spawnPoint = Weapons[0].transform.position;
        setState(enumWeaponStates.Revolver);
    }

    //-------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //-------------------------------------------------------------------------------------------
    void Update()
    {

    }

    //-------------------------------------------------------------------------------------------
    // SetState: Sets the current state to newState
    //
    //      params:
    //          newState: The new enum to be set.
    public void setState(enumWeaponStates newState)
    {
        // This sets the different weapons gameobjects to the player.
        switch (newState)
        {
            case enumWeaponStates.Revolver:

                Weapons[2].transform.position = spawnPoint;
                Weapons[0].transform.position = m_GOWeaponPos.transform.position;
                break;

            case enumWeaponStates.FluidGun:
                
                Weapons[0].transform.position = spawnPoint;
                Weapons[1].transform.position = m_GOWeaponPos.transform.position;
                waterParticles.SetActive(true);
                break;

            case enumWeaponStates.WaterJetpack:
                
                Weapons[1].transform.position = spawnPoint;
                waterParticles.SetActive(false);
                Weapons[2].transform.position = m_GOWeaponPos.transform.position;
                break;

            default:
                break;
        }
        // Sets the current state to the new state.
        currentState = newState;
    }
}
