using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Template class for creating a singleton of a specific type inheriting from MonoBehaviour.
/// </summary>
/// <typeparam name="TSingleton">Singleton type</typeparam>
public class SingletonTemplate<TSingleton> : MonoBehaviour where TSingleton : MonoBehaviour 
{
    #region F/P
    /// <summary>
    /// Static instance of the singleton
    /// </summary>
    static TSingleton instance = null;

    /// <summary>
    /// Static getter for instance
    /// </summary>
    public static TSingleton Instance => instance;
    #endregion

    void Awake() => InitSingleton();

    /// <summary>
    /// Create a new singleton or destroy new instance if singleton already exists.
    /// </summary>
    void InitSingleton()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as TSingleton;
        name += $"MANAGER => {instance.name}";
    }
}
