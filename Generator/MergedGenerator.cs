using System.Text;
using System.Text.RegularExpressions;

namespace Generator;

public partial class MergedGenerator : Form
{
    public MergedGenerator()
    {
        InitializeComponent();
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        bool somethingHappend = false;
        string path = txtPath.Text;  // Get the path from the form
        string entityName = txtEntityName.Text;  // Entity name
        string moduleName = txtModuleName.Text.ToUpper();  // Module short name
        string namespaceName = txtNamespace.Text;  // Namespace for the DTOs
        List<(string Type, string Name)> properties = GetEntityProperties(txtProperties.Text);  // Entity properties

        // Validate inputs
        if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(entityName)
            || string.IsNullOrWhiteSpace(moduleName) || string.IsNullOrEmpty(namespaceName))
        {
            MessageBox.Show("Please fill in all fields.");
            return;
        }

        // If no DTOs are selected, show a message and return
        if (!chkAddDto.Checked & !chkAddDto.Checked & !chkUpdateDto.Checked & !chkInterface.Checked)
        {
            MessageBox.Show("Please select at least one DTO type to generate.");
            return;
        }

        string dtosPath = Path.Combine(path, $"{namespaceName}.DOMAIN", "DTOs", moduleName, $"{entityName}Dtos");

        if ((chkAddDto.Checked || chkAddDto.Checked || chkUpdateDto.Checked) & (!Path.Exists(dtosPath) & properties != null & properties.Count > 0))
            Directory.CreateDirectory(dtosPath);

        // Generate the files based on the selected options
        if (chkDto.Checked & properties != null & properties.Count > 0)
        {
            string dtoContent = GenerateDto(entityName, moduleName, GetEntityProperties(txtProperties.Text, true));
            File.WriteAllText(Path.Combine(dtosPath, $"{entityName}Dto.cs"), dtoContent);
            somethingHappend = true;
        }

        if (chkAddDto.Checked & properties != null & properties.Count > 0)
        {
            string addDtoContent = GenerateAddDto(entityName, moduleName, properties);
            File.WriteAllText(Path.Combine(dtosPath, $"{entityName}AddDto.cs"), addDtoContent);
            somethingHappend = true;
        }

        if (chkUpdateDto.Checked & properties != null & properties.Count > 0)
        {
            string updateDtoContent = GenerateUpdateDto(entityName, moduleName, properties);
            File.WriteAllText(Path.Combine(dtosPath, $"{entityName}UpdateDto.cs"), updateDtoContent);
            somethingHappend = true;
        }

        if (chkInterface.Checked & properties != null & properties.Count > 0)
        {
            _dbContextName = null;

            var dialog = new GetDbContextName();
            dialog.DbContextNameSaved += OnDbContextNameSaved;
            dialog.DbContextNameCancelled += OnDbContextNameCancelled;

            dialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(_dbContextName))
                GenerateFiles(path, entityName, moduleName, namespaceName, _dbContextName);
            else
                MessageBox.Show("DbContext name not provided. Interface files are not generated.");
            somethingHappend = true;
        }

        if (somethingHappend)
            MessageBox.Show($"Files created!");
    }


    #region Generate DTO Methods
    // Method to generate the content for Dto
    private string GenerateDto(string entityName, string moduleName, List<(string Type, string Name)> properties)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"namespace ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
        sb.AppendLine($"public sealed class {entityName}Dto : BaseDto");
        sb.AppendLine("{");

        foreach (var prop in properties)
        {
            sb.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    // Method to generate the content for AddDto
    private string GenerateAddDto(string entityName, string moduleName, List<(string Type, string Name)> properties)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"namespace ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
        sb.AppendLine($"public sealed class {entityName}AddDto");
        sb.AppendLine("{");

        foreach (var prop in properties)
        {
            if (prop.Name != "Id")
            {
                sb.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
            }
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    // Method to generate the content for UpdateDto
    private string GenerateUpdateDto(string entityName, string moduleName, List<(string Type, string Name)> properties)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"namespace ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
        sb.AppendLine($"public sealed class {entityName}UpdateDto : BaseUpdateDto");
        sb.AppendLine("{");

        foreach (var prop in properties)
        {
            if (prop.Name != "Id")
            {
                sb.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
            }
        }

        sb.AppendLine("}");
        return sb.ToString();
    }
    #endregion

    #region Generate Interface Methods
    private string _dbContextName = string.Empty;
    private void OnDbContextNameSaved(object? sender, EventArgs e)
    {
        var form = sender as GetDbContextName;
        if (form == null) return;

        if (e is DbContextNameEventArgs args)
            _dbContextName = args.DbContextName;

        form.Close();
    }

    private void OnDbContextNameCancelled(object sender, EventArgs e)
    {
        var form = sender as GetDbContextName;
        if (form == null) return;

        _dbContextName = null;

        form.Close();
    }

    private void GenerateFiles(string path, string entityName, string moduleName, string namespaceName, string dbContextName)
    {
        string bllPath = Path.Combine(path, $"{namespaceName}.BLL");
        string dalPath = Path.Combine(path, $"{namespaceName}.DAL");

        // Ensure the root directory exists
        if (!Directory.Exists(bllPath))
            Directory.CreateDirectory(bllPath);
        if (!Directory.Exists(dalPath))
            Directory.CreateDirectory(dalPath);

        //BLL.I[Entity]Service
        string abstractPath = Path.Combine(bllPath, "Abstract", moduleName);
        if (!Directory.Exists(abstractPath))
            Directory.CreateDirectory(abstractPath);
        string interfaceContent = GenerateInterface(entityName, moduleName, namespaceName);
        File.WriteAllText(Path.Combine(abstractPath, $"I{entityName}Service.cs"), interfaceContent);

        //BLL.[Entity]Service
        string concretePath = Path.Combine(bllPath, "Concrete", moduleName);
        if (!Directory.Exists(concretePath))
            Directory.CreateDirectory(concretePath);
        string classContent = GenerateImplementation(entityName, moduleName, namespaceName);
        File.WriteAllText(Path.Combine(concretePath, $"{entityName}Service.cs"), classContent);

        //BLL.[Entity]Dto
        string profilesPath = Path.Combine(bllPath, "AutoMapper", "Profiles", moduleName);
        if (!Directory.Exists(profilesPath))
            Directory.CreateDirectory(profilesPath);
        string mapperContent = GenerateMapper(entityName, moduleName, namespaceName);
        File.WriteAllText(Path.Combine(profilesPath, $"{entityName}Profile.cs"), mapperContent);

        //BLL.I[Entity]Repository
        string repositoryInterfacePath = Path.Combine(bllPath, "Repositories", moduleName);
        if (!Directory.Exists(repositoryInterfacePath))
            Directory.CreateDirectory(repositoryInterfacePath);
        string repositoryInterfaceContent = GenerateRepositoryInterface(bllPath, entityName, moduleName, namespaceName);
        File.WriteAllText(Path.Combine(repositoryInterfacePath, $"I{entityName}Repository.cs"), repositoryInterfaceContent);

        //DAL.[Entity]Repository
        string repositoryPath = Path.Combine(dalPath, "Concrate", "EntityFramework", "Repositories", moduleName);
        if (!Directory.Exists(repositoryPath))
            Directory.CreateDirectory(repositoryPath);
        string repositoryContent = GenerateRepository(dalPath, entityName, moduleName, namespaceName, dbContextName);
        File.WriteAllText(Path.Combine(repositoryPath, $"{entityName}Repository.cs"), repositoryContent);

    }

    // Generate Interface
    static string GenerateInterface(string entityName, string moduleName, string namespaceName)
    {
        var camelCaseEntityName = ToCamelCase(entityName);
        return $@"using Microsoft.EntityFrameworkCore.Query;     
using {namespaceName}.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
using {namespaceName}.DOMAIN.Entities.{moduleName};
using NArchitecture.Core.Persistence.Paging;
using {namespaceName}.CORE.Utilities.Results.Abstract;
using System.Linq.Expressions;

namespace {namespaceName}.BLL.Abstract.{moduleName};

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
    static string GenerateImplementation(string entityName, string moduleName, string namespaceName)
    {
        var camelCaseEntityName = ToCamelCase(entityName);
        return $@"using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using {namespaceName}.CORE.Utilities.Results.Abstract;
using {namespaceName}.BLL.Abstract.{moduleName};
using {namespaceName}.BLL.Repositories.{moduleName};
using {namespaceName}.CORE.Utilities.Results.ComplexTypes;
using {namespaceName}.CORE.Utilities.Results.Concrete;
using NArchitecture.Core.Persistence.Paging;
using {namespaceName}.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
using {namespaceName}.DOMAIN.Entities.{moduleName};
using System.Linq.Expressions;
using FluentValidation;

namespace {namespaceName}.BLL.Concrete.{moduleName};

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
    static string GenerateMapper(string entityName, string moduleName, string namespaceName)
    {
        return $@"using AutoMapper;    
using NArchitecture.Core.Persistence.Paging;
using {namespaceName}.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;
using {namespaceName}.DOMAIN.Entities.{moduleName};

namespace {namespaceName}.BLL.AutoMapper.Profiles.{moduleName};

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

    private string GenerateRepositoryInterface(string path, string entityName, string moduleName, string namespaceName)
    {
        // Generate the interface content
        return $@"using {namespaceName}.DOMAIN.Entities.{moduleName};
using NArchitecture.Core.Persistence.Repositories;

namespace {namespaceName}.BLL.Repositories.{moduleName};

public interface I{entityName}Repository : IAsyncRepository<{entityName}, Guid>, IRepository<{entityName}, Guid>
{{
}}
";
    }

    // Generate Repository
    private string GenerateRepository(string path, string entityName, string moduleName, string namespaceName, string dbContextName)
    {
        // Generate the interface content
        return $@"using {namespaceName}.BLL.Repositories.{moduleName};
using {namespaceName}.DAL.Concrete.EntityFramework.Contexts;
using {namespaceName}.DOMAIN.Entities.{moduleName};
using NArchitecture.Core.Persistence.Repositories;

namespace {namespaceName}.DAL.Concrete.EntityFramework.Repositories.{moduleName};

public class {entityName}Repository : EfRepositoryBase<{entityName}, Guid, {dbContextName}DbContext>, I{entityName}Repository
{{
    public {entityName}Repository({namespaceName}DbContext context) : base(context)
    {{
    }}
}}
";
    }

    #endregion

    #region Helper functions
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
            else if (textBox == txtNamespace)
                textBox.Text = "Enter Namespace";
            else if (textBox == txtProperties)
                textBox.Text = "Entity Properties (one per line)";
        }
    }



    /// <summary>
    /// Verilen tam C# sınıf metni içinden (using, namespace vs. dahil)
    /// yalnızca sınıf gövdesindeki “public … { get; set; } [= …;]” 
    /// biçimindeki property tanımlarını bulur ve (Type, Name) listesi olarak döner.
    /// 
    /// Eğer convertNavToDto true ise:
    ///   - “XyzId” ve “Xyz” çiftlerini silmez,
    ///   - Ancak “Xyz” (navigation) property’sinin tipini “XyzDto” olarak döner.
    /// Eğer convertNavToDto false ise:
    ///   - “XyzId” ve “Xyz” çifti varsa yalnızca “XyzId” döner, “Xyz” atlanır.
    /// </summary>
    private List<(string Type, string Name)> GetEntityProperties(
        string input,
        bool convertNavToDto = false)
    {
        var properties = new List<(string Type, string Name)>();
        if (string.IsNullOrWhiteSpace(input))
            return properties;

        // ----------------------------------------------------
        // 1) "public class" bildirimi tespiti için Regex
        // ----------------------------------------------------
        var patternClass = @"\bpublic\s+class\b";
        var regexClass = new Regex(patternClass, RegexOptions.Compiled);

        // ----------------------------------------------------
        // 2) "property" satırlarını yakalamak için Regex
        // ----------------------------------------------------
        //
        //  ^\s*                     Satır başında boşluk olabilir
        //  public\s+                "public" ve ardından en az bir boşluk
        //  (?:\w+\s+)*              İsteğe bağlı modifier’lar (virtual, required, vb.)
        //  (?<type>[\w\.<>\?,\s]+?)  Tip grubu
        //  \s+                      Tip ile isim arasında en az bir boşluk
        //  (?<name>\w+)             Property adı grubu
        //  \s*                      İsimden sonra boşluk olabilir
        //  \{\s*get;\s*set;\s*\}    "{ get; set; }" bloğu
        //  \s*(?:=[^;]+;)?          İsteğe bağlı "= initializer;"
        //  \s*$                     Satır sonunda boşluk olabilir
        //
        var patternProperty = @"^\s*public\s+(?:\w+\s+)*(?<type>[\w\.<>\?,\s]+?)\s+(?<name>\w+)\s*\{\s*get;\s*set;\s*\}\s*(?:=[^;]+;)?\s*$";
        var regexProperty = new Regex(patternProperty, RegexOptions.Compiled);

        // ----------------------------------------------------
        // 3) Satır satır ilerleyip önce "class {" ardından property’leri yakala
        // ----------------------------------------------------
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        bool foundClassDeclaration = false;
        bool insideClass = false;
        int braceCount = 0;

        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();

            // 3a) Henüz "public class" bildirimini görmediysek, arıyoruz:
            if (!foundClassDeclaration)
            {
                if (regexClass.IsMatch(line))
                {
                    foundClassDeclaration = true;
                    // Aynı satırda "{" varsa doğrudan gövdeye gir
                    if (line.Contains("{"))
                    {
                        insideClass = true;
                        // Birden fazla '{' veya '}' varsa da saymak için:
                        braceCount = CountChar(line, '{') - CountChar(line, '}');
                    }
                }
                continue;
            }

            // 3b) "public class" bulundu ama henüz gövde açılmadıysa:
            if (foundClassDeclaration && !insideClass)
            {
                if (line.Contains("{"))
                {
                    insideClass = true;
                    braceCount = CountChar(line, '{') - CountChar(line, '}');
                }
                continue;
            }

            // 3c) Sınıf gövdesinin içindeysek:
            if (insideClass)
            {
                if (line.Contains("{"))
                    braceCount += CountChar(line, '{');
                if (line.Contains("}"))
                    braceCount -= CountChar(line, '}');

                // Sınıf kapanmışsa döngüyü bitir
                if (braceCount <= 0)
                    break;

                // Satır bir property mi bakalım
                var match = regexProperty.Match(line);
                if (match.Success)
                {
                    string type = match.Groups["type"].Value.Trim();
                    string name = match.Groups["name"].Value.Trim();
                    properties.Add((type, name));
                }
            }
        }

        // ----------------------------------------------------
        // 4) "XyzId" ↔ "Xyz" ilişkisinin işlenmesi
        // ----------------------------------------------------
        // Tüm XyzId’lerden "Xyz" base adlarını topla
        var baseNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var (type, name) in properties)
        {
            if (name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) && name.Length > 2)
            {
                baseNames.Add(name.Substring(0, name.Length - 2));
            }
        }

        var result = new List<(string Type, string Name)>();
        foreach (var (type, name) in properties)
        {
            // Eğer bu property bir "Xyz" (navigation) satırıysa:
            if (baseNames.Contains(name))
            {
                if (convertNavToDto)
                {
                    // Navigasyon property’sinin tipini "XyzDto" yap
                    string dtoType = $"{type.Trim()}Dto";
                    result.Add((dtoType, name));
                }
                // convertNavToDto false ise navigasyon property’sini atla
                continue;
            }

            // Aksi halde ("XyzId" veya diğer normal property’ler):
            result.Add((type, name));
        }

        return result;
    }

    /// <summary>
    /// Bir satırdaki ('{' veya '}') karakterlerinin kaç defa tekrar ettiğini sayar.
    /// </summary>
    private int CountChar(string str, char c)
    {
        int count = 0;
        foreach (var ch in str)
            if (ch == c)
                count++;
        return count;
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
    #endregion
}
