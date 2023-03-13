using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=Xq-rypmsTaA&ab_channel=TechnicalFriends
//link to youtube video where I got most of this code from
public class Shoot : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Gunshot;
    public Transform BulletSpawner;
    public float bulletSpeed = 50;
    public GameObject player;

    int ammo;
    Vector2 bulletDirection;
    float bulletAngle;

    Vector3 mouseDistance;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this line is from peer mentors along with most of the line in the first 'if' below
        mouseDistance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;

        ammo = PlayerPrefs.GetInt("ammo");
        //collects the necessary information to determine the direction that a bullet should go and sets it equal to a variable, same for the angle
        bulletDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        bulletAngle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;

        //allows bullet to go in direction of mouse
        BulletSpawner.rotation = Quaternion.Euler(0, 0, bulletAngle);
        //if the player has ammo and the mouse is in front of the player and they press the left mouse button, a bullet will be spawned at a gameobject connected to the player and move in the direction of the mouse cursor on screen. an animation will be played with a sound effect to make it seem more realistic like a gun has been fired. the bullet will despawn after a bit. the player's ammo count in the ui will also decrease by 1
        if ((ammo > 0 && mouseDistance.x > 0 && Movement2D.facingRight) || (ammo > 0 && mouseDistance.x < 0 && !Movement2D.facingRight))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject bulletClone = Instantiate(Bullet);
                GameObject GunshotAnimation = Instantiate(Gunshot);

                GunshotAnimation.transform.position = BulletSpawner.position;

                if (Input.GetAxis("Horizontal") < 0)
                {
                    GunshotAnimation.transform.localScale = new Vector2(-1f, 1f);
                }
                else if (Input.GetAxis("Horizontal") > 0)
                {
                    GunshotAnimation.transform.localScale = new Vector2(1f, 1f);
                }

                bulletClone.transform.position = BulletSpawner.position;
                bulletClone.transform.rotation = Quaternion.Euler(0, 0, bulletAngle);

                bulletClone.GetComponent<Rigidbody2D>().velocity = BulletSpawner.right * bulletSpeed;

                Destroy(bulletClone.gameObject, 2);
                Destroy(GunshotAnimation.gameObject, .05f);

                ammo--;
                PlayerPrefs.SetInt("ammo", ammo);
                GameObject.Find("ammoUI").GetComponent<Text>().text = "Ammo: " + ammo;
            }
        }
    }
}