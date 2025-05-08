namespace CrmService.DTO
{
    public record AppointmentRequestDto
    {
        public string FullName { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public int ServiceItemId { get; init; }
        public int MasterId { get; init; }
        public DateTime Time { get; init; }             // в UTC!
        public long TelegramChatId { get; init; }
    }
}
