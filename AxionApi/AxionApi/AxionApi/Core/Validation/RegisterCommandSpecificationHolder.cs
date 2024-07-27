using Domain.Commands;
using Validot;

namespace Core.Validation;

internal sealed class RegisterCommandSpecificationHolder : ISpecificationHolder<RegisterCommand>
{
    public Specification<RegisterCommand> Specification { get; }

    public RegisterCommandSpecificationHolder()
    {
        Specification<RegisterCommand> registerCommandSpecification = s => s
            .Member(m => m.Address, m => m
                .NotEmpty()
                .NotWhiteSpace())
            .Member(m => m.Firstname, m => m
                .NotEmpty()
                .NotWhiteSpace())
            .Member(m => m.PhoneNumber, m => m
                .NotEmpty()
                .NotWhiteSpace())
            .Member(m => m.Lastname, m => m
                .NotEmpty()
                .NotWhiteSpace())
            .Member(m => m.Username, m => m
                .NotEmpty()
                .NotWhiteSpace())
            .Member(m => m.Password, m => m
                .NotEmpty()
                .NotWhiteSpace())
            .Member(m => m.Email, m => m
                .NotEmpty()
                .NotWhiteSpace()
                .Email());

        Specification = registerCommandSpecification;
    }
}