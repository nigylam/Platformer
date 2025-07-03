using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollectableSound : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (_audioSource.isPlaying)
            StartCoroutine(WaitTillAudioPlaying());

        _audioSource.Play();
    }

    private IEnumerator WaitTillAudioPlaying()
    {
        while (_audioSource.isPlaying)
        {
            yield return null;
        }
    }
}
