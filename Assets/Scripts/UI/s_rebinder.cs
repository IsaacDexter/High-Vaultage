using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class s_rebinder : MonoBehaviour
{
    [SerializeField] private InputActionReference m_action;
    [SerializeField] private s_player m_player;

    private InputActionRebindingExtensions.RebindingOperation m_rebindingOperation;
    public InputActionMap m_playerInput;

    public void SetKey(GameObject buttonObject)
    {
        Button button = buttonObject.GetComponent<Button>();
        TextMeshProUGUI text = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
        StartRebinding(button, text);
        //
    }

    public void StartRebinding(Button button, TextMeshProUGUI text)
    {
        button.interactable = false;
        m_player.GetComponent<s_player>().m_playerInput.SwitchCurrentActionMap("Binding");

        m_rebindingOperation = m_action.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => Rebind(button, text))
            .Start();
    }

    private void Rebind(Button button, TextMeshProUGUI text)
    {
        //Dispose of allocated memory
        m_rebindingOperation.Dispose();
        //Reenable button
        button.interactable = true;
        //Let player interact with Ui again
        m_player.GetComponent<s_player>().m_playerInput.SwitchCurrentActionMap("UI");
        //Get the binding for the current control scheme.
        int bindingIndex = m_action.action.GetBindingIndexForControl(m_action.action.controls[0]);
        //Set the buttons text
        text.text = InputControlPath.ToHumanReadableString(
            m_action.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);    //Ommit the device from the text
    }
}
