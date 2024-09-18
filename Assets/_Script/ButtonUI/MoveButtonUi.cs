using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class MoveButtonUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector2 input = Vector2.zero;                        // Giá trị đầu vào của joystick (Vector3 với Z = 0)   
    public float Horizontal => input.x;                          // Biến để lấy giá trị ngang (X) của joystick
    public float Vertical => input.y;                            // Biến để lấy giá trị doc (Y) của joystick
    public float HandleRange = 1;                                 // Phạm vi di chuyển của joystick
    [SerializeField] protected RectTransform background;           // Nền của joystick
    [SerializeField] protected RectTransform handle;               // Tay cầm của joystick

    protected Canvas canvas;
    protected Camera cam;
    public virtual void Start()
    {
        handle.anchoredPosition = Vector2.zero;                  // Đặt tay cầm joystick ở giữa
    }

    // Phương thức gọi khi nhấn vào joystick
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);

    }

    // Phương thức gọi khi kéo joystick
    public virtual void OnDrag(PointerEventData eventData)
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position); // Lấy vị trí của nền joystick
        Vector2 radius = background.sizeDelta / 2; // Tính bán kính của nền
        input = (eventData.position - position) / radius; // Tính input dựa trên vị trí
        HandleInput(input.magnitude, input.normalized); // Xử lý input
        handle.anchoredPosition = input * radius * HandleRange; // Di chuyển tay cầm 
    }

    // Xử lý input của joystick
    public void HandleInput(float magnitude, Vector2 normalised)
    {
        if (magnitude > 1) // Nếu input lớn hơn 1 thì chuẩn hóa
            input = normalised;
    }

    // Phương thức gọi khi thả joystick
    public virtual void  OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero; // Đặt lại giá trị đầu vào về 0
        handle.anchoredPosition = Vector2.zero; // Đặt lại tay cầm về vị trí giữa
    }

    // Phương thức trả về hướng từ gốc tọa độ (0, 0) đến vị trí input
    public Vector2  GetDirection()
    {
        return input.normalized; // Trả về vector hướng 
    }
    public void Update()
    {
        input = GetDirection(); // Lấy hướng
        
    }
}
