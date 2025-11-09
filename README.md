![image alt](https://github.com/Pls8/FirstEntityFW/blob/master/MainRepoCover_00000.jpg?raw=true)

### ➭ Entity Framework Core — Conventional & Fluent API Relationship Example

This project demonstrates how **Entity Framework Core (EF Core)** automatically creates relationships between entities using **Conventions**, and how to override or customize them using the **Fluent API**.
```
EFCoreRelationshipExample/
├── Models/
│   ├── ClassName.cs
│   └── ClassName.cs
├── Context/
│   └── AppDbContext.cs
├── Config/
│   ├── ClassNameConfiguration.cs
│   └── ClassNameConfiguration.cs
└── -
```
---

### ➭ Convention Coding

Entity Framework Core uses **conventions** to infer relationships between entities based on property names and types.  
For example:
- A collection navigation property like `ICollection<ClassName>` implies **one-to-many**.
- A reference navigation property like `ClassName classname` implies(instance) **many-to-one**.
- DataAnnotation in Class it self: [ ForeignKey(nameof(Instance)) ] | [Key] | [Precision(18, 2)] | [Required] | [MaxLength(number)]

---

### ➭ Fluent API Configuration
If you want to explicitly define or customize the relationship, use the Fluent API in your Project.
#### Context / DbContext.cs
```
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Apply all configurations from the Config assembly
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
```
#### Config / ClassNameConfig.cs
```
public class CategoryConfiguration : IEntityTypeConfiguration<ClassName>
{                                                           // ^-- ctrl + . to add AssimplyConnection
    public void Configure(EntityTypeBuilder<ClassName> builder)
    {
        // this Fluent API Configuration
        // this config instead of DataAnnotation [Flag] [Key] [ForignKey]

        builder.ToTable("TableName");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.Name)
            .IsUnique();
    }
}
```
---
### ➭ Detailed Summary
| Feature                         | Convention-Based Mapping                                       | Fluent API Mapping                                           |
| ------------------------------- | -------------------------------------------------------------- | ------------------------------------------------------------ |
| **Setup**                       | Automatic                                                      | Manual                                                       |
| **Configuration Location**      | In entity property definitions                                 | Inside `OnModelCreating()` of `DbContext`                    |
| **Foreign Key Detection**       | Auto-detected via naming pattern (`EntityId`)                  | Explicitly declared via `.HasForeignKey()`                   |
| **Relationship Type**           | Inferred (1:1, 1:N, N:N)                                       | Fully controlled by `.HasOne()`, `.WithMany()`, `.WithOne()` |
| **Navigation Properties**       | Auto-matched by class and collection naming                    | Must be explicitly defined if non-standard                   |
| **Delete Behavior**             | Default (`Cascade` for required, `ClientSetNull` for optional) | Customizable with `.OnDelete(DeleteBehavior)`                |
| **Index Creation**              | Automatic on foreign keys                                      | Automatic, or can be customized                              |
| **Data Annotation Alternative** | Supported (`[ForeignKey]`, `[InverseProperty]`, etc.)          | Supported together with Fluent configuration              |
| **Flexibility**                 | Limited to EF naming conventions                               | Full control over keys, names, and constraints               |
| **Use Case**                    | Simple models that follow naming rules                         | Complex schemas, custom keys, or special constraints         |
| **Ease of Maintenance**         | Easier to read                                                 | More verbose but explicit                                    |
| **Performance Impact**          | Same at runtime                                                | Same — only affects model configuration                      |
| **Recommended For**             | Small to medium projects                                       | Enterprise-level or multi-assembly data layers               |

