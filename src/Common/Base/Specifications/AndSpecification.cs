namespace AGTec.Common.Base.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this._left = left;
            this._right = right;
        }

        public override bool IsSatisfiedBy(T candidate) => _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
    }
}