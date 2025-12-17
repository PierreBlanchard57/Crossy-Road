using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    public partial class CyclicEvent
    {
        private int current=0;
        private int cycleDuration,minDuration,maxDuration;
        private Callable callableEvent;//only procedures!
        private bool random;
        public CyclicEvent(int cycleDuration,Callable callableEvent) {
            this.cycleDuration = cycleDuration;
            this.callableEvent = callableEvent;
            random = false;
        }
        public CyclicEvent(int minDuration,int maxDuration, Callable callableEvent)
        {
            this.minDuration = minDuration;
            this.maxDuration=maxDuration;
            this.callableEvent = callableEvent;
            cycleDuration= new Random().Next(minDuration,maxDuration);
            random = true;
        }
        public void stepOneFrame()
        {
            current++;
            if (current == cycleDuration) {
                current = 0;
                callableEvent.Call();
                if (random) cycleDuration = new Random().Next(minDuration, maxDuration);
            }
        }
    }
}
