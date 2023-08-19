using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private ConstantForce2D force2D;
    private Vector2 forceDirection;

    public GameObject playerSprite_1;
    public GameObject playerSprite_2;

    bool isIron = true;

    public Transform startPos;

    public GameObject vfxOnDeath;

    private void Start()
    {
        Instance = this;

        transform.position = startPos.position;

        force2D = GetComponent<ConstantForce2D>();
        SetDeafaultState();
    }

    private void OnMouseDown()
    {
        if (isIron)
        {
            isIron = false;
        } 
        else
        {
            isIron = true;
        }

        ChangeMaterial(isIron);

        forceDirection = forceDirection * -1;

        force2D.force = forceDirection;
    }

    private void SetDeafaultState()
    {
        isIron = true;
        ChangeMaterial(isIron);
        forceDirection = new Vector2(0f, -5);
        force2D.force = forceDirection;
    }

    private void ChangeMaterial(bool isIron)
    {
        if (isIron)
        {
            playerSprite_1.SetActive(true);
            playerSprite_2.SetActive(false);
        } 
        else
        {
            playerSprite_1.SetActive(false);
            playerSprite_2.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Smile")
        {
            collision.gameObject.GetComponent<Smile>().Destroy();
        } 
        else if (collision.gameObject.tag == "Obstacle")
        {
            GameObject explosion = Instantiate(vfxOnDeath, transform.position, transform.rotation);
            Destroy(explosion, .75f);
            ReSetPos();
        }
    }

    public void ReSetPos()
    {
        transform.position = startPos.position;
        SetDeafaultState();
    }
}
