using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Item targetItem;
    public int damageGun = 10;
    public float rangeGun = 100f;
    public bool seeRangeGun;
    public float fireRate = 10f;
    public float impactForce = 60f;
    public float reloadTime = 1f;

    public int ammo;
    public int currentAmmo;
    public int maxAmmo;

    public Camera targetCamWeapon;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        ammo = Inventory.instanceInventory.GetItemCount(targetItem);

        if(isReloading)
        {
            return;
        }

        if(currentAmmo <= 0)
        {
            if (ammo > 0)
            {              
                StartCoroutine(Reloading());
            }
            return;
        }

        Shooting();
        Reload();
    }

    public void Shooting()
    {
        if(Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;

        currentAmmo--;

        muzzleFlash.Play();

        if(Physics.Raycast(targetCamWeapon.transform.position, targetCamWeapon.transform.forward, out hit, rangeGun))
        {            
           // Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(damageGun);
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }

    public void Reload()
    {
        if (ammo > 0)
        {           
            if (Input.GetKey(KeyCode.R))
            {
                StartCoroutine(Reloading());
            }
        }       
    }

    public IEnumerator Reloading()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);
        
        int diffe = maxAmmo - currentAmmo;
        
        currentAmmo += diffe;                    

        isReloading = false;

        Inventory.instanceInventory.RemoveItem(targetItem, diffe);
    }
    
    void OnDrawGizmos()
    {
        if(seeRangeGun)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangeGun);
        }
    }
}
