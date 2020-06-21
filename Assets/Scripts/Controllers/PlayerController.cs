using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // left mouse btn
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Move to where we hit
                // Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);

                // stop focusing any object
                RemoveFocus();
            }
        }

        // right mouse btn
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // check if we it an interactable, if we did set it as our focus
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }
    private void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }
}
