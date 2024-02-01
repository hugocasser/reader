using Application.Abstractions.Services;
using Application.Services;
using Application.Validation.Validators.Authors;
using Application.Validation.Validators.Books;
using Application.Validation.Validators.Categories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBooksService, BooksService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<IAuthorsService, AuthorsService>();
        
        return services;
    }
    
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateBookInfoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateBookTextValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateAuthorValidator>();
        
        return services;
    }
    
}