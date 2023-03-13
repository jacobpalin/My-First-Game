using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used same references as the enemy script
public class BossMovement : MonoBehaviour
{
    public float bossSpeed;
    public float LOS;
    public float meleeRange;
    public float castRange;
    public float meleeRate = 0f;
    public float castRate = 0f;
    float nextFireTime;
    float nextCastTime;
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
        //sets a float to the distance from the boss to the player
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        //looks for object spawned by animation to set castRange to 0
        if (GameObject.Find("FinishedCasting(Clone)"))
        {
            castRange = 0f;
        }
        else
        {
            castRange = 29f;
        }

        //move to player if in line of sight (LOS) and out of melee and cast ranges
        if (distanceFromPlayer < LOS && distanceFromPlayer > meleeRange && distanceFromPlayer > castRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, bossSpeed * Time.deltaTime);
            MovingToPlayer();
        }

        //cast ranged attack if in range
        else if (distanceFromPlayer <= castRange && nextCastTime < Time.time)
        {
            nextCastTime = Time.time + castRate;
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("IsCast");
        }

        //melee player if in range
        else if (distanceFromPlayer <= meleeRange && nextFireTime < Time.time)
        {
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("IsAttack");
            nextFireTime = Time.time + meleeRate;
        }
    }

    void MovingToPlayer()
    {
        //not sure what this line is doing
        int direction = player.transform.position.x < transform.position.x ? -1 : 1;

        //this if/else statement makes the boss face the player no matter what side they are on
        if (player.transform.position.x > transform.position.x)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(-15f, 15f);
            }
        }
        else
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(15f, 15f);
            }
        }
        animator.SetBool("IsMoving", true);
    }

    //draws the green lines showing ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LOS);
        Gizmos.DrawWireSphere(transform.position, meleeRange);
        Gizmos.DrawWireSphere(transform.position, castRange);
    }
}