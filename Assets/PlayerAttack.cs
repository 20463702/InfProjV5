using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weaponry.Ranged;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private SpriteRenderer arrowGfx;
    [SerializeField] private Slider bowPowerSlider;
    [SerializeField] private Transform bow;

    [SerializeField] private float bowPower;
    [SerializeField] private float maxBowCharge;

    private float _bowCharge;
    private bool _canFire = true;

    private void Start()
    {
        bowPowerSlider.value = 0f;
        bowPowerSlider.maxValue = maxBowCharge;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _canFire)
        {
            ChargeBow();
        }
        else if (Input.GetMouseButtonUp(0) && _canFire)
        {
            FireBow();
        }
        else
        {
            if (_bowCharge > 0f)
            {
                _bowCharge -= 0.1f * Time.deltaTime;
            }
            else
            {
                _bowCharge = 0f;
                _canFire = true;
            }

            bowPowerSlider.value = _bowCharge;
        }
    }

    private void ChargeBow()
    {
        arrowGfx.enabled = true;
        _bowCharge += Time.deltaTime;
        
        bowPowerSlider.value = _bowCharge;
        if (_bowCharge > maxBowCharge)
        {
            bowPowerSlider.value = maxBowCharge;
        }
    }

    private void FireBow()
    {
        if (_bowCharge > maxBowCharge) _bowCharge = maxBowCharge;

        float arrowSpeed = _bowCharge + bowPower;
        float angle = Utility.AngleTowardsMouse(bow.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        Arrow arrow = Instantiate(arrowPrefab, bow.position, rot).GetComponent<Arrow>();
        arrow.ArrowVelocity = arrowSpeed;

        _canFire = false;
        arrowGfx.enabled = false;
    }
}
