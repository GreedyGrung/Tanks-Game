using System.Collections;
using UnityEngine;

namespace _TankGame.App.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}