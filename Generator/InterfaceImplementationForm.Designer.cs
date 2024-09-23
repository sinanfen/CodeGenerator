namespace Generator;

public partial class InterfaceImplementationForm : Form
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtPath;
    private TextBox txtEntityName;
    private TextBox txtModuleName;
    private Button btnGenerate;
    private Button btnReset;
    private Button btnBrowse;
    private ToolTip toolTip;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        txtPath = new TextBox();
        txtEntityName = new TextBox();
        txtModuleName = new TextBox();
        btnGenerate = new Button();
        btnReset = new Button();
        btnBrowse = new Button();
        toolTip = new ToolTip(components);
        SuspendLayout();
        // 
        // txtPath
        // 
        txtPath.ForeColor = Color.Gray;
        txtPath.Location = new Point(12, 12);
        txtPath.Name = "txtPath";
        txtPath.Size = new Size(600, 23);
        txtPath.TabIndex = 0;
        txtPath.Text = "Project Path";
        toolTip.SetToolTip(txtPath, "Enter the path where the generated files will be saved.");
        txtPath.Enter += RemovePlaceholder;
        txtPath.Leave += AddPlaceholder;
        // 
        // txtEntityName
        // 
        txtEntityName.ForeColor = Color.Gray;
        txtEntityName.Location = new Point(12, 50);
        txtEntityName.Name = "txtEntityName";
        txtEntityName.Size = new Size(681, 23);
        txtEntityName.TabIndex = 1;
        txtEntityName.Text = "Enter Entity Name";
        toolTip.SetToolTip(txtEntityName, "Enter the name of the Entity class.");
        txtEntityName.Enter += RemovePlaceholder;
        txtEntityName.Leave += AddPlaceholder;
        // 
        // txtModuleName
        // 
        txtModuleName.ForeColor = Color.Gray;
        txtModuleName.Location = new Point(12, 88);
        txtModuleName.Name = "txtModuleName";
        txtModuleName.Size = new Size(681, 23);
        txtModuleName.TabIndex = 2;
        txtModuleName.Text = "Enter Module Short Name";
        toolTip.SetToolTip(txtModuleName, "Enter the short name of the module (e.g., CRM, HR).");
        txtModuleName.Enter += RemovePlaceholder;
        txtModuleName.Leave += AddPlaceholder;
        // 
        // btnGenerate
        // 
        btnGenerate.Location = new Point(593, 126);
        btnGenerate.Name = "btnGenerate";
        btnGenerate.Size = new Size(100, 30);
        btnGenerate.TabIndex = 3;
        btnGenerate.Text = "Generate";
        toolTip.SetToolTip(btnGenerate, "Click to generate the interface, class, and AutoMapper profile.");
        btnGenerate.UseVisualStyleBackColor = true;
        btnGenerate.Click += btnGenerate_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(487, 126);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(100, 30);
        btnReset.TabIndex = 4;
        btnReset.Text = "Reset";
        toolTip.SetToolTip(btnReset, "Click to reset all input fields.");
        btnReset.UseVisualStyleBackColor = true;
        btnReset.Click += btnReset_Click;
        // 
        // btnBrowse
        // 
        btnBrowse.Location = new Point(618, 12);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new Size(75, 23);
        btnBrowse.TabIndex = 5;
        btnBrowse.Text = "Browse";
        toolTip.SetToolTip(btnBrowse, "Click to select a folder for the project path.");
        btnBrowse.UseVisualStyleBackColor = true;
        btnBrowse.Click += btnBrowse_Click;
        // 
        // InterfaceImplementationForm
        // 
        ClientSize = new Size(708, 179);
        Controls.Add(txtPath);
        Controls.Add(txtEntityName);
        Controls.Add(txtModuleName);
        Controls.Add(btnGenerate);
        Controls.Add(btnReset);
        Controls.Add(btnBrowse);
        Name = "InterfaceImplementationForm";
        Text = "Interface/Implementation Generator";
        Load += InterfaceImplementationForm_Load;
        ResumeLayout(false);
        PerformLayout();
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

        return char.ToLower(str[0]) + str.Substring(1);
    }

    // Generate Interface
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

        namespace MODISO.BLL.Concrete.{moduleName};

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
                        return null;
                    }}
                }}

                public async Task<Paginate<{entityName}Dto>?> GetListAsync(
                    Expression<Func<{entityName}, bool>>? predicate = null,
                    Func<IQueryable<{entityName}>, IOrderedQueryable<{entityName}>>? orderBy = null,
                    Func<IQueryable<{entityName}>, IIncludableQueryable<{entityName}, object>>? include = null,
                    int index = 0,
                    int size = 10,
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
                        return null;
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
                        return null;
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
                        return null;
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
                        return null;
                    }}
                }}

                public async Task<IDataResult<{entityName}Dto>> AddAsync({entityName}AddDto {camelCaseEntityName}AddDto, CancellationToken cancellationToken)
                {{
                    try
                    {{
                        var {camelCaseEntityName} = _mapper.Map<{entityName}>({camelCaseEntityName}AddDto);
                        await _{camelCaseEntityName}Repository.AddAsync({camelCaseEntityName});
                        var resultData = _mapper.Map<{entityName}Dto>({camelCaseEntityName});
                        return new DataResult<{entityName}Dto>(ResultStatus.Success, ""The {entityName} has been added successfully."", resultData);
                    }}
                    catch (Exception ex)
                    {{
                        _logger.LogError(ex, ""Error in {{MethodName}}. Failed to add {entityName}. Details: {{ExceptionMessage}}"", nameof(AddAsync), ex.Message);
                        return new DataResult<{entityName}Dto>(ResultStatus.Error, ""An unexpected error occurred. Please try again."", null);
                    }}
                }}

                public async Task<IDataResult<{entityName}Dto>> UpdateAsync({entityName}UpdateDto {camelCaseEntityName}UpdateDto, CancellationToken cancellationToken)
                {{
                    try
                    {{
                        var {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(x => x.Id == {camelCaseEntityName}UpdateDto.Id, cancellationToken: cancellationToken);
                        {camelCaseEntityName} = _mapper.Map({camelCaseEntityName}UpdateDto, {camelCaseEntityName});
                        await _{camelCaseEntityName}Repository.UpdateAsync({camelCaseEntityName});
                        var resultData = _mapper.Map<{entityName}Dto>({camelCaseEntityName});
                        return new DataResult<{entityName}Dto>(ResultStatus.Success, ""The {entityName} has been updated successfully."", resultData);
                    }}
                    catch (Exception ex)
                    {{
                        _logger.LogError(ex, ""Error in {{MethodName}}. Failed to update {entityName} with ID {{Id}}. Details: {{ExceptionMessage}}"", nameof(UpdateAsync), {camelCaseEntityName}UpdateDto?.Id, ex.Message);
                        return new DataResult<{entityName}Dto>(ResultStatus.Error, ""An unexpected error occurred. Please try again."", null);
                    }}
                }}

                public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
                {{
                    try
                    {{
                        var {camelCaseEntityName} = await _{camelCaseEntityName}Repository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);
                        await _{camelCaseEntityName}Repository.DeleteAsync({camelCaseEntityName}, cancellationToken: cancellationToken);
                        return new Result(ResultStatus.Success, ""The {entityName} has been deleted successfully."");
                    }}
                    catch (Exception ex)
                    {{
                        _logger.LogError(ex, ""Error in {{MethodName}}. Failed to delete {entityName} with ID {{Id}}. Details: {{ExceptionMessage}}"", nameof(DeleteAsync), id, ex.Message);
                        return new Result(ResultStatus.Error, ""An unexpected error occurred. Please try again."");
                    }}
                }}
            }}
        ";
    }

    // Generate Mapper
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
                    }}
                ";
    }

    private string GenerateRepository(string path, string entityName, string moduleName)
    {
        // Generate the interface content
        return $@"
        using MODISO.DOMAIN.Entities.{moduleName};
        using NArchitecture.Core.Persistence.Repositories;

        namespace MODISO.BLL.Repositories.{moduleName};
        
            public interface I{entityName}Repository : IAsyncRepository<{entityName}, Guid>, IRepository<{entityName}, Guid>
            {{
            }}
        ";
    }

}