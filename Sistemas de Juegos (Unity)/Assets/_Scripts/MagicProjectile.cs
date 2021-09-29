using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class MagicProjectile : MonoBehaviour, IProjectile, IPooleable
{
    [Header("Visual Effects Objects")]
    [SerializeField] private GameObject _projectileParticles;
    [SerializeField] private GameObject _projectileImpactParticles;
    [SerializeField] private GameObject _projectileMuzzleParticles;
    [SerializeField] private GameObject[] _projectileTrailParticles;

    [SerializeField] private GameObject _OGprojectileParticles;
    [SerializeField] private GameObject _OGprojectileImpactParticles;
    [SerializeField] private GameObject _OGprojectileMuzzleParticles;
    [SerializeField] private GameObject[] _OGprojectileTrailParticles;

    [Header("Projectile Info")]
    [SerializeField] private float _lifeDuration = 10f;
    private float _lifeDurationOriginal;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _myRigidbody;
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private Weapon _owner;

    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.
    private bool hasCollided = false;

    private void Awake()
    {
        _OGprojectileParticles = _projectileParticles;
        _OGprojectileImpactParticles = _projectileImpactParticles;
        _OGprojectileMuzzleParticles = _projectileMuzzleParticles;
        _OGprojectileTrailParticles = _projectileTrailParticles;
    }

    private void Start()
    {
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        _speed = _owner.WeaponStats.WeaponSpellSpeed;
        _lifeDurationOriginal = _lifeDuration;

        _projectileParticles = Instantiate(_OGprojectileParticles, transform.position, transform.rotation) as GameObject;
        _projectileParticles.transform.SetParent(transform);

        if (_projectileMuzzleParticles)
        {
            _projectileMuzzleParticles = Instantiate(_OGprojectileMuzzleParticles, transform.position, transform.rotation) as GameObject;
            Destroy(_projectileMuzzleParticles, 1.5f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((_targetLayers & 1 << other.gameObject.layer) != 0)
        {
            var character = other.GetComponent<Enemy>();

            if (character != null)
            {
                character.TakeDamage(_owner.WeaponStats.WeaponDamage);
                CameraShaker.Instance.ShakeOnce(2f, 4f, 0.1f, 1f);
            }
            

            if (!hasCollided)
            {
                hasCollided = true;

                foreach (Transform child in this.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                _projectileImpactParticles = Instantiate(_OGprojectileImpactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

                foreach (GameObject trail in _projectileTrailParticles)
                {
                    GameObject curTrail = transform.Find(_projectileParticles.name + "/" + trail.name).gameObject;
                    curTrail.transform.parent = null;
                    Destroy(curTrail, 3f);
                }
                Destroy(_projectileParticles, 3f);
                Destroy(_projectileImpactParticles, 5f);
                Reset();
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
        _lifeDuration -= Time.deltaTime;

        if (_lifeDuration <= 0)
        {
            Reset();
        }

        else
        {
            Vector3 movement = transform.forward * _speed * Time.deltaTime;
            _myRigidbody.MovePosition(transform.position + movement);
        }

    }

    private void Update()
    {
        Travel();
    }

    // IPoolable.
    public bool Active { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public object Owner { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public void Reset()
    {
        transform.position = Vector3.one * 9000;
        _lifeDuration = _lifeDurationOriginal;
        gameObject.SetActive(false);
        GameManager.Instance.PlayerBulletsPool.Recycle(this.gameObject);
        hasCollided = false;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        SpawnProjectile();
    }
}
