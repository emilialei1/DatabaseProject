using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HighScore
{
    public int Score { get; set; }

    public string Name { get; set; }

    public int ID { get; set; }

    public HighScore(int id, int score, string name)
    {
        this.Score = score;
        this.Name = name;
        this.ID = id;
    }
}
