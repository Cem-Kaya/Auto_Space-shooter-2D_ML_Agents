using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class MyScript : MonoBehaviour
{
    [SerializeField]    
    private float _speed = 8f;
    public float _speedMultiplier = 2f;    
    [SerializeField] private GameObject _laserPrefab;
    public GameObject _tripleShotPrefab;
    public float _fireRate = 0.05f;
    public float _canfire = -1f;
    public int _lives = 3;
    public SpawnManager _spawnManager;
    public bool _isTripleShotActive = false;
    public bool _isSpeedBoostActive = false;
    public bool _isShieldActive = false;
    public GameObject shieldvisualizer;
    private int _score = 0;
    public TextMeshProUGUI text;
    public UI_Manager _uimanager;
    public GameManager _gameManager;
    public GameObject _leftEngine, _rightEngine;
    public bool left_thrust = false;
    public AudioClip _lasersound;
    public AudioSource _audioSource;
    public AudioClip _audioexplosion;
    public GameObject _enemylaser;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();
       
        if (_spawnManager == null)
        {
            Debug.LogError("The spawn Manager is NULL.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("Audiosource on the playerr  is null !");
        }
        else
        {
            _audioSource.clip = _lasersound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            FireLaser();
        }
            
      
    }
    void CalculateMovement()
    {
        float horizontaLInput = Input.GetAxis("Horizontal");
        float verticaLInput = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.left * horizontalInput * Time.deltaTime * _speed);
        // transform.Translate(Vector3.up * verticalInput * Time.deltaTime * _speed);
        //  transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        // if player position on the y is greater then 0
        // y position = 0
        // else if  position  on the y is less then  -3.8f
        // y position  = -3.8f
        Vector3 direction = new Vector3(horizontaLInput, verticaLInput, 0);
       
        // else speed boost multiplier 
        transform.Translate(direction * _speed * Time.deltaTime);
         
      if (transform.position.y >= 0)
      {
        transform.position = new Vector3(transform.position.x, 0, 0);
      }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        //if player on the x > 11
        //x po = 11
        //else if player on the x is less then -11
        //x pos = 11

        if (transform.position.x > 8.3f)
        {
            transform.position = new Vector3(-8.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.3f)
        {
            transform.position = new Vector3(8.3f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        //  if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        // Debug.Log("Space Key pressed");
        _canfire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + new  Vector3(-2.40f, 1.09f, 0) , Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.09f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }
   
    public void Damage()
    {
        // return;
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            shieldvisualizer.SetActive(false);
            return;
        }

        _lives--;
        

        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
            
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        _uimanager.SetImageAtIndex(_lives);
        _uimanager.UpdateLives(_lives);
        
        if (_lives < 1)
        {   
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
       
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
   
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    } 

    public void ShieldsActive()
    {
        _isShieldActive = true;
        shieldvisualizer.SetActive(true);
    }
    
    public void AddScore(int points)
    {
        _score += points;
        text.text = "score: " + _score.ToString() ;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag +"  " +collision.gameObject.name );
        if (collision.gameObject.tag == "Enemylaser" )
        {
            //Debug.Log("Collider is working EL");
            Damage();
        }
        //Debug.Log("Collider is working EL");

    }
}