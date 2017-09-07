using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplayScript : MonoBehaviour
{
    private Text _text;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Player");
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = string.Format("{0}", Math.Round(_player.transform.position.y));
    }
}
