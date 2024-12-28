using System.Collections;
using UnityEngine;

namespace TankGame.App.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}