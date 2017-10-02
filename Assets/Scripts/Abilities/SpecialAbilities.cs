using UnityEngine;
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
    private int _closeCallsRequired;

    private void Start()
    {
        GameManager.Instance.GameData.CloseCalls = 0;
        _closeCallsRequired = GetCooldown();
        _enemy = GameObject.Find("Enemy (Hunter)").GetComponent<Enemy>();
        _buttonImage = GameObject.Find("SpecialAbility").GetComponent<Image>();
        _buttonImage.canvasRenderer.SetAlpha(UnusableAlpha);
    }

    void Update()
    {
        var tempHound = GameObject.Find("Enemy (Hound)");
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

    private int GetCooldown()
    {
        switch (ChosenAbility)
        {
            case SpecialAbility.Nothing:
                return int.MaxValue;
            case SpecialAbility.Claw:
                return Claw_CloseCallsRequired;
            default:
                return int.MaxValue;
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
                Claw();
                break;
        }
    }

    public int Claw_CloseCallsRequired;
    public int Claw_SlowDuration;
    public float Claw_SlowFactor;
    public float Claw_Range;
    public GameObject Claw_Animation;

    private void Claw()
    {
        if (_enemy.IsInRange(Claw_Range, this.gameObject.transform))
        {
            var clawAnim = GameObject.Instantiate(Claw_Animation);
            clawAnim.transform.position = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y, _enemy.transform.position.z - 1);
            _enemy.GetComponent<EnemyFiniteStateMachine>().Slow(Claw_SlowDuration, Claw_SlowFactor);
        }

        if (_hound != null)
        {
            if (_hound.IsInRange(Claw_Range, this.gameObject.transform))
            {
                var clawAnim = GameObject.Instantiate(Claw_Animation);
<<<<<<< HEAD
                clawAnim.transform.position = _enemy.transform.position;
                SoundManager.instance.PlaySound("DogDies");
=======
                clawAnim.transform.position = _hound.transform.position;
>>>>>>> e5be68addda4aa8f7295e476d3b9c8a9e83c161c
                GameObject.Destroy(_hound.gameObject);
            }
        }
    }
}
