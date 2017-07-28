using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    public GameObject BlinkCast;
    public Vector2 velocity;
    bool canCast = true;
    public Vector2 offset = new Vector2(0.4f, 0.1f);
    public float cooldown = 5f;
    PlayerMovement playerMove;
    PlayerManager playerManager;
    public Transform player;
    GameObject go;
    public bool goExist = false;

	// Use this for initialization
	void Start () {
        playerMove = GetComponentInParent<PlayerMovement>();
        playerManager = GetComponent<PlayerManager>();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T) && canCast)
        {
            playerManager.LoseMana(30);
            //Start cooldown count down
            StartCoroutine(CanCast());
            go = Instantiate(BlinkCast, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
            goExist = true;
            if (!playerMove.facingRight)
            {
                go.transform.Rotate(0, 180, 0);
            }
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);

        }

        if(canCast == false && goExist == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Tele");
                player.transform.position = go.transform.position + new Vector3(0f, 1f);
                Destroy(go);
                goExist = false;
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator CanCast()
    {
        //cooldown start at 1
        canCast = false;
        yield return new WaitForSeconds(cooldown);
        //0
        canCast = true;
    }
}
