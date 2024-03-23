using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ThrowStone : MonoBehaviour
{
    private float _damage;

    public float damage => _damage;

    public void Shoot(Vector3 position, Vector3 target, float force, float damage)
    {
        var stone = Instantiate(gameObject, position, Quaternion.identity);

        var rb = stone.GetComponent<Rigidbody>();
        var st = stone.GetComponent<ThrowStone>();

        rb.AddForce((target - position) * force);
        st._damage = damage;
    }

    public void Spawn(Transform transform, Vector3 position, float damage, float lifeTime)
    {
        var stone = Instantiate(gameObject, transform);
        stone.transform.position = position;

        var st = stone.GetComponent<ThrowStone>();

        st._damage = damage;
        Destroy(st.gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger ==  false)
        {
            Destroy(gameObject);
        }
    }
}