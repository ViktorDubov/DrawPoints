using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

using Scripts.Core.EventBus;

namespace Scripts.Core.UI
{
    public class TipView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        
        [SerializeField]
        private WeakSubscription<TipMessage> tipSubscription;

        public void Awake()
        {
            tipSubscription = new WeakSubscription<TipMessage>(ShowTip);
        }

        private void ShowTip(TipMessage message)
        {
            text.text = message.Tip;
            HideAfterDelay(message.ShowDelaySec).Forget();
        }
        private async UniTask HideAfterDelay(float delaySec)
        {
            await UniTask.Delay(Mathf.RoundToInt(delaySec * 1000));
            text.text = "";
        }

    }
}

