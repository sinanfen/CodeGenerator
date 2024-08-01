class Program
{
    /// <summary>
    /// Generates Interface-Implementation and AutoMapper Profile classes (IEntityService, EntityService, EntityProfile) for business logic layer (BLL) based on provided entity.
    /// Prompts the user to enter the entity and module name, then generates the selected interface & classes
    /// and saves them to the "Generated" directory in the current environment.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>    
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Enter the Entity name: ");
            string entityName = Console.ReadLine();

            Console.Write("Enter the Module name: ");
            string moduleName = Console.ReadLine().ToUpper();

            string interfaceContent = GenerateInterface(entityName, moduleName);
            string classContent = GenerateClass(entityName, moduleName);
            string mapperContent = GenerateMapper(entityName, moduleName);

            string path = Path.Combine(Environment.CurrentDirectory, "Generated");
            Directory.CreateDirectory(path);

            File.WriteAllText(Path.Combine(path, $"I{entityName}Service.cs"), interfaceContent);
            File.WriteAllText(Path.Combine(path, $"{entityName}Service.cs"), classContent);
            File.WriteAllText(Path.Combine(path, $"{entityName}Profile.cs"), mapperContent);

            Console.WriteLine($"Files generated successfully in {path}");
            Console.WriteLine("---------------------------------------------");
        }

    }

    static string GenerateInterface(string entityName, string moduleName)
    {
        var camelCaseEntityName = ToCamelCase(entityName);
        return $@"
    using Microsoft.EntityFrameworkCore.Query;     
    using MODISO.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
    using MODISO.DOMAIN.Entities.{moduleName};
    using NArchitecture.Core.Persistence.Paging;
    using MODISO.CORE.Utilities.Results.Abstract;
    using System.Linq.Expressions;
    
    namespace MODISO.BLL.Abstract.{moduleName}
    {{
        public interface I{entityName}Service
        {{
            Task<{entityName}Dto?> GetAsync(
                Expression<Func<{entityName}, bool>> predicate,
                Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null,
                bool withDeleted = false,
                bool enableTracking = true,
                CancellationToken cancellationToken = default
            );
            Task<Paginate<{entityName}Dto>?> GetListAsync(
                Expression<Func<{entityName}, bool>>? predicate = null,
                Func<IQueryable<{entityName}>, IOrderedQueryable<{entityName}>>? orderBy = null,
                Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null,
                int index = 0,
                int size = 10,
                bool withDeleted = false,
                bool enableTracking = true,
                CancellationToken cancellationToken = default
            );
            Task<{entityName}Dto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
            Task<IList<{entityName}Dto>> GetAllAsync(CancellationToken cancellationToken, int index = 0, int size = int.MaxValue);
            Task<IList<{entityName}Dto>> GetAllAsync(Expression<Func<{entityName}, bool>> predicate, Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default);            
            Task<IDataResult<{entityName}Dto>> AddAsync({entityName}AddDto {camelCaseEntityName}AddDto, CancellationToken cancellationToken);
            Task<IDataResult<{entityName}Dto>> UpdateAsync({entityName}UpdateDto {camelCaseEntityName}UpdateDto, CancellationToken cancellationToken);
            Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
        }}
    }}";
    }

    static string GenerateClass(string entityName, string moduleName)
    {
        var camelCaseEntityName = ToCamelCase(entityName);

        return $@"
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;   
    using Microsoft.Extensions.Logging;
    using MODISO.CORE.Utilities.Results.Abstract;
    using MODISO.BLL.Abstract.{moduleName};
    using MODISO.BLL.Repositories.{moduleName};
    using MODISO.CORE.Utilities.Results.ComplexTypes;
    using MODISO.CORE.Utilities.Results.Concrete;
    using NArchitecture.Core.Persistence.Paging;
    using MODISO.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
    using MODISO.DOMAIN.Entities.{moduleName};
    using System.Linq.Expressions;
    using FluentValidation;

    namespace MODISO.BLL.Concrete.{moduleName}
    {{
        public class {entityName}Service : I{entityName}Service
        {{
            private readonly I{entityName}Repository _{camelCaseEntityName}Repository;
            private readonly IMapper _mapper;
            //private readonly {entityName}AddDtoValidator _validator;
            private readonly ILogger<{entityName}Service> _logger;

            public {entityName}Service(I{entityName}Repository {camelCaseEntityName}Repository, IMapper mapper, /*{entityName}AddDtoValidator validator,*/ ILogger<{entityName}Service> logger)
            {{
                _{camelCaseEntityName}Repository = {camelCaseEntityName}Repository;
                _mapper = mapper;
                //_validator = validator;
                _logger = logger;
            }}

            public async Task<{entityName}Dto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            {{
                try
                {{
                    var {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);
                    var {camelCaseEntityName}Dto = _mapper.Map<{entityName}Dto>({camelCaseEntityName});
                    return {camelCaseEntityName}Dto;
                }}
                catch (Exception ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. Failed to get {entityName} with ID {{Id}}. Details: {{ExceptionMessage}}"",
                        nameof(GetByIdAsync), id, ex.Message);
                    return null;
                }}
            }}

            public async Task<IList<{entityName}Dto>> GetAllAsync(CancellationToken cancellationToken, int index = 0, int size = int.MaxValue)
            {{
                try
                {{
                    var {camelCaseEntityName}s = await _{camelCaseEntityName}Repository.GetListAsync(index: index, size: size, cancellationToken: cancellationToken);
                    var {camelCaseEntityName}Dtos = _mapper.Map<List<{entityName}Dto>>({camelCaseEntityName}s.Items);
                    return {camelCaseEntityName}Dtos;
                }}
                catch (Exception ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. Failed to get all {entityName}s. Index: {{Index}}, Size: {{Size}}. Details: {{ExceptionMessage}}"",
                     nameof(GetAllAsync), index, size, ex.Message);
                    return null;
                }}
            }}

            public async Task<IList<{entityName}Dto>> GetAllAsync(Expression<Func<{entityName}, bool>> predicate, Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
            {{
                try
                {{
                    var {camelCaseEntityName}s = await _{camelCaseEntityName}Repository.GetListAsync(predicate: predicate, include: include, withDeleted: withDeleted, enableTracking: enableTracking, cancellationToken: cancellationToken);
                    var {camelCaseEntityName}Dtos = _mapper.Map<List<{entityName}Dto>>({camelCaseEntityName}s.Items);
                    return {camelCaseEntityName}Dtos;
                }}
                catch (Exception ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. Failed to get all {entityName}s. Predicate: {{Predicate}}, Include: {{Include}}, withDeleted: {{withDeleted}}, enableTracking: {{enableTracking. Details: {{ExceptionMessage}}"",
                     nameof(GetAllAsync), predicate, include, withDeleted, enableTracking, ex.Message);
                    return null;
                }}
            }}

            public async Task<{entityName}Dto?> GetAsync(Expression<Func<{entityName}, bool>> predicate, Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
            {{
                try
                {{
                    {entityName}? {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
                    return _mapper.Map<{entityName}Dto>({camelCaseEntityName});
                }}
                catch (Exception ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. Failed to get {entityName} with predicate. WithDeleted: {{WithDeleted}}, EnableTracking: {{EnableTracking}}. Details: {{ExceptionMessage}}"",
                        nameof(GetAsync), withDeleted, enableTracking, ex.Message);
                    return null;
                }}
            }}

            public async Task<Paginate<{entityName}Dto>?> GetListAsync(Expression<Func<{entityName}, bool>>? predicate = null, Func<IQueryable<{entityName}>, IOrderedQueryable<{entityName}>>? orderBy = null, Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
            {{
                try
                {{
                    IPaginate<{entityName}> {camelCaseEntityName}List = await _{camelCaseEntityName}Repository.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
                    return _mapper.Map<Paginate<{entityName}Dto>>({camelCaseEntityName}List);
                }}
                catch (Exception ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. Failed to get {entityName} list. Index: {{Index}}, Size: {{Size}}, WithDeleted: {{WithDeleted}}, EnableTracking: {{EnableTracking}}. Details: {{ExceptionMessage}}"",
                        nameof(GetListAsync), index, size, withDeleted, enableTracking, ex.Message);
                    return null;
                }}
            }}

            public async Task<IDataResult<{entityName}Dto>> AddAsync({entityName}AddDto {camelCaseEntityName}AddDto, CancellationToken cancellationToken)
            {{
                try
                {{
                    if ({camelCaseEntityName}AddDto == null)
                        return new DataResult<{entityName}Dto>(ResultStatus.Error, ""{entityName} data cannot be null."", null);

                    // var validationResult = _validator.Validate({camelCaseEntityName}AddDto);
                    // if (!validationResult.IsValid)
                    //    return new DataResult<{entityName}Dto>(ResultStatus.Error, ""Validation failed. Please check the provided data."", null);

                    var {camelCaseEntityName} = _mapper.Map<{entityName}>({camelCaseEntityName}AddDto);
                    await _{camelCaseEntityName}Repository.AddAsync({camelCaseEntityName});
                    var resultData = _mapper.Map<{entityName}Dto>({camelCaseEntityName});
                    return new DataResult<{entityName}Dto>(ResultStatus.Success, ""The {entityName} has been added successfully."", resultData);
                }}
                catch (ValidationException ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. Validation failed for {entityName} data. Details: {{ExceptionMessage}}"",
                        nameof(AddAsync), ex.Message);
                    return new DataResult<{entityName}Dto>(ResultStatus.Error, ex.Message, null);
                }}
                catch (Exception ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. An unexpected error occurred while adding the {entityName}. Details: {{ExceptionMessage}}"",
                        nameof(AddAsync), ex.Message);
                    return new DataResult<{entityName}Dto>(ResultStatus.Error, ""An unexpected error occurred. Please try again."", null);
                }}
            }}

            public async Task<IDataResult<{entityName}Dto>> UpdateAsync({entityName}UpdateDto {camelCaseEntityName}UpdateDto, CancellationToken cancellationToken)
            {{
                try
                {{
                    if ({camelCaseEntityName}UpdateDto == null)
                        return new DataResult<{entityName}Dto>(ResultStatus.Error, ""{entityName} data cannot be null."", null);

                    {entityName}? {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(x => x.Id == {camelCaseEntityName}UpdateDto.Id, cancellationToken: cancellationToken);
                    if ({camelCaseEntityName} is null)
                        return new DataResult<{entityName}Dto>(ResultStatus.Error, $""{entityName} with ID {{{{{camelCaseEntityName}UpdateDto.Id}}}} not found."", null);

                    {camelCaseEntityName} = _mapper.Map({camelCaseEntityName}UpdateDto, {camelCaseEntityName});
                    await _{camelCaseEntityName}Repository.UpdateAsync({camelCaseEntityName});
                    var resultData = _mapper.Map<{entityName}Dto>({camelCaseEntityName});
                    return new DataResult<{entityName}Dto>(ResultStatus.Success, ""The {entityName} has been updated successfully."", resultData);
                }}
                catch (Exception ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. An unexpected error occurred while updating the {entityName} with ID {{Id}}. Details: {{ExceptionMessage}}"",
                        nameof(UpdateAsync), {camelCaseEntityName}UpdateDto?.Id, ex.Message);
                    return new DataResult<{entityName}Dto>(ResultStatus.Error, ""An unexpected error occurred. Please try again."", null);
                }}
            }}

            public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
            {{
                try
                {{
                    var {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);
                    if ({camelCaseEntityName} == null)
                        return new Result(ResultStatus.Error, $""{entityName} with ID {{{{id}}}} not found."");

                    await _{camelCaseEntityName}Repository.DeleteAsync({camelCaseEntityName}, cancellationToken: cancellationToken);
                    return new Result(ResultStatus.Success, ""The {entityName} has been deleted successfully."");
                }}
                catch (Exception ex)
                {{
                    _logger.LogError(ex, ""Error in {{MethodName}}. An unexpected error occurred while deleting the {entityName} with ID {{Id}}. Details: {{ExceptionMessage}}"",
                        nameof(DeleteAsync), id, ex.Message);
                    return new Result(ResultStatus.Error, ""An unexpected error occurred. Please try again."");
                }}
            }}
        }}
    }}";
    }

    static string GenerateMapper(string entityName, string moduleName)
    {
        return $@"
        using AutoMapper;    
        using NArchitecture.Core.Persistence.Paging;
        using MODISO.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
        using MODISO.DOMAIN.Entities.{moduleName};

        namespace MODISO.BLL.AutoMapper.Profiles.{moduleName};

        public class {entityName}Profile : Profile
        {{
            public {entityName}Profile()
            {{
                CreateMap<{entityName}, {entityName}Dto>().ReverseMap();
                CreateMap<{entityName}AddDto, {entityName}>();
                CreateMap<{entityName}UpdateDto, {entityName}>();
                CreateMap<{entityName}Dto, {entityName}UpdateDto>();
                CreateMap<IPaginate<{entityName}>, Paginate<{entityName}Dto>>();
            }}
        }}";
    }

    public static string ToCamelCase(string str)
    {
        if (string.IsNullOrEmpty(str) || str.Length < 2)
            return str.ToLower();

        return char.ToLower(str[0]) + str.Substring(1);
    }

}