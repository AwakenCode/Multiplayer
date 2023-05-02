using System;
using System.Collections;
using Infrastructure.Boot;
using UnityEngine.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Service
{
    public class SceneLoader : IService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, bool isAdditive = false, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(name, isAdditive, onLoaded));

        public void AddDontDestroyObject(GameObject gameObject) => Object.DontDestroyOnLoad(gameObject);
        
        private IEnumerator LoadScene(string target, bool isAdditive, Action onLoaded)
        {
            if(SceneManager.GetActiveScene().name == target)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitScene = isAdditive ? SceneManager.LoadSceneAsync(target, LoadSceneMode.Additive) :
                SceneManager.LoadSceneAsync(target, LoadSceneMode.Single);

            while(waitScene.isDone == false)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}