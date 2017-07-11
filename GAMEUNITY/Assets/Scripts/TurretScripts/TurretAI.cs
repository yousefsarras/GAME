using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;

    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 10f;
    public float bulletTimer;

    public bool awake = false;
    public bool lookingRight = false;

    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootingPointLeft;
    public Transform shootingPointRight;

    //search for player
    float nextTimeToSearch = 0f;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        //target = gameObject.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if(target == null)
        {
            FindPlayer();
            return;
        }

        anim.SetBool("Awake", awake);
        anim.SetBool("LookingRight", lookingRight);

        RangeCheck();

        if(target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }

        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }
    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        //Debug.Log("Distance : " + distance);
        if(distance < wakeRange)
        {
            awake = true;
        }

        if(distance > wakeRange)
        {
            awake = false;
        }
    }
    
    public void Attack(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;

        if(bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            //Left
            if (!attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootingPointLeft.transform.position, shootingPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }

            //Right
            if (attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootingPointRight.transform.position, shootingPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
        }
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                target = searchResult.transform.GetComponent<Transform>();
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
