using Microsoft.Extensions.Options;
using PresentationLayer.Options;

namespace PresentationLayer.Validators;

[OptionsValidator]
public partial class AdminSeederOptionsValidator : IValidateOptions<AdminSeederOptions>;