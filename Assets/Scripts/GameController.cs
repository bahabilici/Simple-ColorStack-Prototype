using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;


    [SerializeField] Text scoreDisplay;
   
    int score;


    private void Start()
    {
        
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateScore(int valueIn)
    {
        score += valueIn;
        scoreDisplay.text = score.ToString();
    }

  

}
