using UnityEngine;

public class Gizmo : MonoBehaviour
{
    [Header("Settings")]
    public GizmoType drawType;
    public Color gizmoColor = Color.yellow;
    public float gizmoSizeAndRadius = 1.0f;
    public Vector3 gizmoNoUniformSize = new Vector3(1.0f, 1.0f, 1.0f);
    public bool UniformSize = true;


    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        if (UniformSize)
        {
            if (drawType == GizmoType.QUAD)
                Gizmos.DrawCube(transform.position, new Vector3(gizmoSizeAndRadius, gizmoSizeAndRadius, gizmoSizeAndRadius));
            else if (drawType == GizmoType.QUAD_WIRE)
                Gizmos.DrawWireCube(transform.position, new Vector3(gizmoSizeAndRadius, gizmoSizeAndRadius, gizmoSizeAndRadius));
        }
        else
        {
            if (drawType == GizmoType.QUAD)
                Gizmos.DrawCube(transform.position, new Vector3(gizmoNoUniformSize.x, gizmoNoUniformSize.y, gizmoNoUniformSize.z));
            else if (drawType == GizmoType.QUAD_WIRE)
                Gizmos.DrawWireCube(transform.position, new Vector3(gizmoNoUniformSize.x, gizmoNoUniformSize.y, gizmoNoUniformSize.z));
        }

        if (drawType == GizmoType.SPHERE)
            Gizmos.DrawSphere(transform.position, gizmoSizeAndRadius);
        else if (drawType == GizmoType.SPHERE_WIRE)
            Gizmos.DrawWireSphere(transform.position, gizmoSizeAndRadius);

    }
}
