using UnityEngine;

/**
 * This class handles the swapping of the weapons between transformations.
 */
public class WeaponManager : MonoBehaviour
{
    // current state of the player
    public BasePossessionState currentState;
    // Array of available weapons
    public GameObject[] weaponArr;
    // Reference to current weapon
    public GameObject currWeapon; 
    // Reference to the weapon manager's transform.
    private Transform weaponManager;
    // Count of the total number of weapons.
    private int totalWeapons;
    // Index of the currently active weapon.
    private int currWeaponIndex;

    private void Awake()
    {
        // initialising the reference to the weaponManager
        weaponManager = GetComponent<Transform>();
        totalWeapons = weaponManager.childCount;

        // array of available weapons
        weaponArr = new GameObject[totalWeapons];

        // initialising the current active weapon
        for (int i = 0; i < weaponArr.Length; i++)
        {
            weaponArr[i] = weaponManager.GetChild(i).gameObject;
            weaponArr[i].SetActive(false);
        }

        weaponArr[0].SetActive(true);
        currWeapon = weaponArr[0];
    }

    // Updating the state to update the weapon
    public void SetState(BasePossessionState nextState)
    {
        if (nextState != null && !currentState.Same(nextState))
        {
            currentState = nextState;
            if (currentState.ToString() == "Default")
            {
                SetActiveWeapon(0);
            }
             else if (currentState.ToString() == "SkeletonTransformation")
            {
                SetActiveWeapon(1);
            } 
            else if (currentState.ToString() == "FridgeTransformation")
            {
                SetActiveWeapon(2);
            }
            else
            {
                return;
            }
        }
    }

    // Setting the active weapon
    private void SetActiveWeapon(int i)
    {
        weaponArr[currWeaponIndex].SetActive(false); // deactivates current weapon
        currWeaponIndex = i; // sets the weapon to the state's weapon
        weaponArr[currWeaponIndex].SetActive(true); // activates the current weapon
        currWeapon = weaponArr[currWeaponIndex]; // changes the weapon to the current weapon
        GetComponent<PlayerWeapon>().updateWeapon(currWeapon);
    }
}