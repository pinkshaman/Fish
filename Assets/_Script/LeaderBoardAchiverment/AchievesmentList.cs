using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievesment", menuName = "Achievesment/newAchievesment")]

[Serializable]
public class AchievesmentList : ScriptableObject
{
    public List<Achievesment> achievesmentLists;
}
