using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int ID;
    public string name;
    public int level;
    public float playerExperience;
    public Sprite playerAvatar;
    public int whilePearl;
    public int blackPearl;
    public int fishMainID;
}
[Serializable]
public class PlayerDataBase
{
    public List<PlayerData> PlayerDataBases;
}