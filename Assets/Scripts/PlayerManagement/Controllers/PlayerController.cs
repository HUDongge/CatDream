using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerManagement._Controllers
{

    public class PlayerController : MonoBehaviour, IController
    {
        public float moveSpeed = 5f;
        public float jumpForce = 3f;
        private bool isGrounded;

        private Rigidbody2D rb;
        private Animator animator;

        public LayerMask groundLayer;

        public IArchitecture GetArchitecture()
        {
            return MainGameArchitecture.Interface;
        }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        //    animator = GetComponent<Animator>();
        }

        void Update()
        {
           
            Move();

            Jump();
         
            isGrounded = CheckGrounded();
        }

        void Move()
        {
            float moveInput = 0;

            if (Input.GetKey(KeyCode.A))  // 左
                moveInput = -1f;
            else if (Input.GetKey(KeyCode.D)) // 右
                moveInput = 1f;

            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        
        }

        void Jump()
        {
            // 跳跃
            if ((Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        bool CheckGrounded()
        {          
            return Physics2D.OverlapCircle(transform.position - new Vector3(0, 0.5f, 0), 0.1f, groundLayer);
        }

        // 碰撞触发事件：水死亡
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Water"))
            {
                Die();
            }

            // 碰撞拾取毛线球
            if (other.CompareTag("TargetItem"))
            {
                PickUpTargetItem(other.gameObject);
            }
        }

        void Die()
        {
            // 处理死亡逻辑
            Debug.Log("Player died in water");
           
        }

        void PickUpTargetItem(GameObject targetItem)
        {
            // 处理拾取逻辑
            Debug.Log("target picked up!");

            Destroy(targetItem); 
        }


    }
}