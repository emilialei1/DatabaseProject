using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreScript : MonoBehaviour
{
    public GameObject score;
    public GameObject scoreName;
    public GameObject rank;

    public void SetScore(string name, string score, string rank)
    {
        this.score.GetComponent<TMP_Text>().text = score;
        this.scoreName.GetComponent<TMP_Text>().text = name;
        this.rank.GetComponent<TMP_Text>().text = rank;
    }
}
