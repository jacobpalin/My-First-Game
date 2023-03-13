using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//copied from book and repurposed
public class Buttons : MonoBehaviour
{
    //starts game if player clicks on play on the main menu
    public void startIntro()
    {
        SceneManager.LoadScene("Intro Sequence");
    }
    //called by animation event in intro sequence at the end that starts the game
    public void startLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    //closes the game if quit is clicked on the main menu
    public void quitApp()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    //goes back to the main menu from the win/lose scenes
    public void mainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        //removes the player's ui from the screen on the win and lose scenes which i may remove the score part so the player can see their final score after the game is over
        if (SceneManager.GetActiveScene().name == "Win" || SceneManager.GetActiveScene().name == "Lose")
        {
            GameObject.Find("ammoUI").GetComponent<Text>().text = "";
            GameObject.Find("healthUI").GetComponent<Text>().text = "";
            GameObject.Find("scoreUI").GetComponent<Text>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
