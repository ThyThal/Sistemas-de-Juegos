using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableReward : MonoBehaviour, ILootable
{
    [SerializeField] private GameObject _reward;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _lootRarityRadius;
    [SerializeField] private GameObject _lootRarityBeam;
    [SerializeField] private AudioClip[] _pickupClips;
    [SerializeField] private AudioSource _pickupSource;
    [SerializeField] private bool _isPlayerClose;
    private GameObject _spawned;

    public GameObject Reward => _reward;
    public Weapon Weapon => _weapon;

    private void Start()
    {
        if (_reward != null)
        {
            _weapon = _reward.GetComponent<Weapon>();
            _spawned = Instantiate(_reward, transform.position, Quaternion.identity);
            _spawned.transform.SetParent(transform);
            _lootRarityBeam = Instantiate(_weapon.WeaponStats.RarityBeam, transform);
            _lootRarityRadius = Instantiate(_weapon.WeaponStats.RarityRadius, transform);
        }
    }

    public void GrabLootable()
    {
        PlayerController player = GameManager.Instance.Player;

        if (_isPlayerClose == true && _reward != null)
        {
            GameManager.Instance.TutorialManager.TutorialLoot.AddGrabbed();
            _pickupSource.PlayOneShot(_pickupClips[Random.Range(0, _pickupClips.Length)]);

            if (player.PlayerInventory.CurrentWeapon != null)
            {
                player.PlayerInventory.RemoveWeapon();
            }

            player.PlayerInventory.LootWeapons(_spawned);
            _lootRarityBeam.GetComponent<ParticleSystem>()?.Stop();
            _lootRarityRadius.GetComponent<ParticleSystem>()?.Stop();
            _weapon = null;
            _reward = null;
            //Destroy(this.gameObject);
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
