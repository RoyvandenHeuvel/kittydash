﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseCallScript : MonoBehaviour
{
    private Player _player;
    private bool _coroutineRunning;
    private bool _closeCallOnCooldown;

    public float CloseCallDistance;
    public int CloseCallDuration;
    public int CloseCallTextDuration;
    public int CloseCallFontSize;
    public List<string> CloseCallMessages;
    public Font CloseCallFont;
    public Color CloseCallTextColor;

    public int CloseCallCooldown;

    void Start()
    {
        _coroutineRunning = false;
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnDestroy()
    {
        GameObject.Destroy(UItextGO);
    }

    void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <= CloseCallDistance && !_coroutineRunning)
        {
            _coroutineRunning = true;
            StartCoroutine(CloseCallStart());
        }
    }

    private IEnumerator CloseCallStart()
    {
        for (int f = CloseCallDuration; f > 0; f--)
        {
            if (Vector3.Distance(_player.transform.position, transform.position) > CloseCallDistance || _closeCallOnCooldown)
            {
                _coroutineRunning = false;
                yield break;
            }
            yield return null;
        }

        GameManager.Instance.GameData.CloseCalls++;
        StartCoroutine(CooldownHandler());
        StartCoroutine(CloseCallTextHandler());
        
        _coroutineRunning = false;
    }

    public GameObject UItextGO;

    private IEnumerator CloseCallTextHandler()
    {
        var canvasTransform = GameObject.Find("Canvas").transform;
        var canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        UItextGO = new GameObject("CloseCallText");
        UItextGO.transform.SetParent(canvasTransform);

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(this.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(WorldObject_ScreenPosition.x, WorldObject_ScreenPosition.y);
        trans.localScale = new Vector3(1, 1, 1);

        Text text = UItextGO.AddComponent<Text>();
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.text = CloseCallMessages[Random.Range(0, CloseCallMessages.Count)];
        text.font = CloseCallFont;
        text.fontSize = CloseCallFontSize;
        text.color = CloseCallTextColor;
        SoundManager.instance.PlaySound("Close");
        

        for (int i = CloseCallTextDuration; i > 0; i--)
        {
            yield return null;
        }

        GameObject.Destroy(UItextGO);
        UItextGO = null;
    }

    private IEnumerator CooldownHandler()
    {
        _closeCallOnCooldown = true;
        for (int i = CloseCallCooldown; i > 0; i--)
        {
            yield return null;
        }
        _closeCallOnCooldown = false;
    }
}
