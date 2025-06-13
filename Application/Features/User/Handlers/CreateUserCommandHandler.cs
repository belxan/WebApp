using Application.DTOs.User;
using Application.Features.User.Commands;
using AutoMapper;

namespace Application.Features.User.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Entities.User>(request.User);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<UserResponseDto>(user);
    }
}