using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StandController : MonoBehaviour
{
    [Header("Global")]
    public int value;
    private bool clickable;
    
    public float cooldownTime = 5f;
    private float lastUsedTime = 0;

    private TMP_Text info;

    [Header("Activate Conditions")]
    public bool active = false;

    public int cost;
    public Material activeMat;

    [Header("Local")] public bool autoClick = false;
    
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
        info.text = gameObject.name + "\nCost: " + cost.ToString();
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
            StartCoroutine(StartCollection());

        if (!active && GM.donationValue >= cost)
        {
            active = true;
            slider.gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<MeshRenderer>().material = activeMat;
            GM.SpendDono(cost);

            info.text = "speed" + cooldownTime.ToString() + "\nValue:" + value.ToString();
        }
    }

    IEnumerator StartCollection()
    {
        lastUsedTime = Time.time;
        yield return new WaitForSeconds(cooldownTime);
        GM.AddDono(value);
    }

    void UpdateSlider()
    {
        if (!clickable)
            slider.value = (Time.time-lastUsedTime)/cooldownTime;
        if (clickable)
        {
            slider.value = 0;
        }
    }

    void AutoClick()
    {
        if (autoClick && clickable && active)
        {
            StartCoroutine(StartCollection());
        }   
    }
}
