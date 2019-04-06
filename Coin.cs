using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private AudioClip _coinPickUp;
    [SerializeField]
    private Rigidbody rb;

    private UI_Manager _uiManager;

    //check for collision (onTrigger)
    //check if Player
    //check for E key press
    //give player coin
    //destroy the coin

    private void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player")
        {
            UI_Manager _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
            Player _player = GameObject.Find("Player").GetComponent<Player>();
            if (_player.playerHasCoin == false)
            {
                _uiManager.CollectCoinOverlayOn();
            }
            else if(_player.playerHasCoin == true)
            {
                _uiManager.CollectCoinOverlayOff();
            }
            rb.WakeUp();

            if (Input.GetKeyDown(KeyCode.E))
            {

                if(_player != null)
                {
                    Debug.Log("Player Has Coin");
                    _player.playerHasCoin = true;
                    AudioSource.PlayClipAtPoint(_coinPickUp, transform.position, 1f);
                    _uiManager.CollectCoinOverlayOff();


                    if (_uiManager !=null)
                    {
                        _uiManager.CollectedCoin();

                    }
                    Destroy(this.gameObject, 0.5f);
                }
            }   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            UI_Manager _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
            _uiManager.CollectCoinOverlayOff();
        }
    }
}
