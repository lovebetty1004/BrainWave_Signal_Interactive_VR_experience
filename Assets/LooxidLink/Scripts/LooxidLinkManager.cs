/**
 * @author   Looxidlabs
 * @version  1.0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Looxid.Link
{
    public class LooxidLinkManager : MonoBehaviour
    {
        #region Singleton

        private static LooxidLinkManager _instance;
        public static LooxidLinkManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(LooxidLinkManager)) as LooxidLinkManager;
                    if (_instance == null)
                    {
                        _instance = new GameObject("LooxidLinkManager").AddComponent<LooxidLinkManager>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        #endregion



        #region Variables

        private NetworkManager networkManager;

        private LinkCoreStatus linkCoreStatus = LinkCoreStatus.Disconnected;
        private LinkHubStatus linkHubStatus = LinkHubStatus.Disconnected;

        private bool isInitialized = false;

        [HideInInspector]
        public bool isLinkCoreConnected = false;
        [HideInInspector]
        public bool isLinkHubConnected = false;

        private bool isPrevLinkCoreConnected = false;
        private bool isPrevLinkHubConnected = false;

        public static System.Action OnLinkCoreConnected;
        public static System.Action OnLinkCoreDisconnected;
        public static System.Action OnLinkHubConnected;
        public static System.Action OnLinkHubDisconnected;

        // Colors
        public static Color32 linkColor = new Color32(124, 64, 254, 255);

        #endregion


        #region MonoBehavior Life Cycle

        void OnEnable()
        {
            if (isInitialized && linkCoreStatus == LinkCoreStatus.Disconnected)
            {
                StartCoroutine(AutoConnection());
            }
        }

        void OnApplicationQuit()
        {
            isInitialized = false;
        }

        void Update()
        {
            if ( Input.GetKeyUp(KeyCode.X) )
            {
                isInitialized = false;
                //networkManager.DisconnectMessage();
            }

            if (networkManager == null) return;

            linkCoreStatus = networkManager.LinkCoreStatus;
            isLinkCoreConnected = (linkCoreStatus == LinkCoreStatus.Connected);

            linkHubStatus = networkManager.LinkHubStatus;
            isLinkHubConnected = (linkHubStatus == LinkHubStatus.Connected);

            if (isLinkCoreConnected != isPrevLinkCoreConnected)
            {
                if (isLinkCoreConnected)
                {
                    if (OnLinkCoreConnected != null) OnLinkCoreConnected.Invoke();
                }
                else
                {
                    if (OnLinkCoreDisconnected != null) OnLinkCoreDisconnected.Invoke();
                }
            }
            isPrevLinkCoreConnected = isLinkCoreConnected;

            if (isLinkHubConnected != isPrevLinkHubConnected)
            {
                if (isLinkHubConnected)
                {
                    if (OnLinkHubConnected != null) OnLinkHubConnected.Invoke();
                }
                else
                {
                    if (OnLinkHubDisconnected != null) OnLinkHubDisconnected.Invoke();
                }
            }
            isPrevLinkHubConnected = isLinkHubConnected;
        }

        #endregion



        #region Initialize

        public bool Initialize()
        {
            LXDebug.Log("Initialized!");

            if (networkManager == null)
            {
                networkManager = gameObject.AddComponent<NetworkManager>();
            }
            isInitialized = networkManager.Initialize();

            StartCoroutine(AutoConnection());

            return true;
        }

        #endregion



        #region Connect & Disconnect

        IEnumerator AutoConnection()
        {
            LXDebug.Log("Connect to LooxidLink...");
            while (true)
            {
                if (isInitialized && linkCoreStatus == LinkCoreStatus.Disconnected)
                {
                    networkManager.Connect();
                }
                yield return new WaitForSeconds(1.0f);
            }
        }

        #endregion


        #region Debug Setting

        public void SetDebug(bool isDebug)
        {
            LXDebug.isDebug = isDebug;
        }

        #endregion
    }
}