using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

namespace Button.Event
{
    public class LongPressEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action onLongPress;
        public Action<float> onProgress;

        public float timeLongPressClickInMS = 2000f;
        private bool isClicked=false;
        Coroutine coroutine;
        public void OnPointerDown(PointerEventData eventData)
        {
            isClicked = true;
            if (coroutine == null)
                coroutine = StartCoroutine(LongPressMechanic());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetLongPress();
        }
        private IEnumerator LongPressMechanic()
        {
            float timer = 0;
            while (isClicked)
            {
                timer += Time.deltaTime;
                onProgress(timer / (timeLongPressClickInMS / 1000));
                if (timer>= (timeLongPressClickInMS/1000))
                {
                    onLongPress.Invoke();
                    ResetLongPress(); 
                }
                yield return new WaitForEndOfFrame();
            }
        }
        private void ResetLongPress()
        {
            isClicked = false;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = null;
            onProgress(0);
        }
    }       
}
