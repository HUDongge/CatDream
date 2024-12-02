using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


    public class PlayerController : SingletonMono<PlayerController>
{
        public float moveSpeed = 5f;
        public float jumpHeight = 6f;
        public float movement;

        private bool isGrounded=true;
        private bool facingLeft = false;
        private Rigidbody2D rb;
        public float deathDelay = 1f;     // 死亡后的延迟时间
        private Vector3 lastCollisionPoint;  // 用于保存玩家死前接触的碰撞体位置

        public static event Action<Vector3> OnPlayerDeath;  // 事件声明
        public LayerMask groundLayer;
        private Animator anim;
       
        public ScenesManager scenesManager;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
           lastCollisionPoint = transform.position;
           // Debug.Log(lastCollisionPoint);
            anim = GetComponent<Animator>();
            ScenesManager.Instance.playerInScene = gameObject.scene.name;
      
    }

        void Update()
        {

             movement = Input.GetAxis("Horizontal");
          // movement = Input.GetAxisRaw("Horizontal");
             filp();
             transform.position += new Vector3(movement * moveSpeed * Time.deltaTime, 0f, 0f);

            if ((Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.W)) && isGrounded)
            {               
                 Jump();
                 anim.SetBool("jump_up", true);
                 isGrounded = false;
            }   
            if(isGrounded)
            {
                anim.SetBool("jump_up", false);
            }

            if (scenesManager == null) 
            {
                scenesManager =  FindObjectOfType<ScenesManager>();
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
            if(Input.GetKey(KeyCode.A) && facingLeft)
            {
                transform.eulerAngles = new Vector3(0f,-180f,0f);
                facingLeft = false;
            }
            else if(Input.GetKey(KeyCode.D) && facingLeft ==false)
            {
                transform.eulerAngles = new Vector3(0f,0f,0f);
                facingLeft = true;

            }
        }

  

        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.collider.tag == "Water")
            {
                Die();
            }
            if (other.collider.tag == "Ground")
            {
               isGrounded = true;            
            //  lastCollisionPoint = other.contacts[0].point;  // 记录第一个接触点的位置

            }
        }


      /*  private void OnCollisionExit2D(Collision2D other)
        {
        if (other.collider.tag == "Ground")
        {
            isGrounded = false;  // 离开地面时，设置 isGrounded 为 false
        }
        }*/

    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log(scenesManager.currentActiveScene);
        int currentSceneIndex = GridSelector.GetIndexByName(gameObject.scene.name);
        Debug.Log($"playerController脚本所在场景名字:{gameObject.scene.name}");
        Debug.Log($"OnTriggerEnter2D当前玩家所在九宫格号:{currentSceneIndex}");
     
        if (other.CompareTag("RightBlock"))   //从右边走的话，看下一个场景的左边是否有通道
        {
            Debug.Log("RightBlock");
            ScenesManager.Instance.SwitchScene(currentSceneIndex, "right");
            
        }
        else if (other.CompareTag("LeftBlock"))
        {
            Debug.Log("LeftBlock");
            ScenesManager.Instance.SwitchScene(currentSceneIndex, "left");
        }
        else if (other.CompareTag("DownBlock"))
        {
            Debug.Log("DownBlock");
            ScenesManager.Instance.SwitchScene(currentSceneIndex, "down");
        }

    }

        void Die()
        {
            OnPlayerDeath.Invoke(lastCollisionPoint);  // 传递玩家死前位置
            anim.SetBool("IsDead", true);
            Destroy(gameObject.transform.parent.gameObject);
        }

    }
