using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this is mostly code from the book that I repurposed for my game and a little bit of my own thoughts to make things work properly
public class EnemyCollisions : MonoBehaviour
{
    public int enemyHP = 2;
    int score;

    Animator animator;
    public AudioClip hit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //decreases enemy's hp and plays a sound effect and if the hp is 0 then the death method is called
    public void DecreaseHP()
    {
        enemyHP--;
        Debug.Log("enemy hp = " + enemyHP);
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().clip = hit;
        if (enemyHP <= 0)
        {
            enemyDead();
        }
    }
    //updates player's score, plays enemy death animation, disables the enemy's ability to move and shoot while the animation is being played, destroys the object after animation is done (player can still take damage if they step on the enemy which wasn't intended but works out in the end because the enemy is supposed to be a glob of toxic waste and would burn the player like acid)
    void enemyDead()
    {
        score = PlayerPrefs.GetInt("score");
        score = score + 50;
        PlayerPrefs.SetInt("score", score);

        animator.SetTrigger("isDead");

        GetComponent<Enemy>().shootingRange = 0f;
        GetComponent<Enemy>().LOS = 0f;

        Destroy(gameObject, 2f);

        updateScore();
    }
    //hurts the enemy if a player bullet collides with it and destroys the bullet
    void OnTriggerEnter2D(Collider2D coll)
    {
        string tagName = coll.gameObject.tag;

        if (tagName == "playerBullet")
        {
            if (enemyHP > 0)
            {
                Destroy(coll.gameObject);
                DecreaseHP();
            }
        }
    }
    //updates player's score
    public void updateScore()
    {
        score = PlayerPrefs.GetInt("score");
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
    }
}
