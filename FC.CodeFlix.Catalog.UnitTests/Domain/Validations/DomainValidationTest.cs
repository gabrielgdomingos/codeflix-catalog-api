using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.Validations;
using FluentAssertions;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Validations
{
    [Collection(nameof(DomainValidationTestFixture))]
    public class DomainValidationTest
    {

        private readonly DomainValidationTestFixture _domainValidationTestFixture;

        public DomainValidationTest(DomainValidationTestFixture domainValidationTestFixture)
            => _domainValidationTestFixture = domainValidationTestFixture;

        [Fact(DisplayName = nameof(NotNullSuccess))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullSuccess()
        {
            //Arrange
            var values = new
            {
                targetName = _domainValidationTestFixture.GetTargetName(),
                targetValue = _domainValidationTestFixture.GetTargetValue()
            };

            //Act
            var validation = ()
                => DomainValidation.NotNull(values.targetName, values.targetValue);

            //Assert
            validation.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(NotNullError))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullError()
        {
            //Arrange
            var values = new
            {
                targetName = _domainValidationTestFixture.GetTargetName()
            };

            //Act
            var validation = ()
                => DomainValidation.NotNull(values.targetName, null!);

            //Assert
            validation.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{values.targetName} should not be null");
        }

        [Fact(DisplayName = nameof(NotNullOrEmptySuccess))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptySuccess()
        {
            //Arrange
            var values = new
            {
                targetName = _domainValidationTestFixture.GetTargetName(),
                targetValue = _domainValidationTestFixture.GetTargetValue()
            };

            //Act
            var validation = ()
                => DomainValidation.NotNullOrEmpty(values.targetName, values.targetValue);

            //Assert
            validation.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(NotNullOrEmptyError))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NotNullOrEmptyError(string? value)
        {
            //Arrange
            var values = new
            {
                targetName = _domainValidationTestFixture.GetTargetName(),
                targetValue = value
            };

            //Act
            var validation = ()
                => DomainValidation.NotNullOrEmpty(values.targetName, values.targetValue);

            //Assert
            validation.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{values.targetName} should not be null or empty");
        }

        [Theory(DisplayName = nameof(MinLengthSuccess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("ab", 1)]
        [InlineData("ab", 2)]
        public void MinLengthSuccess(string value, int minLength)
        {
            //Arrange
            var values = new
            {
                TargetName = _domainValidationTestFixture.GetTargetName(),
                TargetValue = value,
                MinLength = minLength
            };

            //Act
            var validation = ()
                => DomainValidation.MinLength(values.TargetName, values.TargetValue, values.MinLength);

            //Assert
            validation.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(MinLengthError))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("ab", 3)]
        [InlineData("ab", 4)]
        public void MinLengthError(string value, int minLength)
        {
            //Arrange
            var values = new
            {
                TargetName = _domainValidationTestFixture.GetTargetName(),
                TargetValue = value,
                MinLength = minLength
            };

            //Act
            var validation = ()
                => DomainValidation.MinLength(values.TargetName, values.TargetValue, values.MinLength);

            //Assert
            validation.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{values.TargetName} should be at leats {values.MinLength} characters long");
        }

        [Theory(DisplayName = nameof(MaxLengthSuccess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("abcde", 5)]
        [InlineData("abcde", 6)]
        public void MaxLengthSuccess(string value, int maxLength)
        {
            //Arrange
            var values = new
            {
                TargetName = _domainValidationTestFixture.GetTargetName(),
                TargetValue = value,
                MaxLength = maxLength
            };

            //Act
            var validation = ()
                => DomainValidation.MaxLength(values.TargetName, values.TargetValue, values.MaxLength);

            //Assert
            validation.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(MaxLengthError))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("abcde", 3)]
        [InlineData("abcde", 4)]
        public void MaxLengthError(string value, int maxLength)
        {
            //Arrange
            var values = new
            {
                TargetName = _domainValidationTestFixture.GetTargetName(),
                TargetValue = value,
                MaxLength = maxLength
            };

            //Act
            var validation = ()
                => DomainValidation.MaxLength(values.TargetName, values.TargetValue, values.MaxLength);

            //Assert
            validation.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{values.TargetName} should be at most {values.MaxLength} characters long");
        }
    }
}
