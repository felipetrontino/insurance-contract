﻿using System.Diagnostics.CodeAnalysis;

namespace Insurance.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public static class ValidationMessage
    {
        public const string InputInvalid = "Input invalid.";
        public const string IdInvalid = "Id invalid";
        public const string NameInvalid = "Name invalid.";
        public const string LastNameInvalid = "Last name invalid.";
        public const string EntityNotFound = "Entity not found.";
        public const string ContractInvalid = "Contract invalid.";
        public const string ContractExists = "Contract exists.";
        public const string ContractNotExists = "Contract not exists";
        public const string ContractFinished = "Contract is finished.";
    }
}