namespace Model
{
	public struct DataSeries<T>
	{
		public DataSeries(string target, DataPoint<T>[] datapoints) : this()
		{
			Target = target;
			Datapoints = datapoints;
		}

		public string Target { get; }
		public DataPoint<T>[] Datapoints { get; }
    }
}
