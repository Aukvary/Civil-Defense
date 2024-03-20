using UnityEngine;

public class FreeCameraState : CameraState
{
    public FreeCameraState(GameObject gameObject, float rotateSpeed) :
        base(gameObject, rotateSpeed){}

    public override void Rotate()
    {
        transform.position = followObject.position + Vector3.up;

        float xAngle = localAngle.x - (Time.deltaTime * rotateSpeed * Input.GetAxis(Constants.MouseY));

        if (xAngle > 180)
            xAngle -= 360;

        xAngle = Mathf.Clamp(xAngle, minAngle, maxAngle);

        float yAngle = localAngle.y + (Time.deltaTime * rotateSpeed * Input.GetAxis(Constants.MouseX));

        localAngle = new Vector3 (xAngle, yAngle);
    }
}
