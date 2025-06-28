using System.Collections;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _audioJump;
    [SerializeField] private AudioClip _audioDamage;

    private float _volume = 1;
    private WaitUntil _waitForDisable;
    private Coroutine _disableSound;

    private void Awake()
    {
        _waitForDisable = new WaitUntil(() => _audiosource.isPlaying == false);
    }

    public void DisableSound()
    {
        _disableSound = StartCoroutine(DisableSoundAfterDelay());
    }

    public void PlayJumpSound()
    {
        _audiosource.clip = _audioJump;
        _audiosource.Play();
    }

    public void PlayDamageSound()
    {
        _audiosource.clip = _audioDamage;
        _audiosource.Play();
    }

    public void EnableSound()
    {
        StopDisablingSound();
        _audiosource.volume = _volume;
    }

    private void StopDisablingSound()
    {
        if (_disableSound != null)
            StopCoroutine(_disableSound);
    }

    private IEnumerator DisableSoundAfterDelay()
    {
        yield return _waitForDisable;

        _audiosource.volume = 0;
    }
}
