using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private Animator Anim;

    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    public void Toggle()
    {
        if (active)
        {
            active = false;
            Anim.SetBool("SlideIn",false);
        }
        else
        {
            active = true;
            Anim.SetBool("SlideIn",true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
