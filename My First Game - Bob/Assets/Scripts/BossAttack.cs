using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=sPiVz1k-fEs&t=422s&ab_channel=Brackeys
//used this as reference for melee part and then I did my own part for the range attack based off of the video
public class BossAttack : MonoBehaviour
{
    public float hitRange = 4f;
    public LayerMask attackMask;
    public Transform attackPoint;

    public GameObject spellCast;
    public GameObject spellHitBox;
    public GameObject spellCastPoint;
    public GameObject castFinished;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MeleeAttack()
    {
        //converts the gameobject used for the attack's hit range into a variable to allow for detecting if the player should get hit if they are in that range and then decreases the player's health if they are hit
        Collider2D damagePlayer = Physics2D.OverlapCircle(attackPoint.position, hitRange, attackMask);
        if(damagePlayer != null)
        {
            damagePlayer.GetComponent<Collisions>().DecreaseHealthFromBoss();
        }
    }

    public void RangeAttack()
    {
        //this is called by an event in the boss casting animation and spawns the attack above the player on a gameobject connected to them then destroys the attack after it is finished with it's animation
        GameObject castAnimationClone = Instantiate(spellCast, spellCastPoint.transform.position, Quaternion.identity);
        GameObject HitBoxClone = Instantiate(spellHitBox, spellCastPoint.transform.position, Quaternion.identity);
        Destroy(castAnimationClone, 1.6f);
        Destroy(HitBoxClone, 1.6f);
    }

    public void RangeAttackFinished()
    {
        //this is called by an event at the end of the boss casting animation that spawns an invisible object that is used in an if statement to determine if the boss's ranged attack should be casted again
        GameObject FinishedCastingClone = Instantiate(castFinished, spellCastPoint.transform.position, Quaternion.identity);
        Destroy(FinishedCastingClone, 10f);
    }
    //displays green lines when selecting the right object that shows the hitrange
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, hitRange);
    }
}
