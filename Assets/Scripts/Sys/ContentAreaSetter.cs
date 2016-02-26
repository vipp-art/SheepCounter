using UnityEngine;
using UnityEngine.UI;

namespace Sys
{
    [ExecuteInEditMode]
    [RequireComponent( typeof( RectTransform ) )]
    public class ContentAreaSetter
        : MonoBehaviour
    {
        void Start()
        {
            Set();
        }

        void Set()
        {
            var canvas = transform.parent.GetComponent<CanvasScaler>();
            if ( canvas ) {
                var resolution = canvas.referenceResolution;

                var t = GetComponent<RectTransform>();
                t.anchoredPosition = new Vector2( .5f, .5f );
                t.anchorMin = new Vector2( .5f, .5f );
                t.anchorMax = new Vector2( .5f, .5f );
                t.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, resolution.x );
                t.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, resolution.y );

                t.localPosition = Vector3.zero;
            }
        }

#if UNITY_EDITOR
        void Update()
        {
            Set();
        }
#endif
    }
}
