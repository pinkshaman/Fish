using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string ID;
    public string name;
    public int level;
    public float playerExperience;
    public Sprite playerAvatar;
    public int whitePearl;
    public int blackPearl;
    public int fishMainID;

    public PlayerData(string id, string name, int level, float exp, Sprite playerAvatar, int whitePearl, int blackPearl, int fishMainID)
    {
        this.ID = id;
        this.name = name;
        this.level = level;
        this.playerExperience = exp;
        this.playerAvatar = playerAvatar;
        this.blackPearl = blackPearl;
        this.whitePearl = whitePearl;
        this.fishMainID = fishMainID;

    }
}
[Serializable]
public class PlayerDataBase
{
    public List<PlayerData> PlayerDataBases;
}