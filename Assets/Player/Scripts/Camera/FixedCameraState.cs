using UnityEngine;

public class FixedCameraState : CameraState
{
    public FixedCameraState(GameObject gameObject, float rotateSpeed) :
        base(gameObject, rotateSpeed){}

    public override void Rotate()
    {
        transform.position = followObject.transform.position + Vector3.up;
        float xAngle = localAngle.x - (Time.deltaTime * rotateSpeed * Input.GetAxis(Constants.MouseY));
        if (xAngle > 180)
            xAngle -= 360;

        xAngle = Mathf.Clamp(xAngle, minAngle, maxAngle);

        localAngle = new Vector3(xAngle, 0);

        float yAngle = followObjectAngle.y + (Time.deltaTime * rotateSpeed * Input.GetAxis(Constants.MouseX));

        followObjectAngle = new Vector3(0, yAngle);
    }
}
