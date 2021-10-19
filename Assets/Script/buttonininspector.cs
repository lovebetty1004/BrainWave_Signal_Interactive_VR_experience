using UnityEngine;
using UnityEditor;
using Looxid.Link;
using UnityEngine.Playables;


[CustomEditor(typeof(Record))]
public class buttonininspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Record a = (Record)target;
        if (GUILayout.Button("start record"))
        {
            a.startrecord();
            a.easyhint.SetActive(false);
            a.normalhint.SetActive(false);
            a.hardhint.SetActive(false);
        }
        else if (GUILayout.Button("start without record"))
        {
            a.easyhint.SetActive(false);
            a.normalhint.SetActive(false);
            a.hardhint.SetActive(false);
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
