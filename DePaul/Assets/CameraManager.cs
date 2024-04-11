using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class CameraManager : MonoBehaviour
{
    [Header("reference game manager to allign stages")]
    public int[] zoomStages;
    public float speed;
    public float zoomLevel;

    public Vector3 startingPos;
    public Vector3 endingPos;

    public int stage = 0;

    private float currentZoom;
    private float targetZoom;

    private float sinTime;
    
    // Update is called once per frame
    void UpdateCamera()
    {
        float x = Mathf.Lerp(startingPos.x, endingPos.x, zoomLevel/100);
        float y = Mathf.Lerp(startingPos.y, endingPos.y, zoomLevel/100);
        float z = Mathf.Lerp(startingPos.z, endingPos.z, zoomLevel/100);
        Vector3 pos = new Vector3(x,y,z);
        transform.position = pos;
    }

    private void Update()
    {
        if (zoomLevel != targetZoom)
        {
            sinTime += Time.deltaTime * speed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = evaluate(sinTime);
            zoomLevel = Mathf.Lerp(currentZoom, targetZoom, t);
            UpdateCamera();
        }
        
        if (Input.GetKeyDown("space"))
        {
            SwitchStage(stage+1);
        }
    }
    
    public void SwitchStage(int stg)
    {
        if (stg >= zoomStages.Length)
            return;
        else
        {
            stage = stg;
            sinTime = 0;
            currentZoom = targetZoom;
            targetZoom = zoomStages[stage];
        }

    }

    public float evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + .5f;
    }
}
