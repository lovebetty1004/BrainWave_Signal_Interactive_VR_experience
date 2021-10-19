using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using OfficeOpenXml;
using System.IO;
using Tobii.XR;
using Tobii.G2OM;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Playables;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Looxid.Link
{
    public class movable1 : MonoBehaviour
    {
        public Rigidbody rb;
        public float createFeatureIndexExcel(EEGSensorID channel)
        {
            float beta=0;
            List<EEGFeatureIndex> featureIndexList = LooxidLinkData.GetEEGFeatureIndexData(channel, 1.0f);
            // Debug.Log("LIstCount = "+featureIndexList.Count);
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
                beta = (float)LooxidLinkUtility.Scale(betaDataList.Min(), betaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].beta);
            }
            return beta;
        }
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Debug.Log(transform.position.y);
            float b1=0;
            float b2=0;
            float b3=0;
            float b4=0;
            float b5=0;
            float b6=0;
            b1 = createFeatureIndexExcel(EEGSensorID.AF3);
            b2 = createFeatureIndexExcel(EEGSensorID.AF4);
            b3 = createFeatureIndexExcel(EEGSensorID.AF7);
            b4 = createFeatureIndexExcel(EEGSensorID.AF8);
            b5 = createFeatureIndexExcel(EEGSensorID.Fp1);
            b6 = createFeatureIndexExcel(EEGSensorID.Fp2);
            Debug.Log(b1+b2+b3+b4+b5+b6);
            if(this.transform.position.y >= 20)
            {
                Debug.Log("constraintsY");
                rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
            if(this.transform.position.x >= 40)
            {
            }
            if (b1 + b2 + b3 + b4 + b5 + b6 >= 3 && this.transform.position.y < 20)
            {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(0, 2 ,0);
            }
                // rb.constraints = RigidbodyConstraints.FreezePositionY;
            else if(b1 + b2 + b3 + b4 + b5 + b6 < 3)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(0, -2 ,0);
            }

            else if(this.transform.position.y >= 20 && b1 + b2 + b3 + b4 + b5 + b6 >= 3)
                rb.AddForce(2, 0,0);
            
        }
    }
}
