using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private readonly int DisapearAnimation = Animator.StringToHash("Disapear");
    private readonly string AnimatorBaseLayer = "Base Layer";

    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Character>() != null)
            StartCoroutine(Disapear());
    }

    private IEnumerator Disapear()
    {
        _animator.Play(DisapearAnimation);
        _audioSource.Play();

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(_animator.GetLayerIndex(AnimatorBaseLayer)).length);

        gameObject.SetActive(false);
    }
}
