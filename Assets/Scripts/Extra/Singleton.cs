using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour를 상속 받았고
// T는 MonoBehaviour를 상속받은 클래스여야 한다.
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}
