using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using UnityEditor.Overlays;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public TMP_Text textComponent;
    public Image image;
    public GameObject overlay;
    public Image overlaySprite;
    public GameObject textBox;
    public float textSpeed;

    public string[] lines;
    public Sprite[] images;
    public Sprite[] overlays;

    private int frameIndex = 0;
    private int lastTextIndex = -1;

    public InputAction nextLineActionA;
    public InputAction nextLineActionX;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool canWrite = false;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;

        // Bind the A and X buttons
        nextLineActionA = new InputAction(binding: "<XRController>{RightHand}/primaryButton"); // A button
        nextLineActionX = new InputAction(binding: "<XRController>{LeftHand}/secondaryButton"); // X button

        // Assign the same method for both actions
        nextLineActionA.performed += ctx => Next();
        nextLineActionX.performed += ctx => Next();

        nextLineActionA.Enable();
        nextLineActionX.Enable();

        // Initializes first frame
        UpdateFrame();
    }

    void UpdateFrame()
    {
        // Sets states for each frame
        switch (frameIndex)
        {
            case 0:
                SetFrame(0, 0, false, true, false); break;
            case 1:
                SetFrame(0, 1, false, true, false); break;
            case 2:
                SetFrame(0, 2, false, true, false); break;
            case 3:
                SetFrame(0, 3, false, true, false); break;
            case 4:
                SetFrame(0, 4, false, true, false); break;
            case 5:
                SetFrame(0, 5, false, true, false); break;
            case 6:
                SetFrame(0, -1, true, false, true, 0); break;
            case 7:
                SetFrame(1, 6, false, true, false); break;
            case 8:
                SetFrame(2, 6, false, true, false); break;
            case 9:
                SetFrame(3, 7, false, true, false); break;
            case 10:
                SetFrame(4, 7, false, true, false); break;
            case 11:
                SetFrame(5, 7, false, true, false); break;
            case 12:
                SetFrame(6, 8, false, true, false); break;
            case 13:
                SetFrame(6, -1, true, false, true, 1); break;
            case 14:
                SetFrame(7, 9, false, true, false); break;
            case 15:
                SetFrame(8, 9, false, true, false); break;
            case 16:
                SetFrame(9, 9, false, true, false); break;
            case 17:
                SetFrame(10, 10, false, true, false); break;
            case 18:
                SetFrame(11, 10, false, true, false); break;
            default:
                Debug.Log("Tutorial ending called");
                EndTutorial();  break;
        }
    }

    void SetFrame(int imageIndex, int textIndex, bool overlayActive, bool textboxActive, bool enableOverlay = false, int overlayIndex = -1)
    {
        // Sets image sprite to match current index
        if (imageIndex >= 0 && imageIndex < images.Length)
        {
            image.sprite = images[imageIndex];
        }

        // Runs as long as there are still lines in the list
        if (textIndex >= 0 && textIndex < lines.Length)
        {
            // Ensures that canWrite and textboxActive always have the same value, so that text is not being written in a disabled textbox
            canWrite = textboxActive;

            // If the textIndex is different from the previous frame and the textbox is active, it enables the object and writes the lines
            if (textIndex != lastTextIndex && textboxActive)
            {
                textBox.SetActive(true);
                StartTyping(lines[textIndex]);
            }
            // If the textbox is inactive, it disables the object and also the ability to write
            else
            {
                textBox.SetActive(false);
            }

            // Sets previous index to reference in next frame 
            lastTextIndex = textIndex;
        }

        // Sets the textbox as active as textIndex != lastTextIndex in line 120 is no longer being met and shortly disables it
        textBox.SetActive(textboxActive);

        // Only enables the overlay object if the overlay is active
        overlay.SetActive(overlayActive);
        // Sets overlay sprite to match current index
        if (overlayActive && overlayIndex < overlays.Length)
        {
            Debug.Log($"Frame {frameIndex}: Overlay index set to {overlayIndex}");
            overlaySprite.sprite = overlays[overlayIndex];
        }
        else if (!overlayActive)
        {
            overlaySprite.sprite = null;
        }
    }

    void StartTyping(string line)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    // Writes the line letter for letter
    IEnumerator TypeLine(string line)
    {
        // Sets typing to true, so input will show whole line on line 183
        isTyping = true;
        // Removes previous line 
        textComponent.text = string.Empty;

        // Writes the lines out letter for letter at textSpeed
        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // Sets typing to false, so input will shift line on line 192
        isTyping = false;
    }

    void Next()
    {
        // Shows full line if line is still being written and button is pressed
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
            textComponent.text = lines[lastTextIndex];
            return;
        }

        // Shows next line if whole line is already displayed
        frameIndex++;
        UpdateFrame();
    }

    // Ends tutorial if button is pressed on last frame
    private void EndTutorial()
    {
        Debug.Log("Tutorial finished");
        nextLineActionA.Disable();
        nextLineActionX.Disable();
        Debug.Log("Tutorial bindings disabled");
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}