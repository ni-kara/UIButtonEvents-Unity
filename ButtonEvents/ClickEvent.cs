using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Button.Event
{
    public class ClickEvent : MonoBehaviour, IPointerClickHandler
    {        
        public Action onClick;
        public void OnPointerClick(PointerEventData eventData)
        {           
            if (onClick != null) 
                onClick.Invoke();           
        }

    }

}

