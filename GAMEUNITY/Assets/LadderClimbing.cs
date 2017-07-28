using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : MonoBehaviour {

    public float ladderCenter;

    public void Start()
    {
        ladderCenter = this.GetComponent<BoxCollider2D>().offset.x;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Ladder center: " + ladderCenter);
            collision.GetComponent<PlayerMovement>().climbingLadder = true;
            //collision.GetComponent<PlayerMovement>().velocity.x = ladderCenter;
            //float playerX = collision.GetComponent<BoxCollider2D>().offset.x;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().climbingLadder = false;
        }
    }
}
