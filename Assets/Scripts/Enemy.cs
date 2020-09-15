using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anima;
    protected AudioSource audSou;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anima = GetComponent<Animator>();
        audSou = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        audSou.Play();
        anima.SetTrigger("death");
    }
}
