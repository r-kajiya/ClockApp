using System.Collections;
using ClockApp;
using UnityEngine.SceneManagement;

namespace ClockApp
{
    public class SceneUseCase : IUseCase
    {
        string _currentSceneName;
        
        public IEnumerator Change(string sceneName)
        {
            if (string.IsNullOrEmpty(_currentSceneName) == false)
            {
                if (_currentSceneName == sceneName)
                {
                    yield break;
                }
                
                yield return Unload(_currentSceneName);
            }

            _currentSceneName = sceneName;
            yield return Load(sceneName);
        }
        
        public void OnUpdate(float dt) { }
        
        IEnumerator Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

            var scene = SceneManager.GetSceneByName(sceneName);

            while ( !scene.isLoaded )
            {
                yield return null;
            }

            SceneManager.SetActiveScene(scene);
        }

        IEnumerator Unload(string sceneName)
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            
            while ( asyncOperation is { isDone: false } )
            {
                yield return null;
            }
        }
    }
}