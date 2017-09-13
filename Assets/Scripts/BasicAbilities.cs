using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using Assets.Scripts;
using UnityEngine.UI;

public class BasicAbilities : MonoBehaviour
{

    public string ButtonName;

    public enum Ability
    {
        Nothing = 0,
        Knockback,
        Slow
    }

    public Ability ChosenAbility;
    public float CooldownAlpha;

    private Image _buttonImage;
    private float _timeSinceLastUse;
    private Enemy _enemy;
    private float _cooldown;

    private void Start()
    {
        // Starting off with player being able to use their ability.
        _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        _buttonImage = GameObject.Find("BasicAbility").GetComponent<Image>();
        _timeSinceLastUse = _cooldown;
    }

    void Update()
    {
        if (_timeSinceLastUse < _cooldown * 1.5f)
            _timeSinceLastUse += Time.deltaTime;
        if (_timeSinceLastUse < _cooldown)
        {
            _buttonImage.canvasRenderer.SetAlpha(CooldownAlpha);
        }

        if (_timeSinceLastUse >= _cooldown)
        {
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
        }
    }
    public float Knockback_Cooldown;
    public float Knockback_KnockbackSpeed;
    public float Knockback_KnockbackDuration;
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
        for (float i = Knockback_KnockbackDuration; i >= 0; i -= 0.1f)
        {
            _enemy.gameObject.transform.position += _knockbackDirection * Knockback_KnockbackSpeed;
            yield return null;
        }
    }

    public float Slow_Cooldown;
    public float Slow_SlowDuration;
    public float Slow_SlowFactor;
    public float Slow_Range;

    private void Slow()
    {
        if (_enemy.IsInRange(Knockback_Range, this.gameObject.transform))
        {
            StartCoroutine(_enemy.GetComponent<EnemyFiniteStateMachine>().Slow(Slow_SlowDuration, Slow_SlowFactor));
        }
    }

}
