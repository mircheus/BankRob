using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{ 
       [SerializeField] private InputAction _mouseClick;
       [SerializeField] private float _mouseDragPhysicsSpeed = 10;
       [SerializeField] private float _mouseDragSpeed;
       [SerializeField] private LayerMask _draggableLayer;

       private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
       private Vector3 _velocity = Vector3.zero;
       private Camera _mainCamera;

       private void Awake()
       {
              _mainCamera = Camera.main;
       }

       private void OnEnable()
       {
              _mouseClick.Enable();
              _mouseClick.performed += MousePressed;
       }

       private void OnDisable()
       {
              _mouseClick.Disable();
       }

       private void MousePressed(InputAction.CallbackContext context)
       {
              Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
              RaycastHit hit;

              if (Physics.Raycast(ray, out hit, 10000f, _draggableLayer))
              {
                     bool isDraggable = hit.transform.gameObject.TryGetComponent(out IDrag iDragComponent);
                     
                     if (hit.collider != null && isDraggable)
                     {
                            StartCoroutine(DragUpdate(hit.collider.gameObject));
                     }
              }
       }

       private IEnumerator DragUpdate(GameObject clickedObject)
       {
              float initialDistance = Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);
              float initialCoordinateZ = clickedObject.transform.position.z;
              
              clickedObject.TryGetComponent<Rigidbody>(out var rigidbody);
              clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
              iDragComponent?.OnStartDrag();
              
              while (_mouseClick.ReadValue<float>() != 0)
              { 
                     Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                     
                     if (rigidbody != null)
                     {
                            Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                            rigidbody.velocity = direction * _mouseDragPhysicsSpeed;
                            yield return _waitForFixedUpdate;
                     }
                     else
                     {
                            Vector3 tempRay = ray.GetPoint(initialDistance);
                            Vector3 target = new Vector3(tempRay.x, tempRay.y, initialCoordinateZ);
                            // clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref _velocity, _mouseDragSpeed);
                            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, target, ref _velocity, _mouseDragSpeed);
                            yield return null;
                     }
              }
              
              iDragComponent?.OnEndDrag();
       }
}
