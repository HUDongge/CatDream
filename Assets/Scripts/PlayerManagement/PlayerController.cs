using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpHeight = 6f;
        public float movement;

        private bool isGrounded=true;
        private bool facingLeft = true;
        private Rigidbody2D rb;
        private Animator animator;
        public float deathDelay = 3f;     // 死亡后的延迟时间
        private Vector3 lastCollisionPoint;  // 用于保存玩家死前接触的碰撞体位置

        public static event Action<Vector3> OnPlayerDeath;  // 事件声明
        public LayerMask groundLayer;



    void Start()
        {
            rb = GetComponent<Rigidbody2D>();
           lastCollisionPoint = transform.position; 
        //    animator = GetComponent<Animator>();
        }

        void Update()
        {
            
            movement = Input.GetAxis("Horizontal");
            filp();
            transform.position += new Vector3(movement * moveSpeed * Time.deltaTime, 0f, 0f);

            if ((Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.W)) && isGrounded)
            {
                Jump();
                isGrounded = false;
            }
        
           // CheckGroundPosition();

    }

       

        void Jump()
        {
            Vector2 velocity = rb.velocity;
            velocity.y = jumpHeight;
            rb.velocity = velocity;

        }

        void filp()
        {
            if(movement>0f && facingLeft)
            {
                transform.eulerAngles = new Vector3(0f,-180f,0f);
                facingLeft = false;
            }
            else if(movement<0f && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0f,0f,0f);
                facingLeft = true;

            }
        }

  /*  private void CheckGroundPosition()
    {
        // 从玩家发射射线向下检测地面  
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        if (hit.collider != null)
        {
            lastCollisionPoint = hit.point; // 记录最近的地面位置  
        }
    }*/

        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.collider.tag == "Water")
            {
                Die();
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
          //  lastCollisionPoint = other.contacts[0].point;  // 记录第一个接触点的位置
                                                                                                      // 
        }
    }



        void Die()
        {
            OnPlayerDeath.Invoke(lastCollisionPoint);  // 传递玩家死前位置
            Destroy(this.gameObject);
        }

    }
