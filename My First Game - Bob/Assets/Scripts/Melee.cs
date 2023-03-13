using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=sPiVz1k-fEs&t=422s&ab_channel=Brackeys
//https://www.youtube.com/watch?v=1QfxdUpVh5I&ab_channel=Blackthornprod
//links to youtube videos where I got most of this code from
public class Melee : MonoBehaviour
{

    private float attackSpeed;
    public float startAttackSpeed;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemies;

    public Animator animator;
    public AudioClip swing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //allows player to melee whenever the melee is off cooldown
        if (attackSpeed <= 0)
        {
            //melee is used when right mouse button is clicked
            if(Input.GetButtonDown("Fire2"))
            {
                //plays a sound effect and animation and hits anything in the attack range labelled with the enemy layer in unity
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);
                animator.SetTrigger("IsAttacking");
                GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().clip = swing;
                foreach (Collider2D enemyObject in enemiesToDamage)
                    {
                        OnTriggerEnter2D(enemyObject);
                    }

                attackSpeed = startAttackSpeed;
            }
        }
        else
        {
            attackSpeed -= Time.deltaTime;
        }
    }
    //this is used to call the respective decreaseHP methods based off of what is in the melee's hit range and what it hits
    void OnTriggerEnter2D(Collider2D coll)
    {
        string tagName = coll.gameObject.tag;

        if (tagName == "spawner")
        {
            coll.GetComponent<EnemySpawner>().DecreaseHP();
        }

        if (tagName == "enemy")
        {
            coll.GetComponent<EnemyCollisions>().DecreaseHP();
        }

        if (tagName == "boss")
        {
            coll.GetComponent<BossScript>().DecreaseBossHP();
        }
    }
    //draws a green line to show the melee's range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
