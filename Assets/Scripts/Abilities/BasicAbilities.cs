using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using Assets.Scripts;
using UnityEngine.UI;
using System;

public class BasicAbilities : MonoBehaviour
{

    public string ButtonName;

    public enum Ability
    {
        Nothing = 0,
        Knockback,
        Slow,
        BallOfWool
    }

    public Ability ChosenAbility;
    public float CooldownAlpha;

    public Font CooldownFont;
    public int CooldownFontSize;
    public Color CooldownColor;

    private Image _buttonImage;
    private Text _label;
    private float _timeSinceLastUse;
    private Enemy _enemy;
    private float _cooldown;

    private void Start()
    {
        // Starting off with player being able to use their ability.
        _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        _buttonImage = GameObject.Find("BasicAbility").GetComponent<Image>();

        var newGO = new GameObject("Label");
        newGO.transform.localPosition = Vector3.zero;
        newGO.transform.SetParent(GameObject.Find("BasicAbility").transform);
        newGO.AddComponent<Text>();
        _label = newGO.GetComponent<Text>();
        _label.alignment = TextAnchor.MiddleCenter;
        _label.horizontalOverflow = HorizontalWrapMode.Overflow;
        _label.verticalOverflow = VerticalWrapMode.Overflow;
        _label.transform.localScale = new Vector3(1, 1, 1);
        _label.font = CooldownFont;
        _label.fontSize = CooldownFontSize;
        _label.color = CooldownColor;
        _label.transform.localPosition = Vector3.zero;
        _timeSinceLastUse = _cooldown;
    }

    void Update()
    {
        if (_timeSinceLastUse < _cooldown * 1.5f)
            _timeSinceLastUse += Time.deltaTime;
        if (_timeSinceLastUse < _cooldown)
        {
            _label.text = string.Format("{0}s", Math.Round(_cooldown - _timeSinceLastUse, 1));
            _buttonImage.canvasRenderer.SetAlpha(CooldownAlpha);
        }

        if (_timeSinceLastUse >= _cooldown)
        {
            _label.text = "READY!";

            _buttonImage.canvasRenderer.SetAlpha(1f);
            Ability toUseAbility = GetAbility();
            if (toUseAbility == Ability.Nothing)
                return;
            if (toUseAbility == ChosenAbility)
                PerformAbility(toUseAbility);
        }
    }

    private Ability GetAbility()
    {
        if (CnInputManager.GetButtonDown(ButtonName))
        {
            return ChosenAbility;
        }

        return Ability.Nothing;
    }

    private void PerformAbility(Ability ability)
    {
        _timeSinceLastUse = 0;

        switch (ability)
        {
            case Ability.Knockback:
                _cooldown = Knockback_Cooldown;
                Knockback();
                break;
            case Ability.Slow:
                _cooldown = Slow_Cooldown;
                Slow();
                break;
            case Ability.BallOfWool:
                _cooldown = BallOfWool_Cooldown;
                BallOfWool();
                break;
        }
    }
    public float Knockback_Cooldown;
    public float Knockback_KnockbackSpeed;
    public int Knockback_KnockbackDuration;
    public float Knockback_Range;

    private Vector3 _knockbackDirection;

    private void Knockback()
    {
        // TODO: Play animation & sound.
        if (_enemy.IsInRange(Knockback_Range, this.gameObject.transform))
        {
            _knockbackDirection = _enemy.transform.position - this.gameObject.transform.position;
            _knockbackDirection.Normalize();

            StartCoroutine(KnockbackCoroutine());
        }
    }

    private IEnumerator KnockbackCoroutine()
    {
        for (int i = Knockback_KnockbackDuration; i >= 0; i--)
        {
            _enemy.gameObject.transform.position += _knockbackDirection * Knockback_KnockbackSpeed;
            yield return null;
        }
    }

    public float Slow_Cooldown;
    public int Slow_SlowDuration;
    public float Slow_SlowFactor;
    public float Slow_Range;

    private void Slow()
    {
        if (_enemy.IsInRange(Knockback_Range, this.gameObject.transform))
        {
            _enemy.GetComponent<EnemyFiniteStateMachine>().Slow(Slow_SlowDuration, Slow_SlowFactor);
        }
    }

    public float BallOfWool_Cooldown;
    public int BallOfWool_SlowDuration;
    public float BallOfWool_SlowFactor;
    public GameObject BallOfWool_GameObject;

    private void BallOfWool()
    {
        var ballOfWool = GameObject.Instantiate(BallOfWool_GameObject);
        ballOfWool.GetComponent<BallOfWoolScript>().SlowFactor = BallOfWool_SlowFactor;
        ballOfWool.GetComponent<BallOfWoolScript>().SlowDuration = BallOfWool_SlowDuration;
        ballOfWool.transform.position = this.gameObject.transform.position;
    }

}
