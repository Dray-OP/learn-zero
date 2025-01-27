﻿using System.Collections.Generic;
using Asd.AbpZeroTemplate.Editions.Dto;
using Asd.AbpZeroTemplate.MultiTenancy.Payments;

namespace Asd.AbpZeroTemplate.Web.Models.Payment
{
    public class UpgradeEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public PaymentPeriodType PaymentPeriodType { get; set; }

        public SubscriptionPaymentType SubscriptionPaymentType { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}