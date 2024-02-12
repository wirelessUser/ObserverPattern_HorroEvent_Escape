using System;

public class EventController 
{
    public Action baseClaasEvent;


    public  void AddLister(Action lister)
    {
        baseClaasEvent += lister;
    }
    public void RemoveLister(Action lister) => baseClaasEvent -= lister;


    public void InvokeEvent() 
    {
   
        if (baseClaasEvent != null) baseClaasEvent.Invoke();
    
    }
   
}
