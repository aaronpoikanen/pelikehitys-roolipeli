using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector2 lastMovement;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed;

    DoorController currentDoor;

    [Header("Oven napit (liitä Inspectorista)")]
    [SerializeField] Button openButton;
    [SerializeField] Button closeButton;
    [SerializeField] Button lockButton;
    [SerializeField] Button unlockButton;

    void Start()
    {
        lastMovement = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();

        if (openButton == null || closeButton == null || lockButton == null || unlockButton == null)
        {
            Debug.LogError("Yksi tai useampi oven nappi puuttuu! Liitä napit Inspectorista.");
            return;
        }

        openButton.onClick.AddListener(OnOpenButton);
        closeButton.onClick.AddListener(OnCloseButton);
        lockButton.onClick.AddListener(OnLockButton);
        unlockButton.onClick.AddListener(OnUnlockButton);

        SetDoorButtons(false);
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + lastMovement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = collision.GetComponent<DoorController>();
            SetDoorButtons(true);
            Debug.Log("Found Door");
        }
        else if (collision.CompareTag("Merchant"))
        {
            Debug.Log("Found Merchant");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = null;
            SetDoorButtons(false);
            Debug.Log("Left Door");
        }
    }

    void OnMoveAction(InputValue value)
    {
        lastMovement = value.Get<Vector2>();
    }

    void OnOpenButton()
    {
        if (currentDoor != null)
            currentDoor.ReceiveAction(DoorController.DoorAction.Open);
    }

    void OnCloseButton()
    {
        if (currentDoor != null)
            currentDoor.ReceiveAction(DoorController.DoorAction.Close);
    }

    void OnLockButton()
    {
        if (currentDoor != null)
            currentDoor.ReceiveAction(DoorController.DoorAction.Lock);
    }

    void OnUnlockButton()
    {
        if (currentDoor != null)
            currentDoor.ReceiveAction(DoorController.DoorAction.Unlock);
    }

    void SetDoorButtons(bool state)
    {
        if (openButton != null) openButton.gameObject.SetActive(state);
        if (closeButton != null) closeButton.gameObject.SetActive(state);
        if (lockButton != null) lockButton.gameObject.SetActive(state);
        if (unlockButton != null) unlockButton.gameObject.SetActive(state);
    }
}
