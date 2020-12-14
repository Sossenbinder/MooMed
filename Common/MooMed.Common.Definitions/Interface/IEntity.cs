using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MooMed.Common.Definitions.Interface
{
    public interface IEntity<TKeyType>
    {
        [Key]
        [Column("Id")]
        public TKeyType Id { get; set; }
    }

    public abstract class AbstractEntity<TKeyType> : IEntity<TKeyType>
    {
        public abstract TKeyType Id { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is IEntity<TKeyType> comparisonItem))
            {
                return false;
            }

            return Id!.Equals(comparisonItem.Id);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TKeyType>.Default.GetHashCode(Id!);
        }
    }
}