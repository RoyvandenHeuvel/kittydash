using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTapControl : MonoBehaviour, IPointerDownHandler
{
    void IPointerDownHandler.OnPointerDown(PointerEventData ped)
    {
        GameObject.Find("Player").GetComponent<PlayerMovementTapControlsTest>().targetLocation = Camera.main.ScreenToWorldPoint(ped.pointerPressRaycast.screenPosition);
    }
}
