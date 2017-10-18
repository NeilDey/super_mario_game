using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeBlock : MonoBehaviour {

    public AnimationCurve anim;
    public int coinsInBlock = 5;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].point.y < transform.position.y) {

            StartCoroutine(RunAnimation());



            // If block contains coins
            if (coinsInBlock > 0)
            {

                // Play coin sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.getCoin);

                // Increase the Score Text component
                increaseTextUIScore();

                coinsInBlock--;

            }

        }
    }

        IEnumerator RunAnimation()
        { 
            Vector2 startPos = transform.position;
            for (float x = 0; x < anim.keys[anim.length - 1].time; x += Time.deltaTime) {
                transform.position = new Vector2(startPos.x,
                    startPos.y + anim.Evaluate(x));

                yield return null;
            }
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


