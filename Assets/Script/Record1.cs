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
using UnityEngine.SceneManagement;

namespace Looxid.Link
{

    public class Record1 : MonoBehaviour
    {

        // public PlayableDirector timeline1;
        // public PlayableDirector timeline2;
        // public PlayableDirector timeline3;
        // public PlayableDirector timeline4;
        // List<string> Filename = new List<string>();
        

        [Header("Excel Filename")]
        public string Workbookname;
        public int Playtime;
        //[Header("Situation")]
        //public enum Situation { Situation1, Situation2, Situation3, Situaion4, Situation5, Situation6 };
        [HideInInspector]
        public List<float> attentionlist = new List<float>();
        [HideInInspector]
        public List<float> relaxationlist = new List<float>();
        [HideInInspector]
        public List<float> mindcountlist = new List<float>();
        [HideInInspector]
        public List<float> AF3deltalist = new List<float>();
        [HideInInspector]
        public List<float> AF3thetalist = new List<float>();
        [HideInInspector]
        public List<float> AF3alphalist = new List<float>();
        [HideInInspector]
        public List<float> AF3betalist = new List<float>();
        [HideInInspector]
        public List<float> AF3gammalist = new List<float>();
        [HideInInspector]
        public List<float> AF4deltalist = new List<float>();
        [HideInInspector]
        public List<float> AF4thetalist = new List<float>();
        [HideInInspector]
        public List<float> AF4alphalist = new List<float>();
        [HideInInspector]
        public List<float> AF4betalist = new List<float>();
        [HideInInspector]
        public List<float> AF4gammalist = new List<float>();
        [HideInInspector]
        public List<float> AF7deltalist = new List<float>();
        [HideInInspector]
        public List<float> AF7thetalist = new List<float>();
        [HideInInspector]
        public List<float> AF7alphalist = new List<float>();
        [HideInInspector]
        public List<float> AF7betalist = new List<float>();
        [HideInInspector]
        public List<float> AF7gammalist = new List<float>();
        [HideInInspector]
        public List<float> AF8deltalist = new List<float>();
        [HideInInspector]
        public List<float> AF8thetalist = new List<float>();
        [HideInInspector]
        public List<float> AF8alphalist = new List<float>();
        [HideInInspector]
        public List<float> AF8betalist = new List<float>();
        [HideInInspector]
        public List<float> AF8gammalist = new List<float>();
        [HideInInspector]
        public List<float> FP1deltalist = new List<float>();
        [HideInInspector]
        public List<float> FP1thetalist = new List<float>();
        [HideInInspector]
        public List<float> FP1alphalist = new List<float>();
        [HideInInspector]
        public List<float> FP1betalist = new List<float>();
        [HideInInspector]
        public List<float> FP1gammalist = new List<float>();
        [HideInInspector]
        public List<float> FP2deltalist = new List<float>();
        [HideInInspector]
        public List<float> FP2thetalist = new List<float>();
        [HideInInspector]
        public List<float> FP2alphalist = new List<float>();
        [HideInInspector]
        public List<float> FP2betalist = new List<float>();
        [HideInInspector]
        public List<float> FP2gammalist = new List<float>();
        [HideInInspector]
        public List<float> eyexlist = new List<float>();
        [HideInInspector]
        public List<float> eyeylist = new List<float>();
        [HideInInspector]
        public List<float> eyezlist = new List<float>();
        [HideInInspector]
        public List<float> directionx = new List<float>();
        [HideInInspector]
        public List<float> directiony = new List<float>();
        [HideInInspector]
        public List<float> directionz = new List<float>();
        [HideInInspector]
        public List<float> target1 = new List<float>();
        [HideInInspector]
        public List<float> target2 = new List<float>();

        FileInfo CreateExcel(string workbookname)
        {
            string outPutDir = Application.dataPath + "\\" + workbookname + ".xls";
            FileInfo newFile = new FileInfo(outPutDir);
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook   
                Debug.Log("Delete File");
                newFile = new FileInfo(outPutDir);
            }
            return newFile;
        }
        void createFeatureIndexExcel(EEGSensorID channel, List<float> delta, List<float> theta, List<float> alpha, List<float> beta, List<float> gamma)
        {
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
                delta.Add((float)LooxidLinkUtility.Scale(deltaDataList.Min(), deltaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].delta));
                theta.Add((float)LooxidLinkUtility.Scale(thetaDataList.Min(), thetaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].theta));
                alpha.Add((float)LooxidLinkUtility.Scale(alphaDataList.Min(), alphaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].alpha));
                beta.Add((float)LooxidLinkUtility.Scale(betaDataList.Min(), betaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].beta));
                gamma.Add((float)LooxidLinkUtility.Scale(gammaDataList.Min(), gammaDataList.Max(), 0.0f, 1.0f, featureIndexList[0].gamma));
                //count;
            }
        }
        ExcelWorksheet Addsheet(ExcelPackage package, string name)
        {
            ExcelWorksheet featuresheet;
            featuresheet = package.Workbook.Worksheets.Add(name);
            featuresheet.Cells[1, 1].Value = "Delta";
            featuresheet.Cells[1, 2].Value = "Theta";
            featuresheet.Cells[1, 3].Value = "Alpha";
            featuresheet.Cells[1, 4].Value = "Beta";
            featuresheet.Cells[1, 5].Value = "Gamma";
            featuresheet.Cells[1, 6].Value = "Time";
            return featuresheet;
        }

        // public void writeyedata(ExcelWorksheet sheet, int count)
        // {
        //     sheet.Cells[count, 7].Value= 1;
        // }
        [HideInInspector]
        public FileInfo excelfile;
        // int Mindcount = 0;
        public ExcelPackage[] package = new ExcelPackage[6];
        ExcelWorksheet[] worksheet = new ExcelWorksheet[6];
        ExcelWorksheet[] worksheet1 = new ExcelWorksheet[6];
        ExcelWorksheet[] worksheet2 = new ExcelWorksheet[6];
        ExcelWorksheet[] worksheet3 = new ExcelWorksheet[6];
        ExcelWorksheet[] worksheet4 = new ExcelWorksheet[6];
        ExcelWorksheet[] worksheet5 = new ExcelWorksheet[6];
        ExcelWorksheet[] worksheet6 = new ExcelWorksheet[6];

        public ExcelWorksheet[] worksheet7 = new ExcelWorksheet[6];
        [HideInInspector]
        public bool flag = false;
        [HideInInspector]
        public bool flag2 = false;
        [HideInInspector]
        public int eyecount;
        [HideInInspector]
        public int number = 0;
        [HideInInspector]
        public int mindcount;

        //public bool flag = true;

        // Start is called before the first frame update

        void Start()
        {

            // timeline1 = GetComponent<PlayableDirector>();
            // timeline1.time = 0;
            // timeline1.Evaluate();
            // timeline1.Stop();
            // timeline2.time = 0;
            // timeline2.Evaluate();
            // timeline2.Stop();
            // timeline3.time = 0;
            // timeline3.Evaluate();
            // timeline3.Stop();
            // timeline4.time = 0;
            // timeline4.Evaluate();
            // timeline4.Stop();
            //Debug.Log(timeline1.time);
            //number = (int)tmp;
            // Debug.Log("number = " + number);
            for (int i = 0; i < 3; i++)
            {
                string name = (i + 1).ToString();
                //Debug.Log("name = "+name);
                excelfile = CreateExcel(Workbookname + "_" + name);
                package[i] = new ExcelPackage(excelfile);
                Debug.Log("package complete");
                worksheet[i] = package[i].Workbook.Worksheets.Add("MindIndex");
                worksheet[i].Cells[1, 1].Value = "Attention";
                worksheet[i].Cells[1, 2].Value = "Relaxation";
                worksheet[i].Cells[1, 3].Value = "Time";
                worksheet1[i] = Addsheet(package[i], "FeatureIndex_AF3");
                worksheet2[i] = Addsheet(package[i], "FeatureIndex_AF4");
                worksheet3[i] = Addsheet(package[i], "FeatureIndex_AF7");
                worksheet4[i] = Addsheet(package[i], "FeatureIndex_AF8");
                worksheet5[i] = Addsheet(package[i], "FeatureIndex_FP1");
                worksheet6[i] = Addsheet(package[i], "FeatureIndex_FP2");
                worksheet7[i] = package[i].Workbook.Worksheets.Add("EyeData");
                worksheet7[i].Cells[1, 1].Value = "Origin_x";
                worksheet7[i].Cells[1, 2].Value = "Origin_y";
                worksheet7[i].Cells[1, 3].Value = "Origin_z";
                worksheet7[i].Cells[1, 4].Value = "Direction_x";
                worksheet7[i].Cells[1, 5].Value = "Direction_y";
                worksheet7[i].Cells[1, 6].Value = "Direction_z";
                worksheet7[i].Cells[1, 7].Value = "Object1";
                worksheet7[i].Cells[1, 8].Value = "Object2";
                package[i].Save();

            }

        }

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        public void startrecord()
        {
            Debug.Log("START RECORD");
            attentionlist.Clear();
            relaxationlist.Clear();
            mindcountlist.Clear();
            AF3deltalist.Clear();
            AF3thetalist.Clear();
            AF3alphalist.Clear();
            AF3betalist.Clear();
            AF3gammalist.Clear();
            AF4deltalist.Clear();
            AF4thetalist.Clear();
            AF4alphalist.Clear();
            AF4betalist.Clear();
            AF4gammalist.Clear();
            AF7deltalist.Clear();
            AF7thetalist.Clear();
            AF7alphalist.Clear();
            AF7betalist.Clear();
            AF7gammalist.Clear();
            FP1deltalist.Clear();
            FP1thetalist.Clear();
            FP1alphalist.Clear();
            FP1betalist.Clear();
            FP1gammalist.Clear();
            FP2deltalist.Clear();
            FP2thetalist.Clear();
            FP2alphalist.Clear();
            FP2betalist.Clear();
            FP2gammalist.Clear();
            eyexlist.Clear();
            eyeylist.Clear();
            eyezlist.Clear();
            directionx.Clear();
            directiony.Clear();
            directionz.Clear();
            target1.Clear();
            target2.Clear();
            flag = true;
            flag2 = false;
            // Debug.Log("click start");
            //if(flag == true)
            StartCoroutine(RecieveData());

        }
        IEnumerator RecieveData()
        {
            mindcount = 1;
            float time = 0;
            //int featurecount = 1;
            //float couttime = 0f;
            eyecount = 1;
            // List<double> testlist = new List<double>();
            while (this.gameObject.activeSelf && flag == true)
            {
                if (mindcount == Playtime+1)
                {
                    Debug.Log("break");
                    break;
                }
                    
                // Debug.Log(mindcount);
                mindcount++;

                MindIndex mindIndexData = LooxidLinkData.GetMindIndexData();
                // Check if gaze ray is valid
                RaycastHit hit;
                Ray ray;
                //eyecount += 1;
                var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
                if (eyeTrackingData.GazeRay.IsValid)
                {
                    // The origin of the gaze ray is a 3D point
                    var rayOrigin = eyeTrackingData.GazeRay.Origin;

                    // The direction of the gaze ray is a normalized direction vector
                    var rayDirection = eyeTrackingData.GazeRay.Direction;

                    //ray = new Ray(rayOrigin, rayDirection);
                    Debug.DrawRay(rayOrigin, rayDirection * 1000f, Color.red, 2);
                    // Debug.Log("Origin = "+rayOrigin.x);
                    // Debug.Log("Direction = "+rayDirection.x);
                    eyexlist.Add(rayOrigin.x);
                    eyeylist.Add(rayOrigin.y);
                    eyezlist.Add(rayOrigin.z);
                    directionx.Add(rayDirection.x);
                    directiony.Add(rayDirection.y);
                    directionz.Add(rayDirection.z);
                    target1.Add(2f);
                    target2.Add(2f);
                    //worksheet7[number].Cells[eyecount, 7].Value = 2;
                    //Debug.Log("writevalue to 2");
                    //worksheet7[number].Cells[eyecount, 8].Value = 2;
                }
                else
                {
                    eyexlist.Add(-1.0f);
                    eyeylist.Add(-1.0f);
                    eyezlist.Add(-1.0f);
                    directionx.Add(-1.0f);
                    directiony.Add(-1.0f);
                    directionz.Add(-1.0f);
                    target1.Add(-1f);
                    target2.Add(-1f);
                }

                float attention = (float)LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndexData.attention);
                float relaxation = (float)LooxidLinkUtility.Scale(LooxidLink.MIND_INDEX_SCALE_MIN, LooxidLink.MIND_INDEX_SCALE_MAX, 0.0f, 1.0f, mindIndexData.relaxation);
                attentionlist.Add(attention);
                relaxationlist.Add(relaxation);
                if (time <= 15.5f)
                    mindcountlist.Add(time);
                else
                {
                    time = 0;
                    mindcountlist.Add(time);
                }

                createFeatureIndexExcel(EEGSensorID.AF3, AF3deltalist, AF3thetalist, AF3alphalist, AF3betalist, AF3gammalist);
                createFeatureIndexExcel(EEGSensorID.AF4, AF4deltalist, AF4thetalist, AF4alphalist, AF4betalist, AF4gammalist);
                createFeatureIndexExcel(EEGSensorID.AF7, AF7deltalist, AF7thetalist, AF7alphalist, AF7betalist, AF7gammalist);
                createFeatureIndexExcel(EEGSensorID.AF8, AF8deltalist, AF8thetalist, AF8alphalist, AF8betalist, AF8gammalist);
                createFeatureIndexExcel(EEGSensorID.Fp1, FP1deltalist, FP1thetalist, FP1alphalist, FP1betalist, FP1gammalist);
                createFeatureIndexExcel(EEGSensorID.Fp2, FP2deltalist, FP2thetalist, FP2alphalist, FP2betalist, FP2gammalist);
                time += 0.5f;
                yield return new WaitForSeconds(0.5f);

                //Debug.Log("check finish");
            }
            //Debug.Log("mindlistlength = " + attentionlist.Count + relaxationlist.Count);
            //Debug.Log(mindcount);
        }
       public void savetoexcel()
        {
            //if(flag2 == true)
            //{
            Debug.Log("count" + attentionlist.Count+"||"+ AF3deltalist.Count);
                for (int i = 0; i < attentionlist.Count; i++)
                {
                    worksheet[number].Cells[i + 2, 1].Value = attentionlist[i];
                    worksheet[number].Cells[i + 2, 2].Value = relaxationlist[i];
                    worksheet[number].Cells[i + 2, 3].Value = mindcountlist[i];
                    worksheet1[number].Cells[i + 2, 1].Value = AF3deltalist[i];
                    worksheet1[number].Cells[i + 2, 2].Value = AF3thetalist[i];
                    worksheet1[number].Cells[i + 2, 3].Value = AF3alphalist[i];
                    worksheet1[number].Cells[i + 2, 4].Value = AF3betalist[i];
                    worksheet1[number].Cells[i + 2, 5].Value = AF3gammalist[i];
                    worksheet1[number].Cells[i + 2, 6].Value = mindcountlist[i];
                    worksheet2[number].Cells[i + 2, 1].Value = AF4deltalist[i];
                    worksheet2[number].Cells[i + 2, 2].Value = AF4thetalist[i];
                    worksheet2[number].Cells[i + 2, 3].Value = AF4alphalist[i];
                    worksheet2[number].Cells[i + 2, 4].Value = AF4betalist[i];
                    worksheet2[number].Cells[i + 2, 5].Value = AF4gammalist[i];
                    worksheet2[number].Cells[i + 2, 6].Value = mindcountlist[i];
                    worksheet3[number].Cells[i + 2, 1].Value = AF7deltalist[i];
                    worksheet3[number].Cells[i + 2, 2].Value = AF7thetalist[i];
                    worksheet3[number].Cells[i + 2, 3].Value = AF7alphalist[i];
                    worksheet3[number].Cells[i + 2, 4].Value = AF7betalist[i];
                    worksheet3[number].Cells[i + 2, 5].Value = AF7gammalist[i];
                    worksheet3[number].Cells[i + 2, 6].Value = mindcountlist[i];
                    worksheet4[number].Cells[i + 2, 1].Value = AF8deltalist[i];
                    worksheet4[number].Cells[i + 2, 2].Value = AF8thetalist[i];
                    worksheet4[number].Cells[i + 2, 3].Value = AF8alphalist[i];
                    worksheet4[number].Cells[i + 2, 4].Value = AF8betalist[i];
                    worksheet4[number].Cells[i + 2, 5].Value = AF8gammalist[i];
                    worksheet4[number].Cells[i + 2, 6].Value = mindcountlist[i];
                    worksheet5[number].Cells[i + 2, 1].Value = FP1deltalist[i];
                    worksheet5[number].Cells[i + 2, 2].Value = FP1thetalist[i];
                    worksheet5[number].Cells[i + 2, 3].Value = FP1alphalist[i];
                    worksheet5[number].Cells[i + 2, 4].Value = FP1betalist[i];
                    worksheet5[number].Cells[i + 2, 5].Value = FP1gammalist[i];
                    worksheet5[number].Cells[i + 2, 6].Value = mindcountlist[i];
                    worksheet6[number].Cells[i + 2, 1].Value = FP2deltalist[i];
                    worksheet6[number].Cells[i + 2, 2].Value = FP2thetalist[i];
                    worksheet6[number].Cells[i + 2, 3].Value = FP2alphalist[i];
                    worksheet6[number].Cells[i + 2, 4].Value = FP2betalist[i];
                    worksheet6[number].Cells[i + 2, 5].Value = FP2gammalist[i];
                    worksheet6[number].Cells[i + 2, 6].Value = mindcountlist[i];
                    worksheet7[number].Cells[i + 2, 1].Value = eyexlist[i];
                    worksheet7[number].Cells[i + 2, 2].Value = eyeylist[i];
                    worksheet7[number].Cells[i + 2, 3].Value = eyezlist[i];
                    worksheet7[number].Cells[i + 2, 4].Value = directionx[i];
                    worksheet7[number].Cells[i + 2, 5].Value = directiony[i];
                    worksheet7[number].Cells[i + 2, 6].Value = directionz[i];
                    worksheet7[number].Cells[i + 2, 7].Value = target1[i];
                    //Debug.Log("writevalue to 2");
                    worksheet7[number].Cells[i + 2, 8].Value = target2[i];
                    worksheet7[number].Cells[i + 2, 9].Value = mindcountlist[i];

                if (i == Playtime-1)
                        Debug.Log("Write to excel successfully!");
                }
                package[number].Save();

            //}
        }
        
}
}

