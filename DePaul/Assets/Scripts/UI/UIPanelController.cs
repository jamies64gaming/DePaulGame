using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    public Animator animator;
    public bool slideIn = false; // Assuming this is your boolean variable controlling the panel animation

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the animator component is assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Function to toggle the panel animation
    public void TogglePanel()
    {
        // Toggle the slideIn boolean
        slideIn = !slideIn;
        // Set the "SlideIn" parameter in the animator
        animator.SetBool("SlideIn", slideIn);
    }
}
