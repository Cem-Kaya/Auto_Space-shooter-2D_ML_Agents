using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float _speed = 3.0f;
    public AudioSource _audioSource;
    public AudioClip _clip;
    // ID for Powerups
    // 0 = Triple shot
    // 1 = Speed
    // 2 = Shields

    public int powerupID;
    Vector3 start_pos;
    // Start is called before the first frame update
    void Start()
    {
        start_pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < start_pos.y - 15f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D OTHER)
    {
        if (OTHER.tag == "Cube")
        {
            MyScript Cube = OTHER.transform.GetComponent<MyScript>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (Cube != null)
            { 
                switch(powerupID)
                {
                    case 0:
                        Cube.TripleShotActive();
                        break;
                    case 1:
                        Cube.SpeedBoostActive();
                        break;
                    case 2:
                        Cube.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }
            }
            Destroy(this.gameObject);
           
        }
    }
}