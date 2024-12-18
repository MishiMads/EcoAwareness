using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogueEntry
{
    public Sprite image;
    [TextArea] public string dialogueText;
}
public class TutorialManager : MonoBehaviour
{
   
    public Image displayImage;
    public TMP_Text dialogueText;
    public List<DialogueEntry> dialogueEntries;

    private int currentText = 0;

    public InputActionReference pressA;

    private void OnEnable()
    {
        pressA.action.Enable();
        pressA.action.performed += OnPressA;
    }

    private void OnDisable()
    {
        pressA.action.performed -= OnPressA;
        pressA.action.Disable();
    }

    void Start()
    {
        if (dialogueEntries.Count > 0)
        {
            UpdateDialogue();
        }
    }

    void OnPressA(InputAction.CallbackContext context)
    {
        NextDialogue();
    }

    public void NextDialogue()
    {
        currentText = (currentText + 1) % dialogueEntries.Count;
        UpdateDialogue();
    }

    private void UpdateDialogue()
    {
        dialogueText.text = dialogueEntries[currentText].dialogueText;
        displayImage.sprite = dialogueEntries[currentText].image;
    }
}
