using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    public float _rotateSpeed = 3.0f;
    public GameObject _explosionPrefab;
    public SpawnManager _spawnmanager;
    // Start is called before the first frame update
    void Start()
    {
        
        //
    }

    public void start()
    {
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*_rotateSpeed*Time.deltaTime);
       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnmanager.StartSpawning();
            Destroy(this.gameObject,0.5f);
        }
    }
}
