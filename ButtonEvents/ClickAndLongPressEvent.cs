using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Button.Event
{
    public class ClickAndLongPressEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action onClick;
        public Action onLongPress;
        public Action<float> onProgress;

        private float timeLongPressClickInMS = 2000f;
        private float timeClickInMS = 500f;
        private bool isClicked = false;
        private bool isLongPressActivate = false;
        private Coroutine coroutine;

       private float timer = 0;
        public void OnPointerDown(PointerEventData eventData)
        {
            isClicked = true;
            if (coroutine == null)
                coroutine = StartCoroutine(LongPressMechanic());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (timer <= (timeClickInMS / 1000) && !isLongPressActivate)            
                onClick.Invoke();
            isLongPressActivate = false;
            ResetLongPress();
        }

        private IEnumerator LongPressMechanic()
        {
            while (isClicked)
            {
                timer += Time.deltaTime;

                if (onProgress != null)
                    onProgress.Invoke(timer / (timeLongPressClickInMS / 1000));
                if (timer >= (timeLongPressClickInMS / 1000))
                {
                    onLongPress.Invoke();
                    isLongPressActivate = true;
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
            timer = 0;
            coroutine = null;
            if (onProgress != null)
                onProgress.Invoke(0);
        }

        /// <summary>
        /// The default long press time is 2000ms
        /// </summary>
        /// <param name="timeInMS"></param>
        public void SetTimeForLongPress(int timeInMS = 2000) =>
            this.timeLongPressClickInMS = timeInMS;

    }


}
