using Application.DTOs.User;

namespace Application.Features.User.Queries;
public record GetUserByIdQuery(int Id) : IRequest<UserResponseDto?>;