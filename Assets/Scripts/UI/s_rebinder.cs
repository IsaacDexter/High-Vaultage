using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class s_rebinder : MonoBehaviour
{
    private s_player m_player;
    private PlayerInput m_playerInput;

    [SerializeReference] private InputActionReference[] m_actions;

    private InputActionRebindingExtensions.RebindingOperation m_rebindingOperation;
    public InputActionMap m_actionMap;

    private void Start()
    {
        m_player = gameObject.transform.root.GetComponent<s_player>();
        if (m_player == null)
        {
            print("button has no reference to player!");
        }
        m_playerInput = m_player.m_playerInput;
    }

    public void SetKey()
    {
        Button button = gameObject.GetComponent<Button>();
        TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        StartRebinding(button, text);
    }

    /// <summary>Prevents the user drom interaction with the UI, then launches a new rebinding operation to perform and interactive rebinding, then deallocate itself on completion</summary>
    /// <param name="button">The button to disable</param>
    /// <param name="text">The text to edit</param>
    public void StartRebinding(Button button, TextMeshProUGUI text)
    {
        //Stop the user from pressing the button again
        text.text = "Listening...";
        button.interactable = false;

        //Get the player's input action map, and set it to a dedicated one so they cannot interact while rebinding.
        m_playerInput.SwitchCurrentActionMap("Binding");

        m_rebindingOperation =
            m_actions[0].action.PerformInteractiveRebinding()   //Perfrom a rebinding
            .OnMatchWaitForAnother(0.1f)                    //Delay
            .OnComplete(operation => Rebind(button, text))  //When complete, call rebind.
            .Start();
    }

    /// <summary>Disposes of allocated memory by the rebinding operation, allows the user to interact once again, rebinds the control and sets the visual text on the button to match the button/</summary>
    /// <param name="button">The button to reenable</param>
    /// <param name="text">The text to alter</param>
    /// <param name="operationIndex">The index of the binding operation to dispose of</param>
    private void Rebind(Button button, TextMeshProUGUI text)
    {
        //Dispose of allocated memory
        m_rebindingOperation.Dispose();
        //Reenable button
        button.interactable = true;
        //Let player interact with Ui again
        m_playerInput.SwitchCurrentActionMap("UI");
        //Get the binding for the current control scheme.
        int bindingIndex = m_actions[0].action.GetBindingIndexForControl(m_actions[0].action.controls[0]);
        //Set the buttons text
        text.text = InputControlPath.ToHumanReadableString(
            m_actions[0].action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);    //Ommit the device from the text
        
        for (int i = 1; i < m_actions.Length; i++)  //For every other action, apply it the same binding as the initial action
        {
            m_actions[i].action.AddBinding(m_actions[0].action.bindings[bindingIndex].effectivePath);
        }
    }
}
