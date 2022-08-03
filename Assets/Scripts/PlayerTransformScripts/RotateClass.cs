using UnityEngine;

public class RotateClass
{
    private readonly GameObject _rotationObj;

    public int CurrentRotationState;
    
    public RotateClass(GameObject main)
    {
        _rotationObj = main;
    }

    public void TurnLeft()
    {
        _rotationObj.transform.rotation = Quaternion.AngleAxis(180f, Vector3.down);
        CurrentRotationState = -1;
    }

    public void TurnRight()
    {
        _rotationObj.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        CurrentRotationState = 1;
    }
}
