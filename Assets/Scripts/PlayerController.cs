using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator anima;
    [SerializeField]
    private bool isGround,isHurt,isJump,jumpPressed;
    private int jumpCount;

    public Collider2D coll;
    public Collider2D disColl;

    public LayerMask ground;    //地面
    public TextMeshProUGUI cherryNum;
    //public AudioSource jumpAudio,hurtAudio,cherryAudio,deathAudio;
    public Transform top,buttom;

    public float speed;
    public float jumpForce;
    public int cherryCount;

    // Start is called before the first frame update
    //void Start()
    //{
    //    rigidbody = GetComponent<Rigidbody2D>();
    //    anima = GetComponent<Animator>();
    //}

    //// Update is called once per frame
    //void FixedUpdate()  //FixedUpdate保证不同设备相同的效果
    //{
    //    if(!isHurt)Movement();
    //    SwitchAnim();
    //    isGround = Physics2D.OverlapCircle(buttom.position, 0.2f, ground);
    //    Jump();
    //}

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anima = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        Crouch();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(buttom.position, 0.1f, ground);
        Jump();
        if (!isHurt) GroundMovement();
        SwitchAnima();
    }

    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(horizontalMove * speed, rigidbody.velocity.y);
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && !isGround)
        {
            isJump = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnima()
    {
        anima.SetFloat("running", Mathf.Abs(rigidbody.velocity.x));

        if (isGround)
        {
            anima.SetBool("falling", false);
        }
        else if (rigidbody.velocity.y > 0)
        {
            anima.SetBool("jumping", true);
            anima.SetBool("falling", false);
        }
        else
        {
            anima.SetBool("jumping", false);
            anima.SetBool("falling", true);
        }
        if (isHurt)
        {
            if (Mathf.Abs(rigidbody.velocity.x) < 1f)
            {
                anima.SetBool("hurt", false);
                isHurt = false;
            }
        }
    }

    void Crouch()
    {
        if (Input.GetButton("Crouch")&&isGround)
        {
            anima.SetBool("crouch", true);
            disColl.enabled = false;
        }
        else if (!Physics2D.OverlapCircle(top.position,0.2f,ground))
        {
            anima.SetBool("crouch", false);
            disColl.enabled = true;
        }
    }

    public void CherryCount()
    {
        cherryNum.text = (++cherryCount).ToString();
    }

    void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collections")
        {
            collision.GetComponent<Animator>().Play("Collected");
        }
        else if(collision.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            SoundMananger.instance.DeathAudio();
            Invoke("ReStart", 2f); //两秒钟后执行名为ReStart的函数；
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(anima.GetBool("falling"))
            {
                enemy.JumpOn();
                Debug.Log("踩到啦！");
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                anima.SetBool("falling", false);
                anima.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rigidbody.velocity = new Vector2(-3, rigidbody.velocity.y);
                SoundMananger.instance.HurtAudio();
                anima.SetBool("hurt", true);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rigidbody.velocity = new Vector2(3, rigidbody.velocity.y);
                SoundMananger.instance.HurtAudio();
                anima.SetBool("hurt", true);
                isHurt = true;
            }
        }
    }

    //void Movement() //移动
    //{
    //    float horizontalMove = Input.GetAxis("Horizontal");
    //    float facedirection = Input.GetAxisRaw("Horizontal");

    //    rigidbody.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rigidbody.velocity.y);
    //    //* Time.deltaTimene能得到一个平滑、不跳帧的运动方式
    //    anima.SetFloat("running",Mathf.Abs(facedirection));

    //    if (facedirection != 0) //随着左右移动改变人物方向
    //    {
    //        transform.localScale = new Vector3(facedirection, 1, 1);
    //    }

    //    //if (Input.GetButtonDown("Jump")) //按空格跳跃
    //    //{
    //    //    if(coll.IsTouchingLayers(ground)) //如果人物接触到了地面
    //    //    {
    //    //        jumpAudio.Play(); //播放跳跃音效
    //    //        rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime); //施加向上跳的速度
    //    //        anima.SetBool("falling", false);
    //    //        anima.SetBool("jumping", true); //动画切换
    //    //    }
    //    //}
    //    Crouch();
    //}

    //void SwitchAnim()//动画控制
    //{
    //    if (rigidbody.velocity.y < 0.1 && !coll.IsTouchingLayers(ground))
    //    {
    //        anima.SetBool("jumping", false);
    //        anima.SetBool("falling", true);
    //    }
    //    else if (isHurt)
    //    {
    //        if (Mathf.Abs(rigidbody.velocity.x) < 0.01f)
    //        {
    //            anima.SetBool("hurt",false);
    //            isHurt = false;
    //        }
    //    }
    //    else if (coll.IsTouchingLayers(ground)){    //如果与ground有碰撞
    //        anima.SetBool("falling", false);
    //    }
    //}

    //void Jump()
    //{
    //    if (isGround)
    //    {
    //        extraJump = 2;
    //    }
    //    if (Input.GetButtonDown("Jump") && extraJump > 0)
    //    {
    //        SoundMananger.instance.JumpAudio(); //播放跳跃音效
    //        extraJump--;
    //        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce * Time.fixedDeltaTime); //施加向上跳的速度
    //        anima.SetBool("falling", false);
    //        anima.SetBool("jumping", true); //动画切换
    //        Debug.Log("正常跳！");
    //        Debug.Log(extraJump);
    //    }
    //    //if (Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
    //    //{
    //    //    jumpAudio.Play(); //播放跳跃音效
    //    //    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * Time.fixedDeltaTime); //施加向上跳的速度
    //    //    anima.SetBool("falling", false);
    //    //    anima.SetBool("jumping", true); //动画切换
    //    //    Debug.Log("莫名其妙的跳！");
    //    //}
    //}
}
