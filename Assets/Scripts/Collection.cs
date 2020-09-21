using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
    public Text cherryNum;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CherryAdd()
    {
        FindObjectOfType<PlayerController>().CherryCount();
        SoundMananger.instance.CherryAudio();
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
