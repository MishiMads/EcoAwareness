using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTravel : MonoBehaviour
{
    // Scene indexes for transitions
    private const int pastSceneIndex = 1;
    private const int futureSceneIndex = 2;
    private bool canTravel = false;

    // Plane for fade effect
    public GameObject fadePlane; 
    private Material fadeMaterial; 
    private float fadeSpeed = 2f;
    
    // GameObjects to detect collisions
    public GameObject rightHand; 
    public GameObject watch;          

    public enum FadeDirection
    {
        In, // Alpha = 1
        Out // Alpha = 0
    }

    private void Awake()
    {
        // Get the material attached to the plane
        if (fadePlane != null)
        {
            Renderer renderer = fadePlane.GetComponent<Renderer>();
            if (renderer != null)
            {
                fadeMaterial = renderer.material;
            }
            else
            {
                Debug.LogError("FadePlane does not have a Renderer component.");
            }
        }
        else
        {
            Debug.LogError("FadePlane GameObject is not assigned.");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canTravel=false;
        Debug.Log("Scene loaded");
        StartCoroutine(Fade(FadeDirection.Out));
        StartCoroutine(TravelDelay());
    }

    // Coroutine for fading the scene in and out
    private IEnumerator Fade(FadeDirection fadeDirection)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;

        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetMaterialAlpha(ref alpha, fadeDirection);
                yield return null;
            }
            fadePlane.SetActive(false);
        }
        else
        {
            fadePlane.SetActive(true);
            while (alpha <= fadeEndValue)
            {
                SetMaterialAlpha(ref alpha, fadeDirection);
                yield return null;
            }
        }
    }

    // Adjust the material's alpha value for fade effect
    private void SetMaterialAlpha(ref float alpha, FadeDirection fadeDirection)
    {
        if (fadeMaterial != null)
        {
            Color color = fadeMaterial.color;
            color.a = alpha;
            fadeMaterial.color = color;

            alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the right hand
        if (other.gameObject == rightHand.GetComponent<Collider>().gameObject && canTravel==true)
        {
            Debug.Log("Hand and watch collision detected, transitioning scenes.");
            StartCoroutine(TimeTravelling());
        }
    }

    public IEnumerator TimeTravelling()
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
    private IEnumerator TravelDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        
        canTravel = true;
    }
}
