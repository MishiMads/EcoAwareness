using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using  UnityEngine.UI;

// This is a class that holds image and associated text
[System.Serializable] //making it serialized allows the class to show up in the inspector
public class ImageText
{
    public string text;
    public Sprite image;
}

public class Dialogue : MonoBehaviour
{

    public List<ImageText> ImageList; //list of images associated with text
    public TextMeshProUGUI textComponent; //the TEXTMeshProUGUI used for text
    public string[] lines; //A list of lines 
    public float textSpeed; //how fast the text should be written
    public Image imageRender; //the image render used for images in the speech bubble
    
    public int index; // the line currently being read by the program. This is not currently relevant in the final version
    //as all animals only have one line and its used to reference an image

    private InputAction nextLineAction; //i need this for the input system to work

    
    //private bool isImage = false;
    private Sprite imageToLoad;

    //the awake onenable and ondisable functions were all used for testing the setup and were not used in the final
    //version, They simply allow us to use the input action system, to call functions on button presses.
    private void Awake()
    {
        //this lets me call the OnNextLinePressed() function by pressing b
        nextLineAction = new InputAction(binding: "<Keyboard>/b"); 
        nextLineAction.performed += ctx => OnNextLinePressed();
    }
    //OnEnable and onDisable just makes the the system start or stop listening for the b button
    void OnEnable()
    {
        nextLineAction.Enable();
    }
    
    void OnDisable()
    {
        nextLineAction.Disable();
    }
    
    //the functions in Start are for testing the dialouge system independent of anything else
    void Start()
    {
        //imageRender.sprite = null;
        //textComponent.text = string.Empty;
        //StartDialouge();
    }

    // Update is called once per frame
    void Update()
    {
        //textComponent.text = string.Empty;
        //StartDialouge();
    }
    
    //this functions completes a sentence instantly or skips to the next line if the sentence is complete, or the box is displaying an image
    private void OnNextLinePressed()
    {
       
        if (textComponent.text == lines[index-1] || imageToLoad!=null)
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            
            textComponent.text = lines[index-1];
        }
    }
    //this function starts the dialouge by setting the dialouge index to 0 and playing the first line
    public void StartDialouge()
    {
        index = 0;
        NextLine();
    }

  //this  corutine types out a line.

    IEnumerator TypeLine()
    {
        
        //type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }
//this function sets up the image
    private void pressentImage(Sprite image)
    {
        
        textComponent.text = string.Empty;
        imageRender.sprite = image;
    }
//this function wipes clean the speech bubble and then checks if the the next line is an image reference or just a line.
//it then calls the appropriate function.
    void NextLine()
    {
        if (index < lines.Length)
        {
            imageToLoad = null;
            textComponent.text = string.Empty;
            imageRender.sprite = null;
            foreach (var ImageText in ImageList)
            {
                if (lines[index] == ImageText.text)
                {
                    imageToLoad = ImageText.image;
                    //index++;
                    break;
                }

               
            }

            if (imageToLoad!=null)
            {
                pressentImage(imageToLoad);
            }
            else
            {
                StartCoroutine(TypeLine());
            }
            index++;
            
        }
        else
        {
            gameObject.SetActive(false); //deativate the dialogue window when dialogue is done
        }
    }
}
