using System.Collections;
using Service;
using UnityEngine;

namespace Infrastructure.Boot
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}