using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curriculum
{
    public string activities, unit, title, id;

    public Curriculum()
    {
        activities = CurriculumManager.activites;
        unit = CurriculumManager.unit;
        title = CurriculumManager.title;
        id = CurriculumManager.id;
    }

    public Curriculum(IDictionary<string, object> dict)
    {
        this.activities = dict["activities"].ToString();
        this.unit = dict["unit"].ToString();
        this.title = dict["title"].ToString();
        this.id = dict["id"].ToString();
    }
}
