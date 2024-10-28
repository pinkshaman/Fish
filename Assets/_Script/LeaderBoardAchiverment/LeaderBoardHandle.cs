using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardHandle : MonoBehaviour
{
    public LeaderBoard leaderboard;
    public Text ranking;
    public TMP_Text playerName;
    public Text score;
    //public Image rewardIMG;
    //public Image rankBackground;

    public void SetDataLeaderBoard(LeaderBoard leaderBoard)
    {
        this.leaderboard = leaderBoard;
        UpdateUILeaderBoard();

    }
    public void UpdateUILeaderBoard()
    {
        ranking.text = leaderboard.rank.ToString();
        playerName.text = leaderboard.playername;
        score.text = leaderboard.rankScore;

    }
}
