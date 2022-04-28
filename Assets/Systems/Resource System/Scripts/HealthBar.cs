using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image fill;
        [SerializeField]
        private Image highlight;
        [SerializeField, Min(0)]
        private float changeTime;
        private Coroutine ChangingFillPercentageCoroutine = null;
        private Camera camera;
        private Health _health;
        public Health health
        {
            set
            {
                if (_health)
                    _health.OnHealthAmountChanged -= OnHealthAmountChanged;

                _health = value;
                if (_health)
                    _health.OnHealthAmountChanged += OnHealthAmountChanged;
            }
        }

        private void OnHealthAmountChanged(Health health)
        {
            if (ChangingFillPercentageCoroutine != null)
                StopCoroutine(ChangingFillPercentageCoroutine);

            ChangingFillPercentageCoroutine = StartCoroutine(DoChangingFillPercentage());
        }

        private IEnumerator DoChangingFillPercentage()
        {
            float initialFillPercent = fill.fillAmount;
            float initialHighlightPercent = highlight.fillAmount;
            float targetPercent = _health.currentHealth / _health.maxHealth;
            float initialTime = Time.time;
            float elapsedTime = 0;
            bool increasing = initialFillPercent < targetPercent;

            fill.fillAmount = increasing ? initialFillPercent : targetPercent;
            highlight.fillAmount = increasing ? targetPercent : initialHighlightPercent;

            while (elapsedTime < changeTime)
            {
                elapsedTime = Time.time - initialTime;

                if (increasing)
                {
                    float percent = Mathf.Lerp(initialFillPercent, targetPercent, elapsedTime / changeTime);
                    fill.fillAmount = percent;
                }
                else
                {
                    float percent = Mathf.Lerp(initialHighlightPercent, targetPercent, elapsedTime / changeTime);
                    highlight.fillAmount = percent;
                }

                yield return null;
            }

            fill.fillAmount = targetPercent;
            highlight.fillAmount = targetPercent;

            ChangingFillPercentageCoroutine = null;
        }

        private void LateUpdate()
        {
            if (!camera)
                camera = Camera.main;
            transform.position = camera.WorldToScreenPoint(_health.uiPosition.position);
        }

        private void OnDestroy()
        {
            if (_health)
                health = null;
        }
    }
}
