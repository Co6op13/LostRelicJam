using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackagePool : ObjectPooler
{
    #region Singleton
    public static PackagePool Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
}
