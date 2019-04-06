using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private GameObject _pauseBG;
    [SerializeField]
    private GameObject _resumeGame;
    [SerializeField]
    private GameObject _quitGame;
    [SerializeField]
    private GameObject _pressEToCollectCoin;

    public bool _gamePaused;

    [SerializeField]
    public GameObject _coinInventory;

    [SerializeField]
    private Text ammoCount;

    public void UpdateAmmo(int count)
    {
        ammoCount.text = "Ammo:" + count;
    }
    public void CollectedCoin()
    {
        _coinInventory.SetActive(true);

    }

    private void Start()
    {
        UnPause();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            UnPause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused == false)
        {
            Cursor.lockState = CursorLockMode.None;
            GamePause();
        }
    }


    public void GamePause()
    {
        _pauseBG.SetActive(true);
        _resumeGame.SetActive(true);
        _quitGame.SetActive(true);
        Time.timeScale = 0f;
        _gamePaused = true;
    }
    public void UnPause()
    {
        _pauseBG.SetActive(false);
        _resumeGame.SetActive(false);
        _quitGame.SetActive(false);
        Time.timeScale = 1f;
        _gamePaused = false;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void CollectCoinOverlayOn()
    {
        _pressEToCollectCoin.SetActive(true);
    }
    public void CollectCoinOverlayOff()
    {
        _pressEToCollectCoin.SetActive(false);
    }
}
