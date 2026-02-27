using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapScrollingVertical : MonoBehaviour
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
            float distance = Mathf.Abs(contentRect.anchoredPosition.y + panSlots[i].transform.localPosition.y);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanId = i;
            }
        }
        if (isScrolling) return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, -panSlots[selectedPanId].transform.localPosition.y, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, contentVector.y);
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
    }
}
