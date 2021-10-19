using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraresetscript : MonoBehaviour
{
 
   
    public GameObject ObjA;
    public GameObject ObjB;
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var yRotation = ObjB.transform.eulerAngles.y;
        ObjA.transform.Rotate(ObjA.transform.eulerAngles.x, -yRotation, ObjA.transform.eulerAngles.z);
    }
}
