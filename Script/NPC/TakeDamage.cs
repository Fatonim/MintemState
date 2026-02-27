using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float health;
    public bool haveRes = false;

    private SpriteRenderer sprite;
    private Material matBlink;
    private Material matDefault;

    public GameObject destroyEffect;
    public Transform EffectPos;
    public Vector3 EffectScale;

    public GameObject damageEffect;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load<Material>("Prefabs/Map/EnemyBlink");
        matDefault = sprite.material;
    }

    public void TakeDamages(int damage)
    {
        GameObject damagePoint = Instantiate(damageEffect, EffectPos) as GameObject;
        damagePoint.transform.GetChild(0).GetComponent<TextMesh>().text = "" + damage;
        health -= damage;
        sprite.material = matBlink;
        if (health <= 0)
        {
            if (haveRes) GetComponent<DropResources>().ResourcesDrop();
            destroyEffect.transform.localScale = EffectScale;
            Instantiate(destroyEffect, EffectPos.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }

    private void ResetMaterial()
    {
        sprite.material = matDefault;
    }

}
