using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class FishShopData 
{
    public int FishShopID;
   
}
[Serializable]
public class FishShopDataBase
{
    public List<FishShopData> FisShopDatas;
}
