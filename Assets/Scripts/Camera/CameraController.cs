using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Transform target, player;
    public float zoomSpeed = 2f;
    public Image aim;   
    float mouseX, mouseY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        CamControl();
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        if(Input.GetKey(KeyCode.LeftAlt))
        {
            aim.gameObject.SetActive(false);
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            aim.gameObject.SetActive(true);
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            player.rotation = Quaternion.Euler(0, mouseX, 0);  
        }        
    } 
}
