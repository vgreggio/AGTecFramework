namespace AGTec.Common.Base.Specifications
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _other;
        public NotSpecification(ISpecification<T> other) => this._other = other;
        public override bool IsSatisfiedBy(T candidate) => !_other.IsSatisfiedBy(candidate);
    }
}