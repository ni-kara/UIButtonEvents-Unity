using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

namespace Button.Event
{
    public class DoubleClickEvent : MonoBehaviour, IPointerClickHandler
    {
        public Action onDoubleClick;
        public float timeDistanceBetweenClickInMS = 0.5f;
        private int clickCount=0;
        private Coroutine coroutine;

        public void OnPointerClick(PointerEventData eventData)
        {
            clickCount++;
            if (coroutine == null)
                coroutine = StartCoroutine(DoubleClickMechanic());
        }      
        private IEnumerator DoubleClickMechanic()
        {
            float time=0;
            while (true)
            {                
                time += Time.deltaTime;
                if (time > timeDistanceBetweenClickInMS)
                    ResetDoubleClickMechanic();
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
