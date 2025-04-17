using System;

namespace App.Core.Timer
{
    public interface ITimer
    {
        public TimeSpan GetTime();
    }
}