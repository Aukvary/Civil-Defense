using UnityEngine;
using UnityEngine.AI;

class AutoPlayerController : PlayerControllState
{
    private NavMeshAgent _navMeshAgent;

    private Camera _camera;

    public override float speed 
    { 
        get => base.speed; 
        set
        { 
            base.speed = value;
            _navMeshAgent.speed = value;
        } 
    }

    public Camera camera
    {
        get => _camera;

        set => _camera = value;
    }

    public AutoPlayerController(GameObject gameObject, Animator animator, Camera camera):
        base(gameObject, animator)
    {
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        _navMeshAgent.speed = speed;

        _camera = camera;
    }

    private Ray viewDirection => _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

    public override void FixedUpdate()
    {
        
    }

    public override void Update() 
    {
        SetAnimation();
        if (!Input.GetKeyDown(KeyCode.W) || !IsTarget)
            return;

        RaycastHit hit;

        if (!Physics.Raycast(viewDirection, out hit))
            return;

        direction = transform.forward;
        _navMeshAgent.destination = hit.point;
    }

    private void SetAnimation()
    {
        if (_navMeshAgent.hasPath)
            animator.SetInteger("RunAnimation", 1);
        else
            animator.SetInteger("RunAnimation", 0);
    }
}