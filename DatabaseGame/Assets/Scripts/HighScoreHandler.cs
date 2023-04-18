using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{
    List<HighScoreElement> highScoreList = new List<HighScoreElement>();
    [SerializeField] int maxCount = 7;

    private void Start()
    {
        LoadHighScores();
    }

    private void LoadHighScores()
    {

    }

    private void SaveHighScores()
    {

    }

    public void AddHighScoreIfPossible(HighScoreElement element)
    {

    }
}
