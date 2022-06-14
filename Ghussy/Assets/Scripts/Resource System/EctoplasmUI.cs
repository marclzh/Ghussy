using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EctoplasmUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EctoplasmText;
   
    public void DisplayCurrentEctoplasmAmount(int ectoplasmAmount)
    {
        EctoplasmText.text = ectoplasmAmount.ToString();
    }
}
