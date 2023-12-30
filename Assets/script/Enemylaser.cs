using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemylaser : MonoBehaviour
{
    public float _speed = 0.2f;
    // Start is called before the first frame update
    float startt_y = 0;
    void Start()
    {
        startt_y = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(_speed*Vector3.down*0.2f);
       
        //Debug.Log(transform.localPosition.y);
        if (transform.localPosition.y < startt_y - 8f)
        {  
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
