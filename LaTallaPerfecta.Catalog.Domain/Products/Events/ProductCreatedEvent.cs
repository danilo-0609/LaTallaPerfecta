﻿using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Products.Events;

public sealed record ProductCreatedEvent(ProductId ProductId) : IDomainEvent;
