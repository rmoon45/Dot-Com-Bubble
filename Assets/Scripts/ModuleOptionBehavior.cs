using System;
using UnityEngine;

public class ModuleOptionBehavior : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        TimeManager.Instance.FireNewDay += DestroyModuleOptionSpawn;
    }

    /// <summary>
    ///  Destroy the gameobject this script is attached to
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void DestroyModuleOptionSpawn(object sender, EventArgs e) {
        if (gameObject != null) {
            Destroy(gameObject);
        }
        TimeManager.Instance.FireNewDay -= DestroyModuleOptionSpawn;
    }

}
