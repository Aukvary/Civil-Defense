using UnityEngine;

public abstract class Dome : MonoBehaviour
{
    private float _lifeTime;

    private float _currentLifeTime;

    public Dome Create(Vector3 position, float lifeTime)
    {
        var obj = Instantiate(gameObject, position, Quaternion.identity);

        var dome = obj.GetComponent<Dome>();

        dome._lifeTime = lifeTime;

        return dome;
    }

    private void Update()
    {
        if (_lifeTime > _currentLifeTime)
        {
            _currentLifeTime += Time.deltaTime;
            return;
        }
        DestroyDome();
    }

    public virtual void DestroyDome()
    {
        transform.Translate(0, -100, 0);
        Destroy(gameObject, 0.1f);
    }
}