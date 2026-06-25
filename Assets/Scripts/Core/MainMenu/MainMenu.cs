using UnityEngine;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject saveSlotsPanel;
        public void NewGame()
        {
            saveSlotsPanel.SetActive(true);
        }

        public void ContineGame()
        {

        }

        public void OpenSettings()
        {

        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
