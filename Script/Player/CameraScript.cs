using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float CameraMove;
    public float shakePower;
    public float shakeRotation;
    public float shakeTime;

    private float shakeTimeRemaining;
    private Vector3 posEnd, posSmooth;

    void Start()
    {
        transform.position = new Vector3(200, 200, -10);
    }

    private void FixedUpdate()
    {
        FolowPlayer();
    }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
            CameraShake();
    }

    private void CameraShake()
    {
        shakeTimeRemaining -= Time.deltaTime;
        float xAmount = Random.Range(-1f, 1f) * shakePower;
        float yAmount = Random.Range(-1f, 1f) * shakePower;

        transform.position += new Vector3(xAmount, yAmount, 0);
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f)); 
    } 

    public void StartShake()
    {
        shakeTimeRemaining = shakeTime;
    }

    private void FolowPlayer()
    {
        posEnd = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        posSmooth = Vector3.Lerp(transform.position, posEnd, CameraMove);
        transform.position = posSmooth;
    }
}
