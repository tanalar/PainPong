using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks;

    public static Action<string> onLoose;

    private void Start()
    {
        StartCoroutine(LooseCheck());
    }

    private IEnumerator LooseCheck()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] == null)
            {
                blocks.RemoveAt(i);
            }
        }
        if (blocks.Count <= 0)
        {
            onLoose?.Invoke(gameObject.name);
            Destroy(gameObject);
        }
        StartCoroutine(LooseCheck());
    }
}
