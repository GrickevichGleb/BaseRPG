using RPG.Attributes;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health healthComponent = null;
        [SerializeField] private RectTransform foreground = null;
        [SerializeField] private Canvas rootCanvas = null; 
        
        void Update()
        {
            if (Mathf.Approximately(healthComponent.GetFration(), 0)
                || Mathf.Approximately(healthComponent.GetFration(), 1))
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(healthComponent.GetFration(), 1, 1);
        }
    }
}
