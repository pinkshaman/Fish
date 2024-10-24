using UnityEngine;
using UnityEngine.UI;

public class FishDisplay : MonoBehaviour
{
    public Image imageShow;
    public FishData data;
  


    public void SetImage(FishData fishData, int scale)
    {
        this.data = fishData;
        this.imageShow.sprite = fishData.fishSprite;
        UpdateImage(scale);
    }
    public void UpdateImage(int scale)
    {      
        if (data.scalePoint > scale)
        {
            SetBlack();
        }
        else
        { SetWhite(); }
    }

    public void SetBlack()
    {
        this.imageShow.color = Color.black;
    }
    public void SetWhite()
    {
        this.imageShow.color = Color.white;
    }
    
}
