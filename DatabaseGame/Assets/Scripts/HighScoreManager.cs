using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    private List<HighScore> highScores = new List<HighScore>();

    public GameObject scorePrefab;
    public GameObject nameDialog;

    public SpacePress spacePress;

    public TMP_InputField enterName;

    public Transform scoreParent;

    private string connectionString;

    public int topRanks;
    public int saveScores;
    public int dataScore;

    void Start()
    {

        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.db";

        DeleteExtraScores();

        CreateTable(); 

        ShowScores();
    }

    private void Update()
    {
        dataScore = spacePress.score;
    }

    private void CreateTable()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = string.Format("CREATE TABLE if not exists HighScores (PlayerID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE , Name TEXT NOT NULL , Score INTEGER NOT NULL)");

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    public void EnterName()
    {
        if(enterName.text != string.Empty)
        {
            InsertScore(enterName.text, dataScore);
            enterName.text = string.Empty;

            nameDialog.SetActive(false);

            ShowScores();
        }
    }

    private void InsertScore(string name, int newScore)
    {
        GetScores();

        int hsCount = highScores.Count;

        if(highScores.Count > 0)
        {
            HighScore lowestScore = highScores[highScores.Count - 1];
            if(lowestScore != null && saveScores > 0 && highScores.Count >= saveScores && newScore > lowestScore.Score)
            {
                DeleteScore(lowestScore.ID);
                hsCount--;
            }
        }

        if (hsCount < saveScores)
        {
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();

                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    string sqlQuery = string.Format("INSERT INTO HighScores(Name, Score) VALUES(\"{0}\",\"{1}\")", name, newScore);

                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
        }       
    }

    private void GetScores()
    {
        highScores.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM HighScores";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        highScores.Add(new HighScore(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1)));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        highScores.Sort();
    }
    private void DeleteScore(int id)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = string.Format("DELETE FROM HighScores WHERE PlayerID = \"{0}\"", id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void ShowScores()
    {
        GetScores();

        foreach (GameObject score in GameObject.FindGameObjectsWithTag("Score"))
        {
            Destroy(score);
        }

        for (int i =0; i < topRanks; i++)
        {

            if(i <= highScores.Count - 1)
            {
                GameObject tmpObjec = Instantiate(scorePrefab);
                HighScore tmpScore = highScores[i];

                tmpObjec.GetComponent<HighScoreScript>().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString());
                tmpObjec.transform.SetParent(scoreParent);
                tmpObjec.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void DeleteExtraScores()
    {
        GetScores();

        if(saveScores <= highScores.Count)
        {
            int deleteCount = highScores.Count - saveScores;
            highScores.Reverse();

            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();

                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {

                    for(int i = 0; i < deleteCount; i++)
                    {
                        string sqlQuery = string.Format("DELETE FROM HighScores WHERE PlayerID = \"{0}\"", highScores[i].ID);

                        dbCmd.CommandText = sqlQuery;
                        dbCmd.ExecuteScalar();

                    }
                    dbConnection.Close();
                }
            }
        }
    }
}
