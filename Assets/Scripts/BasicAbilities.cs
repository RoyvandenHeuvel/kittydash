using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using Assets.Scripts;

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

    private float _timeSinceLastUse;

    private void Start()
    {
        // Starting off with player being able to use their ability.
        _timeSinceLastUse = Cooldown;
    }

    void Update()
    {
        if (_timeSinceLastUse < Cooldown * 1.5f)
            _timeSinceLastUse += Time.deltaTime;
        if (_timeSinceLastUse >= Cooldown)
        {
            Ability wantedAction = GetAbility();
            if (wantedAction == Ability.Nothing)
                return;
            if ((wantedAction != ChosenAbility) || (_timeSinceLastUse >= 1.0f))
                PerformAbility(wantedAction);
        }
    }

    private Ability GetAbility()
    {
        if (CnInputManager.GetButtonDown(ButtonName))
        {
            return Ability.BasicAttack;
        }

        return Ability.Nothing;
    }

    private void PerformAbility(Ability action)
    {
        ChosenAbility = action;
        _timeSinceLastUse = 0;

        switch (ChosenAbility)
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
            Vector3 direction = transform.position - this.gameObject.transform.position;
            direction.Normalize();

            enemy.gameObject.transform.position += direction * BasicAttack_KnockbackStrength;
        }
    }

}
