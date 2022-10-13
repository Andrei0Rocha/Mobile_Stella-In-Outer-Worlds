using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject TrailPrefab;
    public float Speed, DashForce = 8;
    public float JumpForce;
    private Rigidbody2D rig;
    public bool isJumping;
    public bool doubleJump;
    private Animator anim;
    bool CanDash = true;
    GameObject trail;

    // Start is called before the first frame update
    void Start()
    {
       rig = GetComponent<Rigidbody2D>(); 
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        if (trail)
        {
            trail.transform.position = transform.position;
        }
    }

    IEnumerator CooldownDash()
    {
        trail = Instantiate(TrailPrefab, transform.position, Quaternion.identity);
        trail.GetComponent<TrailRenderer>().emitting = true;
        CanDash = false;
        yield return new WaitForSeconds(2);
        trail.GetComponent<TrailRenderer>().emitting = false;
        Destroy(trail);
        CanDash = true;
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        
        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3 (0f,0f,0f);
        }
        
        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3 (0f,180f,0f);
        }

        if (Input.GetAxis("Horizontal") ==  0f)
        {
            anim.SetBool("walk", false); 
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            Vector2 DashPosition = transform.right*DashForce;

            rig.AddForce(DashPosition, ForceMode2D.Impulse);
            StartCoroutine("CooldownDash");
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        if(!isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            doubleJump = true;
            anim.SetBool("jump", true);
        }
        else
        if (doubleJump)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            doubleJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if (collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 10)
        {
            GameController.instance.Shownextlvl();
        }
    }

        void OnCollisionExit2D(Collision2D collision)
    {
         if (collision.gameObject.layer == 8)
         {
            isJumping = true;
         }
    }
}
