using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lairinus.UI.Status;

public class StatusDisplays : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth = 1000;

    public int maxMana = 1000;
    public int currentMana = 0;

    [SerializeField] private UIStatusBar _statusBar = null;
    [SerializeField] private UIStatusBar _quantityStatusBar = null;
    [SerializeField] private UIStatusBar _separateSpriteBar = null;
    [SerializeField] private UIStatusBar _healthbar2 = null;
    [SerializeField] private UIStatusBar _manabarSeparateSprites1 = null;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentHealth > 0)
            currentHealth--;

        if (currentMana < maxMana)
            currentMana++;

        _statusBar.UpdateStatusBar(currentHealth, maxHealth);
        _quantityStatusBar.UpdateStatusBar(currentHealth, maxHealth);
        _separateSpriteBar.UpdateStatusBar(currentHealth, maxHealth);
        _healthbar2.UpdateStatusBar(currentHealth, maxHealth);
        _manabarSeparateSprites1.UpdateStatusBar(currentMana, maxMana);
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