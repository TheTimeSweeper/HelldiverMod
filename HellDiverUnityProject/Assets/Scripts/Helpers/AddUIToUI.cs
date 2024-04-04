using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AddUIToUI : MonoBehaviour
{
    [SerializeField]
    private string childPath;


    [SerializeField]
    private GameObject UIPrefab;

    [SerializeField]
    private GameObject stratagemPrefab;
    [Space]
    [SerializeField]
    private GameObject spawnedUI;

    public void AddHellDiverUI() {

        spawnedUI = PrefabUtility.InstantiatePrefab(UIPrefab, transform.GetChild(0).Find(childPath)) as GameObject;
        PrefabUtility.InstantiatePrefab(stratagemPrefab, spawnedUI.transform.GetChild(0));
    }
}
