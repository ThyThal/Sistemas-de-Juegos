using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableReward : MonoBehaviour, ILootable
{
    [SerializeField] private GameObject _reward;
    [SerializeField] private GameObject _lootRadius;
    [SerializeField] private bool _isPlayerClose;
    private GameObject _spawned;

    private void Start()
    {
        if (_reward != null)
        {
            _spawned = Instantiate(_reward, transform.position, Quaternion.identity);
            _spawned.transform.SetParent(transform);
        }
    }

    public void GrabLootable()
    {
        PlayerController player = GameManager.Instance.Player;

        if (_isPlayerClose == true)
        {
            if (player.PlayerInventory.CurrentWeapon != null)
            {
                player.PlayerInventory.RemoveWeapon();
            }

            player.PlayerInventory.LootWeapons(_spawned);
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter()
    {
        GameManager.Instance.Player.AddReward(this);
        _lootRadius.SetActive(false);
        _isPlayerClose = true;
    }

    public void OnTriggerExit()
    {
        GameManager.Instance.Player.RemoveReward();
        _lootRadius.SetActive(true);
        _isPlayerClose = false;
    }
}
