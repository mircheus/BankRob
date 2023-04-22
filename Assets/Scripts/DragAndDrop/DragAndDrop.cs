using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
       [SerializeField] private InputAction _press;
       [SerializeField] private InputAction _screenPosition;
       [SerializeField] private float _dragPhysicsSpeed = 10;
       [SerializeField] private float _dragSpeed;
       [SerializeField] private LayerMask _draggableLayer;

       private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
       private Vector3 _velocity = Vector3.zero;
       private Camera _mainCamera;
       private bool _isDragging = false;

       private void Awake()
       {
              _mainCamera = Camera.main;
       }

       private void OnEnable()
       {
              _press.Enable();
              _screenPosition.Enable();
              _press.performed += TouchPressed;
              _press.canceled += TouchReleased;
       }

       private void OnDisable()
       {
              _press.performed -= TouchPressed;
              _press.canceled -= TouchReleased;
              _press.Disable();
              _screenPosition.Disable();
       }

       private void TouchPressed(InputAction.CallbackContext context)
       {
              Ray ray = _mainCamera.ScreenPointToRay(_screenPosition.ReadValue<Vector2>());
 
              if (Physics.Raycast(ray, out RaycastHit hit, 10000f, _draggableLayer))
              {
                     bool isDraggable = hit.transform.gameObject.TryGetComponent(out IDrag iDragComponent);
                     
                     if (hit.collider != null && isDraggable)
                     {
                            StartCoroutine(DragUpdate(hit.collider.gameObject));
                     }
              }   
       }

       private void TouchReleased(InputAction.CallbackContext context)
       {
              _isDragging = false;
       }
       
       private IEnumerator DragUpdate(GameObject clickedObject)
       {
              float initialDistance = Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);
              float initialCoordinateZ = clickedObject.transform.position.z;
              
              clickedObject.TryGetComponent<Rigidbody>(out var rigidbody);
              clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
              iDragComponent?.OnStartDrag();

              _isDragging = true;
              
              while (_isDragging)
              { 
                     Ray ray = _mainCamera.ScreenPointToRay(_screenPosition.ReadValue<Vector2>());
                     
                     if (rigidbody != null)
                     {
                            Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                            rigidbody.velocity = direction * _dragPhysicsSpeed;
                            yield return _waitForFixedUpdate;
                     }
                     else
                     {
                            Vector3 tempRay = ray.GetPoint(initialDistance);
                            Vector3 target = new Vector3(tempRay.x, tempRay.y, initialCoordinateZ);
                            // clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref _velocity, _mouseDragSpeed);
                            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, target, ref _velocity, _dragSpeed);
                            yield return null;
                     }
              }
              
              iDragComponent?.OnEndDrag();
       }
}
