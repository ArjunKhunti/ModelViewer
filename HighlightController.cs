using UnityEngine;

public class HighlightController : MonoBehaviour
{
    public RandomObject highlightObject;

    public void SelectObject(RandomObject highlightObject)
    {
        if (this.highlightObject != null)
        {
            this.highlightObject.StopHighlight();
        }

        this.highlightObject = highlightObject;
        this.highlightObject.StartHighlight();
    }
}