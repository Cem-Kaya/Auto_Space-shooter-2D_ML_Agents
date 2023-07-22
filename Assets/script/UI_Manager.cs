using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // handle to text
    public TextMeshProUGUI _scoreText;
    public Sprite [] _liveSprites;
    public Image _Livesimg;
    public MyScript _script;
    public TextMeshProUGUI _gameovertext;
    public TextMeshProUGUI _restart;
    public GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // _scoreText.text = "score: " + 50;
        // assign text component to the handle
        // _gameovertext.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();


        if ( _gameManager == null)
        {
            Debug.Log("GameManager is Null");
        }
    }

   // int score123 = 0;
    // Update is called once per frame
    void Update()
    {
       

        // transform.Find("Score").gameObject.GetComponent<TextMeshProUGUI>().text += ( "score: " +   "50").ToString() ;
        // score123++;

    }
    public void UpdateLives(int currentLives)
    {
        // give it a new one based on the currentlives index
        //  _Livesimg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            _gameovertext.gameObject.SetActive(true);
           transform.Find("Restart_key").gameObject.SetActive(true);
            _gameManager.GameOver();
        }
    }

    public void SetImageAtIndex(int i)
    {
      //  Debug.Log("i is " + i.ToString());
      //  Debug.Log("lives srite length :" + _liveSprites.Length.ToString());
        if (i >= 0 && i < _liveSprites.Length)
        {
            _Livesimg.sprite = _liveSprites[i];
         //   Debug.Log("should change the sprite !");
        }
        else
        {
          //  Debug.Log("index out of rage !");
        }



    }
             
}
