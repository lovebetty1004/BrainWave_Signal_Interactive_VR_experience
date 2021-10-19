using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using OfficeOpenXml;
using System.IO;
using Tobii.XR;
using Tobii.G2OM;

namespace Looxid.Link
{
    public enum Tab2DVisualizer
    {
        SENSOR_STATUS = 0,
        MIND_INDEX = 1,
        FEATURE_INDEX = 2,
        RAW_SIGNAL = 3
    }
    // public enum Filename{Situation1, Situation2, Situation3, Situaion4};

    public class _2DVisualizer : MonoBehaviour
    {
        // List<string> Filename = new List<string>();


        // [Header("Excel Filename")]
        // public Filename Workbookname;
        [Header("Tabs")]
        public Tab2DVisualizer SelectTab = Tab2DVisualizer.SENSOR_STATUS;
        public GameObject[] Tabs;
        public GameObject[] Panels;

        [Header("Sensor Status")]
        public Image AF3SensorImage;
        public Image AF4SensorImage;
        public Image Fp1SensorImage;
        public Image Fp2SensorImage;
        public Image AF7SensorImage;
        public Image AF8SensorImage;

        [Header("Mind Index")]
        public BarIndicator LeftActivityIndicator;
        public BarIndicator RightActivityIndicator;
        public BarIndicator AttentionIndicator;
        public BarIndicator RelaxationIndicator;

        [Header("Feature Index")]
        public EEGSensorID SelectChannel;
        public BarIndicator DeltaIndicator;
        public BarIndicator ThetaIndicator;
        public BarIndicator AlphaIndicator;
        public BarIndicator BetaIndicator;
        public BarIndicator GammaIndicator;
        public Toggle[] ChannelToggles;

        [Header("Raw Signal")]
        public LineChart AF3Chart;
        public LineChart AF4Chart;
        public LineChart Fp1Chart;
        public LineChart Fp2Chart;
        public LineChart AF7Chart;
        public LineChart AF8Chart;

        private Color32 BackColor = new Color32(255, 255, 255, 255);
        private Color32 TextColor = new Color32(10, 10, 10, 255);

        void Start()
        {
            
            LooxidLinkManager.Instance.SetDebug(true);
            LooxidLinkManager.Instance.Initialize();
            
        }
        void OnEnable()
        {
            StartCoroutine(RecieveData());
        }

        IEnumerator RecieveData()
        {

            while (this.gameObject.activeSelf)
            {
                if (this.SelectTab == Tab2DVisualizer.SENSOR_STATUS)
                {
                    Color32 offColor = new Color32(64, 64, 64, 255);

                    EEGSensor AF3Sensor = LooxidLinkData.GetEEGSensorData(EEGSensorID.AF3);
                    AF3SensorImage.color = AF3Sensor.isSensorOn ? (Color)LooxidLinkManager.linkColor : (Color)offColor;

                    EEGSensor AF4Sensor = LooxidLinkData.GetEEGSensorData(EEGSensorID.AF4);
                    AF4SensorImage.color = AF4Sensor.isSensorOn ? (Color)LooxidLinkManager.linkColor : (Color)offColor;

                    EEGSensor Fp1Sensor = LooxidLinkData.GetEEGSensorData(EEGSensorID.Fp1);
                    Fp1SensorImage.color = Fp1Sensor.isSensorOn ? (Color)LooxidLinkManager.linkColor : (Color)offColor;

                    EEGSensor Fp2Sensor = LooxidLinkData.GetEEGSensorData(EEGSensorID.Fp2);
                    Fp2SensorImage.color = Fp2Sensor.isSensorOn ? (Color)LooxidLinkManager.linkColor : (Color)offColor;

                    EEGSensor AF7Sensor = LooxidLinkData.GetEEGSensorData(EEGSensorID.AF7);
                    AF7SensorImage.color = AF7Sensor.isSensorOn ? (Color)LooxidLinkManager.linkColor : (Color)offColor;

                    EEGSensor AF8Sensor = LooxidLinkData.GetEEGSensorData(EEGSensorID.AF8);
                    AF8SensorImage.color = AF8Sensor.isSensorOn ? (Color)LooxidLinkManager.linkColor : (Color)offColor;
                }
                else if (this.SelectTab == Tab2DVisualizer.MIND_INDEX)
                {
                    MindIndex mindIndexData = LooxidLinkData.GetMindIndexData();
                    LeftActivityIndicator.SetValue((float)LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndexData.leftActivity));
                    RightActivityIndicator.SetValue((float)LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndexData.rightActivity));
                    AttentionIndicator.SetValue((float)LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndexData.attention));
                    RelaxationIndicator.SetValue((float)LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndexData.relaxation));
                }
                else if (this.SelectTab == Tab2DVisualizer.FEATURE_INDEX)
                {
                    // featurecount++;
                    List<EEGFeatureIndex> featureIndexList = LooxidLinkData.GetEEGFeatureIndexData(SelectChannel, 10.0f);

                    if (featureIndexList.Count > 0)
                    {
                        List<double> deltaDataList = new List<double>();
                        List<double> thetaDataList = new List<double>();
                        List<double> alphaDataList = new List<double>();
                        List<double> betaDataList = new List<double>();
                        List<double> gammaDataList = new List<double>();
                        for (int i = 0; i < featureIndexList.Count; i++)
                        {
                            deltaDataList.Add(featureIndexList[i].delta);
                            thetaDataList.Add(featureIndexList[i].theta);
                            alphaDataList.Add(featureIndexList[i].alpha);
                            betaDataList.Add(featureIndexList[i].beta);
                            gammaDataList.Add(featureIndexList[i].gamma);
                        }
                        //Write Data Out

                        DeltaIndicator.SetValue((float)LooxidLinkUtility.Scale(deltaDataList.Min(), deltaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].delta));
                        ThetaIndicator.SetValue((float)LooxidLinkUtility.Scale(thetaDataList.Min(), thetaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].theta));
                        AlphaIndicator.SetValue((float)LooxidLinkUtility.Scale(alphaDataList.Min(), alphaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].alpha));
                        BetaIndicator.SetValue((float)LooxidLinkUtility.Scale(betaDataList.Min(), betaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].beta));
                        GammaIndicator.SetValue((float)LooxidLinkUtility.Scale(gammaDataList.Min(), gammaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].gamma));
                    }
                }
                else if (this.SelectTab == Tab2DVisualizer.RAW_SIGNAL)
                {
                    EEGRawSignal AF3Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.AF3);
                    EEGRawSignal AF4Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.AF4);
                    EEGRawSignal Fp1Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.Fp1);
                    EEGRawSignal Fp2Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.Fp2);
                    EEGRawSignal AF7Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.AF7);
                    EEGRawSignal AF8Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.AF8);
                    // for(int i = 0; i < AF3Sensor.filteredRawSignal.Count; i++)
                    //     Debug.Log("test ="+ AF3Sensor.filteredRawSignal[i]);
                    AF3Chart.SetValue(AF3Sensor.filteredRawSignal);
                    AF4Chart.SetValue(AF4Sensor.filteredRawSignal);
                    Fp1Chart.SetValue(Fp1Sensor.filteredRawSignal);
                    Fp2Chart.SetValue(Fp2Sensor.filteredRawSignal);
                    AF7Chart.SetValue(AF7Sensor.filteredRawSignal);
                    AF8Chart.SetValue(AF8Sensor.filteredRawSignal);
                }

                yield return new WaitForSeconds(1.0f);
            }
        }

        public void OnClickTabSensorStatus()
        {
            this.SelectTab = Tab2DVisualizer.SENSOR_STATUS;
        }
        public void OnClickTabMindIndex()
        {
            this.SelectTab = Tab2DVisualizer.MIND_INDEX;
        }
        public void OnClickTabFeatureIndex()
        {
            this.SelectTab = Tab2DVisualizer.FEATURE_INDEX;
        }
        public void OnClickTabRawSignal()
        {
            this.SelectTab = Tab2DVisualizer.RAW_SIGNAL;
        }
        public void OnClickLeftButton()
        {
            if (SelectTab > 0) SelectTab--;
            else SelectTab = (Tab2DVisualizer)System.Enum.GetValues(typeof(Tab2DVisualizer)).Length - 1;
        }
        public void OnClickRightButton()
        {
            if (SelectTab < (Tab2DVisualizer)System.Enum.GetValues(typeof(Tab2DVisualizer)).Length - 1) SelectTab++;
            else SelectTab = 0;
        }

        void Update()
        {
            if (Panels != null)
            {
                for (int i = 0; i < Panels.Length; i++)
                {
                    Panels[i].SetActive((Tab2DVisualizer)i == SelectTab);
                }
            }
            if (Tabs != null)
            {
                for (int i = 0; i < Tabs.Length; i++)
                {
                    Image TabImage = Tabs[i].GetComponent<Image>();
                    if (TabImage != null) TabImage.color = ((Tab2DVisualizer)i == SelectTab) ? (Color)LooxidLinkManager.linkColor : (Color)BackColor;

                    Text TabText = Tabs[i].GetComponent<Text>();
                    if (TabText != null) TabText.color = ((Tab2DVisualizer)i == SelectTab) ? BackColor : TextColor;

                    LaserPointerInputItem TabInputItem = Tabs[i].GetComponent<LaserPointerInputItem>();
                    if (TabInputItem != null)
                    {
                        TabInputItem.SetNormalColor(((Tab2DVisualizer)i == SelectTab) ? (Color)LooxidLinkManager.linkColor : (Color)BackColor);
                        TabInputItem.SetTextNormalColor(((Tab2DVisualizer)i == SelectTab) ? BackColor : TextColor);
                    }
                }
            }
            if (ChannelToggles != null)
            {
                for (int i = 0; i < ChannelToggles.Length; i++)
                {
                    ChannelToggles[i].isOn = ((EEGSensorID)i == SelectChannel);
                }
            }
        }

        public void OnSelectChannel(int num)
        {
            this.SelectChannel = (EEGSensorID)num;
        }
    }
}
