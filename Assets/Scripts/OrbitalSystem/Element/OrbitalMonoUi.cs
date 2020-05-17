using System.Globalization;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace OrbitalSystem.Element
{
    public class OrbitalMonoUi : MonoBase, IOrbitalElementUi
    {
        [SerializeField] private TMP_Text orbitalText;

        private Transform targetTransform;
        
        public void Init(Transform target)
        {
            targetTransform = target;
            transform.parent = null;
        }

        public void UpdateData(OrbitalModel model)
        {
            orbitalText.text = model.HealthPoint.ToString(CultureInfo.InvariantCulture);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        protected override void OnUpdate()
        {
            if (targetTransform == null)
            {
                return;
            }

            var target = targetTransform.position;
            
            transform.position = targetTransform.position;
        }
    }
}