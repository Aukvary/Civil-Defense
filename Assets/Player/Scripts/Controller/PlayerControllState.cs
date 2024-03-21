using UnityEngine;

public abstract class PlayerControllState
{
    public bool IsTarget = true;

    private float _speed;

    private Animator _animator;

    private Vector3 _direction;

    private Transform _transform;

    public virtual float speed
    {
        get => _speed;

        set
        {
            if (value > 0) _speed = value;
        }
    }

    public Animator animator
    {
        get => _animator;

        private set => _animator = value;
    }

    public Vector3 direction
    {
        get => _direction;

        protected set => _direction = value;
    }

    public Transform transform
    {
        get => _transform;

        private set => _transform = value;
    }

    public PlayerControllState(GameObject gameObject, Animator animator)
    {
        transform = gameObject.transform;

        this.animator = animator;
    }

    public abstract void Update();

    public abstract void FixedUpdate();
}