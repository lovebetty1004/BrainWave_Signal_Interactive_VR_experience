using System.Collections;
using System.Collections.Specialized;
using UnityEngine;

public class ray : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("a");
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(origin, direction * 1000f, Color.red, 2);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == "Cube (2)")
                Debug.Log("2");
            
        }

    }
}
