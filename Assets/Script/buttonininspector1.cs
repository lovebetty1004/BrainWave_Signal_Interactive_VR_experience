using UnityEngine;
using UnityEditor;
using Looxid.Link;
using UnityEngine.Playables;


[CustomEditor(typeof(Record1))]
public class buttonininspector1 : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Record1 a = (Record1)target;
        if (GUILayout.Button("start record"))
        {
            a.startrecord();
        }
        else if (GUILayout.Button("start without record"))
        {
        }
        else if (GUILayout.Button("write to excel 1"))
        {
            a.number = 0;
            a.savetoexcel();
        }
        else if (GUILayout.Button("write to excel 2"))
        {
            a.number = 1;
            a.savetoexcel();
        }
        else if (GUILayout.Button("write to excel 3"))
        {
            a.number = 2;
            a.savetoexcel();
        }
    }
}
