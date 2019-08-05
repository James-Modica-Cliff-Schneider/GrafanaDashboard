using System;

namespace Model
{
	public struct DataPoint<T>
    {
		public DataPoint(DateTime dateTime, T value)
		{
			DateTime = dateTime;
			Value = value;
		}

		public DateTime DateTime { get; }
		public T Value { get; }
	}
}
