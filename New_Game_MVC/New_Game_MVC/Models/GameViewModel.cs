namespace New_Game_MVC.Models
{

    public enum Choice
    {
        Rock,
        Paper,
        Scissors
    }
    public class GameViewModel
    {
       
            public Choice PlayerChoice { get; set; }
            public string GameResult { get; set; }
            public List<GameResult> GameHistory { get; set; } = new List<GameResult>();
            public int Wins { get; set; }
            public int Losses { get; set; }
            public int Draws { get; set; }
        }

    }
