using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerController : MonoBehaviour
{
    [Header("Global")]
    public int value;
    public int cost;
    public int costToRun;

    public string name;
    
    private bool clickable;

    private TMP_Text info;

    [Header("Activate Conditions")]
    public bool active = false;
    public Material activeMat;

    [Header("Local")] 
    public bool autoClick = false;
    public string standType;

    private GameObject[] stands;
    private GameManager GM;

    private ExternalCommunication EC;
    // Start is called before the first frame update
    void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        info = transform.GetChild(2).Find("Info").GetComponent<TMP_Text>();
        info.text = name + "\nCost: " + cost.ToString();
        
        stands = GameObject.FindGameObjectsWithTag(standType);
        EC = GetComponent<ExternalCommunication>();
                
    }

    void OnMouseDown()
    {

        if (!active && GM.donationValue >= cost)
        {
            Buy();
        }
    }
    
    public void activate()
    {
        foreach (GameObject stand in stands)
        {
            stand.GetComponent<ExternalCommunication>().AutoClickActive = true;
        }
        
    }

    void Buy()
    {
        active = true;
        EC.active = active;
        GM.SpendDono(cost);
        transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = activeMat;
        info.text = name + "is active";
        activate();
    }
}
