using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ui;
    public GameObject gameOver;
    void Start()
    {
        ui.SetActive(false);
        gameOver.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLineStart")
        {
            ui.SetActive(true);
        }
        if (other.tag == "FinishLineEnd")
        {
            ui.SetActive(false);
            gameOver.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
