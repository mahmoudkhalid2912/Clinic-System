namespace ClinicManagementSystem.Infrastructure.Helpers
{
    public static class EmailBuilder
    {
        public static string Build(string templateFileName, Dictionary<string, string> templateModel)
        {
            var currentDir = Directory.GetCurrentDirectory();

            var templatePath = Path.Combine(currentDir, "Tamplates", $"{templateFileName}.html");

            if (!File.Exists(templatePath))
                throw new Exception($"Current Directory: {currentDir}, Template Path: {templatePath}");

            var body = File.ReadAllText(templatePath);

            foreach (var item in templateModel)
            {
                body = body.Replace($"{{{{{item.Key}}}}}", item.Value);
            }

            return body;
        }
    }
}