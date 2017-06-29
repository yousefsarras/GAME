using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Controller2D))]
public class PlayerMovement : MonoBehaviour {
    //Animation Handle
    [HideInInspector]
    public Animator anim;

    //Jump handle
    public float maxJumpHeight = 2;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = 0.4f;
    public bool grounded;

    //Move handle
    public float moveSpeed = 6;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;

    //Smoothing
    float velocityXSmoothing;
    float accelerationTimeAirBourne = 0.2f; //move slower in air
    float acceleratonTimeGrounded = 0.1f;

    //reference to controller2d class for movement
    Controller2D controller;

    //Handle attack
    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCd = 0.3f;
    public Collider2D attackTrigger;

    //Reference to player
    public PlayerManager playerInfo;

    void Start () {
        controller = GetComponent<Controller2D>();
        playerInfo = GetComponent<PlayerManager>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("Grounded", grounded);
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Check if there is ground
        if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            if (input.y != -1)
            {
                velocity.y = maxJumpVelocity;
            }
        }

        //short jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        //melee attack
        if(Input.GetKeyDown("f") && !attacking)
        {
            //requirement to cast attack
            if (playerInfo.getMana() >= 10)
            {
                //start attacking
                attacking = true;
                playerInfo.LoseMana(10);
                //set to the initial time to max cd time
                attackTimer = attackCd;
                //being the collider2d
                attackTrigger.enabled = true;
                anim.SetTrigger("mAttack");
            }
            else
            {
                Debug.Log("Out of Mana");
            }
            
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                input.x = 0;
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }

    float targetVelocityX = input.x * moveSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)? acceleratonTimeGrounded : accelerationTimeAirBourne);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);

        //Solve accumulating gravity
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        //Animation
        if (!controller.collisions.below)
        {
            grounded = false;
        }
        else
        {
            grounded = true;
        }

        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        //Flip
        if (Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
