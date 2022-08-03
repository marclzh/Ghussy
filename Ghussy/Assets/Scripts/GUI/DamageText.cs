using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float yAxisOffSet = .15f;

    void Start()
    {

        // Random text offset
        float xValue = Random.Range(-1 * 0.05f, 0.05f);
        float yValue = Random.Range(-1 * 0.05f, 0.05f);
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(xValue, yValue + yAxisOffSet, 0);

    }

    private void Update()
    {
        // Flip text as needed
        RectTransform rt = transform.GetComponent<RectTransform>();
        rt.localScale = rt.localScale.x < 0 ? new Vector3(-1 * rt.localScale.x, rt.localScale.y, 1) : rt.localScale;
    }

}
