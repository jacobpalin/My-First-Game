using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I think this was already in the shooter stub and I repurposed it for the enemy bullets
public class Bullet : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bullet;

    void Start()
    {
        //this is used in the enemy slime's bullets to find the current position of the player and goes to the player's position but does not track the player
        bullet = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 move = (target.transform.position - transform.position).normalized * speed;
        bullet.velocity = new Vector2(move.x, move.y);
        Destroy(this.gameObject, 2);
    }

    void Update()
    {

    }
}
