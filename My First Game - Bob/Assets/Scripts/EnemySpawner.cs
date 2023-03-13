using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this is basically a copy of the enemyCollisions script with a few extra lines that I threw in to make the spawner work after trial and error of using different pieces of code that I learned about in the book
public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Spawner;
    public float LOS;
    public float spawnRate = 10f;
    public float nextSpawn;
    private Transform player;
    public int spawnerHP = 5;
    int score;

    public GameObject explosion;
    public AudioClip hit;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        //spawns enemies if the player is in the spawner's line of sight and the spawn timer is ready
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < LOS && nextSpawn < Time.time)
        {
            Instantiate(Enemy, Spawner.transform.position, Quaternion.identity);
            nextSpawn = Time.time + spawnRate;
        }
    }
    //decreases the spawner's hp if it is hit and plays a sound effect
    public void DecreaseHP()
    {
        spawnerHP--;
        Debug.Log("spawner hp = " + spawnerHP);
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().clip = hit;
        if (spawnerHP <= 0)
        {
            spawnerDead();
        }
    }
    //destroys spawner, updates score, spawns an explosion object in place of the spawner that is destroyed after it finishes playing
    void spawnerDead()
    {
        score = PlayerPrefs.GetInt("score");
        score = score + 100;
        PlayerPrefs.SetInt("score", score);

        GameObject exp = (GameObject)(Instantiate(explosion, transform.position, Quaternion.identity));
        Destroy(exp, 2f);

        Destroy(gameObject);

        updateScore();
    }
    //hurts spawner if a player bullet collides with it and destroys bullet
    void OnTriggerEnter2D(Collider2D coll)
    {
        string tagName = coll.gameObject.tag;

        if (tagName == "playerBullet")
        {
            if (spawnerHP > 0)
            {
                Destroy(coll.gameObject);
                DecreaseHP();
            }
        }
    }
    //updates the score
    public void updateScore()
    {
        score = PlayerPrefs.GetInt("score");
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
    }
    //draws a green line for the spawner's line of sight
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LOS);
    }
}
