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

    private void Start()
    {
        characterAvatar = GetComponent<DynamicCharacterAvatar>();
        characterAvatar.CharacterUpdated.AddListener(OnCharacterUpdated);
        characterAvatar.CharacterCreated.AddListener(OnCharacterCreated);
        heightSlider.onValueChanged.AddListener(OnHeightChange);
        muscleSlider.onValueChanged.AddListener(OnMuscleChange);
        weightSlider.onValueChanged.AddListener(OnWeightChange);
    }

    void OnCharacterCreated(UMAData data)
    {
        DNA = characterAvatar.GetDNA();
    }
    void OnCharacterUpdated(UMAData data)
    {

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
}
