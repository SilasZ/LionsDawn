using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humansList : MonoBehaviour
{
    List<PersonMovement> humans=new List<PersonMovement>();
    int humansMax=0;
    bool humansMaxSet = false;

    int hoursAlreadyGone = 0;
    public void KillPeople(int hours)
    {
        humans.Clear();
        foreach(PersonMovement person in FindObjectsOfType<PersonMovement>())
        {
            if (!person.GetComponentInParent<Place>()) humans.Add(person);
        }

        if (!humansMaxSet)
        {
            humansMax = humans.Count;
            humansMaxSet = true;
        }

        float hoursDyingNow = hours - hoursAlreadyGone;
        hoursAlreadyGone = hours;

        int dyingPeople =Mathf.Max((int) ((humansMax / 20) * hoursDyingNow),1);

        for (int x=0; x < dyingPeople; x++)
        {
            int deadGuyNumber = Random.Range(0, humans.Count);
            PersonMovement deadGuy = humans[deadGuyNumber];
            humans.RemoveAt(deadGuyNumber);
            deadGuy.Die();
            if(CheckIfGameOver()) return;
        }
    }

    bool CheckIfGameOver()
    {
        if (humans.Count == 0)
        {
            FindObjectOfType<UIPlayerStatus>().GameFinished(hoursAlreadyGone);
            return true;
        }
        return false;
    }
}
