using TankGame.App.Environment;
using UnityEditor;
using UnityEngine;

namespace TankGame.App.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
        public static void RenderCustomGizmo(SpawnMarker marker, GizmoType gizmoType)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(marker.transform.position, 0.25f);
        }
    }
}