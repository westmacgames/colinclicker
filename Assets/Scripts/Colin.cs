﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Colin : MonoBehaviour
{
    //When the objects size reaches this value, destroy it.
    [SerializeField]
    private float m_sizeDespawnThreshold = 0.15f;

    //Speed at which the object shrinks at.
    //Will be set by the spawner.
    [SerializeField]
    public float m_shrinkSpeed = 0;

    public GameObject gameOverButton;

    private void OnMouseDown()
    {
        //Increment the score, and destroy the object.
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<ColinSpawner>().IncrementScore();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Scale the object down.
        transform.localScale = new Vector3(transform.localScale.x - (Time.deltaTime * m_shrinkSpeed),
            transform.localScale.y - (Time.deltaTime * m_shrinkSpeed),
            0);

        //Destroy the object if it reaches the threshold.
        if (transform.localScale.x <= m_sizeDespawnThreshold)
        {
            Instantiate(gameOverButton, GameObject.Find("Canvas").transform);

            //Destroy every colin
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Colin"))
            {
                //ColinSpawner.Instance.RemoveColin(g.GetComponent<Colin>());
                Destroy(g);
            }
            Destroy(GameObject.FindGameObjectWithTag("Spawner"));
            Destroy(this);

            //Game over
            Debug.Log("Game Over");
        }
    }
}
