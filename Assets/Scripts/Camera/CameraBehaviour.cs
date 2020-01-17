using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{
    [Header("References")]
    public Transform target;

    [Header("Behaviour")]
    Vector3 movePoint;


    void Start()
    {
        UpdatePosition();
    }


    void LateUpdate()
    {
        UpdatePosition();
    }


    private void UpdatePosition()
    {
        if (target != null)
            movePoint = target.position;
        else return;

        this.transform.position = new Vector3(movePoint.x, this.transform.position.y, movePoint.z);
    }
}
