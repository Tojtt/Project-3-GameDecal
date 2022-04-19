using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpill : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;
    

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(SpillGas());
    }

    IEnumerator SpillGas()
    {
        Debug.Log("spilling");
        for (int i = 0; i < Random.Range(4, sprites.Length); i++)
        {
            sr.sprite = sprites[i];
            yield return new WaitForSeconds(.1f);
        }
    }
}
