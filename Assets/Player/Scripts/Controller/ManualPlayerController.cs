using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ManualPlayerController : PlayerControllState
{
    private float _jumpForce;

    private float _gravity;

    private float _fallVelocity;

    private CharacterController _controller;

    private int _runAnimation;

    private bool isLand => _controller.isGrounded;

    public ManualPlayerController(GameObject gameobject, Animator animator, float jumpForce, float gravity) :
        base(gameobject, animator)
    {
        _controller = gameobject.GetComponent<CharacterController>();
        _jumpForce = jumpForce;
        _gravity = gravity;
    }

    public override void Update()
    {
        Walk();
    }

    public override void FixedUpdate()
    {
        if (!IsTarget)
            return;

        _controller.Move(direction * Time.fixedDeltaTime);

        _fallVelocity += _gravity * Time.fixedDeltaTime;

        _controller.Move(Vector3.down * _fallVelocity);

        if (isLand)
            _fallVelocity = 0;
    }

    private void Walk()
    {
        var direction = Vector3.zero;

        _runAnimation = 0;

        if (_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _fallVelocity -= _jumpForce;
            animator.SetTrigger("Jump");
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
            _runAnimation = 1;
        }

        if(Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
            _runAnimation = 1;
        }

        if( Input.GetKey(KeyCode.S))
        {
            direction -= transform.forward;
            _runAnimation = 1;
        }

        if(Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
            _runAnimation = 1;
        }
        animator.SetInteger("RunAnimation", _runAnimation);

        base.direction = direction * speed;
    }
}
