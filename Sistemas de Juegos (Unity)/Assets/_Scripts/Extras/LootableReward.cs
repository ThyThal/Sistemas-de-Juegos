using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LootableReward : MonoBehaviour, ILootable
{
    [SerializeField] private GameObject _reward;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _lootRarityRadius;
    [SerializeField] private GameObject _lootRarityBeam;
    [SerializeField] private AudioClip[] _pickupClips;
    [SerializeField] private AudioSource _pickupSource;
    [SerializeField] private bool _isPlayerClose;

    [Header("Events")]
    private GameObject _spawned;

    public GameObject Reward => _reward;
    public Weapon Weapon => _weapon;

    private void Start()
    {
        if (GameManager.Instance.IsTutorial) { SpawnReward(); }
    }

    public void GrabLootable()
    {
        PlayerController player = GameManager.Instance.Player;

        if (_isPlayerClose == true && _reward != null)
        {
            if (GameManager.Instance.IsTutorial) { GameManager.Instance.TutorialManager.TutorialLoot.AddGrabbed(); }
            
            _pickupSource.PlayOneShot(_pickupClips[Random.Range(0, _pickupClips.Length)]);

            if (player.PlayerInventory.CurrentWeapon != null)
            {
                player.PlayerInventory.RemoveWeapon();
            }

            player.PlayerInventory.LootWeapons(_spawned);
            GameManager.Instance.PlayerBulletsPool.Clear();
            _lootRarityBeam.GetComponent<ParticleSystem>()?.Stop();
            _lootRarityRadius.GetComponent<ParticleSystem>()?.Stop();
            _weapon = null;
            _reward = null;
            _isPlayerClose = false;
            //Destroy(this.gameObject);
        }
    }

    [ContextMenu("Spawn Loot")]
    public void SpawnReward()
    {
        if (_reward != null)
        {
            _weapon = _reward.GetComponent<Weapon>();
            _spawned = Instantiate(_reward, transform.position, Quaternion.identity);
            _spawned.transform.position = new Vector3(_spawned.transform.position.x, _spawned.transform.position.y + 0.5f, _spawned.transform.position.z);
            //_spawned.transform.SetParent(transform);
            _lootRarityBeam = Instantiate(_weapon.WeaponStats.RarityBeam, _spawned.transform);
            _lootRarityRadius = Instantiate(_weapon.WeaponStats.RarityRadius, _spawned.transform);
        }
    }

    public void OnTriggerEnter()
    {
        GameManager.Instance.Player.AddReward(this);
        //_lootRadius.SetActive(false);
        _isPlayerClose = true;
    }

    public void OnTriggerExit()
    {
        GameManager.Instance.Player.RemoveReward();
        //_lootRadius.SetActive(true);
        _isPlayerClose = false;
    }
}
