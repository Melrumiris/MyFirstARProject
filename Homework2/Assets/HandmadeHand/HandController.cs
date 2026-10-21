using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AdvancedHandController : MonoBehaviour
{
    [Header("VR Hardware Inputs")]
    public InputActionProperty thumbAction;
    public InputActionProperty indexAction;
    public InputActionProperty middleAction;
    public InputActionProperty ringAction;
    public InputActionProperty pinkyAction;

    [Header("Keyboard Overrides")]
    public bool testWithoutVR = true; 
    public Key thumbKey = Key.Digit1;
    public Key indexKey = Key.Digit2;
    public Key middleKey = Key.Digit3;
    public Key ringKey = Key.Digit4;
    public Key pinkyKey = Key.Digit5;

    private Animator animator;
    private bool thumbToggle, indexToggle, middleToggle, ringToggle, pinkyToggle;
    private readonly float threshold = 0.05f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current[thumbKey].wasPressedThisFrame) thumbToggle = !thumbToggle;
            if (Keyboard.current[indexKey].wasPressedThisFrame) indexToggle = !indexToggle;
            if (Keyboard.current[middleKey].wasPressedThisFrame) middleToggle = !middleToggle;
            if (Keyboard.current[ringKey].wasPressedThisFrame) ringToggle = !ringToggle;
            if (Keyboard.current[pinkyKey].wasPressedThisFrame) pinkyToggle = !pinkyToggle;
        }

        float vrThumb = 0f, vrIndex = 0f, vrMiddle = 0f, vrRing = 0f, vrPinky = 0f;

        if (!testWithoutVR)
        {
            vrThumb = GetSafeVRValue(thumbAction);
            vrIndex = GetSafeVRValue(indexAction);
            vrMiddle = GetSafeVRValue(middleAction);
            vrRing = GetSafeVRValue(ringAction);
            vrPinky = GetSafeVRValue(pinkyAction);
        }

        if (vrThumb > threshold) thumbToggle = false;
        if (vrIndex > threshold) indexToggle = false;
        if (vrMiddle > threshold) middleToggle = false;
        if (vrRing > threshold) ringToggle = false;
        if (vrPinky > threshold) pinkyToggle = false;

        float finalThumb = vrThumb > threshold ? vrThumb : (thumbToggle ? 1f : 0f);
        float finalIndex = vrIndex > threshold ? vrIndex : (indexToggle ? 1f : 0f);
        float finalMiddle = vrMiddle > threshold ? vrMiddle : (middleToggle ? 1f : 0f);
        float finalRing = vrRing > threshold ? vrRing : (ringToggle ? 1f : 0f);
        float finalPinky = vrPinky > threshold ? vrPinky : (pinkyToggle ? 1f : 0f);

        animator.SetFloat("Thumb", finalThumb);
        animator.SetFloat("Index", finalIndex);
        animator.SetFloat("Middle", finalMiddle);
        animator.SetFloat("Ring", finalRing);
        animator.SetFloat("Pinky", finalPinky);
    }

    private float GetSafeVRValue(InputActionProperty property)
    {
        if (property.action != null && property.action.enabled)
        {
            return property.action.ReadValue<float>();
        }
        return 0f;
    }
}