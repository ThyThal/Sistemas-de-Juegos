using UnityEngine;

public interface IFactory<T> where T : MonoBehaviour
{
    T Create(T original);
}
