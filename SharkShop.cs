using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    [SerializeField]
    private AudioClip _winSound;

    private Player _player;

    private UI_Manager _uiManager;
    //check for collision
    //check if player
    //check for E key
    //check if player has coin
    //remove coin from player
    //update inventory display
    //play win sound
    //debug Get Out Of Here!

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player _player = other.GetComponent<Player>();
                if(_player != null)
                {
                    UI_Manager _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

                    if (_player.playerHasCoin == true)
                    {
                        Debug.Log("Here Ya Go!");
                        AudioSource.PlayClipAtPoint(_winSound, transform.position);
                        _player.playerHasCoin = false;
                        _uiManager._coinInventory.SetActive(false);
                        _player.GivePlayerWeapon();
                    }
                    else if(_player.playerHasCoin == false)
                    {
                        Debug.Log("Get Outta Here!");
                    }
                }

            }
        }
    }
}
