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
    public class movable : MonoBehaviour
    {
        // public Record ExcelManager ;
        public bool easy;
        public bool medium;
        public bool hard;
        public Rigidbody rb;
        public GameObject easyhint;
        public GameObject normalhint;
        public GameObject hardhint;
        public GameObject success;
        public GameObject blur_effect;
        bool timerReached = false;
        float timer = 0;
        float rockactive ;
        float rockpush ;
        public AudioSource audio_stone_fall;
        public GameObject audio_start;
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
            if(easy)
            {
                rockactive = 2;
                rockpush= 2.5f;
                easyhint.SetActive(true);
            }
            else if(medium)
            {
                rockactive = 3;
                rockpush = 2f;
                normalhint.SetActive(true);
            }
            else if(hard)
            {
                rockactive = 4;
                rockpush = 1.5f;
                hardhint.SetActive(true);
            }
            Debug.Log("rockactive = " + rockactive );
            rb = GetComponent<Rigidbody>();
            audio_stone_fall=GetComponent<AudioSource>();
            success.SetActive(false);
            blur_effect.SetActive(true);
        }
        void blur()
        {
            if(easyhint.active==true || normalhint.active==true || hardhint.active==true || success.active==true)
            {
                blur_effect.SetActive(true);
            }
            else
            {
                blur_effect.SetActive(false);
            }    
        }
        // Update is called once per frame
        void FixedUpdate()
        {   
            blur();
            // Debug.Log(timerReached);
            if(easyhint.active==false && normalhint.active==false && hardhint.active==false)
            {   
                audio_start.SetActive(true);
                if (!timerReached)
                {
                    timer += Time.deltaTime;
                    Debug.Log("waiting"+timer);
                }
                if (!timerReached && timer > 3)
                {
                    Debug.Log("Run ControlRock");
                    ControlRock();
                    // ExcelManager.startrecord();
                    timerReached = true;
                }
                if (timerReached==true)
                {
                    ControlRock();
                }
            }            
        }
        void ControlRock()
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
            if(this.transform.position.y >= 17)
            {
                Debug.Log("constraintsY");
                rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
            if(this.transform.position.z <= -150)
            {
                Debug.Log("constraintsZ");
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
            }
            if (b1 + b2 + b3 + b4 + b5 + b6 >= rockactive && this.transform.position.y < 17)
            {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(0, rockpush ,0);
            }
                // rb.constraints = RigidbodyConstraints.FreezePositionY;
            else if(b1 + b2 + b3 + b4 + b5 + b6 < rockactive)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(0, -rockpush ,0);
            }

            else if(this.transform.position.y >= 17 && this.transform.position.z > -150 && b1 + b2 + b3 + b4 + b5 + b6 >= rockactive)
            {    
                rb.AddForce(0, 0 ,-rockpush);
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag=="Terrain" && easyhint.active==false && normalhint.active==false && hardhint.active==false)
            {
                audio_stone_fall.Play();
            }
        }
        void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.tag=="WINAREA")
            {
                Debug.Log("WIN");
                success.SetActive(true);
            }
        }
    }
}
