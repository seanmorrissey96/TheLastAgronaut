using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("YYYYYYYY");
        Debug.Log(GetType().FullName);

        DontDestroyOnLoad(gameObject.transform.root);

        if (FindObjectsOfType(GetType()).Length > 2)
        {
            Destroy(gameObject);
        }
    }
}
