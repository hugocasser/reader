using Microsoft.Extensions.Options;
using Presentation.Options;

namespace Presentation.Validators;

[OptionsValidator]
public partial class TokenOptionsValidator : IValidateOptions<TokenOptions>
{
    
}