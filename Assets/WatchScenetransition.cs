using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WatchScenetransition : MonoBehaviour
{
    // Past and future scene Index are the numbers the two scene's are assigned in build settings
    private const int pastSceneIndex = 1;
    private const int futureSceneIndex = 2;
    public RawImage fadeOutUIImage;
    private float fadeSpeed = 2f;
    
    //Gameobjects to detect collisions between the righthand and watch for scene transitions
    public GameObject rightHand; 
    public GameObject watch;          

    public enum FadeDirection
    {
        In, // Alpha = 1
        Out // Alpha = 0
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded");
        StartCoroutine(Fade(FadeDirection.Out));
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //this corutine is for making the scene fade in and out
    private IEnumerator Fade(FadeDirection fadeDirection)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;

        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
            fadeOutUIImage.enabled = false;
        }
        else
        {
            fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
        }
    }

    //this function is for adjust the colour of a screen covering image in order to create a fade effect
    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the right hand
        if (other.gameObject == rightHand.GetComponent<Collider>().gameObject)
        {
            Debug.Log("Hand and watch collision detected, transitioning scenes.");
            StartCoroutine(TimeTravel());
        }
    }

    public IEnumerator TimeTravel()
    {
        StartCoroutine(Fade(FadeDirection.In));
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(fadeSpeed);

        switch (currentSceneIndex)
        {
            case pastSceneIndex:
                LoadScene(futureSceneIndex);
                break;
            case futureSceneIndex:
                LoadScene(pastSceneIndex);
                break;
        }
        yield return null;
    }

    public void LoadScene(int Scene)
    {
        SceneManager.LoadScene(sceneBuildIndex: Scene);
    }


}
