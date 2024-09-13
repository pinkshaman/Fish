using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataBase", menuName = "playerdata/PlayerDataBase", order = 1)]
public class PlayerDataBase :ScriptableObject
{
    public List<PlayerData> playerDatas;
}
