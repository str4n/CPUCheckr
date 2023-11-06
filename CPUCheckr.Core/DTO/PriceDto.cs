namespace CPUCheckr.Core.DTO;

public record PriceDto(double Price)
{
    public static implicit operator double(PriceDto price) => price.Price;
    public static implicit operator PriceDto(double price) => new(price);
}