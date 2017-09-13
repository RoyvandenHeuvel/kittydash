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

    private Image _buttonImage;
    private float _timeSinceLastUse;

    private void Start()
    {
        // Starting off with player being able to use their ability.
        _buttonImage = GameObject.Find("BasicAbility").GetComponent<Image>();
        _timeSinceLastUse = Cooldown;
    }

    void Update()
    {
        if (_timeSinceLastUse < Cooldown * 1.5f)
            _timeSinceLastUse += Time.deltaTime;
        if (_timeSinceLastUse < Cooldown)
        {
            _buttonImage.CrossFadeAlpha(0.1f, Cooldown, false);
        }

        if (_timeSinceLastUse >= Cooldown)
        {
            _buttonImage.CrossFadeAlpha(1f, Cooldown, false);
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

    public float BasicAttack_KnockbackStrength;
    public float BasicAttack_Range;
    private void BasicAttack()
    {
        var enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        // TODO: Play animation probably.
        if (enemy.IsInRange(BasicAttack_Range, this.gameObject.transform))
        {
            Vector3 direction = enemy.transform.position - this.gameObject.transform.position;
            direction.Normalize();

            enemy.gameObject.transform.position += direction * BasicAttack_KnockbackStrength;
        }
    }

}
