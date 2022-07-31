using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellaMovement : MonoBehaviour
{
    //movimentacao
    public Rigidbody2D rb;
    public int moveSpeed;
    private float direction;

    //flip
    private Vector3 facingRight;
    private Vector3 facingLeft;

    //limitador dde jump
    public bool taNoChao;
    public Transform detectaChao;
    public LayerMask oQueEhChao;

    //pulo duplo
    public int pulosExtras = 1;

    void Start()
    {
        //flip
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        
        //movimentacao
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
       //limitador dde jump
       taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhChao);
       
       //jump
       if(Input.GetButtonDown("Jump") && taNoChao == true)
       {
         rb.velocity = Vector2.up * 12;
       }
       //pulo duplo
       if(Input.GetButtonDown("Jump") && taNoChao == false && pulosExtras > 0)
       {
         rb.velocity = Vector2.up * 12;
         pulosExtras--;
       }
       //pulo duplo
       if(taNoChao)
       {
        pulosExtras = 1;
       }

       //movimentacao
        direction = Input.GetAxis("Horizontal");

        //flip
        if(direction > 0)
        {
          //olhando para a direita
          transform.localScale = facingRight;
        }
        if(direction < 0)
        {
          //olhando para a esquerda
          transform.localScale = facingLeft;
        }

        //movimentacao
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }
}
