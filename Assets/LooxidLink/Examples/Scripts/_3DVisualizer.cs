using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Looxid.Link
{
    public enum Tab3DVisualizer
    {
        MIND_INDEX = 0,
        FEATURE_INDEX = 1,
        RAW_SIGNAL = 2
    }

    public enum FeatureIndexEnum
    {
        DELTA = 0,
        THETA = 1,
        ALPHA = 2,
        BETA = 3,
        GAMMA = 4
    }

    public class _3DVisualizer : MonoBehaviour
    {

        [Header("Tabs")]
        public Tab3DVisualizer SelectTab;
        public GameObject MindIndexTab;
        public GameObject FeatureIndexTab;
        public GameObject RawSignalTab;

        [Header("Canvas")]
        public GameObject MindIndexCanvas;
        public GameObject FeatureIndexCanvas;
        public GameObject RawSignalCanvas;

        [Header("Sensor")]
        public GameObject SensorCanvas;
        
        [Header("Panels")]
        public GameObject[] FeatureIndexPanels;
        public GameObject[] RawSignalPanels;

        [Header("Effect")]
        public BrainEffect LeftEffect;
        public BrainEffect[] LeftLineEffect;
        public BrainEffect RightEffect;
        public BrainEffect[] RightLineEffect;

        [Header("Mind Index")]
        public BarChart AttentionChart;
        public BarChart RelaxationChart;
        public BarChart LeftActivityChart;
        public BarChart RightActivityChart;

        [Header("Feature index")]
        public FeatureIndexEnum SelectFeature;
        public Text SelectFeatureText;
        public BarChart FeatureAF3Chart;
        public BarChart FeatureAF4Chart;
        public BarChart FeatureFp1Chart;
        public BarChart FeatureFp2Chart;
        public BarChart FeatureAF7Chart;
        public BarChart FeatureAF8Chart;

        [Header("Raw Signal")]
        public LineChart RawAF3Chart;
        public LineChart RawAF4Chart;
        public LineChart RawFp1Chart;
        public LineChart RawFp2Chart;
        public LineChart RawAF7Chart;
        public LineChart RawAF8Chart;


        void Start()
        {
            OnMindIndexTabClick();

            LooxidLinkManager.Instance.SetDebug(true);
            LooxidLinkManager.Instance.Initialize();
        }

        void OnEnable()
        {
            StartCoroutine(RecieveData());
        }

        public void OnMindIndexTabClick()
        {
            this.SelectTab = Tab3DVisualizer.MIND_INDEX;
        }
        public void OnFeatureIndexTabClick()
        {
            this.SelectTab = Tab3DVisualizer.FEATURE_INDEX;
        }
        public void OnRawSignalTabClick()
        {
            this.SelectTab = Tab3DVisualizer.RAW_SIGNAL;
        }

        IEnumerator RecieveData()
        {
            while (this.gameObject.activeSelf)
            {
                if (SelectTab == Tab3DVisualizer.MIND_INDEX)
                {
                    List<MindIndex> mindIndex = LooxidLinkData.GetMindIndexData(4.0f);

                    List<double> attentionDataList = new List<double>();
                    List<double> relaxationDataList = new List<double>();
                    List<double> leftActivityDataList = new List<double>();
                    List<double> rightActivityDataList = new List<double>();
                    for (int i = 0; i < mindIndex.Count; i++)
                    {
                        attentionDataList.Add(LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndex[i].attention));
                        relaxationDataList.Add(LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndex[i].relaxation));
                        leftActivityDataList.Add(LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndex[i].leftActivity));
                        rightActivityDataList.Add(LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndex[i].rightActivity));
                    }

                    AttentionChart.SetValue(attentionDataList);
                    RelaxationChart.SetValue(relaxationDataList);
                    LeftActivityChart.SetValue(leftActivityDataList);
                    RightActivityChart.SetValue(rightActivityDataList);
                }
                else if (SelectTab == Tab3DVisualizer.FEATURE_INDEX)
                {
                    FeatureAF3Chart.SetValue(GetFeatureDataList(EEGSensorID.AF3));
                    FeatureAF4Chart.SetValue(GetFeatureDataList(EEGSensorID.AF4));
                    FeatureFp1Chart.SetValue(GetFeatureDataList(EEGSensorID.Fp1));
                    FeatureFp2Chart.SetValue(GetFeatureDataList(EEGSensorID.Fp2));
                    FeatureAF7Chart.SetValue(GetFeatureDataList(EEGSensorID.AF7));
                    FeatureAF8Chart.SetValue(GetFeatureDataList(EEGSensorID.AF8));
                }
                else if (SelectTab == Tab3DVisualizer.RAW_SIGNAL)
                {
                    EEGRawSignal AF3Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.AF3);
                    RawAF3Chart.SetValue(AF3Sensor.filteredRawSignal);

                    EEGRawSignal AF4Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.AF4);
                    RawAF4Chart.SetValue(AF4Sensor.filteredRawSignal);

                    EEGRawSignal Fp1Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.Fp1);
                    RawFp1Chart.SetValue(Fp1Sensor.filteredRawSignal);

                    EEGRawSignal Fp2Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.Fp2);
                    RawFp2Chart.SetValue(Fp2Sensor.filteredRawSignal);

                    EEGRawSignal AF7Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.AF7);
                    RawAF7Chart.SetValue(AF7Sensor.filteredRawSignal);

                    EEGRawSignal AF8Sensor = LooxidLinkData.GetEEGRawSignalData(EEGSensorID.AF8);
                    RawAF8Chart.SetValue(AF8Sensor.filteredRawSignal);
                }


                /*float left = LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, (float)LooxidLinkData.GetEEGFeatureData().left_activity);
                    float right = LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, (float)LooxidLinkData.GetEEGFeatureData().right_activity);

                    LeftEffect.alphaValue = 100 + (left * 200);
                    for (int i = 0; i < LeftLineEffect.Length; i++)
                    {
                        LeftLineEffect[i].alphaValue = 10 + (left * 20);
                    }
                    RightEffect.alphaValue = 100 + (right * 200);
                    for (int i = 0; i < LeftLineEffect.Length; i++)
                    {
                        RightLineEffect[i].alphaValue = 10 + (right * 20);
                    }*/

                yield return new WaitForSeconds(0.1f);
            }
        }

        private List<double> GetFeatureDataList(EEGSensorID sensorID)
        {
            List<EEGFeatureIndex> featureScaleList = LooxidLinkData.GetEEGFeatureIndexData(sensorID, 10.0f);
            List<double> ScaleDataList = new List<double>();
            if (featureScaleList.Count > 0)
            {
                for (int i = 0; i < featureScaleList.Count; i++)
                {
                    if (SelectFeature == FeatureIndexEnum.DELTA) ScaleDataList.Add(featureScaleList[i].delta);
                    if (SelectFeature == FeatureIndexEnum.THETA) ScaleDataList.Add(featureScaleList[i].theta);
                    if (SelectFeature == FeatureIndexEnum.ALPHA) ScaleDataList.Add(featureScaleList[i].alpha);
                    if (SelectFeature == FeatureIndexEnum.BETA) ScaleDataList.Add(featureScaleList[i].beta);
                    if (SelectFeature == FeatureIndexEnum.GAMMA) ScaleDataList.Add(featureScaleList[i].gamma);
                }
            }

            List<EEGFeatureIndex> featureDataList = LooxidLinkData.GetEEGFeatureIndexData(sensorID, 4.0f);
            List<double> dataList = new List<double>();
            if (featureDataList.Count > 0)
            {

                //double delta = LooxidLinkUtility.Scale(ScaleDataList.Min(), ScaleDataList.Max(), 0.0f, 1.0f, featureDataList[0].delta);
                //Debug.Log(ScaleDataList.Min() + " - ( " + featureDataList[0].delta + " > " + delta + " ) - " + ScaleDataList.Max());

                for (int i = 0; i < featureDataList.Count; i++)
                {
                    if (SelectFeature == FeatureIndexEnum.DELTA) dataList.Add(LooxidLinkUtility.Scale(ScaleDataList.Min(), ScaleDataList.Max(), 0.0f, 1.0f, featureDataList[i].delta));
                    if (SelectFeature == FeatureIndexEnum.THETA) dataList.Add(LooxidLinkUtility.Scale(ScaleDataList.Min(), ScaleDataList.Max(), 0.0f, 1.0f, featureDataList[i].theta));
                    if (SelectFeature == FeatureIndexEnum.ALPHA) dataList.Add(LooxidLinkUtility.Scale(ScaleDataList.Min(), ScaleDataList.Max(), 0.0f, 1.0f, featureDataList[i].alpha));
                    if (SelectFeature == FeatureIndexEnum.BETA) dataList.Add(LooxidLinkUtility.Scale(ScaleDataList.Min(), ScaleDataList.Max(), 0.0f, 1.0f, featureDataList[i].beta));
                    if (SelectFeature == FeatureIndexEnum.GAMMA) dataList.Add(LooxidLinkUtility.Scale(ScaleDataList.Min(), ScaleDataList.Max(), 0.0f, 1.0f, featureDataList[i].gamma));
                }
            }
            return dataList;
        }

        void FixedUpdate()
        {
            if (MindIndexTab != null) MindIndexTab.SetActive(SelectTab == Tab3DVisualizer.MIND_INDEX);
            if (FeatureIndexTab != null) FeatureIndexTab.SetActive(SelectTab == Tab3DVisualizer.FEATURE_INDEX);
            if (RawSignalTab != null) RawSignalTab.SetActive(SelectTab == Tab3DVisualizer.RAW_SIGNAL);

            if (MindIndexCanvas != null) MindIndexCanvas.SetActive(SelectTab == Tab3DVisualizer.MIND_INDEX);
            if (FeatureIndexCanvas != null) FeatureIndexCanvas.SetActive(SelectTab == Tab3DVisualizer.FEATURE_INDEX);
            if (RawSignalCanvas != null) RawSignalCanvas.SetActive(SelectTab == Tab3DVisualizer.RAW_SIGNAL);

            if (SensorCanvas != null) SensorCanvas.SetActive(SelectTab != Tab3DVisualizer.MIND_INDEX);
        }

        public void OnAllSensorOnButton()
        {
            if (SelectTab == Tab3DVisualizer.FEATURE_INDEX)
            {
                for (int i = 0; i < FeatureIndexPanels.Length; i++)
                {
                    FeatureIndexPanels[i].SetActive(true);
                }
            }
            if (SelectTab == Tab3DVisualizer.RAW_SIGNAL)
            {
                for (int i = 0; i < RawSignalPanels.Length; i++)
                {
                    RawSignalPanels[i].SetActive(true);
                }
            }
        }
        public void OnAllSensorOffButton()
        {
            if (SelectTab == Tab3DVisualizer.FEATURE_INDEX)
            {
                for (int i = 0; i < FeatureIndexPanels.Length; i++)
                {
                    FeatureIndexPanels[i].SetActive(false);
                }
            }
            if (SelectTab == Tab3DVisualizer.RAW_SIGNAL)
            {
                for (int i = 0; i < RawSignalPanels.Length; i++)
                {
                    RawSignalPanels[i].SetActive(false);
                }
            }
        }
        public void OnSensorButton(int num)
        {
            if (SelectTab == Tab3DVisualizer.FEATURE_INDEX)
            {
                FeatureIndexPanels[num].SetActive(!FeatureIndexPanels[num].activeSelf);
            }
            if (SelectTab == Tab3DVisualizer.RAW_SIGNAL)
            {
                RawSignalPanels[num].SetActive(!RawSignalPanels[num].activeSelf);
            }
        }

        public void OnFeatureIndexDeltaButton()
        {
            SelectFeature = FeatureIndexEnum.DELTA;
            SelectFeatureText.text = ("Delta (1~3Hz)");
        }
        public void OnFeatureIndexThetaButton()
        {
            SelectFeature = FeatureIndexEnum.THETA;
            SelectFeatureText.text = ("Theta(4~7Hz)");
        }
        public void OnFeatureIndexAlphaButton()
        {
            SelectFeature = FeatureIndexEnum.ALPHA;
            SelectFeatureText.text = ("Alpha (8~12Hz)");
        }
        public void OnFeatureIndexBetaButton()
        {
            SelectFeature = FeatureIndexEnum.BETA;
            SelectFeatureText.text = ("Beta (13~30Hz)");
        }
        public void OnFeatureIndexGammaButton()
        {
            SelectFeature = FeatureIndexEnum.GAMMA;
            SelectFeatureText.text = ("Gamma (31~45Hz)");
        }
    }
}