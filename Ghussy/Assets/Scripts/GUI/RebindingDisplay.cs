using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Class handling dynamic rebinding logic as part of settings options
 */
public class RebindingDisplay : MonoBehaviour
{
    // Input Action References
    [SerializeField] private InputActionReference fireWeaponIA;
    [SerializeField] private InputActionReference useAbilityIA;
    [SerializeField] private InputActionReference interactIA;
    [SerializeField] private PlayerController playerController;

    // UI Elements
    [SerializeField] private TMP_Text FW_bindingDisplayNameText;
    [SerializeField] private GameObject FW_startRebindObject;
    [SerializeField] private GameObject FW_waitingForInputObject;

    [SerializeField] private TMP_Text UA_bindingDisplayNameText;
    [SerializeField] private GameObject UA_startRebindObject;
    [SerializeField] private GameObject UA_waitingForInputObject;

    [SerializeField] private TMP_Text I_bindingDisplayNameText;
    [SerializeField] private GameObject I_startRebindObject;
    [SerializeField] private GameObject I_waitingForInputObject;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private const string RebindsKey = "rebinds";

    private void Start()
    {
        // Retrieve Rebinds 
        string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);

        if (string.IsNullOrEmpty(rebinds)) { return; }

        // Load Bindings
        playerController.playerInput.actions.LoadBindingOverridesFromJson(rebinds);

        // Initialise Display for Fire Binding
        int FW_bindingIndex = fireWeaponIA.action.GetBindingIndex();
        FW_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                fireWeaponIA.action.bindings[FW_bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

        // Initialise Display for Ability Binding
        int UA_bindingIndex = useAbilityIA.action.GetBindingIndex();
        UA_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                useAbilityIA.action.bindings[UA_bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

        // Initialise Display for Interact Binding
        int I_bindingIndex = interactIA.action.GetBindingIndex();
        I_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                interactIA.action.bindings[I_bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

    }

    // Save Bindings
    public void Save()
    {
        // Update and Save Bindings
        string rebinds = playerController.playerInput.actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString(RebindsKey, rebinds);
    }

    // Dynamic Binding Operation
    public void StartRebinding(string name)
    {
        if (playerController != null) { playerController.ActionMapMenuChange(); }

        if (name == "Fire")
        {
            FW_startRebindObject.SetActive(false);
            FW_waitingForInputObject.SetActive(true);

            rebindingOperation = fireWeaponIA.action.PerformInteractiveRebinding()
                              .OnMatchWaitForAnother(0.1f)
                              .OnComplete(operation =>
                              {
                                  if (CheckDuplicates(fireWeaponIA.action) || ExcludeBinding(fireWeaponIA.action, "<Keyboard>/escape"))
                                  {
                                      // Audio Queue
                                      AudioManager.Instance.Play("Fail");

                                      // Ask for another binding
                                      fireWeaponIA.action.RemoveBindingOverride(0);
                                      CleanUp();
                                      StartRebinding(name);
                                      return;
                                  }
                                  else
                                  {
                                      RebindComplete(name);
                                  }

                              })
                           .Start();
        }
        else if (name == "Ability")
        {
            UA_startRebindObject.SetActive(false);
            UA_waitingForInputObject.SetActive(true);

            rebindingOperation = useAbilityIA.action.PerformInteractiveRebinding()
                   .OnMatchWaitForAnother(0.1f)
                   .OnComplete(operation =>
                   {
                       if (CheckDuplicates(useAbilityIA.action) || ExcludeBinding(useAbilityIA.action, "<Keyboard>/escape"))
                       {
                           // Audio Queue
                           AudioManager.Instance.Play("Fail");

                           // Ask for another binding
                           useAbilityIA.action.RemoveBindingOverride(0);
                           CleanUp();
                           StartRebinding(name);
                           return;
                       }
                       else
                       {
                           RebindComplete(name);
                       }

                   })
                   .Start();
        }
        else if (name == "Interact")
        {
            I_startRebindObject.SetActive(false);
            I_waitingForInputObject.SetActive(true);

            rebindingOperation = interactIA.action.PerformInteractiveRebinding()
                   .OnMatchWaitForAnother(0.1f)
                  .OnComplete(operation =>
                  {
                      if (CheckDuplicates(interactIA.action) || ExcludeBinding(interactIA.action, "<Keyboard>/escape"))
                      {
                          // Audio Queue
                          AudioManager.Instance.Play("Fail");

                          // Ask for another binding
                          interactIA.action.RemoveBindingOverride(0);
                          CleanUp();
                          StartRebinding(name);
                          return;
                      }
                      else
                      {
                          RebindComplete(name);
                      }

                  })
                   .Start();
        }

    }

    // Prevent binding to a given path
    private bool ExcludeBinding(InputAction action, string effectivePath)
    {
        int bindingIndex = action.GetBindingIndex();
        InputBinding newBinding = action.bindings[bindingIndex];
        if (newBinding.effectivePath == effectivePath)
        {
            return true;
        }
        return false;
    }

    // Checks for duplicate bindings with rest of bindings
    private bool CheckDuplicates(InputAction action)
    {
        int bindingIndex = action.GetBindingIndex();
        InputBinding newBinding = action.bindings[bindingIndex];
        foreach (InputBinding binding in action.actionMap.bindings)
        {

            if (binding.action == newBinding.action)
            {
                continue;
            }

            if (binding.effectivePath == newBinding.effectivePath)
            {
                // Duplicate Binding Found
                return true;
            }
        }
        return false;
    }

    // Assign bindings and update text
    private void RebindComplete(string name)
    {
        if (name == "Fire")
        {
            // Update Display for Fire Binding
            int FW_bindingIndex = fireWeaponIA.action.GetBindingIndex();

            FW_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                fireWeaponIA.action.bindings[FW_bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            FW_startRebindObject.SetActive(true);
            FW_waitingForInputObject.SetActive(false);
        }
        else if (name == "Ability")
        {
            // Update Display for Ability Binding
            int UA_bindingIndex = useAbilityIA.action.GetBindingIndex();

            UA_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                useAbilityIA.action.bindings[UA_bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            UA_startRebindObject.SetActive(true);
            UA_waitingForInputObject.SetActive(false);
        }
        else if (name == "Interact")
        {
            // Update Display for Interact Binding
            int I_bindingIndex = interactIA.action.GetBindingIndex();

            I_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                interactIA.action.bindings[I_bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            I_startRebindObject.SetActive(true);
            I_waitingForInputObject.SetActive(false);
        }

        // Handles Memory 
        CleanUp();
        Save();
    }

    // Clean up memory
    private void CleanUp()
    {
        rebindingOperation?.Dispose();
        rebindingOperation = null;
    }

    // Reset Button
    public void ResetBindings()
    {
        // Audio
        AudioManager.Instance.Play("Click");

        // Reset Fire Bindings
        int FW_bindingIndex = fireWeaponIA.action.GetBindingIndex();
        fireWeaponIA.action.ApplyBindingOverride("<Mouse>/leftButton");

        FW_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
        fireWeaponIA.action.bindings[FW_bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);


        // Reset Ability Bindings
        int UA_bindingIndex = useAbilityIA.action.GetBindingIndex();
        useAbilityIA.action.ApplyBindingOverride("<Mouse>/rightButton");

        UA_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
        useAbilityIA.action.bindings[UA_bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);


        // Reset Interact Bindings
        int I_bindingIndex = interactIA.action.GetBindingIndex();
        interactIA.action.ApplyBindingOverride("<Keyboard>/e");

        I_bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
        interactIA.action.bindings[I_bindingIndex].effectivePath,
             InputControlPath.HumanReadableStringOptions.OmitDevice);

        // Handles Memory
        Save();
        CleanUp();

    }
}
