using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    private UI_Manager UIManager;
    private float _sensitivity = -1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }
    private void Look()
    {
        UI_Manager UIManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if(UIManager._gamePaused == false)
        {
            float _mouseY = Input.GetAxis("Mouse Y");
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.x += _mouseY * _sensitivity;
            transform.localEulerAngles = newRotation;
        }
    }
}
