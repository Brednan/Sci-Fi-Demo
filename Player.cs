using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private string sceneName;
    private Scene currentScene;

    private CharacterController _charControl;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.807f;

    private float shootingCoolDown;
    [SerializeField]
    private GameObject _gameWeapon;

    [SerializeField]
    private AudioSource _shootSound;

    public GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarker;
    private GameObject hitMarker;

    [SerializeField]
    public int currentAmmo;
    private int maxAmmo = 50;

    private UI_Manager UIManager;
    private Coin _coin;

    public bool isFiring;
    public bool playerHasCoin;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        _charControl = GetComponent<CharacterController>();

        string sceneName = currentScene.name;

        isFiring = false;

        currentAmmo = maxAmmo;

        UIManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        UIManager.UpdateAmmo(currentAmmo);

        shootingCoolDown = Time.time + 0.2f;

        Coin _coin = GameObject.Find("Coin").GetComponent<Coin>();

        playerHasCoin = false;

        RemovePlayerWeapon();

        UIManager.CollectCoinOverlayOff();
    }

    // Update is called once per frame
    void Update()
    {
        //if left click
        //cast ray from center point of main camera

        CrossHair();

        StopFiring();

        CalculateMovement();

        Reload();

        if(isFiring == true)
        {
            _muzzleFlash.gameObject.SetActive(true);
        }
        else
        {
            _muzzleFlash.gameObject.SetActive(false);

        }
        CursorLock();
    }
    void CalculateMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.TransformDirection(velocity);
        _charControl.Move(velocity * Time.deltaTime);
    }
    void CrossHair()
    {
        if (Input.GetMouseButton(0) && UIManager._gamePaused == false)
        {
            if (_shootSound.isPlaying == false && currentAmmo > 0 && _gameWeapon.activeSelf == true)
            {
                _shootSound.Play();
            }

            if (Time.time > shootingCoolDown && _gameWeapon.activeSelf == true)
            {
                if (currentAmmo > 0)
                {
                    isFiring = true;
                    Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hitInfo;
                    currentAmmo--;
                    UIManager.UpdateAmmo(currentAmmo);
                    shootingCoolDown = Time.time + 0.2f;

                    if (Physics.Raycast(rayOrigin, out hitInfo))
                    {
                        Debug.Log("RayCast Hit:" + hitInfo.transform.name);
                        GameObject hitMarker = Instantiate(_hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                        Destroy(hitMarker, 1f);
                    }
                }
                else if (currentAmmo == 0)
                {
                    isFiring = false;
                    _shootSound.Stop();
                }
                else if(Time.time < shootingCoolDown)
                {
                    isFiring = false;

                }
            }

        }
        else
        {
            _shootSound.Stop();
            isFiring = false;
        }
    }
    void StopFiring()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }
    }
    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentAmmo = maxAmmo;
            UIManager.UpdateAmmo(currentAmmo);

        }
    }
    public void GivePlayerWeapon()
    {
        _gameWeapon.SetActive(true);
    } 

    public void RemovePlayerWeapon()
    {
        _gameWeapon.SetActive(false);
    }
    private void CursorLock()
    {
        if(UIManager._gamePaused == false && sceneName == "Game")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(sceneName == "Main Menu")
        {
            Cursor.visible = true;
        }
    }
}
