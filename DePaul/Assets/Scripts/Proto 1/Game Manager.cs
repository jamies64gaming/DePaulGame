using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int donationValue = 0;

    public string donationPreText = "";
    [SerializeField] private TMP_Text donationTextBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        donationTextBox.text = donationPreText + " " +donationValue.ToString();
    }

    public void AddDono(int value)
    {
        donationValue += value;
    }
    
    public void SpendDono(int value)
    {
        donationValue -= value;
    }
}
