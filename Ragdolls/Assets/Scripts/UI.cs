using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------------
//  Class:
//      UI: The UI class is used for majority of the UI functionality/interactions.
//-------------------------------------------------------------------------------------------
public class UI : MonoBehaviour
{
    // Text to know that you're currently reloading.
    public GameObject reloadText;
    // Text for when you need to reload
    public GameObject outOfAmmoText;
    // The player as a gameObject.
    public GameObject player;

    //-------------------------------------------------------------------------------------------
    // Use this for initialization.
    //-------------------------------------------------------------------------------------------
    void Start()
    {

    }

    //-------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //-------------------------------------------------------------------------------------------
    void Update()
    {
        //-------------------------------------------------------------------------------------------
        // if reloading, show text otherwise have SetActive to false.
        //-------------------------------------------------------------------------------------------
        if (player.GetComponent<PlayerController>().reloading)
        {
            reloadText.SetActive(true);
        }
        else
        {
            reloadText.SetActive(false);
        }

        //-------------------------------------------------------------------------------------------
        // If the player is out of ammo, show this text.
        //-------------------------------------------------------------------------------------------
        if (player.GetComponent<PlayerController>().bulletCounter >= 6 &&
            !player.GetComponent<PlayerController>().reloading)
        {
            outOfAmmoText.SetActive(true);
        }
        // Otherwise, don't show it.
        else
        {
            outOfAmmoText.SetActive(false);
        }

    }
}
