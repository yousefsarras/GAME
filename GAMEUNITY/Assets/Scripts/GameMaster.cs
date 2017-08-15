using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Takes care of stuff outside of the player class or object
public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    private void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public int spawnDelay = 5;
    public Transform playerPrefab;
    public Transform spawnPoint;

    public IEnumerator RespawnPlayer(PlayerManager player)
    {
        yield return new WaitForSeconds(spawnDelay);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.transform.position = spawnPoint.position;
        player.cam.enabled = true;
        player.dead = false;
    }

    //Player argument access player class
    //static so no reference to gamemaster everytime we kill a player and simply pass reference here
    public void KillPlayer(PlayerManager player)
    {
		Debug.Log ("Trying to kill the player");
        player.transform.position = new Vector3(-63, -13, 0);
        player.GetComponent<PlayerMovement>().enabled = false;
        gm.StartCoroutine(gm.RespawnPlayer(player));
        player.playerStats.currentHealth = player.playerStats.maxHealth;
    }

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    public static void KillTurret(TurretManager turret)
    {
        //Destroy expects a gameobject
        Destroy(turret.gameObject);
    }
}
