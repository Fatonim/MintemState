using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rot_Weapons : MonoBehaviour
{
    public Player player;
    public float offset1, offset2;
    public float offset;

    [HideInInspector] public float offsetAngle;

    private void Start()
    {
        offset = offset1;
    }

    private void Update()
    {
        float rotZ = Mathf.Atan2(player.difference.y, player.difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, (rotZ + offset) * transform.localScale.x);
    }
}
