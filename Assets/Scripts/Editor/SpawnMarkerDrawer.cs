using Common;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerDrawer : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
        public static void RenderCustomGizmo(SpawnMarker marker, GizmoType gizmoType)
        {
            Gizmos.color = marker.Color;
            Gizmos.DrawSphere(marker.transform.position, marker.Radius);
        }
    }
}