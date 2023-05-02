using System.Linq;
using Common;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelData))]
    public class SpawnMarkerCollectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var data = (LevelData) target;
            
            if(GUILayout.Button("Collect"))
            {
                data.SpawnPoints = FindObjectsOfType<SpawnMarker>().Select(marker => marker.transform.position).ToList();
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}