using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Smile : MonoBehaviour
{
    public GameObject explosionVfx;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Destroy()
    {
        StartCoroutine(SelfDestroyCoroutine());
    }

    private IEnumerator SelfDestroyCoroutine()
    {
        anim.SetTrigger("Scale");

        yield return new WaitForSeconds(1);

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].smiles.Remove(this);

        GameObject explosion = Instantiate(explosionVfx, transform.position, transform.rotation);
        Destroy(explosion, .75f);
        Destroy(gameObject);
    }
}