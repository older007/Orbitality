using UnityEngine;

namespace OrbitalSystem.Element
{
    public class OrbitalElementMover : MonoBehaviour, IElementMover<OrbitalModel>
    {
        [SerializeField] private GameObject objectToMove;
        public OrbitalModel ElementModel { get; private set; }

        private Transform moveTransform => objectToMove.transform;
        
        public void Init(OrbitalModel data)
        {
            ElementModel = data;
            
            objectToMove.transform.localScale = ElementModel.Size;
            objectToMove.transform.position = ElementModel.Position;

        }

        public void MoveAndRotate()
        {
            var orbitDesiredPosition = (ElementModel.Position - Vector3.zero).normalized * ElementModel.Radius + Vector3.zero;
            var localPosition = moveTransform.localPosition;
            
            localPosition = Vector3.Slerp(localPosition, orbitDesiredPosition, Time.deltaTime * ElementModel.MoveSpeed);
            moveTransform.localPosition = localPosition;
            moveTransform.RotateAround (Vector3.zero, Vector3.up, ElementModel.RotateSpeed * Time.deltaTime);
            
            ElementModel.Position = localPosition;
        }
    }
}