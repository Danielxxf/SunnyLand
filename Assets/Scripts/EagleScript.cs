using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EagleScript : Enemy
{
    private Rigidbody2D rb;

    private bool faceUp = true;
    private float speed=200;

    public Transform topPoint, bottomPoint;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        if (faceUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed * Time.fixedDeltaTime);
            if (rb.position.y>topPoint.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed * Time.fixedDeltaTime);
                faceUp = false;
            }
        }
        else
        {
            if (rb.position.y < bottomPoint.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed * Time.fixedDeltaTime);
                faceUp = true;
            }
        }
    }
}
