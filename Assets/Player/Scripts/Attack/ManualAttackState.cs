using UnityEngine;

public class ManualAttackState : AttackState
{
    private Camera _camera;

    private Ray _viewDirection => _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

    public ManualAttackState(Bullet bullet, Transform spawnerTransform, Camera camera) :
        base(bullet, spawnerTransform)
    {
        _camera = camera;
    }

    public override void Attack(float damage)
    {
        SetTimer();
        if (!Input.GetKeyDown(KeyCode.Mouse0) || !IsTarget)
            return;

        RaycastHit hit;

        if (!Physics.Raycast(_viewDirection, out hit)) return;

        spawnerTransform.LookAt(hit.point);
        Shoot(damage);
    }
}
