using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace DefaultNamespace.Monetization
{
    public class AdService : MonoBehaviour
    {
        [SerializeField] private Bank[] _floorBanks;
        [SerializeField] private string _rewardedID;

        [SerializeField] private GameObject _thanksMessage;
        [SerializeField] private GameObject _infoMessage;

        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Image _progressBar;
        
        [SerializeField] private GameObject _faderDelayer;
        [SerializeField] private TMP_Text _delayerText;
        private readonly WaitForSeconds _waiter = new WaitForSeconds(1f);

        private float _timer;
        private float _timerCooldown;

        public void OnButtonPushed()
        {
            if (_timer > 0.1f || _timerCooldown > 0.1f)
                return;

            StartCoroutine(AdDelay());
        }

        private void SetMaxPrices()
        {
            AudioListener.volume = 1f;
            _floorBanks.ToList().ForEach(x => x.SetMaxPrice());
            StartCoroutine(AdRewardedTimer());
        }

        IEnumerator AdRewardedTimer()
        {
            _thanksMessage.SetActive(true);
            _infoMessage.SetActive(false);

            _timerText.text = "Наслаждайтесь макс. ценой еще 20 секунд.";
            _progressBar.transform.parent.gameObject.SetActive(true);
            _progressBar.color = Color.green;
            while (_timer < 20f)
            {
                yield return null;
                
                _timerText.text = $"Наслаждайтесь макс. ценой еще {20f - _timer:0.0} секунд.";
                _timer += Time.deltaTime;
                _progressBar.transform.localScale = new Vector3(Mathf.Clamp01(1f - _timer / 20f), 1f, 1f);
            }

            _timer = 0f;
            
            StartCoroutine(AdRewardedCooldown());
        }
        
        IEnumerator AdRewardedCooldown()
        {
            _timerText.text = "Вы сможете посмотреть рекламу через: 120 секунд";
            
            _progressBar.color = Color.red;
            while (_timerCooldown < 120f)
            {
                yield return null;
                
                _timerText.text = $"Вы сможете посмотреть рекламу через: {120f - _timerCooldown:0.0} секунд.";
                _timerCooldown += Time.deltaTime;
                _progressBar.transform.localScale = new Vector3(Mathf.Clamp01(1f - _timerCooldown / 120f), 1f, 1f);
            }

            _timerCooldown = 0f;
            _progressBar.transform.parent.gameObject.SetActive(false);
            _thanksMessage.SetActive(false);
            _infoMessage.SetActive(true);
        }
        
        private IEnumerator AdDelay()
        {
            int time = 3;
            _faderDelayer.SetActive(true);
            _delayerText.text = "Реклама начнется через: 3";
            AudioListener.volume = 0f;
            while (time > 0)
            {
                yield return _waiter;
                time--;
                _delayerText.text = $"Реклама начнется через: {time}";

            }
        
            _faderDelayer.SetActive(false);
            YG2.RewardedAdvShow(_rewardedID, SetMaxPrices);
        }
    }
}