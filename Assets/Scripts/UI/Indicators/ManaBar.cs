using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaSlider;

    public Gradient manaGradient;
    public Image manaFill;

    public void SetMaxMana(int mana)
    {
        manaSlider.maxValue = mana;
        manaSlider.value = mana;

        manaFill.color = manaGradient.Evaluate(1f);
    }

    public void SetMana(int mana)
    {
        manaSlider.value = mana;

        manaFill.color = manaGradient.Evaluate(manaSlider.normalizedValue);
    }
}
