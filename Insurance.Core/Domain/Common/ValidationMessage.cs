using System.Diagnostics.CodeAnalysis;

namespace Insurance.Core.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public static class ValidationMessage
    {
        public const string InputInvalid = "Input invalid.";
        public const string IdInvalid = "Id invalid";
        public const string NameInvalid = "Name invalid.";
        public const string EntityNotFound = "Entity not found.";
        public const string ContractInvalid = "Contract invalid.";
        public const string ContractNotExists = "Contract not exists";
        public const string ContractExists = "Contract exists.";
    }
}