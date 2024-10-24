using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mapdata", menuName = "Map/newMap")]
[Serializable]
public class MapDataBase : ScriptableObject
{
    public List<MapData> mapDataBases;
}
