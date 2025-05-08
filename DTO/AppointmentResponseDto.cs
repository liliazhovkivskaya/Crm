namespace CrmService.DTO
{
    public record AppointmentResponseDto
    {
        public int Id { get; init; }
        public string Service { get; init; } = string.Empty;
        public string Master { get; init; } = string.Empty;
        public DateTime Time { get; init; }
        public string Status { get; init; } = string.Empty;
        public string ClientName { get; init; } = string.Empty;
    }
}
