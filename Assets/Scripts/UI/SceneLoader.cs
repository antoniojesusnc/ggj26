using UnityEngine;
using UnityEngine.SceneManagement;

namespace ggj26
{ 
    public class SceneLoader : MonoBehaviour
    {
        //[SerializeField] private string sceneName;
        public void LoadScene(int sceneID)
        {
            SceneManager.LoadScene(sceneID);
        }

        public void QuitGame()
        {
            Application.Quit();

            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
