using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private readonly int DisapearAnimation = Animator.StringToHash("Disapear");

    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Character>() != null)
        {
            _audioSource.Play();
            _animator.Play(DisapearAnimation);
        }
    }
}
