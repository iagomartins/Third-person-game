using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int selectedWeapon;
    public GameObject[] weapons;
    //public Animator animatorScope;
    public GameObject scopeOverlay;
    public Camera mainCamera;
    public float scopedFOV = 15f;
    public float zoomFOV = 20f;

    private int lastSelectedWeapon;
    private float normalFOV;
    private bool isScoped = false;
    private bool isZoom = false;
    private bool onScoped = true;
    private bool onWeaponChanged = false;

    void Start()
    {
        SelectWeapon();
        normalFOV = mainCamera.fieldOfView;
    }

    void Update()
    {
        MouseScrollWhell();
        keysAlpha();
        toAim();
    }

    public void toAim()
    {
        isScoped = Input.GetMouseButton(1) && lastSelectedWeapon == selectedWeapon;
        isZoom = Input.GetMouseButton(1) && lastSelectedWeapon == selectedWeapon;
        if (weapons[selectedWeapon] == weapons[4])
        {
            scopeOverlay.SetActive(isScoped);

            if (isScoped)
            {
                OnScoped();
            }
            else
            {
                OnUnscoped();
            }
        }
        else
        {
            if (isZoom)
            {
                zoomWeapon();
            }
            else
            {
                normalZoomWeapon();
                OnUnscoped();
            }
        }
    }

    public void SelectWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == selectedWeapon);
        }
        if (!Input.GetMouseButton(1) && lastSelectedWeapon != selectedWeapon)
        {
            lastSelectedWeapon = selectedWeapon;
        }
    }

    public void MouseScrollWhell()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selectedWeapon++;
            onWeaponChanged = true;
            if (selectedWeapon >= weapons.Length)
            {
                selectedWeapon = 0;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedWeapon--;
            onWeaponChanged = true;
            if (selectedWeapon < 0)
            {
                selectedWeapon = weapons.Length - 1;
            }
        }
        SelectWeapon();
        if (onWeaponChanged)
        {
            onWeaponChanged = false;
            isScoped = false;
            isZoom = false;
            if (selectedWeapon == 4)
            {
                OnUnscoped();
            }
            else
            {
                normalZoomWeapon();
            }
        }
    }

    public void keysAlpha()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            OnUnscoped();
            selectedWeapon = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            OnUnscoped();
            selectedWeapon = 1;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            OnUnscoped();
            selectedWeapon = 2;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            OnUnscoped();
            selectedWeapon = 3;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            OnUnscoped();
            selectedWeapon = 4;
        }
    }

    public void zoomWeapon()
    {
        mainCamera.fieldOfView = zoomFOV;
    }

    public void normalZoomWeapon()
    {
        mainCamera.fieldOfView = normalFOV;
    }

    public void OnUnscoped()
    {
        Debug.Log("Unscoped");
        scopeOverlay.SetActive(false);

        mainCamera.fieldOfView = normalFOV;
    }

    public void OnScoped()
    {
        if (onScoped)
        {
            scopeOverlay.SetActive(true);
            mainCamera.fieldOfView = scopedFOV;
        }
    }
}
