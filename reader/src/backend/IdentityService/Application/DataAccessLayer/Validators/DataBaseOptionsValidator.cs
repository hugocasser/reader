using DataAccessLayer.Options;
using Microsoft.Extensions.Options;

namespace DataAccessLayer.Validators;

[OptionsValidator]
public partial class DataBaseOptionsValidator : IValidateOptions<DataBaseOption>
{
    
}