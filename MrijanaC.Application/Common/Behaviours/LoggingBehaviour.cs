using MediatR;
using MirjanaC.Domain.Entities;
using MrijanaC.Application.Interfaces.Repositories;
using System.Text.Json;

namespace MirjanaC.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoggingBehaviour(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            string userName = string.Empty;

            SystemLog systemLog = new SystemLog
            {
                ResourceType = requestName,
                Event = requestName,
                ResourceAttributes = JsonSerializer.Serialize(request),
                Comment = ""
            };

            await _unitOfWork.Repository<SystemLog>().AddAsync(systemLog);
            return await next();
        }
    }
}
