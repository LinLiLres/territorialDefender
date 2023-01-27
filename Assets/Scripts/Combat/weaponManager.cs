using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

public class weaponManager : MonoBehaviour
{

    public GameObject playerCam;
    public float range = 100f;
    public float damage = 25f;
    public ParticleSystem muzzleFlash;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        if (Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range))
        {
            //Debug.Log("Shoot");
            
            Health health = hit.transform.GetComponent<Health>();
            if(health != null) {
                health.GunTakeDamage(damage);
            }

        }
    }
}
