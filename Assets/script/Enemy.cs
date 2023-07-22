using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float _speed = 3f;
    public MyScript _player;
    public Animator _anim;
    public bool playinganim_ = false;
    public AudioSource _audioSource;
    public GameObject _fire;
    public float fireRate = 1.0f;
    public float downspeed = 1.0f;
    public bool _stop = false;
    // Start is called before the first frame update

    
    void Start()
    {
        StartCoroutine(Laser());
        _player = transform.parent.parent.Find("Agent"). GetComponent<MyScript> ();
        _audioSource = GetComponent<AudioSource> ();

        if(_player == null)
        {
            Debug.LogError("_player is Null");
        }

       _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("The animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        CalculateMovement();
    }
    public void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            float randomx = Random.Range(-7f, 7f);
            transform.position = new Vector3(randomx, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Hit :" + other.transform.name);

      
        if (other.tag == "Cube")
        {

          MyScript Cube =  other.transform.GetComponent<MyScript>();

            if( Cube != null)
            {
                Cube.Damage();
            }

            _anim.SetBool("OnEnemyDeath",true);
            _speed = 0;
            _audioSource.Play();
            Enemydeath();
            Destroy(this.gameObject,2.8f);
        }

        if(other.tag == "Laser")
        {
         
            if(_player != null)
            {
                _player.AddScore(10 );
            }
            _anim.SetBool("OnEnemyDeath", true);
            Destroy(other.gameObject);
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Enemydeath();
            Destroy(this.gameObject,2.8f);
        }
    }
    IEnumerator Laser()
    {
        while (_stop == false)
        {
           
            Vector3 spawnOffset = new Vector3(1.3f,-2,0); 
            GameObject fireobject = Instantiate(_fire, transform.position + spawnOffset, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void Enemydeath()
    {
        _stop = true;
    }
    

    
}
