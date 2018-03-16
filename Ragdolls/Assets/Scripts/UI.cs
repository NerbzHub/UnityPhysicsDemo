using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    public GameObject reloadText;
    public GameObject outOfAmmoText;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		/// if reloading, show text otherwise have setactive to false
        if(player.GetComponent<PlayerController>().reloading)
        {
            reloadText.SetActive(true);
        }
        else
        {
            reloadText.SetActive(false);
        }

        if(player.GetComponent<PlayerController>().bulletCounter >= 6 &&
            !player.GetComponent<PlayerController>().reloading)
        {
            outOfAmmoText.SetActive(true);
        }
        else
        {
            outOfAmmoText.SetActive(false);
        }

    }
}
