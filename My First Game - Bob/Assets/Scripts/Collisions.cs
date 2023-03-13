using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//copied from book and repurposed for my game
public class Collisions : MonoBehaviour
{
    int score, health, ammo;
    public Animator animator;
    public AudioClip collect, hurt;

    // Start is called before the first frame update
    void Start()
    {
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string tagName = coll.collider.gameObject.tag;
        //increases player's ammo count and score on the ui and plays sound effect and destroys the item
        if(tagName == "ammoPickup")
        {
            Destroy(coll.collider.gameObject);

            ammo = PlayerPrefs.GetInt("ammo");
            ammo = ammo + 10;
            PlayerPrefs.SetInt("ammo", ammo);

            score = PlayerPrefs.GetInt("score");
            score = score + 5;
            PlayerPrefs.SetInt("score", score);

            updateUI();
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().clip = collect;
        }
        //increases player's health count and score on the ui and plays sound effect and destroys the item
        if (tagName == "healthPickup")
        {
            Destroy(coll.collider.gameObject);

            health = PlayerPrefs.GetInt("health");
            health = health + 1;
            PlayerPrefs.SetInt("health", health);

            score = PlayerPrefs.GetInt("score");
            score = score + 5;
            PlayerPrefs.SetInt("score", score);

            updateUI();
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().clip = collect;
        }
        //decreases player's hp if they collide with a trap or enemy
        if(tagName == "trap" || tagName == "enemy")
        {
            DecreaseHealth();
        }
        //loads next scene if player collides with the end of the current scene
        if (tagName == "endOfLevel1")
        {
            SceneManager.LoadScene("Level 2");
        }
        //loads next scene if player collides with the end of the current scene
        if (tagName == "endOfLevel2")
        {
            SceneManager.LoadScene("Level 3");
        }
        //loads next scene if player collides with the end of the current scene
        if (tagName == "endOfLevel3")
        {
            SceneManager.LoadScene("Level 4");
        }
        //loads next scene if player collides with the end of the current scene
        if (tagName == "endOfLevel4")
        {
            SceneManager.LoadScene("Level 5");
        }

        updateUI();
    }
    //updates the player's ui
    public void updateUI()
    {
        score = PlayerPrefs.GetInt("score");
        health = PlayerPrefs.GetInt("health");
        ammo = PlayerPrefs.GetInt("ammo");
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
        GameObject.Find("healthUI").GetComponent<Text>().text = "Health: " + health;
        GameObject.Find("ammoUI").GetComponent<Text>().text = "Ammo: " + ammo;
    }
    //decreases the players health by 1 and plays a sound effect, if the player's health reaches 0 then they die and the lose scene is loaded
    public void DecreaseHealth()
    {
        int health = PlayerPrefs.GetInt("health");

        health--;

        PlayerPrefs.SetInt("health", health);

        if (health <= 0)
        {
            animator.SetTrigger("IsDead");

            SceneManager.LoadScene("Lose");
        }
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().clip = hurt;
        print("health: " + health);
    }
    //same thing as above except decreases by 3 because the player is getting hit by the boss which does more damage
    public void DecreaseHealthFromBoss()
    {
        int health = PlayerPrefs.GetInt("health");

        health = health - 3;

        PlayerPrefs.SetInt("health", health);

        if (health <= 0)
        {
            animator.SetTrigger("IsDead");
            SceneManager.LoadScene("Lose");
        }
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().clip = hurt;
        print("health: " + health);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        string tagName = coll.gameObject.tag;
        //if an enemy bullet hits the player, they lose health and that bullet is destroyed
        if (tagName == "enemyBullet")
        {
            Destroy(coll.gameObject);
            DecreaseHealth();
            updateUI();
        }
        //these portals are used in the boss arena to allow the player to move from the top and bottom of the arena if they choose to
        if (coll.gameObject.name == "portalEntrance1")
        {
            transform.position = GameObject.Find("portalExit1").transform.position;
        }
        if (coll.gameObject.name == "portalEntrance2")
        {
            transform.position = GameObject.Find("portalExit2").transform.position;
        }
        if (coll.gameObject.name == "portalEntrance3")
        {
            transform.position = GameObject.Find("portalExit3").transform.position;
        }
        if (coll.gameObject.name == "portalEntrance4")
        {
            transform.position = GameObject.Find("portalExit4").transform.position;
        }

        updateUI();
    }
}