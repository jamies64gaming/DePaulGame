using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StandController : MonoBehaviour
{
    [Header("Global")]
    public int value;
    public int cost;

    public string Name;
    
    private bool clickable;
    
    public float cooldownTime = 5f;
    private float lastUsedTime = 0;

    private TMP_Text info;

    [Header("Activate Conditions")]
    public bool active = false;
    public Material activeMat;

    [Header("Local")] 
    public bool autoClick = false;
    
    private GameManager GM;
    private Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        slider = transform.Find("Canvas").Find("Slider").GetComponent<Slider>();
        lastUsedTime = Time.time - cooldownTime;
        slider.gameObject.SetActive(false);
        info = transform.GetChild(1).Find("Info").GetComponent<TMP_Text>();
        info.text = name + "\nCost: " + cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            CanClick();
            UpdateSlider();
            AutoClick();
        }
    }
    
    void CanClick()
    {
        if (Time.time > lastUsedTime + cooldownTime)
            clickable = true;
        else
            clickable = false;
    }
    void OnMouseDown()
    {
        if (clickable && active)
            StartCollection();

        if (!active && GM.donationValue >= cost)
        {
            active = true;
            slider.gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<MeshRenderer>().material = activeMat;
            GM.SpendDono(cost);

            info.text = name + "\nspeed" + cooldownTime.ToString() + "\nValue:" + value.ToString();
            lastUsedTime = Time.time;
        }
    }

    void StartCollection()
    {
        lastUsedTime = Time.time;
        GM.AddDono(value);
    }

    void UpdateSlider()
    {
        if (!clickable)
            slider.value = (Time.time-lastUsedTime)/cooldownTime;
        if (clickable)
        {
            slider.value = 1;
        }
    }

    void AutoClick()
    {
        if (autoClick && clickable && active)
        {
            StartCollection();
        }   
    }
}
