using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProgressionManager : MonoBehaviour
{
    [Header("Each Stage is a list of\nservices / stands that need to be\nbought in order to progress")]
    public PointList ListOfStages = new PointList();

    public int stage;
    public bool done = false;
    private CameraManager cameraM;

    public ExternalCommunication[] externalCommunications;
    // Start is called before the first frame update
    void Start()
    {
        cameraM = FindObjectOfType<CameraManager>();
        stage = -1;

        ChangeStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (stage >= ListOfStages.Stage.Count || done)
            return;
        
        foreach (ExternalCommunication EC in externalCommunications)
        {
            if(!EC.active)
                return;
        }
        ChangeStage();
        
    }

    void ChangeStage()
    {
        stage ++;
        cameraM.SwitchStage(stage);
        if (stage >= ListOfStages.Stage.Count)
        {
            done = true;
            return;
        }
        externalCommunications = ListOfStages.Stage[stage].activeObjects;
    }
}
