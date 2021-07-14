using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    [SerializeField]
    private GameObject imageBackground;
    [SerializeField]
    private GameObject imageController;
    private Vector3 stickFristPosition;
    public Vector3 joyVec;
    public float x, y;
    float StickRadius;
    public SettingManager settingManager;

    public void PointerDown()
    {
        StickRadius = imageBackground.gameObject.GetComponent<RectTransform>().sizeDelta.y * .3f * settingManager.JoystickSen;
        imageBackground.SetActive(true);
        imageBackground.transform.position = Input.mousePosition;
        imageController.transform.position = Input.mousePosition;
        stickFristPosition = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;

        x = Mathf.Clamp(Mathf.Abs((DragPosition - stickFristPosition).x), 0, StickRadius) / StickRadius;
        y = Mathf.Clamp(Mathf.Abs((DragPosition - stickFristPosition).y), 0, StickRadius) / StickRadius;

        joyVec = (DragPosition - stickFristPosition).normalized;

        float stickDistance = Vector3.Distance(DragPosition, stickFristPosition);

        if (stickDistance < StickRadius)
        {
            imageController.transform.position = stickFristPosition + joyVec * stickDistance;
        }
        else
        {
            imageController.transform.position = stickFristPosition + joyVec * StickRadius;
        }
    }

    public void Drop()
    {
        joyVec = Vector3.zero;
        imageBackground.SetActive(false);
    }
}
