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
using System.Linq.Expressions;

public class Ending : MonoBehaviour
{
    public TMP_Text textComponent;
    public Image image;
    public GameObject textBox;
    public float textSpeed;

    public string[] lines;

    private int frameIndex = 0;
    private int lastTextIndex = -1;

    public InputAction nextLineActionA;
    public InputAction nextLineActionX;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool canWrite = false;

    bool textboxActive = true;
    int imageIndex = 0;

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
                SetFrame(0); break;
            case 1:
                SetFrame(1); break;
            default:
                break;
        }
    }

    void SetFrame(int textIndex)
    {
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
}