using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField]
	private AudioClip _coinPickUp;

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Player player = other.GetComponent<Player>();
				if (player != null)
				{
					player.hasCoin = true;
					AudioSource.PlayClipAtPoint(_coinPickUp, transform.position, 1f);
					UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

					if(uiManager != null)
					{
						uiManager.CollecteedCoin();
					}

					Destroy(this.gameObject);
				}
			}
		}
	}

	//check for collision (onTrigger)
	//check if player enter
	//check for E key press
	//give player the coin
	//play coin sound
	//destroy the coin to give the illusion that it has been picked up

}
