using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Takes care of stuff outside of the player class or object
public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public int spawnDelay = 1;
    public Transform playerPrefab;
    public Transform spawnPoint;

    private void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    //Player argument access player calss
    //static so no reference to gamemaster everytime we kill a player and simply pass reference here
    public static void KillPlayer(PlayerManager player)
    {
        //Destroy expects a gameobject
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }
}
