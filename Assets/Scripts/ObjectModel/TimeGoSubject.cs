using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGoSubject
{
    private List<Person> persons;
    private static TimeGoSubject instance;

    private TimeGoSubject()
    {

    }

    public static TimeGoSubject GetTimeSubject()
    {
        if(instance == null)
        {
            instance = new TimeGoSubject();
        }
        return instance;
    }

    public void Attach(Person person)
    {
        persons.Add(person);
    }

    public void UpdateTime(int space)
    {
        GameRunningData.GetRunningData().date.GoByTime(space);
        foreach(Person person in persons)
        {
            person.UpdatePlace();
        }
    }
    
}
