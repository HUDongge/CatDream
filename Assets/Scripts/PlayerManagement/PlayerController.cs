using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerManagement._Controllers
{

    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpHeight = 6f;
        public float movement;
        public List<Image> collections;

        private bool isGrounded=true;
        private bool facingLeft = true;
        private Rigidbody2D rb;
        private Animator animator;
        public float deathDelay = 3f;     // 死亡后的延迟时间

        [SerializeField]
        private int currentCount=0;



       

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        //    animator = GetComponent<Animator>();
        }

        void Update()
        {
           
            movement = Input.GetAxis("Horizontal");
            filp();
            transform.position += new Vector3(movement * moveSpeed * Time.deltaTime, 0f, 0f);

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Jump();
                isGrounded = false;
            }

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag == "Ground")
            {
                isGrounded = true;
            }
            if (other.collider.tag == "Water")
            {
                Die();
            }
        }
        // 碰撞触发事件：水死亡
        private void OnTriggerEnter2D(Collider2D other)
        {
            
            // 碰撞拾取宝石
            if (other.tag == "Gemstone")
            {
                PickUpTargetItem(other.gameObject);
            }
        }

        void Die()
        {
          
            Destroy(this.gameObject);
          //  StartCoroutine(HandleDeath());
            Debug.Log("Player is die in water");
           
        }
       
       /* IEnumerator HandleDeath()
        {    
            yield return new WaitForSeconds(deathDelay);
            Instantiate(playerPrefab, lastSafePosition, Quaternion.identity);            

           
        }*/
        void PickUpTargetItem(GameObject gemstone)
        {
            currentCount++;
            collections[currentCount - 1].color = Color.yellow;
            Destroy(gemstone); 
        }


    }
}