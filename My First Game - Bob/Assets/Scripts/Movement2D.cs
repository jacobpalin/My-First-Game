using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used the platformer stub's movement and the same code from enemy script to swap the player's direction they face
public class Movement2D : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public bool isGrounded;
    private SpriteRenderer sprite;
    public Animator animator;
    public static bool facingRight;

    float horizontalMove;

    public void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //allows the player to move horizontally at whatever the move speed is set to
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //changes the player's gameobject to face the direction that they are walking in
        if (Input.GetAxis("Horizontal") < 0)
        {
            facingRight = false;
            transform.localScale = new Vector2(-.6f, .6f);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            facingRight = true;
            transform.localScale = new Vector2(.6f, .6f);
        }
        Landed();
        Jump();
    }
    //if the player is grounded, the jumping animation will not play
    void Landed()
    {
        if(isGrounded == true)
        {
            animator.SetBool("IsJump", false);
        }
    }
    //if the player presses the jump button (space) and is grounded, the player will jump into the air and a jump animation will play
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(0f, jumpForce));
            animator.SetBool("IsJump", true);
        }
    }
}