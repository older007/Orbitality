using System.Globalization;
using OrbitalSystem.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace BattleSystem
{
    public class RocketView : MonoBehaviour, IRocketView
    {
        [SerializeField] private TMP_Text callDawnText;
        [SerializeField] private Image viewImage;

        private float callDawn = 0;
        public void SetData(RocketData data)
        {
            viewImage.sprite = data.Icon;

            UpdateText();
        }

        public void UpdateData(RocketData data)
        {
            UpdateText();
        }

        public void UpdateCallDown(float currentCallDown)
        {
            callDawn = currentCallDown;
            
            UpdateText();
        }

        private void UpdateText()
        {
            callDawnText.text = callDawn.ToString("00");
        }
    }
}