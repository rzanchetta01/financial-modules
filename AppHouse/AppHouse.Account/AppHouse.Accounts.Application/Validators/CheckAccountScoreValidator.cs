﻿using AppHouse.SharedKernel.SharedRequests.SharedQueries;
using FluentValidation;

namespace AppHouse.Loans.Core.Validator
{
    public class CheckAccountScoreValidator : AbstractValidator<CheckAccountScoreQueryRequest>
    {
        public CheckAccountScoreValidator()
        {
            RuleFor(req => req.AccountId).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
