using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Achievesment 
{
    public int achivesmentID;
    public Sprite AchievesmentImage;
    public string AchievesmentName;
    public string AchivesmentDecription;
    public RewardBaseUpdate AchievesmentRewardID;
    
    public int totalTask;
    
    public Achievesment(int id,Sprite image,string name,string decription, RewardBase reward,int taskCount)
    {
        this.achivesmentID = id;
        this.AchievesmentImage = image;
        this.AchievesmentName = name;
        this.AchivesmentDecription = decription;      
        this.totalTask = taskCount;
    }
}
