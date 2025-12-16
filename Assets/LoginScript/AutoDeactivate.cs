using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    public byte secToWait;

    void OnEnable()
    {
        Invoke(nameof(DeActiveAfterDelay), secToWait);
    }

    void DeActiveAfterDelay()
    {
        gameObject.SetActive(false);
    }
}