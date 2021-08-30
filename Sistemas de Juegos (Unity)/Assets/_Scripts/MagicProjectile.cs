using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour, IProjectile
{
    [Header("Visual Effects Objects")]
    [SerializeField] private GameObject _projectileParticles;
    [SerializeField] private GameObject _projectileImpactParticles;
    [SerializeField] private GameObject _projectileMuzzleParticles;
    [SerializeField] private GameObject[] _projectileTrailParticles;

    [Header("Projectile Info")]
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _myRigidbody;
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private Weapon _owner;

    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.
    private bool hasCollided = false;

    private void Start()
    {
        _speed = _owner.WeaponStats.WeaponSpellSpeed;



        _projectileParticles = Instantiate(_projectileParticles, transform.position, transform.rotation) as GameObject;
        _projectileParticles.transform.SetParent(transform);

        if (_projectileMuzzleParticles)
        {
            _projectileMuzzleParticles = Instantiate(_projectileMuzzleParticles, transform.position, transform.rotation) as GameObject;
            Destroy(_projectileMuzzleParticles, 1.5f);
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        if ((_targetLayers & 1 << other.gameObject.layer) != 0)
        {
            other.GetComponent<Character>()?.TakeDamage(_owner.WeaponStats.WeaponDamage);

            if (!hasCollided)
            {
                hasCollided = true;

                _projectileImpactParticles = Instantiate(_projectileImpactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

                foreach (GameObject trail in _projectileTrailParticles)
                {
                    GameObject curTrail = transform.Find(_projectileParticles.name + "/" + trail.name).gameObject;
                    curTrail.transform.parent = null;
                    Destroy(curTrail, 3f);
                }
                Destroy(_projectileParticles, 3f);
                Destroy(_projectileImpactParticles, 5f);
                Destroy(gameObject);
                //projectileParticle.Stop();

                ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
                //Component at [0] is that of the parent i.e. this object (if there is any)
                for (int i = 1; i < trails.Length; i++)
                {
                    ParticleSystem trail = trails[i];
                    if (!trail.gameObject.name.Contains("Trail"))
                        continue;

                    trail.transform.SetParent(null);
                    Destroy(trail.gameObject, 2);
                }
            }
        }
    }

    public void SetOwner(Weapon owner)
    {
        _owner = owner;
    }

    public void Travel()
    {
        Vector3 movement = transform.forward * _speed * Time.deltaTime;
        _myRigidbody.MovePosition(transform.position + movement);
    }

    private void Update()
    {
        Travel();
    }
}
