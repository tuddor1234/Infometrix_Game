using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float range = 100f;
    public float fireRate = 0.1f;
    float fireTimer;


    public int bulletsInOneMagazine = 30;
    public int bulletsLeft = 120;
    public int currentBullets;

    
    public ParticleSystem muzzleFlash;
    public GameObject impactPrefab;

    GameObject[] impacts;
    int currentImpact = 0;
    int maxImpacts = 5;

    


    void Start()
    {
        impacts = new GameObject[maxImpacts];
        for(int i = 0; i< maxImpacts;i++)
        {
            impacts[i] = (GameObject)Instantiate(impactPrefab);
        }
        
        currentBullets = bulletsInOneMagazine;
    }

    void Update()
    {

        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            Fire();
           // muzzleFlash.Play();
           
        }
        else if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }

        if (fireTimer < fireRate) fireTimer += Time.deltaTime;


    }

    public void Fire()
    {
        if (fireTimer < fireRate) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit, range))
        {
            impacts[currentImpact].transform.position = hit.point;
          //  impacts[currentImpact].GetComponent<ParticleSystem>().Play();
           Debug.Log("You hit " + hit.ToString());
           
            if (currentImpact+1 >= maxImpacts) currentImpact = 0;
        }

        currentBullets--;
        fireTimer = 0.0f;


    }

    public void Reload()
    {

    }



}
