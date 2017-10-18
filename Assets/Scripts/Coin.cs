using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Needed to manipulate the UI
using UnityEngine.UI;

public class Coin : MonoBehaviour
{

    // Called when something hits the Coin
    void OnCollisionEnter2D(Collision2D col)
    {

        // Play coin sound
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.getCoin);

        // Increase the Score Text component
        increaseTextUIScore();

        Destroy(gameObject);

    }

    // Increases the score the the text UI name passed
    void increaseTextUIScore()
    {

        // Find the Score UI component
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        // Get the string stored in it and convert to an int
        int score = int.Parse(textUIComp.text);

        // Increment the score
        score += 10;

        // Convert the score to a string and update the UI
        textUIComp.text = score.ToString();
    }

}