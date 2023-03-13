using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//copied from book and repurposed
public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //initializes the starting ammo health and score for the player upon game startup
        PlayerPrefs.SetInt("ammo", 0);

        PlayerPrefs.SetInt("health", 3);

        PlayerPrefs.SetInt("score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
