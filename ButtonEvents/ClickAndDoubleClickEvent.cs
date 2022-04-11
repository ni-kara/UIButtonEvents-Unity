using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Button.Event
{
    public class ClickAndDoubleClickEvent : MonoBehaviour, IPointerClickHandler
    {
        public Action onClick;
        public Action onDoubleClick;

        private float timeDistanceBetweenClickInMS = 0.3f;
        private int clickCount = 0;

        private Coroutine coroutine;

        public void OnPointerClick(PointerEventData eventData)
        {
            clickCount++;
            if (coroutine == null)
                coroutine = StartCoroutine(ClickAndDoubleClickMechanic());
        }

        private IEnumerator ClickAndDoubleClickMechanic()
        {
            float time = 0;
            while (true)
            {
                time += Time.deltaTime;
                if (time > timeDistanceBetweenClickInMS)
                {
                    onClick.Invoke();
                    ResetDoubleClickMechanic();
                }
                else
                {
                    if (clickCount >= 2)
                    {
                        onDoubleClick.Invoke();
                        ResetDoubleClickMechanic();
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }

        private void ResetDoubleClickMechanic()
        {
            StopCoroutine(coroutine);
            coroutine = null;
            clickCount = 0;
        }
    }
}
