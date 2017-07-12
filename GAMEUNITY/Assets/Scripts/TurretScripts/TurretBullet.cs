using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManager>().DamagePlayer(20);
            Destroy(gameObject);
        }

        Destroy(gameObject, 1.5f);
    }
}
