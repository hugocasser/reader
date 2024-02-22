using Domain.Abstractions;
using Domain.Abstractions.Events;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface IKafkaService<T> where T : Entity
{
    public Task SendEventAsync(GenericDomainEvent<T> message);
}