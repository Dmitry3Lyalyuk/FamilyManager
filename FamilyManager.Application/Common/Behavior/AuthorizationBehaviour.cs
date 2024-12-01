using FamilyManager.Application.Common.Attributes;
using FamilyManager.Application.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace FamilyManager.Application.Common.Behavior
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest,
        TResponse> where TResponse : notnull
    {
        private readonly IUserAuthorize _user;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(IUserAuthorize user, IIdentityService identityService)
        {
            _user = user;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                if (_user.Id == null)
                {
                    throw new Exception("User Id was not found");
                }
            }

            //roles 
            var attributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrEmpty(a.Roles));
            if (attributesWithRoles.Any())
            {
                var authorized = false;
                foreach (var roles in attributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (var role in roles)
                    {
                        var isInRole = await _identityService.IsInRoleAsync(_user.Id, role);
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                if (!authorized)
                {
                    throw new Exception();
                }
            }
            //policies
            var attributesWithPolicy = authorizeAttributes.Where(a => !string.IsNullOrEmpty(a.Policy));
            if (attributesWithPolicy.Any())
            {
                foreach (var policy in attributesWithPolicy.Select(a => a.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);
                    if (!authorized)
                    {
                        throw new Exception();
                    }
                }
            }
            return await next();
        }

    }
}
