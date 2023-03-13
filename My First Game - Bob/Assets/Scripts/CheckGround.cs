using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//copied from platformer stub
public class CheckGround : MonoBehaviour
{
    GameObject Player;

    private void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }
    //checks if the player is standing on a gameobject with the tags ground or trap to make isGrounded true which is used in another script to allow the player to jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "trap")
        {
            Player.GetComponent<Movement2D>().isGrounded = true;
        }
    }
    //if the player is not touching a gameobject with the ground or trap tags or is in the air then the player will not be able to jump
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "trap")
        {
            Player.GetComponent<Movement2D>().isGrounded = false;
        }
    }
}
