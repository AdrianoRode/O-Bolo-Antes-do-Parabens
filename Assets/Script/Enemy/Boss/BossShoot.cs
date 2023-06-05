using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    void Update()
    {
        transform.Translate(new Vector3(0,0,20f) * Time.deltaTime, Space.Self);
    }
}
