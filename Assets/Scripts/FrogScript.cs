using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;

    public Transform leftPoint, rightPoint;
    public float speed,jumpForce;

    public LayerMask ground;
    private bool faceLeft = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Movement();动画中插入时间来执行，所以此段注释掉
        AnimaSwitch();
    }

    void Movement()
    {
        if (coll.IsTouchingLayers(ground))
        {
            if (transform.position.x < leftPoint.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
            else if (transform.position.x > rightPoint.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
            if (faceLeft)
            {
                rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, jumpForce * Time.fixedDeltaTime);
            }
            else
            {
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime, jumpForce * Time.fixedDeltaTime);
            }
            anima.SetBool("jumping", true);
            anima.SetBool("idle", false);
        }
    }
    void AnimaSwitch()
    {
        if (anima.GetBool("jumping") && rb.velocity.y < 0.1f)
        {
            anima.SetBool("jumping", false);
            anima.SetBool("falling", true);
        }
        if (coll.IsTouchingLayers(ground)&&anima.GetBool("falling"))
        {
            anima.SetBool("falling",false);
            anima.SetBool("idle",true);
        }
    }
}
