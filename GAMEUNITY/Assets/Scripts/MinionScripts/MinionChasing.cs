using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionChasing : MonoBehaviour {

    MinionAI minion;
    public bool targetGone = false;
    bool inAgroRange = false;

	void Start () {
        minion = GetComponentInParent<MinionAI>();
        //StartCoroutine(SearchTarget());
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Turret"))
        {
            targetGone = false;
            inAgroRange = true;
            //Assign collision in the Agro range as currentTarget
            minion.currentTarget = collision.gameObject;
        }
    }


    //Here is player leaves agro zone the minion gives up
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Turret"))
        {
            inAgroRange = false;
        }
    }*/

    IEnumerator SearchTarget()
    {
        //If object is destroyed
        GameObject sResult = GameObject.FindGameObjectWithTag("MinionTarget");
        if(sResult == null)
        {
            targetGone = true;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchTarget());
        }
    }

    private void Update()
    {
        Debug.Log("currentTarget = " + minion.currentTarget);
        if (minion.currentTarget == null)
        {
            targetGone = true;
            Debug.Log("SOLVED");
            minion.currentTarget = minion.baseTarget;
        }
    }


}
