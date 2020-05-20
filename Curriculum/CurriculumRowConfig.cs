using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurriculumRowConfig : MonoBehaviour
{
    public Text unit, title, activities, id;

    public void Initialise(Curriculum curriculum)
    {
        this.unit.text = curriculum.unit;
        this.title.text = curriculum.title;
        this.activities.text = curriculum.activities;
        this.id.text = curriculum.id;
    }
}
