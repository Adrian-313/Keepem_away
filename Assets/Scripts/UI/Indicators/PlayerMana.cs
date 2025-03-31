using UnityEngine;
using System.Collections;

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 10;
    public int currentMana;
    public float regenDelay = 3f; // Tiempo de espera antes de empezar a regenerar
    public float regenRate = 1f; // Tiempo entre cada regeneración de maná

    public ManaBar manaBar;
    private Coroutine regenCoroutine;

    void Start()
    {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseMana(1);
        }
    }

    void UseMana(int manaUsed)
    {
        currentMana -= manaUsed;
        manaBar.SetMana(currentMana);

        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
        }
        regenCoroutine = StartCoroutine(RegenerateMana());
    }

    IEnumerator RegenerateMana()
    {
        yield return new WaitForSeconds(regenDelay);

        while (currentMana < maxMana && GameManager.Instance.isGameOver == false)
        {
            currentMana++;
            manaBar.SetMana(currentMana);
            yield return new WaitForSeconds(regenRate);
        }
    }
}
