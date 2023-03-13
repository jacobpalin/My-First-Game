using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//used what I learned from the book
public class BossScript : MonoBehaviour
{
    public int bossHP = 50;
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
    //decreases the boss's hp and plays a sound effect to indicate it has been hit and if the boss's hp is at 0 after being hit then it will call the bossDead method
    public void DecreaseBossHP()
    {
        bossHP--;
        Debug.Log("boss hp = " + bossHP);
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().clip = hit;
        if (bossHP <= 0)
        {
            bossDead();
        }
    }
    //updates the score after killing the boss and plays the boss dying animation which triggers an event at the end of the animation to load the winning scene
    void bossDead()
    {
        score = PlayerPrefs.GetInt("score");
        score = score + 1000;
        PlayerPrefs.SetInt("score", score);
        animator.SetTrigger("IsDead");
        updateScore();
    }
    //called by the boss death animation to load win scene or, if I have too much time on my hands, an ending animation of player slicing through boss with sword
    public void WinningScreenLoad()
    {
        SceneManager.LoadScene("Win");
    }
    //stores the player's score in a variable called score that allows it to be updated
    public void updateScore()
    {
        score = PlayerPrefs.GetInt("score");
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
    }
    //calls the decreaseBossHP method above if a gameobject with the tag playerBullet collides with the boss and destroys the bullet
    void OnTriggerEnter2D(Collider2D coll)
    {
        string tagName = coll.gameObject.tag;

        if (tagName == "playerBullet")
        {
            if (bossHP > 0)
            {
                Destroy(coll.gameObject);
                DecreaseBossHP();
            }
        }
    }
}
