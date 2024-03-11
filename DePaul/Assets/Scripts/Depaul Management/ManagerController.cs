using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerController : MonoBehaviour
{
    public int cost;
    public string ManagerType;
    public Button Button;
    
    private StandController[] stands;
    private GameManager GM;

    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = Button.transform.GetChild(0).GetComponent<TMP_Text>();
        GM = FindObjectOfType<GameManager>();
        switch (ManagerType)
        {
            case "StandController":
                stands = FindObjectsOfType<StandController>();
                text.text = "Stand Manager\nCost:" + cost.ToString();
                break;
                
            default:
                return;
        }
    }

    public void activate()
    {
        if (GM.donationValue >= cost)
        {
            GM.SpendDono(cost);
            foreach (StandController stand in stands)
            {
                stand.autoClick = true;
            }

            Button.interactable = false;
        }
    }
}
