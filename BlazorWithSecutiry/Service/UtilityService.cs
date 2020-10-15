using BlazorWithSecutiry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorWithSecutiry
{
    public sealed class UtilityService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UtilityService(ApplicationDbContext applicationDbContext) : base()
        {
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<List<string>> MigrationDatabase(CancellationToken cancellationToken)
        {
            var result = new List<string>();
            lock(_applicationDbContext)
            {
                foreach (var item in _applicationDbContext.Migrate())
                {
                    result.Add(item);
                }
            }

            var filtered = result.Where(r => !string.IsNullOrEmpty(r)).ToList();
            return await Task.FromResult(filtered).ConfigureAwait(false);
        }
    }
}
