using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.Domain.Validations
{
    public static class DomainValidation
    {
        public static void NotNull(string targetName, object targetValue)
        {
            if (targetValue is null)
                throw new EntityValidationException($"{targetName} should not be null");
        }

        public static void NotNullOrEmpty(string targetName, string? targetValue)
        {
            if (string.IsNullOrWhiteSpace(targetValue))
                throw new EntityValidationException($"{targetName} should not be null or empty");
        }

        public static void MinLength(string targetName, string targetValue, int minLength)
        {
            if (targetValue.Length < minLength)
                throw new EntityValidationException($"{targetName} should be at leats {minLength} characters long");
        }

        public static void MaxLength(string targetName, string targetValue, int maxLength)
        {
            if (targetValue.Length > maxLength)
                throw new EntityValidationException($"{targetName} should be at most {maxLength} characters long");
        }
    }
}
