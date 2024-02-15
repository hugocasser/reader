using Domain.DomainEvents;
using Domain.Models;
using MediatR;

namespace Domain.Abstractions.Events;

public interface IDomainEvent : INotification;