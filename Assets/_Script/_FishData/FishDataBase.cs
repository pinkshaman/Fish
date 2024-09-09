using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "Fish/newFishData")]
public class FishDataBase :ScriptableObject
{
    public List<FishData> fishDatas;
}
