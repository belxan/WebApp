using Application.DTOs.User;
using Application.Features.User.Commands;
using AutoMapper;
using Domain.Interfaces.Auth;

namespace Application.Features.User.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordService passwordService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Entities.User>(request.User);
        (string hash, string salt) = _passwordService.HashPassword("Test123@");
        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        user.IsActive = true;

        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<UserResponseDto>(user);
    }
}