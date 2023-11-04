using System;
using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Brands.Events;

public record BrandExpiredEvent(BrandId BrandId) : IDomainEvent;

        

