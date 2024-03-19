using System.Runtime.InteropServices;
using UnityEngine;

public abstract class CameraState
{
    private float _minAngle;

    private float _maxAngle;

    private float _rotateSpeed;

    private Transform _transform;

    private Transform _followObject;

    public float minAngle
    {
        get => _minAngle;

        private set => _minAngle = value;
    }

    public float maxAngle
    {
        get => _maxAngle;

        private set => _maxAngle = value;
    }

    public float rotateSpeed
    {
        get => _rotateSpeed;

        private set => _rotateSpeed = value;
    }

    public Transform transform
    {
        get => _transform;

        private set => _transform = value;
    }

    public Transform followObject
    {
        get => _followObject;

        private set => _followObject = value;
    }

    public Vector3 localAngle
    {
        get => transform.localEulerAngles;

        protected set => transform.localEulerAngles = value;
    }

    public Vector3 followObjectAngle
    {
        get => followObject.eulerAngles;

        protected set => followObject.eulerAngles = value;
    }

    public CameraState(GameObject gameObject, float rotateSpeed)
    {
        transform = gameObject.transform;

        followObject = transform.parent;

        this.rotateSpeed = rotateSpeed;
    }

    public void SetAngles(float min, float max)
    {
        if (min < max)
        {
            minAngle = min;
            maxAngle = max;
        }
        else
        {
            minAngle = max;
            maxAngle = min;
        }
    }

    public abstract void Rotate();
}