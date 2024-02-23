using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandController : MonoBehaviour
{
    [Header("Global")]
    public int value;
    private bool clickable;
    
    public float cooldownTime = 5f;
    private float lastUsedTime = 0;

    [Header("Local")] public bool autoClick = false;
    
    private GameManager GM;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        slider = transform.Find("Canvas").Find("Slider").GetComponent<Slider>();
        lastUsedTime = Time.time - cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        CanClick();
        UpdateSlider();
        AutoClick();
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
        if (clickable)
            StartCollection();
    }

    void StartCollection()
    {
        lastUsedTime = Time.time;
        slider.value = 0;
        GM.AddDono(value);
    }

    void UpdateSlider()
    {
        if (!clickable)
            slider.value = (Time.time-lastUsedTime)/cooldownTime;
    }

    void AutoClick()
    {
        if (autoClick && clickable)
        {
            StartCollection();
        }
    }
}
