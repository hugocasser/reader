using BusinessLogicLayer.Options;
using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.Validation.Validators;

[OptionsValidator]
public partial class EmailMessageSenderOptionsValidator : IValidateOptions<EmailMessageSenderOptions>;