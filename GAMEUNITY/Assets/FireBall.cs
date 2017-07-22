using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public int damage = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().DamageEnemy(damage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Obstacle")|| collision.CompareTag("Through"))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 1f);
    }
}
