using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Skeleton Spawners")]
    [SerializeField] private LootableReward _lootableReward;
    [SerializeField] private Spawner<Skeleton> _skeletonSpawner = new Spawner<Skeleton>();
    [SerializeField] private BreakableObject _breakableComponent;
    [SerializeField] private List<Skeleton> _skeletonsPrefabs;
    [SerializeField] private List<Skeleton> _skeletonsSelected;
    [SerializeField] private SphereCollider _spawnerTrigger;
    [SerializeField] private GameObject _spawnerRangeTrigger;

    private bool _isEnabled = false;


    private void Start()
    {
        // _spawnerRangeTrigger.transform.localScale = Vector3.one * _spawnerTrigger.radius * 2;
        _isEnabled = true;
        
    }

    [ContextMenu("Spawn")]
    private void SpawnSkeletons()
    {
        if (_isEnabled)
        {
            foreach (var item in _skeletonsSelected)
            {
                Skeleton skelly = _skeletonSpawner.Create(_skeletonsSelected[Random.Range(0, _skeletonsSelected.Count)]);
                skelly.transform.position = transform.position + (Random.insideUnitSphere * 4f);
                skelly.transform.position = new Vector3(skelly.transform.position.x, 0, skelly.transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _breakableComponent.IsBreakable == false)
        {
            SpawnSkeletons();
            _breakableComponent.IsBreakable = true;
            _spawnerRangeTrigger.SetActive(false);
        }
    }

    public void DisableSpawner()
    {
        _isEnabled = false;
        _spawnerRangeTrigger.SetActive(false);
        GameManager.Instance.SpawnersKilled++;
    }

}
