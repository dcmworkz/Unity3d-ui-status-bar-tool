using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lairinus.UI;

public class StatusDisplays : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth = 1000;

    public int maxMana = 1000;
    public int currentMana = 1000;

    public int monsterCounter = 0;
    public int coinCounter = 0;
    public int lifeCounter = 100;

    // Simple Fill - lingering
    [SerializeField] private UIStatusBar _simpleFill_Lingering_Health = null;

    [SerializeField] private UIStatusBar _simpleFill_Lingering_Mana = null;

    // Simple Fill - No lingering
    [SerializeField] private UIStatusBar _simpleFill_NonLinger_Health = null;

    [SerializeField] private UIStatusBar _simpleFill_NonLinger_Mana = null;

    // Sprite Fill
    [SerializeField] private UIStatusBar _spriteFill_healthbar1 = null;

    [SerializeField] private UIStatusBar _spriteFill_manabar1 = null;
    [SerializeField] private UIStatusBar _spriteFill_healthgrid = null;

    // Quantity
    [SerializeField] private UIStatusBar _quantity_lifeCounter = null;

    [SerializeField] private UIStatusBar _quantity_coinCounter = null;
    [SerializeField] private UIStatusBar _quantity_monsterCounter = null;

    // Use this for initialization
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (monsterCounter < 500)
                monsterCounter++;

            if (coinCounter < 100)
                coinCounter += 3;

            if (lifeCounter > 0)
                lifeCounter--;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentHealth > 0)
            currentHealth--;

        if (currentMana > 0)
            currentMana--;

        _simpleFill_Lingering_Health.UpdateStatusBar(currentHealth, maxHealth);
        _simpleFill_Lingering_Mana.UpdateStatusBar(currentMana, maxMana);
        _simpleFill_NonLinger_Health.UpdateStatusBar(currentHealth, maxHealth);
        _simpleFill_NonLinger_Mana.UpdateStatusBar(currentMana, maxMana);
        _spriteFill_healthbar1.UpdateStatusBar(currentHealth, maxHealth);
        _spriteFill_manabar1.UpdateStatusBar(currentMana, maxMana);
        _spriteFill_healthgrid.UpdateStatusBar(currentHealth, maxHealth);
        _quantity_lifeCounter.UpdateStatusBar(lifeCounter, 100);
        _quantity_coinCounter.UpdateStatusBar(coinCounter, 100);
        _quantity_monsterCounter.UpdateStatusBar(monsterCounter, 500);
    }

    public void OnClick_SetCurrentTo100()
    {
        currentHealth = 100;
        currentMana = 100;
    }

    public void OnClick_SetCurrentTo1000()
    {
        currentHealth = 1000;
        currentMana = 1000;
    }
}