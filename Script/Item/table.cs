using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class table : MonoBehaviour
{
    public GameObject TableUi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TableUi.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TableUi.SetActive(false);
        }
    }
}
