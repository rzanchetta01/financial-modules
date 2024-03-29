﻿using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.SharedKernel.BasicEvents
{
    public record TEventCreated<T>(T Data) : INotification
    {
    }
}
