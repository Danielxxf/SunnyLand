﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enterDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enterDialog.SetActive(false);
    }
}
