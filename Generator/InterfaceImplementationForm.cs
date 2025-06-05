namespace Generator
{
    public partial class InterfaceImplementationForm : Form
    {
        public InterfaceImplementationForm()
        {
            InitializeComponent();
        }

        private void InterfaceImplementationForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // Create a new instance of FolderBrowserDialog
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select the folder where the files will be generated.";

                // Show the dialog and check if the user selected a folder
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Set the selected folder path in txtPath
                    txtPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        // Removes placeholder when user starts typing
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.ForeColor == Color.Gray)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        // Adds placeholder when TextBox is empty
        private void AddPlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.ForeColor = Color.Gray;
                if (textBox == txtPath)
                    textBox.Text = "Project Path";
                else if (textBox == txtEntityName)
                    textBox.Text = "Enter Entity Name";
                else if (textBox == txtModuleName)
                    textBox.Text = "Enter Module Short Name";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset all the input fields to their placeholder state
            txtPath.Text = "Project Path";
            txtPath.ForeColor = Color.Gray;

            txtEntityName.Text = "Enter Entity Name";
            txtEntityName.ForeColor = Color.Gray;

            txtModuleName.Text = "Enter Module Short Name";
            txtModuleName.ForeColor = Color.Gray;
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Get user input values
            string path = txtPath.Text;
            string entityName = txtEntityName.Text;
            string moduleName = txtModuleName.Text.ToUpper();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(entityName) || string.IsNullOrWhiteSpace(moduleName))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Generate files
            GenerateFiles(path, entityName, moduleName);
            MessageBox.Show("Files generated successfully.");
        }

        private void GenerateFiles(string path, string entityName, string moduleName)
        {
            // Ensure the root directory exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //I[Entity]Service
            string abstractPath = Path.Combine(path, "Abstract", moduleName);
            if (!Directory.Exists(abstractPath))
            {
                Directory.CreateDirectory(abstractPath);
            }
            string interfaceContent = GenerateInterface(entityName, moduleName);
            File.WriteAllText(Path.Combine(abstractPath, $"I{entityName}Service.cs"), interfaceContent);

            //[Entity]Service
            string concretePath = Path.Combine(path, "Concrete", moduleName);
            if (!Directory.Exists(concretePath))
            {
                Directory.CreateDirectory(concretePath);
            }
            string classContent = GenerateImplementation(entityName, moduleName);
            File.WriteAllText(Path.Combine(concretePath, $"{entityName}Service.cs"), classContent);

            //[Entity]Dto
            string profilesPath = Path.Combine(path, "AutoMapper", "Profiles", moduleName);
            if (!Directory.Exists(profilesPath))
            {
                Directory.CreateDirectory(profilesPath);
            }
            string mapperContent = GenerateMapper(entityName, moduleName);
            File.WriteAllText(Path.Combine(profilesPath, $"{entityName}Profile.cs"), mapperContent);

            //I[Entity]Repository
            string repositoryInterfacePath = Path.Combine(path, "Repositories", moduleName);
            if (!Directory.Exists(repositoryInterfacePath))
            {
                Directory.CreateDirectory(repositoryInterfacePath);
            }
            string repositoryInterfaceContent = GenerateRepository(path, entityName, moduleName);
            File.WriteAllText(Path.Combine(repositoryInterfacePath, $"I{entityName}Repository.cs"), repositoryInterfaceContent);
        }


        // CamelCase converter
        public static string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str.ToLower();

            // İlk harf 'I' ise küçük 'i' yap
            if (str[0] == 'I')
                return 'i' + str.Substring(1);

            return char.ToLower(str[0]) + str.Substring(1);
        }

        // Generate Interface
        static string GenerateInterface(string entityName, string moduleName)
        {
            var camelCaseEntityName = ToCamelCase(entityName);
            return $@"using Microsoft.EntityFrameworkCore.Query;     
using ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
using ERP.DOMAIN.Entities.{moduleName};
using NArchitecture.Core.Persistence.Paging;
using ERP.CORE.Utilities.Results.Abstract;
using System.Linq.Expressions;

namespace ERP.BLL.Abstract.{moduleName};

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
        int size = int.MaxValue,
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
";
        }

        // Generate Class
        static string GenerateImplementation(string entityName, string moduleName)
        {
            var camelCaseEntityName = ToCamelCase(entityName);
            return $@"using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using ERP.CORE.Utilities.Results.Abstract;
using ERP.BLL.Abstract.{moduleName};
using ERP.BLL.Repositories.{moduleName};
using ERP.CORE.Utilities.Results.ComplexTypes;
using ERP.CORE.Utilities.Results.Concrete;
using NArchitecture.Core.Persistence.Paging;
using ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
using ERP.DOMAIN.Entities.{moduleName};
using System.Linq.Expressions;
using FluentValidation;

namespace ERP.BLL.Concrete.{moduleName};

public class {entityName}Service : I{entityName}Service
{{
    private readonly I{entityName}Repository _{camelCaseEntityName}Repository;
    private readonly IMapper _mapper;
    private readonly ILogger<{entityName}Service> _logger;

    public {entityName}Service(I{entityName}Repository {camelCaseEntityName}Repository, IMapper mapper, ILogger<{entityName}Service> logger)
    {{
        _{camelCaseEntityName}Repository = {camelCaseEntityName}Repository;
        _mapper = mapper;
        _logger = logger;
    }}

    public async Task<{entityName}Dto?> GetAsync(
        Expression<Func<{entityName}, bool>> predicate,
        Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {{
        try
        {{
            var {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
            return _mapper.Map<{entityName}Dto>({camelCaseEntityName});
        }}
        catch (Exception ex)
        {{
            _logger.LogError(ex, ""Error in {{MethodName}}. Failed to retrieve {entityName}. Predicate: {{Predicate}}. Details: {{ExceptionMessage}}"", nameof(GetAsync), predicate, ex.Message);
            throw;
        }}
    }}

    public async Task<Paginate<{entityName}Dto>?> GetListAsync(
        Expression<Func<{entityName}, bool>>? predicate = null,
        Func<IQueryable<{entityName}>, IOrderedQueryable<{entityName}>>? orderBy = null,
        Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null,
        int index = 0,
        int size = int.MaxValue,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {{
        try
        {{
            var {camelCaseEntityName}List = await _{camelCaseEntityName}Repository.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
            return _mapper.Map<Paginate<{entityName}Dto>>({camelCaseEntityName}List);
        }}
        catch (Exception ex)
        {{
            _logger.LogError(ex, ""Error in {{MethodName}}. Failed to retrieve list of {entityName}s. Index: {{Index}}, Size: {{Size}}. Details: {{ExceptionMessage}}"", nameof(GetListAsync), index, size, ex.Message);
            throw;
        }}
    }}

    public async Task<{entityName}Dto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {{
        try
        {{
            var {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);
            return _mapper.Map<{entityName}Dto>({camelCaseEntityName});
        }}
        catch (Exception ex)
        {{
            _logger.LogError(ex, ""Error in {{MethodName}}. Failed to retrieve {entityName} by ID {{Id}}. Details: {{ExceptionMessage}}"", nameof(GetByIdAsync), id, ex.Message);
            throw;
        }}
    }}

    public async Task<IList<{entityName}Dto>> GetAllAsync(CancellationToken cancellationToken, int index = 0, int size = int.MaxValue)
    {{
        try
        {{
            var {camelCaseEntityName}s = await _{camelCaseEntityName}Repository.GetListAsync(index: index, size: size, cancellationToken: cancellationToken);
            return _mapper.Map<List<{entityName}Dto>>({camelCaseEntityName}s.Items);
        }}
        catch (Exception ex)
        {{
            _logger.LogError(ex, ""Error in {{MethodName}}. Failed to retrieve all {entityName}s. Index: {{Index}}, Size: {{Size}}. Details: {{ExceptionMessage}}"", nameof(GetAllAsync), index, size, ex.Message);
            throw;
        }}
    }}

    public async Task<IList<{entityName}Dto>> GetAllAsync(
        Expression<Func<{entityName}, bool>> predicate,
        Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {{
        try
        {{
            var {camelCaseEntityName}s = await _{camelCaseEntityName}Repository.GetListAsync(predicate: predicate, include: include, withDeleted: withDeleted, enableTracking: enableTracking, cancellationToken: cancellationToken);
            return _mapper.Map<List<{entityName}Dto>>({camelCaseEntityName}s.Items);
        }}
        catch (Exception ex)
        {{
            _logger.LogError(ex, ""Error in {{MethodName}}. Failed to retrieve all {entityName}s with predicate. Details: {{ExceptionMessage}}"", nameof(GetAllAsync), ex.Message);
            throw;
        }}
    }}

    public async Task<IDataResult<{entityName}Dto>> AddAsync({entityName}AddDto {camelCaseEntityName}AddDto, CancellationToken cancellationToken)
    {{
        try
        {{
            var {camelCaseEntityName} = _mapper.Map<{entityName}>({camelCaseEntityName}AddDto);
            await _{camelCaseEntityName}Repository.AddAsync({camelCaseEntityName}, cancellationToken);
            var resultData = _mapper.Map<{entityName}Dto>({camelCaseEntityName});
            return new DataResult<{entityName}Dto>(ResultStatus.Success, ""The {entityName} has been added successfully."", resultData);
        }}
        catch (Exception ex)
        {{
            _logger.LogError(ex, ""Error in {{MethodName}}. Failed to add {entityName}. Details: {{ExceptionMessage}}"", nameof(AddAsync), ex.Message);
            throw;
        }}
    }}

    public async Task<IDataResult<{entityName}Dto>> UpdateAsync({entityName}UpdateDto {camelCaseEntityName}UpdateDto, CancellationToken cancellationToken)
    {{
        try
        {{
            var {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(x => x.Id == {camelCaseEntityName}UpdateDto.Id, cancellationToken: cancellationToken);
            if({camelCaseEntityName} is null)
                return new DataResult<{entityName}Dto>(ResultStatus.Warning, ""The {entityName} could not be found"", default!);                        
            {camelCaseEntityName} = _mapper.Map({camelCaseEntityName}UpdateDto, {camelCaseEntityName});
            await _{camelCaseEntityName}Repository.UpdateAsync({camelCaseEntityName}, cancellationToken);
            var resultData = _mapper.Map<{entityName}Dto>({camelCaseEntityName});
            return new DataResult<{entityName}Dto>(ResultStatus.Success, ""The {entityName} has been updated successfully."", resultData);
        }}
        catch (Exception ex)
        {{
            _logger.LogError(ex, ""Error in {{MethodName}}. Failed to update {entityName} with ID {{Id}}. Details: {{ExceptionMessage}}"", nameof(UpdateAsync), {camelCaseEntityName}UpdateDto?.Id, ex.Message);
            throw;
        }}
    }}

    public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {{
        try
        {{
            var {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if({camelCaseEntityName} is null)
                return new Result(ResultStatus.Warning, ""The {entityName} could not be found"");
            await _{camelCaseEntityName}Repository.DeleteAsync({camelCaseEntityName}, cancellationToken: cancellationToken);
            return new Result(ResultStatus.Success, ""The {entityName} has been deleted successfully."");
        }}
        catch (Exception ex)
        {{
            _logger.LogError(ex, ""Error in {{MethodName}}. Failed to delete {entityName} with ID {{Id}}. Details: {{ExceptionMessage}}"", nameof(DeleteAsync), id, ex.Message);
            throw;
        }}
    }}
}}
";
        }

        // Generate Mapper
        static string GenerateMapper(string entityName, string moduleName)
        {
            return $@"using AutoMapper;    
using NArchitecture.Core.Persistence.Paging;
using ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
using ERP.DOMAIN.Entities.{moduleName};

namespace ERP.BLL.AutoMapper.Profiles.{moduleName};

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
}}
";
        }

        private string GenerateRepository(string path, string entityName, string moduleName)
        {
            // Generate the interface content
            return $@"using ERP.DOMAIN.Entities.{moduleName};
using NArchitecture.Core.Persistence.Repositories;

namespace ERP.BLL.Repositories.{moduleName};

public interface I{entityName}Repository : IAsyncRepository<{entityName}, Guid>, IRepository<{entityName}, Guid>
{{
}}
";
        }
    }
}

