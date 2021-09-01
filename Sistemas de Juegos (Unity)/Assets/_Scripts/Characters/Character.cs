using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamagable
{
    [SerializeField] private GameObject _dieParticles;
    [SerializeField] private float _maxLife = 100f;
    [SerializeField] private float _currentLife;
    [SerializeField] private AudioSource _hurtSource;
    [SerializeField] private List<AudioClip> _hurtSounds;

    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private SphereCollider _collider;

    [SerializeField] private bool isTutorial;
    float CurrentLife => _currentLife;


    private void Start()
    {
        //_collider = GetComponent<SphereCollider>();
        //_meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _currentLife = _maxLife;
    }


    public void TakeDamage(float damage)
    {
        _currentLife -= damage;
        _hurtSource.PlayOneShot(_hurtSounds[Random.Range(0, _hurtSounds.Count)]);

        if (_currentLife <= 0)
        {
            CharacterDie();
        }
    }

    private void CharacterDie()
    {
        _dieParticles = Instantiate(_dieParticles, transform.position, Quaternion.identity) as GameObject;
        Destroy(_dieParticles, 2f);
        Disable();
        StartCoroutine(WaitForDie());
    }


    private void Disable()
    {
        _collider.enabled = false;
        _meshRenderer.enabled = false;
    }
    private IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(1f);
        if (isTutorial)
        {
            if (GameManager.Instance.TutorialManager.CurrentIndex == 1)
            {
                GameManager.Instance.TutorialManager.TutorialAttack.TutorialAddKill();
            }

            if (GameManager.Instance.TutorialManager.CurrentIndex == 2)
            {
                GameManager.Instance.TutorialManager.TutorialLoot.TutorialAddKill();
            }
        }
        Destroy(this.gameObject);
    }
}
