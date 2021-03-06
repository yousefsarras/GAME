﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : MonoBehaviour {

    public float ladderCenter;

    public void Start()
    {
        ladderCenter = this.GetComponent<BoxCollider2D>().transform.localPosition.x;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Declare the collision to be player
            PlayerMovement player = collision.GetComponent<PlayerMovement>();


            if (player.getOffLadder == true)
            {
                OnTriggerExit2D(player.GetComponent<Collider2D>());
                return;
            }
            //Set climbing to be true so climbing action is enabled in playerMovement
            player.climbingLadder = true;
            if (player.velocity.y == 0)
            {
                player.anim.SetBool("ClimbingIdle", true);
            }
            else
            {
                player.anim.SetBool("ClimbingIdle", false);
                player.anim.SetBool("Climbing", player.climbingLadder);
            }
            //handle centering
            Vector2 playerPos = new Vector2(ladderCenter, player.transform.position.y);
            player.transform.position = playerPos;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            player.climbingLadder = false;
            player.anim.SetBool("Climbing", player.climbingLadder);
            //If trying to getoff
            //collision.GetComponent<PlayerMovement>().getOffLadder = false;
        }
    }
}
