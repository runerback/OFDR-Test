using System;

namespace OFDR.Common
{
	public class EventArgs<T> : EventArgs
	{
		public EventArgs(T value)
		{
			this.value = value;
		}

		private T value;
		public T Value
		{
			get { return this.value; }
		}
	}
}
