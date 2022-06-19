using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // this script handles the swapping of weapons
    // order of the weapons under the weapon manager matters

    [SerializeField] private BasePossessionState currentState; // current state of the player
    public GameObject[] weaponArr; // Array of available weapons
    public GameObject currWeapon; // Reference to current weapon
    private Transform weaponManager;

    private int totalWeapons;
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
            if (currentState.ToString() == "SkeletonTransformation")
            {
                weaponArr[currWeaponIndex].SetActive(false); // deactivates current weapon
                currWeaponIndex = 1; // sets the weapon to the state's weapon
                weaponArr[currWeaponIndex].SetActive(true); // activates the current weapon
                currWeapon = weaponArr[currWeaponIndex]; // changes the weapon to the current weapon
                GetComponent<PlayerWeapon>().updateWeapon(currWeapon);
            } else
            {
                Debug.Log("no, no, i clean now");
            }
        }
    }




}
