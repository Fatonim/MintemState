using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapScrolling : MonoBehaviour
{
    public RectTransform contentRect;
    public GameObject[] panSlots;
    public float snapSpeed;

    private int selectedPanId;
    private bool isScrolling;

    private Vector2 contentVector;

    private void FixedUpdate()
    {
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panSlots.Length; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x + panSlots[i].transform.localPosition.x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanId = i;
            }
        }
        if (isScrolling) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, -panSlots[selectedPanId].transform.localPosition.x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = new Vector2(contentVector.x, contentRect.anchoredPosition.y);
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
    }
}
