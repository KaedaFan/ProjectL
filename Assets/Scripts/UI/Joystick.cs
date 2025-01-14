using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick: MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _bgImage;
    private Image _handleImage;
    public Vector2 InputDirection { get; private set; }

    private bool _isVirtualJoystickActive = true;
    private void Start()
    {
        _bgImage = GetComponent<Image>();
        _handleImage = transform.transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector2.zero; 
    }

    public void OnDrag(PointerEventData ped)
    {
        if (!_isVirtualJoystickActive) return;

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _bgImage.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / _bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _bgImage.rectTransform.sizeDelta.y);

            InputDirection = new Vector2(pos.x * 2, pos.y * 2);
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            _handleImage.rectTransform.anchoredPosition = new Vector2(
                InputDirection.x * (_bgImage.rectTransform.sizeDelta.x / 2),
                InputDirection.y * (_bgImage.rectTransform.sizeDelta.y / 2));
        }
        
    }

    public void OnPointerUp(PointerEventData ped)
    {
        if (!_isVirtualJoystickActive) return;
        InputDirection = Vector2.zero;
        _handleImage.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData ped)
    {
        if (!_isVirtualJoystickActive) return;
        OnDrag(ped);
    }

    public void EnableVirtualJoystick()
    {
        _isVirtualJoystickActive = true;
    }

    public void DisableVirtualJoystick()
    {
        _isVirtualJoystickActive = false;
        ResetJoystick();
    }

    private void ResetJoystick()
    {
        InputDirection = Vector2.zero;
        if (_handleImage != null)
        {
            _handleImage.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}