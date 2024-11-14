using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class scene_transition : MonoBehaviour
{
    
    // Past and future scene Index are the numbers the two scene's are assigned in build settings
    private const int pastSceneIndex=1;
    private const int futureSceneIndex=2;
    private InputAction NextLineAction; 
    
    public RawImage fadeOutUIImage;
    private float fadeSpeed = 2f;

    //be aware that the thing fading in and out is a black box that covers the screen.
    public enum FadeDirection
    {
        In, //Alpha=1
        Out //Alpha=0
    }
    
    //This function is supposed to trigger when a scene is loaded. This will allow the colour to fade back in when loading a new scene.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene loaded");
        StartCoroutine(Fade(FadeDirection.Out));
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(Fade(FadeDirection.Out));
    }
    
    void OnDisable()
    {
        //Im supposed to do this to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //this and InputAction are for testing the LoadScene function
        NextLineAction = new InputAction(binding: "<Keyboard>/o"); 
        NextLineAction.performed += ctx => StartCoroutine(TimeTravel());
        NextLineAction.Enable();
    }

    //This loads a scene bassed on index number
    public void LoadScene(int Scene)
    {
        SceneManager.LoadScene(sceneBuildIndex: Scene);
    }

    //this function moves between the past scene and the future scene and can be called from other scribts
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
    
    // this is also only for testing
    private void OnDestroy()
    {
        NextLineAction.Disable();
    }
    //this corutine is for making the scene fade in and out
    private IEnumerator Fade(FadeDirection fadeDirection) 
    {
        float alpha = (fadeDirection == FadeDirection.Out)? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out)? 0 : 1;
        if (fadeDirection == FadeDirection.Out) {
            while (alpha >= fadeEndValue)
            {
                SetColorImage (ref alpha, fadeDirection);
                yield return null;
            }
            fadeOutUIImage.enabled = false; 
        } else {
            fadeOutUIImage.enabled = true; 
            while (alpha <= fadeEndValue)
            {
                SetColorImage (ref alpha, fadeDirection);
                yield return null;
            }
        }
    }
    
    //this function is for adjust the colour of a screen covering image in order to create a fade effect
    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
        fadeOutUIImage.color = new Color (fadeOutUIImage.color.r,fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1) ;
    }
    
}
