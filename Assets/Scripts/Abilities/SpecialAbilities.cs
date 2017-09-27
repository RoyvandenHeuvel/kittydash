﻿using UnityEngine;
using CnControls;
using Assets.Scripts;
using UnityEngine.UI;

public class SpecialAbilities : MonoBehaviour
{
    public string ButtonName;

    public enum SpecialAbility
    {
        Nothing = 0,
        Claw,
    }

    public SpecialAbility ChosenAbility;
    public float UnusableAlpha;

    private Image _buttonImage;
    private Enemy _enemy;
    private Enemy _hound;
    private float _closeCallsRequired;

    private void Start()
    {
        GameManager.Instance.GameData.CloseCalls = 0;
        _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        _buttonImage = GameObject.Find("SpecialAbility").GetComponent<Image>();
        _buttonImage.canvasRenderer.SetAlpha(UnusableAlpha);
    }

    void Update()
    {
        var tempHound = GameObject.Find("Hound");
        if (tempHound != null)
        {
            _hound = tempHound.GetComponent<Enemy>();
        }

        if (GameManager.Instance.GameData.CloseCalls < _closeCallsRequired)
        {
            _buttonImage.canvasRenderer.SetAlpha(UnusableAlpha);
        }

        if (GameManager.Instance.GameData.CloseCalls >= _closeCallsRequired)
        {
            _buttonImage.canvasRenderer.SetAlpha(1f);
            SpecialAbility toUseAbility = GetAbility();
            if (toUseAbility == SpecialAbility.Nothing)
                return;
            if (toUseAbility == ChosenAbility)
                PerformAbility(toUseAbility);
        }
    }

    private SpecialAbility GetAbility()
    {
        if (CnInputManager.GetButtonDown(ButtonName))
        {
            return ChosenAbility;
        }

        return SpecialAbility.Nothing;
    }

    private void PerformAbility(SpecialAbility ability)
    {
        GameManager.Instance.GameData.CloseCalls = 0;

        switch (ability)
        {
            case SpecialAbility.Claw:
                _closeCallsRequired = Claw_CloseCallsRequired;
                Claw();
                break;
        }
    }

    public float Claw_CloseCallsRequired;
    public int Claw_SlowDuration;
    public float Claw_SlowFactor;
    public float Claw_Range;
    public GameObject Claw_Animation;

    private void Claw()
    {
        if (_enemy.IsInRange(Claw_Range, this.gameObject.transform))
        {
            var clawAnim = GameObject.Instantiate(Claw_Animation);
            clawAnim.transform.position = _enemy.transform.position;
            _enemy.GetComponent<EnemyFiniteStateMachine>().Slow(Claw_SlowDuration, Claw_SlowFactor);
        }

        if (_hound != null)
        {
            if (_hound.IsInRange(Claw_Range, this.gameObject.transform))
            {
                var clawAnim = GameObject.Instantiate(Claw_Animation);
                clawAnim.transform.position = _enemy.transform.position;
                GameObject.Destroy(_hound.gameObject);
            }
        }
    }
}