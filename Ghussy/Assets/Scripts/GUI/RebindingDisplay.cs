using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class RebindingDisplay : MonoBehaviour
{
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

        string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);

        if (string.IsNullOrEmpty(rebinds)) { return; }

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

    public void Save()
    {
        string rebinds = playerController.playerInput.actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString(RebindsKey, rebinds);
    }

    public void StartRebinding(string name)
    {
       if (playerController != null) { playerController.playerInput.SwitchCurrentActionMap("Menu");  }


        if (name == "Fire")
        {
            FW_startRebindObject.SetActive(false);
            FW_waitingForInputObject.SetActive(true);

            rebindingOperation = fireWeaponIA.action.PerformInteractiveRebinding()
                              .OnMatchWaitForAnother(0.1f)
                              .OnComplete(operation => 
                              {
                                  if (CheckDuplicates(fireWeaponIA.action))
                                  {
                                      // Audio Queue
                                      AudioManager.Instance.Play("Fail");

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
                       if (CheckDuplicates(useAbilityIA.action))
                       {
                           // Audio Queue
                           AudioManager.Instance.Play("Fail");

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
                      if (CheckDuplicates(interactIA.action))
                      {
                          // Audio Queue
                          AudioManager.Instance.Play("Fail");

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

    private bool CheckDuplicates(InputAction action)
    {
        int bindingIndex = action.GetBindingIndex();
        InputBinding newBinding = action.bindings[bindingIndex];
        foreach (InputBinding binding in action.actionMap.bindings)
        {
            Debug.Log("Binding : " + binding.effectivePath + " newBinding: " + newBinding.effectivePath);

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

    private void CleanUp()
    {
        rebindingOperation?.Dispose();
        rebindingOperation = null;
    }

    public void ResetBindings()
    {
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

        Save();
        CleanUp();

    }
}
