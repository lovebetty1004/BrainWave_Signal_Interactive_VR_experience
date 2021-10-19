// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved
using System.IO;
using Tobii.G2OM;
using UnityEngine;
using Looxid.Link;
namespace Tobii.XR.Examples
{
//Monobehaviour which implements the "IGazeFocusable" interface, meaning it will be called on when the object receives focus
    public class HighlightAtGaze1 : MonoBehaviour, IGazeFocusable
    {
        private Color HighlightColor = Color.red;
        // public Color LeftColor = Color.yellow;
        // public Color RightColor = Color.green;
        [HideInInspector]
        public float AnimationTime = 0.1f;
        
        public Record ExcelManager ;
       // public GameObject focusobject;
        private Renderer _renderer;
        private Color _originalColor;
        private Color _targetColor;
        private Vector3 _targetPos;

        //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
        public void GazeFocusChanged(bool hasFocus)
        {
            Debug.Log("Focus or not 1="+hasFocus);
            //If this object received focus, fade the object's color to highlight color
            if (hasFocus)
            {
                _targetColor = HighlightColor;
                if (ExcelManager.flag == true)
                {
                    ExcelManager.target2[ExcelManager.mindcount] = 1f;
                   // ExcelManager.package[ExcelManager.number].Save();
                    //ExcelManager.flag = false;
                }
                // Debug.Log(_targetPos);
            }
            //If this object lost focus, fade the object's color to it's original color
            else
            {
                _targetColor = _originalColor;
                if (ExcelManager.flag == true)
                {
                    //Debug.Log("changevalue");
                    ExcelManager.target2[ExcelManager.mindcount] = 3f;
                    //ExcelManager.package[ExcelManager.number].Save();
                }
            }
        }

        private void Start()
        {
            //_targetPos = focusobject.transform.position;
            //eyecount= 2;
            _renderer = GetComponent<Renderer>();
            _originalColor = _renderer.material.color;
            _targetColor = _originalColor;
        }

        private void Update()
        {
            //This lerp will fade the color of the object
            //eyecount++;
            _renderer.material.color =
                Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / AnimationTime));
        }
    }
}
