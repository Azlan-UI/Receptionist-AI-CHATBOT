using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Application.DTOs.Appointments;

public sealed record UpdateAppointmentStatusRequest(AppointmentStatus Status);
