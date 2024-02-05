﻿using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedCommands
{
    public record CreateLoanRequest(LoanDto LoanDto) : IRequest<bool>;
}
