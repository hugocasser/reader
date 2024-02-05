using Microsoft.Extensions.Options;
using Presentation.Options;

namespace Infrastructure.Validators;

[OptionsValidator]
public partial class MongoOptionsValidator : IValidateOptions<MongoOptions>{}