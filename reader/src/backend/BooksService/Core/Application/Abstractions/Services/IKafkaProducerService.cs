using Domain.Abstractions;
using Domain.Abstractions.Events;
using Domain.Models;
using Microsoft.Extensions.Options;

namespace Application.Abstractions.Services;

public interface IKafkaProducerService<T> where T : Entity
{
    public Task SendEventAsync(GenericDomainEvent<T> bookEvent);
}