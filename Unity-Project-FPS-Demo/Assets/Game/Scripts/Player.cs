using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private CharacterController _controller;
	[SerializeField]
	private float _speed = 3.5f;
	private float _gravity = 9.81f;
	[SerializeField]
	private GameObject _muzzleFlash;
	[SerializeField]
	private GameObject _hitMarkerPrefab;
	[SerializeField]
	private AudioSource _weaponAudio;

	[SerializeField]
	private int currentAmmo;
	private int maxAmmo = 50;

	private bool _isReLoading = false;

	private UIManager _uiManager;

	//variable for has coin
	public bool hasCoin = false;

	[SerializeField]
	private GameObject _weapon;

	// Use this for initialization
	void Start ()
	{
		_controller = GetComponent<CharacterController>();
		//hide mouse cursor
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		currentAmmo = maxAmmo;
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if left click
		//cast ray from center point of main camera

		if (Input.GetMouseButton(0) && currentAmmo > 0)
		{
			Shoot();	
		}
		else
		{
			_muzzleFlash.SetActive(false);
			_weaponAudio.Stop();
		}

		if (Input.GetKeyDown(KeyCode.R) && _isReLoading == false)
		{
			_isReLoading = true;
			StartCoroutine(Reload());
		}
		
		
		//if escape key pressed
		//unhide mouse cursor

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		CalculateMovement();
	}

	void Shoot()
	{
		_muzzleFlash.SetActive(true);
		currentAmmo--;
		_uiManager.UpdateAmmo(currentAmmo);
		//if audio is not playing
		//play audio
		if (_weaponAudio.isPlaying == false)
		{
			_weaponAudio.Play();
		}

		Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hitInfo;

		if (Physics.Raycast(rayOrigin, out hitInfo))
		{
			Debug.Log("Hit: " + hitInfo.transform.name);
			GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
			Destroy(hitMarker, 1f);

			//check if we hit the crate
			Destructable crate = hitInfo.transform.GetComponent<Destructable>();
			//Destroy Crate
			if (crate != null)
			{
				crate.DestroyCrate();
			}

		}
	}

	void CalculateMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
		Vector3 velocity = direction * _speed;
		velocity.y -= _gravity;

		velocity = transform.transform.TransformDirection(velocity);
		_controller.Move(velocity * Time.deltaTime);
	}

	IEnumerator Reload()
	{
		yield return new WaitForSeconds(1.5f);
		currentAmmo = maxAmmo;
		_uiManager.UpdateAmmo(currentAmmo);
		_isReLoading = false;
	}

	public void EnableWeapons()
	{
		_weapon.SetActive(true);
	}
}
