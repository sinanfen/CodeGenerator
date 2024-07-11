using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DtoCreator
{
    /// <summary>
    /// Generates DTO classes (Dto, AddDto, UpdateDto) based on provided entity properties.
    /// Prompts the user to enter the entity name and properties, then generates the selected DTO classes
    /// and saves them to the "Generated" directory in the current environment.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Enter the Entity name (or type 'exit' to quit): ");
            string entityName = Console.ReadLine();
            if (entityName.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            Console.WriteLine("Enter the Entity properties (one per line, empty line to finish):");
            List<(string Type, string Name)> properties = new List<(string, string)>();
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) break;

                line = line.Trim();
                if (line.StartsWith("public ")) line = line.Substring(7);
                int spaceIndex = line.IndexOf(' ');
                if (spaceIndex > 0)
                {
                    string type = line.Substring(0, spaceIndex);
                    string name = line.Substring(spaceIndex + 1).Split('{')[0].Trim();
                    properties.Add((type, name));
                }
            }

            List<string> selectedOptions = SelectDtoOptions();

            string path = Path.Combine(Environment.CurrentDirectory, "Generated");
            Directory.CreateDirectory(path);

            if (selectedOptions.Contains("Dto"))
            {
                string dtoContent = GenerateDto(entityName, properties);
                File.WriteAllText(Path.Combine(path, $"{entityName}Dto.cs"), dtoContent);
            }

            if (selectedOptions.Contains("AddDto"))
            {
                string addDtoContent = GenerateAddDto(entityName, properties);
                File.WriteAllText(Path.Combine(path, $"{entityName}AddDto.cs"), addDtoContent);
            }

            if (selectedOptions.Contains("UpdateDto"))
            {
                string updateDtoContent = GenerateUpdateDto(entityName, properties);
                File.WriteAllText(Path.Combine(path, $"{entityName}UpdateDto.cs"), updateDtoContent);
            }

            Console.WriteLine($"Files generated successfully in {path}");
            Console.WriteLine("---------------------------------------------");       
        }
    }


    static string GenerateDto(string entityName, List<(string Type, string Name)> properties)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public class {entityName}Dto");
        sb.AppendLine("{");
        sb.AppendLine("    public Guid Id { get; set; }");

        foreach (var prop in properties)
        {
            sb.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    static string GenerateAddDto(string entityName, List<(string Type, string Name)> properties)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public class {entityName}AddDto");
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

    static string GenerateUpdateDto(string entityName, List<(string Type, string Name)> properties)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public class {entityName}UpdateDto");
        sb.AppendLine("{");
        sb.AppendLine("    public Guid Id { get; set; }");

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

    static List<string> SelectDtoOptions()
    {
        List<string> options = new List<string> { "Dto", "AddDto", "UpdateDto" };
        List<bool> selected = new List<bool> { false, false, false };

        int currentOption = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select which DTOs to generate (use arrow keys to navigate, space to toggle, enter to confirm):");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == currentOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.WriteLine($"[{(selected[i] ? "X" : " ")}] {options[i]}");
            }

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    currentOption = (currentOption - 1 + options.Count) % options.Count;
                    break;
                case ConsoleKey.DownArrow:
                    currentOption = (currentOption + 1) % options.Count;
                    break;
                case ConsoleKey.Spacebar:
                    selected[currentOption] = !selected[currentOption];
                    break;
                case ConsoleKey.Enter:
                    if (selected.Any(s => s))
                    {
                        return options.Where((o, i) => selected[i]).ToList();
                    }
                    break;
            }
        }
    }
}