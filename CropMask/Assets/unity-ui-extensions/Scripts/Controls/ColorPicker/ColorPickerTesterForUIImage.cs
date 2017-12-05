using UnityEngine;
using System.Collections;

namespace UnityEngine.UI.Extensions.ColorPicker
{
    public class ColorPickerTesterForUIImage : MonoBehaviour
    {

        public Image pickerImageRenderer;
        public ColorPickerControl picker;

        void Awake()
        {
            pickerImageRenderer = GetComponent<Image>();
        }
        // Use this for initialization
        void Start()
        {
            //picker.onValueChanged.AddListener(color =>
            //{
            //    pickerImageRenderer.color = color;
            //});

            picker.onHSVChanged.AddListener((h, s, v) =>
            {
                pickerImageRenderer.color = new Color(h, s, v, 1);
            });
        }
    }
}
