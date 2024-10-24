
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public class MapChallenger : MonoBehaviour
{
    public MapData mapData;
    public Sprite imageBG;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetMap(MapData mapDatas)
    {
        this.mapData = mapDatas;
        UpdateData(mapDatas);
    }
    public void UpdateData(MapData mapDatas)
    {
        imageBG = mapDatas.mapBackGround;  
        spriteRenderer.sprite = imageBG;
    }
}
