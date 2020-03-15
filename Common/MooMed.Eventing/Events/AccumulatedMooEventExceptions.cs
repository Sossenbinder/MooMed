using System;
using System.Collections.Generic;

namespace MooMed.Eventing.Events
{
	public class AccumulatedMooEventExceptions
	{
		public readonly List<Exception> Exceptions;

		public AccumulatedMooEventExceptions()
		{
			Exceptions = new List<Exception>();
		}
	}
}
