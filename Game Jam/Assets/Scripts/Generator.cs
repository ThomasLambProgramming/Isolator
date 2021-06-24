﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //since the generator will kill half of the map (depending on the sides)
    //all we need to know is if its horizontal or vertical
    private GameStateManager gameManager = null;
    public float heightOfHalf = 4f;
    public float widthOfHalf = 4f;

    private bool isOn = false;
    public float onTimer = 2f;
    private float timerForStop = 0;
    public Vector3 middlePointOfVertical = Vector3.zero;
    public Vector3 middlePointOfHorizontal = Vector3.zero;
    public void Start()
    {
        gameManager = FindObjectOfType<GameStateManager>();
    }
    public void Update()
    {
        if (isOn)
        {
            timerForStop += Time.deltaTime;
            if (timerForStop > onTimer)
            {
                isOn = false;
                timerForStop = 0;
            }
            Collider[] colliders;
            if (gameManager.isHorizontal())
            {
                colliders = Physics.OverlapBox(middlePointOfHorizontal, new Vector3(widthOfHalf, 2, heightOfHalf));
            }
            else
            {
                colliders = Physics.OverlapBox(middlePointOfVertical, new Vector3(heightOfHalf, 2, widthOfHalf));
            }
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (other.gameObject.GetComponent<PlayerScript>().ischarged)
            isOn = true;
        }
    }
}