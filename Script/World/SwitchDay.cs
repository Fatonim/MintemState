using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDay : MonoBehaviour
{
    public Light light;
    public float time;

    void Start()
    {
        light.intensity = 1.65f;
        time = 360.0f;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > 720) time -= 720;
        if (time >= 240 && time <= 360) light.intensity = 0.01125f * (time - 240) + 0.35f;
        else if (time >= 600 && time <= 720) light.intensity = 0.01125f * (time - 600) * -1 + 1.7f;
        else if (time <= 240) light.intensity = 0.35f;
        else light.intensity = 1.7f;
    }
}
