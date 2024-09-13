using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDataProgess 
{
    public int progessID;
    public string progessName;
    public int progessLevel;
    public float progessplayerExperience;
    public Sprite progessPlayerAvatar;
    public int progessWhilePearl;
    public int progessBlackPearl;

}
[Serializable]
public class PlayerDataProgessBase
{
    public List<PlayerDataProgess> playerDataProgesses;
}