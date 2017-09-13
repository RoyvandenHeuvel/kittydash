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
        BasicAttack,
    }

    public Ability ChosenAbility;
    public float Cooldown;
    public float CooldownAlpha;

    private Image _buttonImage;
    private float _timeSinceLastUse;
    private Enemy _enemy;

    private void Start()
    {
        // Starting off with player being able to use their ability.
        _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        _buttonImage = GameObject.Find("BasicAbility").GetComponent<Image>();
        _timeSinceLastUse = Cooldown;
    }

    void Update()
    {
        if (_timeSinceLastUse < Cooldown * 1.5f)
            _timeSinceLastUse += Time.deltaTime;
        if (_timeSinceLastUse < Cooldown)
        {
            _buttonImage.canvasRenderer.SetAlpha(CooldownAlpha);
        }

        if (_timeSinceLastUse >= Cooldown)
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
            case Ability.BasicAttack:
                BasicAttack();
                break;
        }
    }

    public float BasicAttack_KnockbackSpeed;
    public float BasicAttack_KnockbackDuration;
    public float BasicAttack_Range;

    private Vector3 _basicAttackDirection;

    private void BasicAttack()
    {
        // TODO: Play animation probably.
        if (_enemy.IsInRange(BasicAttack_Range, this.gameObject.transform))
        {
            _basicAttackDirection = _enemy.transform.position - this.gameObject.transform.position;
            _basicAttackDirection.Normalize();

            StartCoroutine(BasicAttackKnockback());
        }
    }

    private IEnumerator BasicAttackKnockback()
    {
        for (float i = BasicAttack_KnockbackDuration; i >= 0; i -= 0.1f)
        {
            _enemy.gameObject.transform.position += _basicAttackDirection * BasicAttack_KnockbackSpeed;
            yield return null;
        }
    }

}
