using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Scripts.Core.EventBus;

namespace Scripts.Core.UI
{
    public class SetPointCountView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField xCount;
        [SerializeField]
        private TMP_InputField yCount;
        [SerializeField]
        private TMP_InputField zCount;

        private int x = 1;
        private int y = 1;
        private int z = 1;

        public void Awake()
        {
            xCount.text = x.ToString();
            yCount.text = y.ToString();
            zCount.text = z.ToString();
        }
        public void SetXCount()
        {
            if (int.TryParse(xCount.text, out int result))
            {
                if (result > 0)
                {
                    x = result;
                }
                else
                {
                    new TipMessage("x must be greater than 0", 3).Send<TipMessage>();
                    xCount.text = x.ToString();
                }
            }
            else
            {
                new TipMessage("x must be number greater than 0", 3).Send<TipMessage>();
                xCount.text = x.ToString();
            }
        }

        public void SetYCount()
        {
            if (int.TryParse(yCount.text, out int result))
            {
                if (result > 0)
                {
                    y = result;
                }
                else
                {
                    new TipMessage("y must be greater than 0", 3).Send<TipMessage>();
                    xCount.text = y.ToString();
                }
            }
            else
            {
                new TipMessage("y must be number greater than 0", 3).Send<TipMessage>();
                xCount.text = y.ToString();
            }
        }
        public void SetZCount()
        {
            if (int.TryParse(zCount.text, out int result))
            {
                if (result > 0)
                {
                    z = result;
                }
                else
                {
                    new TipMessage("z must be greater than 0", 3).Send<TipMessage>();
                    xCount.text = z.ToString();
                }
            }
            else
            {
                new TipMessage("z must be number greater than 0", 3).Send<TipMessage>();
                xCount.text = z.ToString();
            }
        }

        public void GenerateOnClick()
        {
            (new GeneratePointsMessage(x, y, z)).Send<GeneratePointsMessage>();
        }
        public void ClearPointsOnClick()
        {
            new ClearPointsMessage().Send<ClearPointsMessage>();
        }
    }
}