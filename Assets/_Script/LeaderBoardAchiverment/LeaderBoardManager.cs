using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames.BasicApi;
using System.Linq;


public class LeaderBoardManager : MonoBehaviour
{
    public LeaderBoardList leaderBoardList;
    public LeaderBoardHandle leaderBoardHandle;
    public Transform rootUi;
    private int score ;
    public void Start()
    {
        score = PlayerPrefs.GetInt("CurrentScore");
        ReportScore(score);
        LoadRank();
    }
    public void ShowLeaderboardUi()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_highest_score_test);
    }
    public void ReportScore(int score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_highest_score_test, (bool success) => { });

    }
    public void LoadRank()
    {
        PlayGamesPlatform.Instance.LoadScores(GPGSIds.leaderboard_highest_score_test,
            LeaderboardStart.PlayerCentered,
            100,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
             (LeaderboardScoreData data) =>
             {
                 if (data.Valid)
                 {
                     LoadUsersAndDisplay(data);
                 }

             });
    }
    private void LoadUsersAndDisplay(LeaderboardScoreData lb)
    {
        leaderBoardList.leaderboardLists = new List<LeaderBoard>();
        var userIds = lb.Scores.Select(score => score.userID).ToList();

        Social.LoadUsers(userIds.ToArray(), users =>
        {
            leaderBoardList.leaderboardLists = lb.Scores.Select(score => new LeaderBoard
            {
                playerID = score.userID,
                rank = score.rank,
                playername = FindPlayerName(users, score.userID),
                rankScore = score.formattedValue
            }).ToList();
            leaderBoardList.leaderboardLists = leaderBoardList.leaderboardLists
           .OrderByDescending(leader => int.Parse(leader.rankScore))
           .ToList();

            DisplayLeaderBoard();
        });
    }
    private string FindPlayerName(IUserProfile[] users, string userId)
    {
        foreach (IUserProfile user in users)
        {
            if (user.id == userId)
            {
                return user.userName;
            }
        }
        return "Unknown";
    }
    private void DisplayLeaderBoard()
    {
        foreach (var player in leaderBoardList.leaderboardLists)
        {
            var showinfo = Instantiate(leaderBoardHandle,rootUi);
            showinfo.SetDataLeaderBoard(player);
        }
    }
}
