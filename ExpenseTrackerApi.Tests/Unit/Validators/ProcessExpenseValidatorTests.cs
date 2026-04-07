using FluentAssertions;
using ExpenseTrackerApi.Endpoints.Expenses;

namespace ExpenseTrackerApi.Tests.Unit.Validators;

public class ProcessExpenseValidatorTests
{
    private readonly ProcessExpenseValidator _validator;

    public ProcessExpenseValidatorTests()
    {
        _validator = new ProcessExpenseValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData("Ola")]
    public void Validator_DeveFalhar_QuandoTextoForInvalido(string textoInvalido)
    {
        // Arrange
        var request = new ProcessExpenseRequest { Text = textoInvalido };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validator_DevePassar_QuandoTextoForValido()
    {
        // Arrange
        var request = new ProcessExpenseRequest { Text = "Almoço no restaurante por 15 euros" };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}