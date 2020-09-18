using System;

namespace MooMed.DotNet.Utils.Enum
{
	public abstract class RefEnum
	{
		public int Id { get; }

		public string Name { get; }

		protected RefEnum(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public override string ToString() => Name;

		public override bool Equals(object? obj)
		{
			if (!(obj is RefEnum castEnum))
			{
				return false;
			}

			return GetType() == castEnum.GetType() && Id == castEnum.Id;
		}

		protected bool Equals(RefEnum other)
		{
			return Id == other.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}