using UnityEngine;

public class ManualPlayerController : PlayerControllState
{
    private float _jumpForce;

    private float _gravity;

    private float _fallVelocity = 1;

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

    public override void SetDirection() => 
        direction = Walk() + Jump();

    public override void Move()
    {
        if (!IsTarget)
            return;
        _fallVelocity += _gravity * Time.fixedDeltaTime;

        _controller.Move(direction * Time.fixedDeltaTime);
    }

    private Vector3 Walk()
    {
        var direction = Vector3.zero;

        _runAnimation = 0;

        if(Input.GetKey(KeyCode.W))
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

        return direction * speed;
    }

    private Vector3 Jump()
    {
        var direction = Vector3.down;
        if (isLand && Input.GetKeyDown(KeyCode.Space))
        {
            direction += Vector3.up * _jumpForce;
            animator.SetTrigger("Jump");
        }

        return direction;
    }
}
