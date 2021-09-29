using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Skeleton Spawners")]
    [SerializeField] private LootableReward _lootableReward;
    [SerializeField] private Spawner<Skeleton> _skeletonSpawner = new Spawner<Skeleton>();
    [SerializeField] private List<Skeleton> _skeletonsPrefabs;
    [SerializeField] private SphereCollider _spawnerTrigger;
    [SerializeField] private GameObject _spawnerRangeTrigger;


    private void Start()
    {
        _spawnerRangeTrigger.transform.localScale = Vector3.one * _spawnerTrigger.radius * 2;
    }

    [ContextMenu("Spawn Enemies")]
    private void SpawnSkeletons()
    {
        for (int i = 0; i < 3; i++)
        {
            Skeleton skelly = _skeletonSpawner.Create(_skeletonsPrefabs[Random.Range(0, _skeletonsPrefabs.Count)]);
            skelly.transform.position = transform.position + (Random.insideUnitSphere * 4f);
            skelly.transform.position = new Vector3(skelly.transform.position.x, 0, skelly.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnSkeletons();
        }
    }

}
