using System;
using OrbitalSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace GameMode
{
    public class GameModeView : MonoBehaviour, IMonoView<string, Action>
    {
        [SerializeField] private Image backGround;
        [SerializeField] private TMP_Text textElement;
        [SerializeField] private TMP_Text currentMode;
        [SerializeField] private Button backToMenu;

        private Action clickCallback;
        
        private void Awake()
        {
            backGround.gameObject.SetActive(false);
            backToMenu.gameObject.SetActive(false);
            textElement.text = string.Empty;
            currentMode.text = string.Empty;
            
            backToMenu.onClick.AddListener(BackClick);
        }

        private void BackClick()
        {
            clickCallback?.Invoke();
        }
        
        public void SetData(string data1, Action data2)
        {
            currentMode.text = data1;
            clickCallback = data2;
        }

        public void UpdateData(string data)
        {
            backToMenu.gameObject.SetActive(true);
            backGround.gameObject.SetActive(true);
            currentMode.text = string.Empty;
            textElement.text = data;
        }
    }
}