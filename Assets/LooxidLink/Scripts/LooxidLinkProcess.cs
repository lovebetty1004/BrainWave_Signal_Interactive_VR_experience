using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using Looxid.Link;

public class LooxidLinkProcess
{
    [RuntimeInitializeOnLoadMethod]
    static void RuntimeInitWrapper()
    {
        #if !UNITY_EDITOR
            OnInit();
        #endif
    }
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#endif
    static void OnInit() {
        string roamingFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        string looxidlabsFolderPath = "looxidlabs";
        string looxidlinkFolderPath = "LooxidLinkCore";
        string appLocationFileName = "app_location.txt";

        string roamingFile = roamingFolderPath + "\\" + looxidlabsFolderPath + "\\" + looxidlinkFolderPath + "\\" + appLocationFileName;
        if (!File.Exists(roamingFile))
        {
            LXDebug.LogError("Roaming file is corrupted: " + roamingFile);
        }
        StreamReader reader = new System.IO.StreamReader(roamingFile);

        string appPath = reader.ReadToEnd();
        if (!File.Exists(appPath))
        {
            LXDebug.LogError("Core file is corrupted: " + appPath);
        }

        //LXDebug.Log("Core App Start: " + appPath);
        Process.Start(appPath);
    }
}