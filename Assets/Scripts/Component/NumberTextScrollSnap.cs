using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public class NumberTextScrollSnap : MonoBehaviour
    {
        const int DECELERATION_FRAME_COUNT_MAX = 2;
        const float DECELERATION_SCROLL_VELOCITY = 50.0f;

        [SerializeField]
        int _numberMax;

        [SerializeField]
        float _lineSpace;

        int _currentNumber = 0;
        public int CurrentNumber => _currentNumber;
        float _prevVerticalVelocity;
        int _decelerationFrameCount;
        readonly List<float> _numberNormalizedPositionList = new();
        ScrollRect _scroll;
        ScrollRect Scroll
        {
            get
            {
                if (_scroll == null)
                {
                    _scroll = GetComponent<ScrollRect>();
                }

                return _scroll;
            }
        }

        void Awake()
        {
            BuildNumberList();
        }

        void BuildNumberList()
        {
            _numberNormalizedPositionList.Clear();
            int countMax = _numberMax;

            for (int i = 0; i < countMax; i++)
            {
                _numberNormalizedPositionList.Add(_lineSpace * i);
            }

            _numberNormalizedPositionList[_numberMax - 1] = 1.0f;
        }

        void Update()
        {
            float scrollVelocity = Mathf.Abs(Scroll.velocity.y);
            if (scrollVelocity <= DECELERATION_SCROLL_VELOCITY)
            {
                if (_decelerationFrameCount == 0)
                {
                    _prevVerticalVelocity = 0.0f;
                }

                if (_prevVerticalVelocity >= scrollVelocity)
                {
                    _prevVerticalVelocity = scrollVelocity;
                    _decelerationFrameCount++;

                    if (_decelerationFrameCount > DECELERATION_FRAME_COUNT_MAX)
                    {
                        _decelerationFrameCount = 0;
                        Stop();
                    }
                }
            }
            else
            {
                _decelerationFrameCount = 0;
                Nearest(_numberNormalizedPositionList, Scroll.verticalNormalizedPosition, out _currentNumber);
            }
        }

        void Stop()
        {
            Scroll.velocity = Vector2.zero;
            Scroll.verticalNormalizedPosition = Nearest(_numberNormalizedPositionList, Scroll.verticalNormalizedPosition, out _currentNumber);
        }

        float Nearest(List<float> floatList, float targetValue, out int currentNumber)
        {
            var min = floatList.Min(value => Mathf.Abs(value - targetValue));
            int index = floatList.FindIndex(value => Mathf.Approximately(Mathf.Abs(value - targetValue), min));
            currentNumber = floatList.Count - index - 1;
            return floatList[index];
        }
    }
}