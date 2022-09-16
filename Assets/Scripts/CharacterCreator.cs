using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UMA;
using UMA.CharacterSystem;

public class CharacterCreator : MonoBehaviour
{
    private DynamicCharacterAvatar characterAvatar;
    private Dictionary<string, DnaSetter> DNA;

    [SerializeField]
    private Slider heightSlider, muscleSlider, weightSlider;
    [SerializeField]
    private UMAWardrobeRecipe[] maleHair, femaleHair;
    [SerializeField]
    private Color[] skinColors;

    private void Start()
    {
        characterAvatar = GetComponent<DynamicCharacterAvatar>();
        characterAvatar.CharacterUpdated.AddListener(OnCharacterUpdated);
        characterAvatar.CharacterCreated.AddListener(OnCharacterCreated);
        heightSlider.onValueChanged.AddListener(OnHeightChange);
        muscleSlider.onValueChanged.AddListener(OnMuscleChange);
        weightSlider.onValueChanged.AddListener(OnWeightChange);
    }
    private void OnDisable()
    {
        characterAvatar.CharacterUpdated.RemoveListener(OnCharacterUpdated);
        characterAvatar.CharacterCreated.RemoveListener(OnCharacterCreated);
        heightSlider.onValueChanged.RemoveListener(OnHeightChange);
        muscleSlider.onValueChanged.RemoveListener(OnMuscleChange);
        weightSlider.onValueChanged.RemoveListener(OnWeightChange);
    }

    public void ChangeHair(int hair)
    {
        if(characterAvatar.activeRace.name == "HumanMaleDCS")
        {
            characterAvatar.SetSlot(maleHair[hair]);
        }
        if (characterAvatar.activeRace.name == "HumanFemaleDCS")
        {
            characterAvatar.SetSlot(femaleHair[hair]);
        }

        characterAvatar.BuildCharacter();
    }
    public void ChangeSkinColor(int skinColor)
    {
        characterAvatar.SetColor("Skin", skinColors[skinColor]);
        characterAvatar.UpdateColors(true);
    }
    public void ChangeSex(int sex)
    {
        characterAvatar.ChangeRace(sex == 0 ? "HumanFemaleDCS" : "HumanMaleDCS");
        characterAvatar.BuildCharacter();
    }
    public void SaveCharacter()
    {
        PlayerPrefs.SetString("CharacterData", characterAvatar.GetCurrentRecipe());
    }

    void OnCharacterCreated(UMAData data)
    {
        DNA = characterAvatar.GetDNA();
    }
    void OnCharacterUpdated(UMAData data)
    {
        DNA = characterAvatar.GetDNA();
        UpdateSliders();
    }

    void OnHeightChange(float height)
    {
        DNA["height"].Set(height);
        characterAvatar.BuildCharacter();
    }
    void OnMuscleChange(float muscle)
    {
        DNA["lowerMuscle"].Set(muscle);
        DNA["upperMuscle"].Set(muscle);
        characterAvatar.BuildCharacter();
    }
    void OnWeightChange(float weight)
    {
        DNA["lowerWeight"].Set(weight);
        DNA["upperWeight"].Set(weight);
        characterAvatar.BuildCharacter();
    }
    void UpdateSliders()
    {
        heightSlider.value = DNA["height"].Get();
        weightSlider.value = DNA["upperWeight"].Get();
        muscleSlider.value = DNA["upperMuscle"].Get();
    }
}
