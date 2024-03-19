class ExplosionDome : Dome
{
    private int _damage;

    public int damage
    {
        get => _damage;
        set
        {
            if (value > 0)
                _damage = value;
        }
    }

    public override void DestroyDome()
    {
        Destroy(gameObject);
    }
}