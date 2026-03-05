using UnityEngine;
using UnityEngine.Rendering;

public class DoorController : MonoBehaviour
{
    // Kuvat oven eri tiloille
    [SerializeField] Sprite ClosedDoorSprite;
    [SerializeField] Sprite OpenDoorSprite;
    [SerializeField] Sprite LockedSprite;
    [SerializeField] Sprite UnlockedSprite;

    BoxCollider2D colliderComp;

    // Näitä värejä käytetään lukkosymbolin piirtämiseen.
    public static Color lockedColor;
    public static Color openColor;

    SpriteRenderer doorSprite; // Oven kuva
    SpriteRenderer lockSprite; // Lapsi gameobjectissa oleva lukon kuva

    // Oven tilat
    private bool isOpen = false;
    private bool isLocked = false;

    // Debug ui
    [SerializeField] bool ShowDebugUI;
    [SerializeField] int DebugFontSize = 32;

    public enum DoorAction
    {
        Open,
        Close,
        Lock,
        Unlock
    }

    void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        colliderComp = GetComponent<BoxCollider2D>();

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        if (sprites.Length == 2 && sprites[0] == doorSprite)
        {
            lockSprite = sprites[1];
        }

        lockedColor = new Color(1.0f, 0.63f, 0.23f);
        openColor = new Color(0.5f, 0.8f, 1.0f);

        // Oven alkutila: kiinni ja avattu
        CloseDoor();
        UnlockDoor();
    }

    /// <summary>
    /// Oveen kohdistuu jokin toiminto joka muuttaa sen tilaa
    /// </summary>
    public void ReceiveAction(DoorAction action)
    {
        switch (action)
        {
            case DoorAction.Open:
                if (!isLocked) OpenDoor();
                break;

            case DoorAction.Close:
                CloseDoor();
                break;

            case DoorAction.Lock:
                if (!isOpen) LockDoor();
                break;

            case DoorAction.Unlock:
                UnlockDoor();
                break;
        }
    }

    private void OpenDoor()
    {
        if (!isLocked)
        {
            doorSprite.sprite = OpenDoorSprite;
            colliderComp.isTrigger = true;
            isOpen = true;
        }
    }

    private void CloseDoor()
    {
        doorSprite.sprite = ClosedDoorSprite;
        colliderComp.isTrigger = false;
        isOpen = false;
    }

    private void LockDoor()
    {
        if (!isOpen)
        {
            lockSprite.sprite = LockedSprite;
            lockSprite.color = lockedColor;
            isLocked = true;
        }
    }

    private void UnlockDoor()
    {
        lockSprite.sprite = UnlockedSprite;
        lockSprite.color = openColor;
        isLocked = false;
    }

    private void OnGUI()
    {
        if (!ShowDebugUI) return;

        GUIStyle buttonStyle = GUI.skin.GetStyle("button");
        GUIStyle labelStyle = GUI.skin.GetStyle("label");
        buttonStyle.fontSize = DebugFontSize;
        labelStyle.fontSize = DebugFontSize;

        Rect guiRect = GetGuiRect();
        GUILayout.BeginArea(guiRect);

        GUILayout.Label("Door");

        if (GUILayout.Button("Open")) OpenDoor();
        if (GUILayout.Button("Close")) CloseDoor();
        if (GUILayout.Button("Lock")) LockDoor();
        if (GUILayout.Button("Unlock")) UnlockDoor();

        GUILayout.EndArea();
    }

    private Rect GetGuiRect()
    {
        Vector3 buttonPos = transform.position;
        buttonPos.x += 1;
        buttonPos.y -= 0.25f;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(buttonPos);
        float screenHeight = Screen.height;

        return new Rect(
            screenPoint.x,
            screenHeight - screenPoint.y,
            DebugFontSize * 8,
            DebugFontSize * 100
        );
    }
}
