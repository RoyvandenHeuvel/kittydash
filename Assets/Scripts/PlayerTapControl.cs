using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTapControl : MonoBehaviour, IPointerDownHandler
{
    void IPointerDownHandler.OnPointerDown(PointerEventData ped)
    {
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), ped.position, ped.pressEventCamera, out localCursor))
            return;
        
        var newPosition = Camera.main.ScreenToWorldPoint(ped.pointerPressRaycast.screenPosition);

        GameObject.Find("Player").GetComponent<PlayerMovementTapControlsTest>().targetLocation = newPosition;
        Debug.Log("Moving to: " + Camera.main.ScreenToWorldPoint(newPosition));
    }
}
