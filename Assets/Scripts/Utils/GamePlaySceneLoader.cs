using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utils
{
    public class GamePlaySceneLoader : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button resetButton;

        [SerializeField] private Button soloModeButton;
        [SerializeField] private Button pveModeButton;
        [SerializeField] private TMP_Text modeText;
        
        private TMP_Text startButtonText;
        
        void Start()
        {
            startButton.onClick.AddListener(StartGame);
            resetButton.onClick.AddListener(ResetGame);
            
            soloModeButton.onClick.AddListener(SoloModeClick);
            pveModeButton.onClick.AddListener(PveModeClick);

            modeText.text = string.Empty;
            startButtonText = startButton.GetComponentInChildren<TMP_Text>();

            ValidateButtons();
        }

        private void PveModeClick()
        {
            PlayerPrefs.SetInt(Constants.GameMode, Constants.Pve);

            modeText.text = Constants.PveDescription;
        }

        private void SoloModeClick()
        {
            PlayerPrefs.SetInt(Constants.GameMode, Constants.Solo);
            
            modeText.text = Constants.SoloDescription;
        }

        private void ValidateButtons()
        {
            var isHaveSave = PlayerPrefs.HasKey(Constants.IsSaved);

            resetButton.gameObject.SetActive(isHaveSave);
            
            startButtonText.text = isHaveSave ? Constants.ContinueButtonText : Constants.PlayButtonText;
            
            soloModeButton.gameObject.SetActive(!isHaveSave);
            pveModeButton.gameObject.SetActive(!isHaveSave);

            if (isHaveSave)
            {
                if (PlayerPrefs.GetInt(Constants.GameMode) == Constants.Pve)
                {
                    PveModeClick();
                }
                else
                {
                    SoloModeClick();
                }
            }
            else
            {
                SoloModeClick();
            }
        }

        private void ResetGame()
        {
            PlayerPrefs.DeleteAll();

            ValidateButtons();
        }

        private void StartGame()
        {
            SceneManager.LoadScene("GamePlayScene");
        }
    }
}
