using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

   
    void Update()
    {
        inputManager.SetIsSprinting(false);
        inputManager.ResetRollInput();
    }
}
