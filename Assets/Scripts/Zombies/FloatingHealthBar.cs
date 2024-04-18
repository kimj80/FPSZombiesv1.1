using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera zombiecamera;
    [SerializeField] private Transform healthTarget;
    [SerializeField] private Vector3 HealthOffset;

    private void Start()
    {

    }
    public void UpdateHeathBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = healthTarget.position + HealthOffset;
    }
}
