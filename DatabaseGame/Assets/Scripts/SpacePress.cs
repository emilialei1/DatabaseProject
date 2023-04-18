using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpacePress : MonoBehaviour
{

    public Animator spaceAnim;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI timerText;

    public GameObject gameOverScreen;

    private int score;
    public bool isGameOn = false;
    public bool isGameOver;
    public float timeLeft = 30.0f;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGameOver == false)
        {
            spaceAnim.SetBool("isKeyDown", true);
            UpdateScore(1);
            isGameOn = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && isGameOver == false)
        {
            spaceAnim.SetBool("isKeyDown", false);
        }

        if(isGameOn == true)
        {
            timerText.text = " " + Mathf.RoundToInt(timeLeft);

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GameOver();
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = " " + score;
        endScoreText.text = "Your score: " + score;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameOver = true;
    }

   /* public IEnumerator StartTimer()
    {
        isGameOn = true;
        yield return new WaitForSeconds(30);
        gameOverScreen.SetActive(true);
    }*/
}
