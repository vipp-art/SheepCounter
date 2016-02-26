using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Sys
{

    public class TouchHandler
        : MonoBehaviour
        , IPointerDownHandler
        , IPointerUpHandler
        , IEventSystemHandler
    {
        private bool isDown_;

        [SerializeField]
        public UnityEngine.UI.Button.ButtonClickedEvent OnDown;

        public void OnPointerDown( PointerEventData eventData )
        {
            isDown_ = true;
        }

        public void OnPointerUp( PointerEventData eventData )
        {
            isDown_ = false;
        }

        void Start()
        {
            isDown_ = false;
        }

        void Update()
        {
            if ( isDown_ ) {
                OnDown.Invoke();
            }
        }
    }
}