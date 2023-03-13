using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//combined movement of platformer and shooter to make the road level movement since it is top-down
public class LevelThreeMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this allows the player to move vertically on the road part of the game
        Vector3 movementVerti = new Vector3(0f, Input.GetAxis("Vertical"));
        transform.position += movementVerti * Time.deltaTime * moveSpeed;

        //this allows the player to move horizontally on the road part of the game
        Vector3 movementHoriz = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movementHoriz * Time.deltaTime * moveSpeed;
    }
}
