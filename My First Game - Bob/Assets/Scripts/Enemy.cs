using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=lHLZxd0O6XY&ab_channel=ChronoABI
//https://www.youtube.com/watch?v=jvtFUfJ6CP8&t=647s&ab_channel=Brackeys
//https://www.youtube.com/watch?v=mm6ctkNTFb8&t=1350s&ab_channel=Thrasonic
//links to youtube videos where I got most of this code from
public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    public float LOS;
    public float shootingRange;
    public float fireRate = 1f;
    public float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        //if the player is in the enemy's line of sight and out of shooting range, the enemy will move to the player until it is in shooting range when it will shoot at the player
        if (distanceFromPlayer < LOS && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, enemySpeed * Time.deltaTime);
            MovingToPlayer();
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            animator.SetBool("isMoving", false);
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void MovingToPlayer()
    {
        int direction = player.transform.position.x < transform.position.x ? -1 : 1;
        //switches the enemy's entire gameobject to look at the player depending on which side they are on
        if (player.transform.position.x < transform.position.x)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(-5f, 5f);
            }
        }
        else
        {
            if(transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(5, 5);
            }
        }
        animator.SetBool("isMoving", true);
    }
    //draws green lines to show the enemy's line of sight and shooting range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LOS);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
