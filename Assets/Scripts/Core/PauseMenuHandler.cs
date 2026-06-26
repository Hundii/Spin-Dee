using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Core
{
    public class PauseMenuHandler : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        private InputSystem_Actions inputActions;
        private float previousTimeScale;

        private void Awake()
        {
            inputActions = new();

        }
        private void OnEnable()
        {
            inputActions.Enable();
        }
        private void Start()
        {
            inputActions.UI.Cancel.performed += HandleCancel;
        }

        private void HandleCancel(InputAction.CallbackContext context)
        {
            if (pauseMenu.activeSelf)
            {
                Close();
                return;
            }
            Open();
        }

        public void Open()
        {
            pauseMenu.SetActive(true);
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        public void Close()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = previousTimeScale;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("GameSelectionScene");
            Time.timeScale = previousTimeScale;
        }

        private void OnDisable()
        {
            inputActions.Disable();
            inputActions.UI.Cancel.performed -= HandleCancel;
        }
    }
}
