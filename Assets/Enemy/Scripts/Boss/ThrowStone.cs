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

        Destroy(st.gameObject, 10f);
    }

    public void Spawn(Transform transform, Vector3 position, float damage, float lifeTime)
    {
        var stone = Instantiate(gameObject, position, Quaternion.identity);
        stone.transform.parent = transform;

        var st = stone.GetComponent<ThrowStone>();

        st._damage = damage;
        Destroy(st.gameObject, lifeTime);
    }
}