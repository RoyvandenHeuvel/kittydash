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
        Slow,
        BallOfWool
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
