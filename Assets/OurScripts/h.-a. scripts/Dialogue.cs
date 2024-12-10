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
    //this script was originally written with the idea that the dialogue boxes should be able to handle both text and 
    //images. However in the end only the image part ended up being used. Making large parts of this script superfluous
    public List<ImageText> ImageList; //list of images and associated text
    public TextMeshProUGUI textComponent; //text field
    public string[] lines; //list of dialogue
    public float textSpeed; //how fast each letter should be written
    public Image imageRender; //where images are rendered 
    
    public int index; //this is what line is currently being read

    private InputAction nextLineAction; //i need this for the input system to work

    
    //private bool isImage = false;
    private Sprite imageToLoad;

    private void Awake()
    {
        //this lets me call the OnNextLinePressed() function by pressing b, used for testing, not used in the final build
        //on account of it being vr
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

  

    IEnumerator TypeLine()
    {
        
        //type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }

    private void pressentImage(Sprite image)
    {
        
        textComponent.text = string.Empty;
        imageRender.sprite = image;
    }

    void NextLine()
    {
        if (index < lines.Length)
        {
            imageToLoad = null;
            textComponent.text = string.Empty;
            imageRender.sprite = null;
            foreach (var ImageText in ImageList) //this check if the line matches the string attached to one of the images
            //if it does it then instead of writting the line it renders the image instead
            {
                if (lines[index] == ImageText.text)
                {
                    imageToLoad = ImageText.image;
                    //break ends the loop if a matching  image is found
                    break;
                }

               
            }
            //this line loads an image if one has been found
            if (imageToLoad!=null)
            {
                pressentImage(imageToLoad);
            }
            //this one writes the line out if no image was found
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
