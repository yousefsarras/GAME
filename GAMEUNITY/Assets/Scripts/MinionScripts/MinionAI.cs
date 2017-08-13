using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinionController2D))]
public class MinionAI : MonoBehaviour
{
    //Basic Reference 
    MinionController2D controller;
    //public Collider2D Turret;
    BoxCollider2D minionHitBox;
    MinionChasing minionChase;

    //Handle Basic Movement
    Vector3 velocity;
    float gravity = -20;
    public float moveSpeed = 8f;
    float direction;

    //Handle Chasing and Target
    public bool chasing = false;
    public GameObject baseTarget;
    [HideInInspector]
    public GameObject currentTarget;
    public float offset = 3f;
    public bool facingRight;
    float attackStop;
    Vector2 attackLocation;
    bool minionCollide = false;

    //Handle Ranged Attack
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletTimer;
    public float shootInterval;
    public Transform shootPoint;


    void Start()
    {
        FindBase();
        controller = GetComponent<MinionController2D>();
        minionChase = GetComponentInChildren<MinionChasing>();
        currentTarget = baseTarget;
    }

    // Update is called once per frame
    void Update()
    {
        SettingOffset();
        direction = Mathf.Sign((transform.position.x - attackStop) * -1);
        //reached destination
        if ((transform.position.x - attackLocation.x) <= 1f && (transform.position.x - attackLocation.x) >= -1f)
        {
            velocity.x = 0;
        }
        //Not within target point
        else {

            //changing directions
            if (direction < 0.1f && facingRight || direction > -0.1f && !facingRight)
            {
                facingRight = !facingRight;
                transform.localScale = new Vector2(transform.localScale.x * -1, 1);
            }

            if(minionCollide == true && minionChase.targetGone == true)
            {
                velocity.x = direction * moveSpeed;
            }
            else if (minionCollide == true && minionChase.targetGone == false)
            {
                velocity.x = 0;
            }
            else
            {
                velocity.x = direction * moveSpeed;
            }

        }

        //Reset the Gravity
        if(controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);
        //Debug.Log(chasing);
    }

    void SettingOffset()
    {
        if (facingRight)
        {
            attackStop = currentTarget.transform.position.x - offset;
            attackLocation = new Vector2(attackStop, 1f);
        }
        else
        {
            attackStop = currentTarget.transform.position.x + offset;
            attackLocation = new Vector2(attackStop, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Minion"))
        {
            minionCollide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Minion"))
        {
            minionCollide = false;
        }
    }

    public void FindBase()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("BasePoint");
        if(sResult == null)
        {
            Debug.LogError("No Base");
        }
        else
        {
            baseTarget = sResult;
        }
    }

}