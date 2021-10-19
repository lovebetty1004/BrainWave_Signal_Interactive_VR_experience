using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Looxid.Link
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineChart : MonoBehaviour
    {
        public int Width = 1000;
        public int Height = 240;

        private LineRenderer line;

        void OnEnable()
        {
            line = GetComponent<LineRenderer>();
            line.positionCount = Width;
        }

        public void SetValue(List<double> datalist)
        {
            if (line == null) return;

            for (int x = 0; x < Width; x++)
            {
                int dataHeight = Height / 2;

                if (x < datalist.Count)
                {
                    dataHeight = Mathf.Clamp(Mathf.FloorToInt(((float)datalist[x] + 0.5f) * (float)Height), 2, Height - 2);
                }

                line.SetPosition(x, new Vector3(x, dataHeight, 0.0f));
            }
        }
    }
}
