using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraManager : MonoBehaviour
{
    public GameObject pivotPoint;

    // Start is called before the first frame update
    void Start()
    {
        pivotPoint = GameObject.Find("Car/PivotPoint");
    }

    // Update is called once per frame
    void Update()
    {
        pivotPoint.transform.localRotation *= Quaternion.Euler(Vector3.up * 5 * Time.deltaTime);
    }
}
