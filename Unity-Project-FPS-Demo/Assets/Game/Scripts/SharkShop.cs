using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
	//check for a collision with customer
	//check if the player
	//check for E key
	//check if player has coin
	//remove coin from player
	//update the inventory display
	//play win sound
	//debug you have no coin

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Player player = other.GetComponent<Player>();
				if (player != null)
				{
					if (player.hasCoin == true)
					{
						player.hasCoin = false;
						UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
						if (uiManager != null)
						{
							uiManager.RemoveCoin();
						}

						AudioSource audio = GetComponent<AudioSource>();
						audio.Play();
						player.EnableWeapons();
					}
					else
					{
						Debug.Log("Get out of here!");
					}
				}
			}
		}
	}

}
