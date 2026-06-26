using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class TutorialSceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject hint1;
        [SerializeField] private GameObject hint2;
        [SerializeField] private GameObject hint3;

        public void ContinueHint1()
        {
            hint1.SetActive(false);
            hint2.SetActive(true);
        }

        public void ContinueHint2()
        {
            hint2.SetActive(false);
            hint3.SetActive(true);
        }

        public void Play()
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
