using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeaderBoard 
{
    public string playerID;
    public int rank;
    public string playername;
    public string rankScore;
}

[Serializable]
public class LeaderBoardList
{
    public List<LeaderBoard> leaderboardLists;
}
