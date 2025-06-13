using Application.DTOs.User;

namespace Application.Features.User.Commands;

public record CreateUserCommand(CreateUserRequestDto User) : IRequest<UserResponseDto>;
