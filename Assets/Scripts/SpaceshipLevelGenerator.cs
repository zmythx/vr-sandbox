using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CustomEditor(typeof(SpaceshipLevelGenerator))]
[CanEditMultipleObjects] // only if you handle it properly
public class SpaceshipLevelGenerator : UnityEditor.Editor
{
    // Start is called before the first frame update
    
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("DO THAT", EditorStyles.miniButton))
        {
            ((SpaceshipLevelGenerator)this.target).GenerateTheLevel();
        }
        DrawDefaultInspector();
    }
    private void GenerateTheLevel()
    {

    }
}
