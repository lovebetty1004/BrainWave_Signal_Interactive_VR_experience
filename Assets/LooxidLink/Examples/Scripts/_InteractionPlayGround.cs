using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

namespace Looxid.Link
{
    public enum InteractionData
    {
        ATTENTION = 0,
        RELAXATION = 1,
        LEFT_ACTIVTY = 2,
        RIGHT_ACTIVITY = 3,
        DELTA_INDEX = 4,
        THETA_INDEX = 5,
        ALPHA_INDEX = 6,
        BETA_INDEX = 7,
        GAMMA_INDEX = 8
    }

    public class _InteractionPlayGround : MonoBehaviour
    {
        public Valve.VR.SteamVR_Action_Boolean menuButtonAction = Valve.VR.SteamVR_Input.GetAction<Valve.VR.SteamVR_Action_Boolean>("InteractionMenu");

        private Player player = null;

        public _2DVisualizer visualizer;

        [Header("Cube")]
        public Transform CubeTransform;
        public GameObject[] CubeTypes;

        [Header("Mateirls")]
        public Material materialB;
        public Material materialO;
        public Material materialR;

        [Header("Selection")]
        public InteractionData selectInteraction;
        public Image[] outlines;

        private List<GameObject> CubeList = new List<GameObject>();

        private List<float> powerData;
        private bool isStart = false;
        private float prevPowerData = -1.0f;

        void Start()
        {
            for (int i = 0; i < CubeTypes.Length; i++)
            {
                CubeList.Add(CubeTypes[i].gameObject);
            }

            player = Player.instance;

            if (player == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No Player instance found in map.");
                Destroy(this.gameObject);
                return;
            }

            powerData = new List<float>();

            materialB.DisableKeyword("_EMISSION");
            materialO.DisableKeyword("_EMISSION");
            materialR.DisableKeyword("_EMISSION");
        }

        void FixedUpdate()
        {
            if (!LooxidLinkManager.Instance.isLinkCoreConnected) return;
            if (!isStart)
            {
                StartCoroutine(SuperPower());
                isStart = true;
            }

            float value = 0.0f;

            if (selectInteraction == InteractionData.ATTENTION)
            {
                MindIndex mindIndexData = LooxidLinkData.GetMindIndexData();
                value = LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, (float)mindIndexData.attention);
            }
            else if (selectInteraction == InteractionData.RELAXATION)
            {
                MindIndex mindIndexData = LooxidLinkData.GetMindIndexData();
                value = LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, (float)mindIndexData.relaxation);
            }
            else if (selectInteraction == InteractionData.LEFT_ACTIVTY)
            {
                MindIndex mindIndexData = LooxidLinkData.GetMindIndexData();
                value = LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, (float)mindIndexData.leftActivity);
            }
            else if (selectInteraction == InteractionData.RIGHT_ACTIVITY)
            {
                MindIndex mindIndexData = LooxidLinkData.GetMindIndexData();
                value = LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, (float)mindIndexData.rightActivity);
            }
            else if (selectInteraction == InteractionData.DELTA_INDEX)
            {
                List<EEGFeatureIndex> featureIndexScaleList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 5.0f);
                List<EEGFeatureIndex> featureIndexList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 1.0f);

                if (featureIndexList.Count > 0)
                {
                    List<double> deltaScaleDataList = new List<double>();
                    for (int i = 0; i < featureIndexScaleList.Count; i++)
                    {
                        deltaScaleDataList.Add(featureIndexScaleList[i].delta);
                    }
                    List<double> deltaDataList = new List<double>();
                    for (int i = 0; i < featureIndexList.Count; i++)
                    {
                        deltaDataList.Add(featureIndexList[i].delta);
                    }
                    value = (float)LooxidLinkUtility.Scale(deltaScaleDataList.Min(), deltaScaleDataList.Max(), 0.0f, 1.0f, deltaDataList.Average());
                }
            }
            else if (selectInteraction == InteractionData.THETA_INDEX)
            {
                List<EEGFeatureIndex> featureIndexScaleList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 5.0f);
                List<EEGFeatureIndex> featureIndexList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 1.0f);

                if (featureIndexList.Count > 0)
                {
                    List<double> thetaScaleDataList = new List<double>();
                    for (int i = 0; i < featureIndexScaleList.Count; i++)
                    {
                        thetaScaleDataList.Add(featureIndexScaleList[i].delta);
                    }
                    List<double> thetaDataList = new List<double>();
                    for (int i = 0; i < featureIndexList.Count; i++)
                    {
                        thetaDataList.Add(featureIndexList[i].delta);
                    }
                    value = (float)LooxidLinkUtility.Scale(thetaScaleDataList.Min(), thetaScaleDataList.Max(), 0.0f, 1.0f, thetaDataList.Average());
                }
            }
            else if (selectInteraction == InteractionData.ALPHA_INDEX)
            {
                List<EEGFeatureIndex> featureIndexScaleList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 5.0f);
                List<EEGFeatureIndex> featureIndexList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 1.0f);

                if (featureIndexList.Count > 0)
                {
                    List<double> alphaScaleDataList = new List<double>();
                    for (int i = 0; i < featureIndexScaleList.Count; i++)
                    {
                        alphaScaleDataList.Add(featureIndexScaleList[i].delta);
                    }
                    List<double> alphaDataList = new List<double>();
                    for (int i = 0; i < featureIndexList.Count; i++)
                    {
                        alphaDataList.Add(featureIndexList[i].delta);
                    }
                    value = (float)LooxidLinkUtility.Scale(alphaScaleDataList.Min(), alphaScaleDataList.Max(), 0.0f, 1.0f, alphaDataList.Average());
                }
            }
            else if (selectInteraction == InteractionData.BETA_INDEX)
            {
                List<EEGFeatureIndex> featureIndexScaleList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 5.0f);
                List<EEGFeatureIndex> featureIndexList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 1.0f);

                if (featureIndexList.Count > 0)
                {
                    List<double> betaScaleDataList = new List<double>();
                    for (int i = 0; i < featureIndexScaleList.Count; i++)
                    {
                        betaScaleDataList.Add(featureIndexScaleList[i].delta);
                    }
                    List<double> betaDataList = new List<double>();
                    for (int i = 0; i < featureIndexList.Count; i++)
                    {
                        betaDataList.Add(featureIndexList[i].delta);
                    }
                    value = (float)LooxidLinkUtility.Scale(betaScaleDataList.Min(), betaScaleDataList.Max(), 0.0f, 1.0f, betaDataList.Average());
                }
            }
            else if (selectInteraction == InteractionData.GAMMA_INDEX)
            {
                List<EEGFeatureIndex> featureIndexScaleList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 5.0f);
                List<EEGFeatureIndex> featureIndexList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 1.0f);

                if (featureIndexList.Count > 0)
                {
                    //test
                    // Debug.Log(featureIndexList);
                    List<double> gammaScaleDataList = new List<double>();
                    for (int i = 0; i < featureIndexScaleList.Count; i++)
                    {
                        gammaScaleDataList.Add(featureIndexScaleList[i].delta);
                    }
                    List<double> gammaDataList = new List<double>();
                    for (int i = 0; i < featureIndexList.Count; i++)
                    {
                        gammaDataList.Add(featureIndexList[i].delta);
                    }
                    value = (float)LooxidLinkUtility.Scale(gammaScaleDataList.Min(), gammaScaleDataList.Max(), 0.0f, 1.0f, gammaDataList.Average());
                }
            }

            powerData.Add(value);
        }

        void Update()
        {
            foreach (Hand hand in player.hands)
            {
                int randomRot = Random.Range(0, 360);
                int randomCube = Random.Range(0, CubeTypes.Length - 1);

                if (IsEligibleForTeleport(hand))
                {
                    bool isMenuButtonClick = menuButtonAction.GetStateUp(hand.handType);

                    if (isMenuButtonClick)
                    {
                        GameObject boxClone = Instantiate(CubeTypes[randomCube], new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 10f), Random.Range(-10f, 10f)),
                            Quaternion.Euler(new Vector3(randomRot, randomRot, randomRot)));
                        boxClone.transform.parent = CubeTransform;
                        CubeList.Add(boxClone);
                    }
                }
            }

            //trygetresult
            // List<EEGFeatureIndex> featureIndexScaleList = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 5.0f);
            
            //Use Feature Index to selectChannel
            // selectChannel = visualizer.AF3SensorImage;

            // List<EEGFeatureIndex> trydata = LooxidLinkData.GetEEGFeatureIndexData(visualizer.SelectChannel, 10.0f);
            // // Debug.Log(visualizer.AF3SensorImage);
            // for(int i = 0; i <trydata.Count; i++)
            // {
            //     Debug.Log("Delta = " + trydata[i].delta);
            //     Debug.Log("Alpha = " + trydata[i].alpha);
            //     Debug.Log("Theta = " + trydata[i].theta);
            //     // Debug.Log("" + trydata[i].)
            // }
            //     // Debug.Log("Data = " + trydata[i].alpha);
        }

        IEnumerator SuperPower()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f);

                if (prevPowerData < 0.0f)
                {
                    prevPowerData = powerData.Average();

                    materialB.DisableKeyword("_EMISSION");
                    materialO.DisableKeyword("_EMISSION");
                    materialR.DisableKeyword("_EMISSION");
                    Physics.gravity = new Vector3(0, -9.81f, 0f);
                }
                else
                {
                    if (powerData.Average() > prevPowerData)
                    {
                        materialB.EnableKeyword("_EMISSION");
                        materialO.EnableKeyword("_EMISSION");
                        materialR.EnableKeyword("_EMISSION");

                        float gravity_power = powerData.Average() * 1.5f;
                        Physics.gravity = new Vector3(0, gravity_power, 0);

                        for (int i = 0; i < CubeList.Count; i++)
                        {
                            CubeList[i].GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f)));
                        }
                    }
                    else
                    {
                        materialB.DisableKeyword("_EMISSION");
                        materialO.DisableKeyword("_EMISSION");
                        materialR.DisableKeyword("_EMISSION");
                        Physics.gravity = new Vector3(0, -9.81f, 0f);
                    }
                }

                prevPowerData = powerData.Average();
                powerData.Clear();
            }
        }

        public void SelectData(int dataType)
        {
            selectInteraction = (InteractionData)dataType;
            for ( int i = 0; i < outlines.Length; i++ )
            {
                outlines[i].color = (dataType == i) ? new Color(0.486f, 0.251f, 1.0f, 1.0f) : new Color(0.486f, 0.251f, 1.0f, 0.0f);
            }
            prevPowerData = -1.0f;
            powerData.Clear();
        }

        public bool IsEligibleForTeleport(Hand hand)
        {
            if (hand == null)
            {
                return false;
            }
            if (!hand.gameObject.activeInHierarchy)
            {
                return false;
            }
            if (hand.hoveringInteractable != null)
            {
                return false;
            }
            return true;
        }
    }
}
