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
        return $@"
        using Microsoft.EntityFrameworkCore.Query;     
        using NArchitecture.Core.Persistence.Paging;
        using System.Linq.Expressions;
        
        namespace MODISO.BLL.Abstract.{moduleName};

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
            Task<{entityName}Dto> AddAsync({entityName}AddDto {entityName.ToLower()}AddDto, CancellationToken cancellationToken);
            Task<{entityName}Dto> UpdateAsync({entityName}UpdateDto {entityName.ToLower()}UpdateDto, CancellationToken cancellationToken);
            Task<{entityName}Dto> DeleteAsync(Guid id, CancellationToken cancellationToken);
        }}";
    }

    static string GenerateClass(string entityName, string moduleName)
    {
        return $@"
        using AutoMapper;
        using Microsoft.EntityFrameworkCore.Query;      
        using NArchitecture.Core.Persistence.Paging;
        using System.Linq.Expressions;

        namespace MODISO.BLL.Concrete.{moduleName};

        public class {entityName}Service : I{entityName}Service
        {{
            private readonly I{entityName}Repository _{entityName.ToLower()}Repository;
            private readonly IMapper _mapper;
            //private readonly {entityName}AddDtoValidator _validator;          

            public {entityName}Service(I{entityName}Repository {entityName.ToLower()}Repository, IMapper mapper)
            {{
                _{entityName.ToLower()}Repository = {entityName.ToLower()}Repository;
                _mapper = mapper;
            }}

            public async Task<{entityName}Dto?> GetAsync(Expression<Func<{entityName}, bool>> predicate, Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
            {{
                try
                {{
                    {entityName}? {entityName.ToLower()} = await _{entityName.ToLower()}Repository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
                    return _mapper.Map<{entityName}Dto>({entityName.ToLower()});
                }}
                catch (Exception)
                {{
                    throw;
                }}
            }}

            public async Task<Paginate<{entityName}Dto>?> GetListAsync(Expression<Func<{entityName}, bool>>? predicate = null, Func<IQueryable<{entityName}>, IOrderedQueryable<{entityName}>>? orderBy = null, Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
            {{
                try
                {{
                    IPaginate<{entityName}> {entityName.ToLower()}List = await _{entityName.ToLower()}Repository.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
                    return _mapper.Map<Paginate<{entityName}Dto>>({entityName.ToLower()}List);
                }}
                catch (Exception)
                {{
                    throw;
                }}
            }}

            public async Task<{entityName}Dto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            {{
                try
                {{
                    var {entityName.ToLower()} = await _{entityName.ToLower()}Repository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);
                    var {entityName.ToLower()}Dto = _mapper.Map<{entityName}Dto>({entityName.ToLower()});
                    return {entityName.ToLower()}Dto;
                }}
                catch (Exception)
                {{
                    throw;
                }}
            }}

            public async Task<IList<{entityName}Dto>> GetAllAsync(CancellationToken cancellationToken, int index = 0, int size = int.MaxValue)
            {{
                try
                {{
                    var {entityName.ToLower()}s = await _{entityName.ToLower()}Repository.GetListAsync(index: index, size: size, orderBy: x => x.OrderBy(x => x.Title), cancellationToken: cancellationToken);
                    var {entityName.ToLower()}Dtos = _mapper.Map<List<{entityName}Dto>>({entityName.ToLower()}s.Items);
                    return {entityName.ToLower()}Dtos;
                }}
                catch (Exception)
                {{
                    throw;
                }}
            }}

            public async Task<{entityName}Dto> AddAsync({entityName}AddDto {entityName.ToLower()}AddDto, CancellationToken cancellationToken)
            {{
                try
                {{
                    //var validationResult = _validator.Validate({entityName.ToLower()}AddDto);
                    //if (!validationResult.IsValid)
                    //    throw new ValidationException(""Validation failed. Please check the provided data."", validationResult.Errors);

                    var {entityName.ToLower()} = _mapper.Map<{entityName}>({entityName.ToLower()}AddDto);
                    await _{entityName.ToLower()}Repository.AddAsync({entityName.ToLower()});
                    return _mapper.Map<{entityName}Dto>({entityName.ToLower()});
                }}
                catch (Exception)
                {{
                    throw;
                }}
            }}

            public async Task<{entityName}Dto> UpdateAsync({entityName}UpdateDto {entityName.ToLower()}UpdateDto, CancellationToken cancellationToken)
            {{
                try
                {{
                    {entityName}? {entityName.ToLower()} = await _{entityName.ToLower()}Repository.GetAsync(x => x.Id == {entityName.ToLower()}UpdateDto.Id, cancellationToken: cancellationToken);
                    if ({entityName.ToLower()} is null)
                        throw new InvalidOperationException($""{entityName} with ID {{{entityName.ToLower()}UpdateDto.Id}} not found."");
                    {entityName.ToLower()} = _mapper.Map({entityName.ToLower()}UpdateDto, {entityName.ToLower()});
                    await _{entityName.ToLower()}Repository.UpdateAsync({entityName.ToLower()});
                    return _mapper.Map<{entityName}Dto>({entityName.ToLower()});
                }}
                catch (Exception)
                {{
                    throw;
                }}
            }}

            public async Task<{entityName}Dto> DeleteAsync(Guid id, CancellationToken cancellationToken)
            {{
                try
                {{
                    var {entityName.ToLower()} = await _{entityName.ToLower()}Repository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);
                    if ({entityName.ToLower()} == null)
                        throw new InvalidOperationException($""{entityName} with ID {{id}} not found."");

                    await _{entityName.ToLower()}Repository.DeleteAsync({entityName.ToLower()}, cancellationToken: cancellationToken);

                    return _mapper.Map<{entityName}Dto>({entityName.ToLower()});
                }}
                catch (Exception)
                {{
                    throw;
                }}
            }}
        }}";
    }

    static string GenerateMapper(string entityName, string moduleName)
    {
        return $@"
        using AutoMapper;    
        using NArchitecture.Core.Persistence.Paging;

        namespace MODISO.BLL.AutoMapper.Profiles.{moduleName};

        public class {entityName}Profile : Profile
        {{
            public {entityName}Profile()
            {{
                CreateMap<{entityName}, {entityName}Dto>().ReverseMap();
                CreateMap<{entityName}AddDto, {entityName}>();
                CreateMap<{entityName}UpdateDto, {entityName}>();
                CreateMap<IPaginate<{entityName}>, Paginate<{entityName}Dto>>();
            }}
        }}";
    }
}