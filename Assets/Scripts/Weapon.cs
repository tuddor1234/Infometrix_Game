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
    public AudioSource audioSource;

    GameObject[] impacts;
    int currentImpact = 0;
    int maxImpacts = 5;

    public AudioClip[] WeaponSounds; // 0-fire 1-reload 2-out of amo

    public Animator anim;



    


    void Start()
    {
        WeaponSounds = new AudioClip[3];
        impacts = new GameObject[maxImpacts];


        audioSource = GetComponent<AudioSource>();
        for(int i=0;i<3;i++)
        {
            WeaponSounds[i] = new AudioClip();
        }
        anim = GetComponent<Animator>();


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
        muzzleFlash.Stop();
        muzzleFlash.Play();

     
        audioSource.Play();
       
        

        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit, range))
        {
            impacts[currentImpact].transform.position = hit.point;
             impacts[currentImpact].GetComponent<ParticleSystem>().Play();
           
    //         Debug.Log("You hit " + hit.ToString());
           
            if (currentImpact+1 >= maxImpacts) currentImpact = 0;
        }

        currentBullets--;
        fireTimer = 0.0f;


    }

    public void Reload()
    {

    }



}
