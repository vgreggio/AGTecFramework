using System;

namespace AGTec.Common.Repository
{
    public sealed class NoTrackingChangeContext : IDisposable
    {
        private readonly ITrackingChangeRepository _repository;
        private readonly bool _initialValue;

        public NoTrackingChangeContext(ITrackingChangeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _initialValue = _repository.AutoDetectChanges;

            _repository.AutoDetectChanges = false;
        }

        public void Dispose()
        {
            _repository.AutoDetectChanges = _initialValue;
        }
    }
}
