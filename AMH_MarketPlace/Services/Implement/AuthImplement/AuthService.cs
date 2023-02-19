using AMH_MarketPlace.DTOs.AuthDto;
using AMH_MarketPlace.Entities.User;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.AuthInterface;
using AMH_MarketPlace.Services.Interface.UserInterface;
using AMH_MarketPlace.Validations;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;
using AMH_MarketPlace.Entities.Enum;
using BCrypt.Net;
using AMH_MarketPlace.Security.JwtUtils;

namespace AMH_MarketPlace.Services.Implement.AuthImplement
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Credential> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IJwtUtil _jwtUtil;
        public AuthService(
            IRepository<Credential> repository,
            IDbPersistence dbPersistence,
            IUserService userService,
            IRoleService roleService,
            IJwtUtil jwtUtil)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _userService = userService;
            _roleService = roleService;
            _jwtUtil = jwtUtil;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                if(!ValidateRequest.ValidateNull(request.Email) || !ValidateRequest.ValidateNull(request.Password))
                {
                    throw new NotNullException(new[] {"Email is Required", "Password is Required"});
                }

                var credentialFind = await _repository.Find(c => c.Email.Equals(request.Email), new[] {"Role"});
                if (credentialFind == null) throw new NotFoundException("Email not Registered");

                var passVerify = BCrypt.Net.BCrypt.Verify(request.Password, credentialFind.Password);
                if (!passVerify) throw new UnAuthorizeException("UnAuthorized");

                var token = _jwtUtil.GenerateToken(credentialFind);

                return new LoginResponse
                {
                    Email = credentialFind.Email,
                    Role = credentialFind.Role.Name,
                    Token = token
                };
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<RegisterResponse> RegisterUser(RegisterRequest request)
        {
            try
            {
                if (!ValidateRequest.ValidateRegister(request)) throw new NotNullException(new[] 
                {
                    "FirstName and LastName cannot be null",
                    "Phone Number cannot be null",
                    "Email cannot be null",
                    "Password cannot be null and minimum 6 Character"
                });

                var createCredential = await _dbPersistence.ExecuteTransactionAsync(async () =>
                {
                    var role = await _roleService.GetOrSaveRole(ERole.User.ToString());

                    var credential = new Credential
                    {
                        Email = request.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        RoleId = role.Id
                    };
                    var saveCredential = await _repository.Save(credential);

                    var saveUser = await _userService.CreateUser(new User
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PhoneNumber = request.PhoneNumber,
                        CredentialId = saveCredential.Id
                    });

                    await _dbPersistence.SaveChangesAsync();

                    return new RegisterResponse
                    {
                        Name = saveUser.FirstName + " " + saveUser.LastName,
                        PhoneNumber = saveUser.PhoneNumber,
                        Email = saveCredential.Email,
                        Role = role.Name
                    };
                });

                return createCredential;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
