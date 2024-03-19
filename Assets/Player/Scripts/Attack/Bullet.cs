using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private Rigidbody _rigidbody;

    private float _damage;

    public float damage
    {
        get
        {
            Destroy(gameObject, 0.1f);
            return _damage;
        }

        private set => _damage = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.AddForce(transform.forward * _bulletSpeed);
    }

    public void Shoot(Transform transform, float damage)
    {
        var bullet =  Instantiate(gameObject, transform.position, transform.rotation);

        bullet.GetComponent<Bullet>().damage = damage;
    }
}