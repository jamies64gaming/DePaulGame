using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{
    public List<string> text;

    public int index = 0;

    public TMP_Text textBox;

    public TMP_Text nextButtonText;
    public TMP_Text prevButtonText;

    public Button nextButton;
    public Button prevButton;
    
    // Start is called before the first frame update
    void Start()
    {
        textBox.text = text[index];
        nextButtonText.text = "Next";
        prevButtonText.text = "Previous";
        prevButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLine()
    {
        if (nextButtonText.text == "Close")
        {
            DeactivateText();
            return;
        }
        index++;
        textBox.text = text[index];
        CheckNextButton();
        CheckPrevButton();
    }

    public void PreviousLine()
    {
        index--;
        textBox.text = text[index];
        CheckNextButton();
        CheckPrevButton();
    }

    void CheckNextButton()
    {
        
        if (index >= text.Count - 1)
        {
            nextButtonText.text = "Close";
        }
        else
        {
            nextButtonText.text = "Next";
        }
    }
    
    void CheckPrevButton()
    {
        
        if (index == 0)
        {
            prevButton.interactable = false;
        }
        else
        {
            prevButton.interactable = true;
        }
    }
    
    public void DeactivateText()
    {
        textBox.gameObject.SetActive(false);
    }
    public void ActivateText()
    {
        textBox.gameObject.SetActive(true);
    }
}
