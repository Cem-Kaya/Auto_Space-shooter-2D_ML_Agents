using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    // speed variable of 8
    [SerializeField]
    private float _speed = 8.0f;
    // Start is called before the first frame update
    float startt_y = 0;
    void Start()
    {
        startt_y = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // translate laser up
        transform.Translate(_speed * Time.deltaTime * Vector3.up);
        // if laser position is greater than 8 y
        // destroy the object
        // Debug.Log(transform.localPosition.y); not spawned in the parent 
        if (transform.localPosition.y > 8f+ startt_y)
        {
            // check if this  object has a parent
            // destroy parent too
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}
