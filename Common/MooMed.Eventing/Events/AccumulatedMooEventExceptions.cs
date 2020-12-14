using System;
using System.Collections;
using System.Collections.Generic;

namespace MooMed.Eventing.Events
{
    public class AccumulatedMooEventExceptions : IEnumerable<Exception>
    {
        public readonly List<Exception> Exceptions;

        public AccumulatedMooEventExceptions()
        {
            Exceptions = new List<Exception>();
        }

        public IEnumerator<Exception> GetEnumerator()
        {
            return Exceptions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}