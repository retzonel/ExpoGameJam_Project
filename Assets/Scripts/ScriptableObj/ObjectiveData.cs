using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObjectiveData", menuName = "ObjectiveData", order = 0)]
public class ObjectiveData : ScriptableObject {
    public Sprite objectiveIcon;
    public string objectiveDescription;
    public GameObject objectiveGO;
}
