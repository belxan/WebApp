using Application.DTOs.User;

namespace Application.Features.User.Queries;
public record GetAllUsersQuery : IRequest<IEnumerable<UserResponseDto>>;