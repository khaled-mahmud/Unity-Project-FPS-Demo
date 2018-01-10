using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField]
	private Text _ammoText;

	[SerializeField]
	private GameObject _coin;

	public void UpdateAmmo(int count)
	{
		_ammoText.text = "Ammo: " + count;
	}

	public void CollecteedCoin()
	{
		_coin.SetActive(true);
	}

	public void RemoveCoin()
	{
		_coin.SetActive(false);
	}
}
