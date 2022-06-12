using System;

namespace Play.Catalog.Service.Dtos
{
    public record CarDto(Guid Id, string Brand, string Model, string Description, decimal Price);

    public record CreateCarDto(string Brand, string Model, string Description, decimal Price);

    public record UpdateCarDto(string Brand, string Model, string Description, decimal Price);


}