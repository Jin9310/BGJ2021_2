using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    private float _killMe = 2f;

    private void Update()
    {
        _killMe -= Time.deltaTime;
        if (_killMe <= 0)
        {
            Destroy(gameObject);
        }
    }
}
