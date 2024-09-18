using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class FloatUI : MoveButtonUi
{
    private RectTransform baseRect;
    public override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        // Chuyển đổi điểm màn hình thành điểm địa phương trong RectTransform
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out Vector2 localPoint))
        {
            // Tính độ lệch của pivot (điểm neo)
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;

            // Trả về vị trí đã được chuyển đổi
            return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }

        // Nếu không chuyển đổi được, trả về (0, 0)
        return Vector2.zero;
    }
}

