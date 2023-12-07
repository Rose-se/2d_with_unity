using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class Singletons : MonoBehaviour
{
    private static Singletons _instance;

    public static Singletons Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Singletons>();

                if(_instance == null)
                {
                    GameObject singletonObject = new GameObject("Singletons");
                    _instance = singletonObject.AddComponent<Singletons>();
                }
            }
            return _instance;
        }
    }
        private void await()
        {
            if(_instance != null) Destroy(obj:this);
            DontDestroyOnLoad(this);
        }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}