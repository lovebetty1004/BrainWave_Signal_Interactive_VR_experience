using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Looxid.Link
{
    public class BrainEffect : MonoBehaviour
    {

        public ParticleSystem brainEffectLight;
        public float alphaValue;
        public ParticleLines brainEffectLine;


        public Color StartColor
        {
            set
            {
                var main = brainEffectLight.main;
                main.startColor = value;
            }
        }

        void Start()
        {
            brainEffectLight = GetComponent<ParticleSystem>();

        }

        void Update()
        {
            Color whiteCol = new Color(255 / 255f, 255 / 255f, 255 / 255f, alphaValue / 255f);
            Color blueCol = new Color(0 / 255f, 107 / 255f, 255 / 255f, alphaValue / 255f);

            if (brainEffectLight.gameObject.name.Equals("LeftBrain"))
            {
                StartColor = whiteCol;
            }
            else if (brainEffectLight.gameObject.name.Equals("Left Effect A"))
            {
                StartColor = blueCol;
                brainEffectLine.startColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, alphaValue * 5 / 255f);
            }
            else if (brainEffectLight.gameObject.name.Equals("Left Effect B"))
            {
                StartColor = blueCol;
                brainEffectLine.startColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, alphaValue * 5 / 255f);
            }
            else if (brainEffectLight.gameObject.name.Equals("RightBrain"))
            {
                StartColor = whiteCol;
            }
            else if (brainEffectLight.gameObject.name.Equals("Right Effect A"))
            {
                StartColor = blueCol;
                brainEffectLine.startColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, alphaValue * 5 / 255f);
            }
            else if (brainEffectLight.gameObject.name.Equals("Right Effect B"))
            {
                StartColor = blueCol;
                brainEffectLine.startColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, alphaValue * 5 / 255f);
            }
        }


    }
}