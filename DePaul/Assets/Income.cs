using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeGenerator : MonoBehaviour
{
    public float waitTime = 5f; // Time to wait in seconds
    public bool isAutomatic = false; // Toggle for automatic collection
    public int collectionAmount;

    [Header("Audio")] 
    public List<AudioClipWithKey> clipList = new List<AudioClipWithKey>();
    
    private Coroutine automaticCoroutine;
    private bool canCollect = false;

    private GameManager GM;
    private AudioManager AM;

    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager"); 
        GM = manager.GetComponent<GameManager>();
        AM = manager.GetComponent<AudioManager>();
        SetMode(isAutomatic);
    }

    public void SetMode(bool isAutomatic)
    {
        this.isAutomatic = isAutomatic;

        if (isAutomatic)
        {
            if (automaticCoroutine == null)
            {
                automaticCoroutine = StartCoroutine(GenerateIncomeAutomatically());
            }
        }
        else
        {
            if (automaticCoroutine != null)
            {
                StopCoroutine(automaticCoroutine);
                automaticCoroutine = null;
            }
            StartCoroutine(ManualIncomeCooldown());
        }
    }

    IEnumerator GenerateIncomeAutomatically()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); // Wait for specified seconds
            CollectMoney();
        }
    }

    void OnMouseDown()
    {
        if (!isAutomatic && canCollect)
        {
            CollectMoney();
            StartCoroutine(ManualIncomeCooldown());
        }
    }

    IEnumerator ManualIncomeCooldown()
    {
        canCollect = false;
        yield return new WaitForSeconds(waitTime); // Wait for specified seconds
        canCollect = true;
    }

    void CollectMoney()
    {
        GM.AddDono(collectionAmount); // Add Dono
        playAudio("CollectMoney");
    }

    // Method to toggle the automatic mode via script
    public void ToggleAutomaticMode(bool isAutomatic)
    {
        SetMode(isAutomatic);
    }
    
    //audio

    void playAudio(string key)
    {
        foreach (AudioClipWithKey audioClipWithKey in clipList)
        {
            if (audioClipWithKey.key == key)
            {
                AM.PlaySource(clip: audioClipWithKey.clip);
            }
        }
    }
}