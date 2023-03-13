using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//copied from book and repurposed
public class KeepGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //keeps the player's ui across levels/scenes
        int nbUIs = GameObject.FindGameObjectsWithTag("player_ui").Length;
        if (FindObjectsOfType(GetType()).Length > 1) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
