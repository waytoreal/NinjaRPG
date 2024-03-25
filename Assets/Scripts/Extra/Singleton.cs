using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour�� ��� �޾Ұ�
// T�� MonoBehaviour�� ��ӹ��� Ŭ�������� �Ѵ�.
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}
