using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSettings : MonoBehaviour
{
    private bool BagOn;
    public GameObject SlotsInBag;

    private void Start()
    {
        BagOn = false;
        SlotsInBag.SetActive(false);
    }

    public void OnClickBag()
    {
        if (!BagOn)
        {
            BagOn = true;
            SlotsInBag.SetActive(true);
        } else
        {
            BagOn = false;
            SlotsInBag.SetActive(false);
        }
    }
}
