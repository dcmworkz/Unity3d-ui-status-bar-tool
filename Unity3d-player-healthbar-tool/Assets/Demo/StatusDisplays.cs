using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lairinus.UI.Status;

public class StatusDisplays : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth = 1000;

    public int maxMana = 1000;
    public int currentMana = 1000;

    [SerializeField] MainStatusBar _statusBar = null;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
            currentHealth--;

        _statusBar.UpdateStatusBar(currentHealth, maxHealth);
    }

    public void OnClick_SetCurrentTo100()
    {
        currentHealth = 100;
    }

    public void OnClick_SetCurrentTo1000()
    {
        currentHealth = 1000;
    }
}