﻿using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using Asd.AbpZeroTemplate.EntityFrameworkCore;
using Asd.AbpZeroTemplate.EntityFrameworkCore.Repositories;

namespace Asd.AbpZeroTemplate.MultiTenancy.Payments
{
    public class SubscriptionPaymentExtensionDataRepository : AbpZeroTemplateRepositoryBase<SubscriptionPaymentExtensionData, long>,
        ISubscriptionPaymentExtensionDataRepository
    {
        public SubscriptionPaymentExtensionDataRepository(IDbContextProvider<AbpZeroTemplateDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<string> GetExtensionDataAsync(long subscriptionPaymentId, string key)
        {
            var data = await FirstOrDefaultAsync(p => p.SubscriptionPaymentId == subscriptionPaymentId && p.Key == key);

            return data?.Value;
        }

        public async Task<long?> GetPaymentIdOrNullAsync(string key, string value)
        {
            var data = await FirstOrDefaultAsync(p => p.Key == key && p.Value == value);
            return data?.SubscriptionPaymentId;
        }

        public async Task SetExtensionDataAsync(long subscriptionPaymentId, string key, string value)
        {
            var data = await FirstOrDefaultAsync(p => p.SubscriptionPaymentId == subscriptionPaymentId && p.Key == key);

            if (data != null)
            {
                await DeleteAsync(data);
            }

            await InsertAsync(new SubscriptionPaymentExtensionData()
            {
                SubscriptionPaymentId = subscriptionPaymentId,
                Key = key,
                Value = value
            });
        }

    }
}
