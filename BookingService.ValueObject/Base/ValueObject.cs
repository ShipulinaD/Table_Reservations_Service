using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.ValueObject.Base;


/// Базовый класс для Value Object'ов
public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents(); 

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;

        using var thisComponents = GetEqualityComponents().GetEnumerator();
        using var otherComponents = other.GetEqualityComponents().GetEnumerator();

        while (thisComponents.MoveNext() && otherComponents.MoveNext())
        {
            if (thisComponents.Current == null && otherComponents.Current != null)
                return false;

            if (thisComponents.Current != null && !thisComponents.Current.Equals(otherComponents.Current))
                return false;
        }

        return !thisComponents.MoveNext() && !otherComponents.MoveNext();
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}
