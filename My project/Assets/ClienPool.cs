using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClienPool : ObjectPooler
{
    #region Singleton
    public static ClienPool Instance;

private void Awake()
{
    Instance = this;
}
    #endregion
}

