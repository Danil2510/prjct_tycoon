using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace.Monetization
{
    public class InAppService : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _infoText;
        
        private float _timer;
        private bool _readyToCash;
        private bool _fridgeBought;

        private const float CooldownDuration = 30f;

        private void Start()
        {
            if (_fridgeBought)
            {
                _infoText.text =
                    $"Вы сможете насладиться пополнением кошелька на +10% через {CooldownDuration - _timer:0.0} секунд!";
                ShowProgressBar();
            }
        }

        // Context invokation
        public void OnButtonPushed()
        {
            if (_fridgeBought)
                Cashout();
            else
                ShowInAppMessage();
        }

        private void ShowInAppMessage()
        {
            // sdk show message
        }

        private void Cashout()
        {
            if (_readyToCash == false)
                return;

            _timer = 0f;
            _readyToCash = false;
            
            UpdateProgressBar();
        }

        private void Update()
        {
            if (_readyToCash || _fridgeBought == false)
                return;
            
            _timer += Time.deltaTime;
            UpdateProgressBar();
            _infoText.text =
                $"Вы сможете насладиться пополнением кошелька на +10% через {CooldownDuration - _timer:0.0} секунд!";
            
            if (_timer >= CooldownDuration)
                _readyToCash = true;
        }

        private void ShowProgressBar()
            => _progressBar.transform.parent.gameObject.SetActive(true);
        
        private void UpdateProgressBar() 
            => _progressBar.transform.localScale = new Vector3(Mathf.Clamp01(_timer / CooldownDuration), 1f, 1f);
    }
}