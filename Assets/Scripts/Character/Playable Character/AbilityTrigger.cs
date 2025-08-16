using System;
using System.Collections;
using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{
    [SerializeField] private float _abilityDurationSec;
    [SerializeField] private float _cooldownDurationSec;

    private VampireAbility _ability;
    private VampireCircle _vampireCircle;
    private Coroutine _abilityDeactivation;
    private Coroutine _abilityCooldown;
    private WaitForSeconds _abilityDurationWait;
    private WaitForSeconds _cooldownDurationWait;
    private bool _isActive = false;
    private bool _canActivate = true;

    public event Action<float> StartedChanging;

    private void Awake()
    {
        _abilityDurationWait = new WaitForSeconds(_abilityDurationSec);
        _cooldownDurationWait = new WaitForSeconds(_cooldownDurationSec);
    }

    private void OnEnable()
    {
        UserInput.AbilityKeyPressed += EnableAbility;
    }

    private void OnDisable()
    {
        UserInput.AbilityKeyPressed -= EnableAbility;
    }

    public void Initialize(VampireAbility ability, VampireCircle circle)
    {
        _ability = ability;
        _vampireCircle = circle;
    }

    private void EnableAbility()
    {
        if (_isActive == false && _canActivate)
        {
            _isActive = true;
            _canActivate = false;
            _ability.Activate(true);
            _vampireCircle.gameObject.SetActive(true);
            _abilityDeactivation = StartCoroutine(DeactivateAbility());
            StartedChanging?.Invoke(_abilityDurationSec);

            if (_abilityCooldown != null)
                StopCoroutine(_abilityCooldown);
        }
    }
    
    private void DisableAbility()
    {
        _isActive = false;
        _ability.Activate(false);
        _vampireCircle.gameObject.SetActive(false);
        _abilityCooldown = StartCoroutine(CooldownAbility());
        StartedChanging?.Invoke(_cooldownDurationSec);

        if (_abilityDeactivation != null)
            StopCoroutine(_abilityDeactivation);
    }

    private IEnumerator DeactivateAbility()
    {
        yield return _abilityDurationWait;
        DisableAbility();
    }

    private IEnumerator CooldownAbility()
    {
        yield return _cooldownDurationWait;
        _canActivate = true;
    }
}
