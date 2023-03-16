namespace AGTec.Common.Base.Specifications;

public class AndNotSpecification<T> : CompositeSpecification<T>
{
    readonly ISpecification<T> _left;
    readonly ISpecification<T> _right;

    public AndNotSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        this._left = left;
        this._right = right;
    }

    public override bool IsSatisfiedBy(T candidate) =>
        _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate) != true;
}