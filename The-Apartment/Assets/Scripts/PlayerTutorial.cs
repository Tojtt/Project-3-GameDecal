using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{

    public Rigidbody2D PlayerRB;
    float x_input;
    float y_input;
    public Animator anim;
    private AudioSource footstep_sound;
    Vector2 currDirection;
    public float movespeed;
    public bool move2D;
    public SpriteRenderer sr;

    public TutorialScript tutorial;

    // Start is called before the first frame update
    void Start()
    {
        footstep_sound = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (tutorial.canMove)
        {
            //get inputs from keyboard
            x_input = Input.GetAxisRaw("Horizontal");
            y_input = Input.GetAxisRaw("Vertical");

            // will run even if there's no gameState

            Move();

            if (!PlayerRB.velocity.Equals(Vector2.zero))
            {
                Debug.Log("Moving");
                if (!footstep_sound.isPlaying)
                {
                    footstep_sound.Play();
                }
            }
            else
            {
                Debug.Log("NOT Moving");
                footstep_sound.Stop();
            }
        }
    }

    private void Move()
    {

        //anim.SetBool("Moving", true);  
        if (x_input > 0)
        {
            //Debug.Log("input working right?");
            PlayerRB.velocity = Vector2.right * movespeed;
            currDirection = Vector2.right;
            anim.SetFloat("Speed", 1);
            sr.flipX = true;
        }
        else if (x_input < 0)
        {
            anim.SetFloat("Speed", 1);
            PlayerRB.velocity = Vector2.left * movespeed;
            currDirection = Vector2.left;
            sr.flipX = false;
        }
        /* else if (y_input > 0 && move2D)  //&& SceneManager.GetActiveScene().name != "HallwayLayout")
        {
            PlayerRB.velocity = Vector2.up * movespeed;
            currDirection = Vector2.up;
            anim.SetFloat("Speed", 1);
            sr.flipX = true;
        }
        else if (y_input < 0 && move2D)  //&& SceneManager.GetActiveScene().name != "HallwayLayout")
        {
            anim.SetFloat("Speed", 1);
            PlayerRB.velocity = Vector2.down * movespeed;
            currDirection = Vector2.down;
            sr.flipX = true;
        } */
        else
        {
            anim.SetFloat("Speed", 0);
            PlayerRB.velocity = Vector2.zero;
            //anim.SetBool("Moving", false);
        }
        //anim.SetFloat("DirX", currDirection.x);
        //anim.SetFloat("DirY", currDirection.y);

    }
}
