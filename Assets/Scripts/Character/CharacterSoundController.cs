using System.Collections;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _audioJump;
    [SerializeField] private AudioClip _audioDamage;
    [SerializeField] private CharacterControler _controller;
    [SerializeField] private CharacterCollides _collides;
    [SerializeField] private Character _character;

    private WaitUntil _waitForDisable;
    private float _volume = 1;
    private bool _isSoundOn;

    private void OnEnable()
    {
        _character.Dead += DisableSound;
        _controller.Jumped += PlayJumpSound;
        _collides.Damaged += PlayDamageSound;

        _waitForDisable = new WaitUntil(() => _audiosource.isPlaying == false);
    }

    private void OnDisable()
    {
        _character.Dead -= DisableSound;
        _controller.Jumped -= PlayJumpSound;
        _collides.Damaged -= PlayDamageSound;
    }

    private IEnumerator DisableSoundAfterDelay()
    {
        yield return _waitForDisable;

        if (_isSoundOn == false)
            _audiosource.volume = 0;
    }

    private void DisableSound(bool isDisabled)
    {
        if (isDisabled)
        {
            StartCoroutine(DisableSoundAfterDelay());
            _isSoundOn = false;
        }
        else
        {
            _audiosource.volume = _volume;
            _isSoundOn = true;
        }
    }

    private void PlayJumpSound()
    {
        _audiosource.clip = _audioJump;
        _audiosource.Play();
    }

    private void PlayDamageSound()
    {
        _audiosource.clip = _audioDamage;
        _audiosource.Play();
    }
}
